using PrekrasnyDomainLayer.Services;
using PrekrasnySklep.Base;
using System.Linq;
using System.Windows;
using Order = PrekrasnySklep.Views.Forms.Order;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class OrderModel : ViewModelBase
    {
        private string _name = "";
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand PlaceOrderCommand { get; }

        public OrderModel()
        {
            PlaceOrderCommand = new RelayCommand(PlaceOrder, CanEdit);
        }

        public void PlaceOrder(object sender)
        {
            new OrderService().PlaceOrder(Name);
            Application.Current.Windows.OfType<Order>().FirstOrDefault()!.Close();
        }

        private bool CanEdit(object parameter)
        {
            return !string.IsNullOrWhiteSpace(_name);
        }
    }
}
