using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class AddProductModel
    {
        public RelayCommand EditProductCommand { get; }
        Product product = new Product();

        public AddProductModel(){
            EditProductCommand = new RelayCommand(Edit, CanEdit);

        }

        public void AddProduct()
        {
            /*product.Price = 10;
            product.Name = "pies";
            product.Category = AppState.SharedContext.Categories.First();
            AppState.SharedContext.Products.Add(product);//product
            AppState.SharedContext.SaveChanges();*/
        }

        private void Edit(object parameter)
        {
            int a = 1;
        }

        private bool CanEdit(object parameter)
        {
            return product is not null;
        }

        


    }



}
