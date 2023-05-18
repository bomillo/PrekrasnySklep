using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.ViewModels.Tabs;
using System.Collections.Generic;

namespace PrekrasnySklep.ViewModels.OrderDispatcher
{
    internal class DispatchViewModel : ViewModelBase
    {
        public RelayCommand ChangeStatusCommand { get; }

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

        public List<OrderItem> Items
        {
            get => order.Items;
        }

        public DispatchViewModel(OrderDispatcherViewModel parent, Order order)
        {
            _parent = parent;
            Order = order;
            ChangeStatusCommand = new RelayCommand(ChangeStatus);
        }

        public void ChangeStatus(object sender)
        {
            Order.Status = PrekrasnyDomainLayer.Models.Enums.OrderStatus.Delivered;
            AppState.SharedContext.Update(Order);
            AppState.SharedContext.SaveChanges();
            _parent.CurrentPanel = new DispatcherListViewModel(_parent);
        }
    }
}
