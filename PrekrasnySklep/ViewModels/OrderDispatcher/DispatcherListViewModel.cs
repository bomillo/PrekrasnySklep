using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.ViewModels.Tabs;
using System.Collections.Generic;
using System.Linq;

namespace PrekrasnySklep.ViewModels.OrderDispatcher
{
    internal class DispatcherListViewModel : ViewModelBase
    {
        public RelayCommand GoToDetailsCommand { get; }

        private readonly OrderDispatcherViewModel _parent;
        public DispatcherListViewModel(OrderDispatcherViewModel parent)
        {
            _parent = parent;
            GoToDetailsCommand = new RelayCommand(GoToDetails, CanExecute);
            LoadOrders();
        }

        private void LoadOrders(string search = "")
        {
            Orders = AppState.SharedContext.Orders
                    .Include(o => o.User)
                    .Include(o => o.Items)
                    .ThenInclude(it => it.Product)
                    .Where(o => o.Status == PrekrasnyDomainLayer.Models.Enums.OrderStatus.Ordered)
                    .Where(o => o.Recepient.Contains(search))
                    .ToList();
        }

        private List<Order> orders;

        public List<Order> Orders
        {
            get => orders;
            set
            {
                orders = value;
                OnPropertyChanged();
            }
        }

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


        private string searchString;

        public string SearchString
        {
            get => searchString;
            set
            {
                searchString = value;
                LoadOrders(searchString ?? "");
                OnPropertyChanged();
            }
        }

        private void GoToDetails(object sender)
        {
            _parent.CurrentPanel = new DispatchViewModel(_parent, Order);
        }

        private bool CanExecute(object sender)
        {
            return Order is not null;
        }
    }
}
