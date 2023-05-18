using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
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
