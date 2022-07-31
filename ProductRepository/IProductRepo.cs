using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.ProductRepository
{
    public interface IProductRepo
    {
        Task<Product> GetProduct(int id);
        Task<int> CreateProduct(Product product);

        //Task<int> UpdateProduct(Product product, int id);
        //Task<int> DeleteAsync(int id, bool isSave = true);
        //Task<int> SaveChangesToDbAsync();
        //Task<bool> EntityExistsAsync(int id);
        List<Product> GetAllProducts();
    }
}
