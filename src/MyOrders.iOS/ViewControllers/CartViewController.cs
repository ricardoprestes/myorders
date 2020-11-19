using Foundation;
using MyOrders.Helpers;
using MyOrders.Services.Abstractions;
using MyOrders.ViewModels;
using System;
using UIKit;

namespace MyOrders.iOS
{
    public partial class CartViewController : UIViewController
    {
        public CartViewModel ViewModel { get; set; }

        public CartViewController(IntPtr handle) : base(handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var cartService = ServiceLocator.Instance.Get<ICartService>();
            ViewModel = new CartViewModel(cartService);
            TableView.Source = new CartDataSource(ViewModel, TableView);
            Title = ViewModel.Title;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            LblAmount.Text = $"{ViewModel.Cart.Count} UN";
            LblTotalValue.Text = $"{ViewModel.Cart.Total:R$ ###,###,##0.00}";
            ViewModel.LoadItems();
            TableView.ReloadData();
        }
    }

    class CartDataSource : UITableViewSource
    {
        readonly CartViewModel _viewModel;
        readonly UITableView _tableView;

        public CartDataSource(CartViewModel viewModel, UITableView tableView)
        {
            _viewModel = viewModel;
            _tableView = tableView;
        }

        public override nint RowsInSection(UITableView tableview, nint section) => _viewModel.Items.Count;

        public override nint NumberOfSections(UITableView tableView) => 1;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("CartEntryViewCell", indexPath);

            var item = _viewModel.Items[indexPath.Row];
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, "CartEntryViewCell");
            }

            cell.TextLabel.Text = item.Product.Name;
            return cell;
        }
    }
}