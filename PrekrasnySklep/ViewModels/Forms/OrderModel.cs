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

        public RelayCommand AddCategoryCommand { get; }

        public OrderModel()
        {
            AddCategoryCommand = new RelayCommand(AddCategory, CanEdit);
        }

        public void AddCategory(object sender)
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
