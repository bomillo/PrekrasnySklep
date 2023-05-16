﻿using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrekrasnySklep.ViewModels.Tabs;

public class OrderManagerViewModel : TabbedViewModel
{
    private ObservableCollection<DisplayOrder> orders;
    private Order? selectedOrder;

    public OrderManagerViewModel() : base(title: "Orders Manager") {
        SelectedOrder = null!;

        Orders = new ObservableCollection<DisplayOrder>(
            AppState.SharedContext.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(it => it.Product)
                .Select(o => new DisplayOrder() { Order = o, TotalPrice = o.Items.Sum(it => it.Quantity * it.Product.Price) })
                .ToList()
        );
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
}