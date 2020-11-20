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
            TableView.Source = new CartDataSource(ViewModel);
            Title = ViewModel.Title;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            LblAmount.Text = $"{ViewModel.Cart.Count} UN";
            LblTotalValue.Text = $"{ViewModel.Cart.Total:R$ ###,###,##0.00}";
            ViewModel.LoadItems();
            TableView.ReloadData();
            BtnFinishOrder.TouchUpInside += OnTouchUpInside;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            BtnFinishOrder.TouchUpInside -= OnTouchUpInside;
        }

        private void OnTouchUpInside(object sender, EventArgs e)
        {
            ShowMessage();
        }

        private void ClearCart()
        {
            ViewModel.ClearCart();
            NavigationController.PopViewController(true);
        }

        private void ShowMessage()
        {
            var alert = UIAlertController.Create(
                                            "Pedido finalizado",
                                            "Seu pedido foi finalizado com sucesso!",
                                            UIAlertControllerStyle.Alert);


            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, action => ClearCart()));
            PresentViewController(alert, true, null);
        }
    }

    class CartDataSource : UITableViewSource
    {
        readonly CartViewModel _viewModel;

        public CartDataSource(CartViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override nint RowsInSection(UITableView tableview, nint section) => _viewModel.Items.Count;

        public override nint NumberOfSections(UITableView tableView) => 1;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("CartEntryViewCell", indexPath) as CartEntryViewCell;
            var item = _viewModel.Items[indexPath.Row];
            cell.SetValues(item);
            return cell;
        }
    }
}