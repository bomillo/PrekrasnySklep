﻿using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrekrasnySklep.ViewModels.Tabs;

public class InventoryViewerViewModel : TabbedViewModel
{
    private readonly ProductService _productService;

    private ObservableCollection<Product> products;
    private Product? selectedProduct;

    public InventoryViewerViewModel() : base(title: "Inventory Viewer") {
        _productService = new ProductService();

        SelectedProduct = null!;
        Products = new ObservableCollection<Product>(AppState.SharedContext.Products.Include(p => p.Category).ToList());
    }

    public Product SelectedProduct
    {
        get => selectedProduct!;
        set
        {
            selectedProduct = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Product> Products
    {
        get => products;
        set
        {
            products = value;
            SelectedProduct = null!;
            OnPropertyChanged();
        }
    }
}
