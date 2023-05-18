using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrekrasnyDomainLayer.Models.Enums;

using Order = PrekrasnyDomainLayer.Models.Order;

namespace PrekrasnySklep.ViewModels.Tabs;

public class OrderManagerViewModel : TabbedViewModel
{
    private ObservableCollection<DisplayOrder> orders;
    private Order? selectedOrder;

    public RelayCommand ChangeStatusCommand { get; }
    public RelayCommand RemoveCommand { get; }

    public OrderManagerViewModel() : base(title: "Orders Manager") {
        SelectedOrder = null!;

        ChangeStatusCommand = new RelayCommand(ChangeStatus, _ => SelectedOrder is not null);
        RemoveCommand = new RelayCommand(Remove, _ => SelectedOrder is not null);

        LoadOrders();
    }

    private void LoadOrders() {
        Orders = new ObservableCollection<DisplayOrder>(
                AppState.SharedContext.Orders
                    .Include(o => o.User)
                    .Include(o => o.Items)
                    .ThenInclude(it => it.Product)
                    .Select(o => new DisplayOrder() { Order = o, TotalPrice = o.Items.Sum(it => it.Quantity * it.Product.Price) })
                    .ToList()
            );
    }

    private void ChangeStatus(object obj)
    {
        SelectedOrder.Status = SelectedOrder.Status == OrderStatus.Ordered ? OrderStatus.Delivered : OrderStatus.Ordered;
        AppState.SharedContext.Orders.Update(SelectedOrder);
        AppState.SharedContext.SaveChanges();
        LoadOrders();
        OnPropertyChanged();
    }

    private void Remove(object obj)
    {
        AppState.SharedContext.Orders.Remove(SelectedOrder);
        AppState.SharedContext.SaveChanges();
        LoadOrders();
        OnPropertyChanged();
    }

    public Order SelectedOrder
    {
        get => selectedOrder!;
        set
        {
            selectedOrder = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<DisplayOrder> Orders
    {
        get => orders;
        set
        {
            orders = value;
            SelectedOrder = null!;
            OnPropertyChanged();
        }
    }

    public override void Sync()
    {
        base.Sync();
        LoadOrders();
        }
}