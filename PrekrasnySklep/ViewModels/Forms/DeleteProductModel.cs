using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;
using PrekrasnySklep.Base;
using PrekrasnySklep.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace PrekrasnySklep.ViewModels.Forms
{
    public class DeleteProductModel : ViewModelBase
    {
        public Product product { get; set; }
        public RelayCommand DeleteProductCommand { get; }
        public DeleteProductModel(Product productToEdit)
        {
            product = productToEdit;
            DeleteProductCommand = new RelayCommand(DeleteProduct);
        }
        public void DeleteProduct(object sender)
        {
            var result = AppState.SharedContext.Products.Remove(product);
            AppState.SharedContext.SaveChanges();
            if (result is not null)
            {
                Application.Current.Windows.OfType<DeleteProduct>().FirstOrDefault()!.Close();
            }
        }
    }
}
