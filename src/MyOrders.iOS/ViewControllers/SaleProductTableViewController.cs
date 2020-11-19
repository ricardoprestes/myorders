using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Foundation;
using MyOrders.Helpers;
using MyOrders.Models;
using MyOrders.Services.Abstractions;
using MyOrders.ViewModels;
using UIKit;

namespace MyOrders.iOS
{
    public partial class SaleProductTableViewController : UITableViewController
    {
        bool _apiRequest;
        Category _category = null;

        UIRefreshControl _refreshControl;

        public MainViewModel ViewModel { get; set; }

        public SaleProductTableViewController(IntPtr handle) : base(handle)
        {
            var apiService = ServiceLocator.Instance.Get<IApiService>();
            var productService = ServiceLocator.Instance.Get<IProductService>();
            var cartService = ServiceLocator.Instance.Get<ICartService>();

            ViewModel = new MainViewModel(apiService, productService, cartService);
            _apiRequest = true;

            _refreshControl = new UIRefreshControl();
            _refreshControl.ValueChanged += RefreshControl_ValueChanged;
            TableView.Add(_refreshControl);
            TableView.Source = new ItemsDataSource(ViewModel);

            Title = ViewModel.Title;

            //ViewModel.PropertyChanged += IsBusy_PropertyChanged;
            //ViewModel.Items.CollectionChanged += Items_CollectionChanged; ;
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (_apiRequest)
                await LoadDataAsync().ConfigureAwait(false);
            await LoadItemsAsync().ConfigureAwait(false);
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InvokeOnMainThread(() => TableView.ReloadData());
        }

        private void RefreshControl_ValueChanged(object sender, EventArgs e)
        {
            if (!ViewModel.IsBusy && _refreshControl.Refreshing)
                ViewModel.LoadItemsAsync(_category);
        }

        private async Task LoadItemsAsync(Category category = null)
        {
            await ViewModel.LoadItemsAsync(category);
            InvokeOnMainThread(() => TableView.ReloadData());
        }

        private async Task LoadDataAsync()
        {
            await ViewModel.LoadDataAsync().ConfigureAwait(false);
            _apiRequest = false;
        }
    }

    class ItemsDataSource : UITableViewSource
    {
        readonly MainViewModel viewModel;

        public ItemsDataSource(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override nint RowsInSection(UITableView tableview, nint section) => viewModel.Items.Count;

        public override nint NumberOfSections(UITableView tableView) => 1;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("SaleProductViewCell", indexPath) as SaleProductCell;

            var item = viewModel.Items[indexPath.Row];
            cell.SetValue(item);

            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            var item = viewModel.Items[indexPath.Row];
            var height = item.Type == Enums.EGroupItemType.Product ? 120 : 40;
            return height;
        }
    }
}