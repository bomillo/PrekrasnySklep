using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.ViewModels.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrekrasnySklep.ViewModels.OrderDispatcher
{
    internal class DispatchViewModel : ViewModelBase
    {
        public RelayCommand AddToCart { get; }

        private OrderDispatcherViewModel _parent;

        private Order order;

        public Order Order
        {
            get => order;
            set
            {
                order = value;
                OnPropertyChanged();
            }
        }

        public List<OrderItem> Items {
            get => order.Items;
        }

        public DispatchViewModel(OrderDispatcherViewModel parent, Order order)
        {
            _parent = parent;
            Order = order;
            AddToCart = new RelayCommand(OpenProductList);
        }

        public void OpenProductList(object sender)
        {
            Order.Status = PrekrasnyDomainLayer.Models.Enums.OrderStatus.Delivered;
            AppState.SharedContext.Update(Order);
            AppState.SharedContext.SaveChanges();
            _parent.CurrentPanel = new DispatcherListViewModel(_parent);
        }
    }
}
