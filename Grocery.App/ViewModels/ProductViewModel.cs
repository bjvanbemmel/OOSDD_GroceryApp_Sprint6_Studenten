﻿using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        public ObservableCollection<Product> Products { get; set; }

        public ProductViewModel(IProductService productService)
        {
            _productService = productService;
            Products = [];
            foreach (Product p in _productService.GetAll()) Products.Add(p);
        }

        [RelayCommand]
        public void UpdateProduct(Product? product)
        {
            if (product == null)
            {
                return;
                //throw new ArgumentNullException();
            }

            _productService.Update(product);
        }
    }
}
