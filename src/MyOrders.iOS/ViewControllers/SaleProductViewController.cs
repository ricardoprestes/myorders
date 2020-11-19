using Foundation;
using MyOrders.Helpers;
using MyOrders.Models;
using MyOrders.Services.Abstractions;
using MyOrders.ViewModels;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UIKit;

namespace MyOrders.iOS
{
    public partial class SaleProductViewController : UIViewController
    {
        bool _apiRequest;
        Category _category = null;

        UIRefreshControl _refreshControl;
        ItemsDataSource _itemsDataSource;

        public MainViewModel ViewModel { get; set; }


        public SaleProductViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            BtnCart.Hidden = true;
            var apiService = ServiceLocator.Instance.Get<IApiService>();
            var productService = ServiceLocator.Instance.Get<IProductService>();
            var cartService = ServiceLocator.Instance.Get<ICartService>();

            ViewModel = new MainViewModel(apiService, productService, cartService);
            _apiRequest = true;

            _refreshControl = new UIRefreshControl();
            _refreshControl.ValueChanged += RefreshControl_ValueChanged;
            TableView.Add(_refreshControl);
            _itemsDataSource = new ItemsDataSource(ViewModel, TableView);
            TableView.Source = _itemsDataSource;

            Title = ViewModel.Title;

            ViewModel.Items.CollectionChanged += Items_CollectionChanged;
        }

        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            _itemsDataSource.ItemAmountChanged += ItemAmountChange;
            await ViewDidAppearAsync().ConfigureAwait(false);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            _itemsDataSource.ItemAmountChanged -= ItemAmountChange;
        }

        private void ItemAmountChange(object sender, EventArgs e)
        {
            ShowCartValue();
        }

        private async Task ViewDidAppearAsync()
        {
            if (_apiRequest)
                await LoadDataAsync().ConfigureAwait(false);
            await LoadItemsAsync().ConfigureAwait(false);
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //InvokeOnMainThread(() => TableView.ReloadData());
            ShowCartValue();
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

        private void ShowCartValue()
        {
            var value = ViewModel.Cart.Total;
            InvokeOnMainThread(() =>
            {
                if (value > 0)
                {
                    BtnCart.SetTitle($"Comprar {value:R$ ###,###,##0.00}", UIControlState.Normal);
                    BtnCart.Hidden = false;
                }
                else
                    BtnCart.Hidden = true;
            });
        }
    }

    class ItemsDataSource : UITableViewSource
    {
        readonly MainViewModel _viewModel;
        readonly UITableView _tableView;

        public event EventHandler ItemAmountChanged;

        public ItemsDataSource(MainViewModel viewModel, UITableView tableView)
        {
            _viewModel = viewModel;
            _tableView = tableView;
        }

        public override nint RowsInSection(UITableView tableview, nint section) => _viewModel.Items.Count;

        public override nint NumberOfSections(UITableView tableView) => 1;

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            var item = _viewModel.Items[indexPath.Row];
            var height = item.Type == Enums.EGroupItemType.Product ? 120 : 40;
            return height;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("SaleProductViewCell", indexPath) as SaleProductCell;

            var item = _viewModel.Items[indexPath.Row];
            cell.SetValue(item);
            cell.SubscribeEvents();
            UnsubscribeEvents(cell);
            SubscribeEvents(cell);
            return cell;
        }

        private void SubscribeEvents(SaleProductCell cell)
        {
            cell.BtnFavoriteClick += OnFavoriteClick;
            cell.BtnAddClick += OnAddClick;
            cell.BtnRemoveClick += OnRemoveClick;
        }

        private void UnsubscribeEvents(SaleProductCell cell)
        {
            cell.BtnFavoriteClick -= OnFavoriteClick;
            cell.BtnAddClick -= OnAddClick;
            cell.BtnRemoveClick -= OnRemoveClick;
        }

        private void OnFavoriteClick(SaleProductCell cell, bool selected)
        {
            var indexPath = _tableView.IndexPathForCell(cell);
            if (indexPath is null)
                return;

            var item = _viewModel.Items[indexPath.Row];
            _viewModel.SetFavorite(item.Product);
            var index = new NSIndexPath[] { indexPath };
            _tableView.ReloadRows(index, UITableViewRowAnimation.Automatic);
        }

        private void OnAddClick(SaleProductCell cell, bool selected)
        {
            var indexPath = _tableView.IndexPathForCell(cell);
            if (indexPath is null)
                return;

            var item = _viewModel.Items[indexPath.Row];
            _viewModel.AddProduct(item.Product);
            var index = new NSIndexPath[] { indexPath };
            _tableView.ReloadRows(index, UITableViewRowAnimation.Automatic);
            ItemAmountChanged?.Invoke(this, new EventArgs());
        }

        private void OnRemoveClick(SaleProductCell cell, bool selected)
        {
            var indexPath = _tableView.IndexPathForCell(cell);
            if (indexPath is null)
                return;
            var item = _viewModel.Items[indexPath.Row];
            _viewModel.RemoveProduct(item.Product);
            var index = new NSIndexPath[] { indexPath };
            _tableView.ReloadRows(index, UITableViewRowAnimation.Automatic);
            ItemAmountChanged?.Invoke(this, new EventArgs());
        }
    }
}