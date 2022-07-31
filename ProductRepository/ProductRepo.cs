using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.GenericRepository;
using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.ProductRepository
{
    public class ProductRepo : IProductRepo
    {

        private readonly IGenericRepo<Product> _repository;

        public ProductRepo(IGenericRepo<Product> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateProduct(Product product)
        {
            bool existingProduct = await _repository.EntityExistsAsync(product.Id);

            if (existingProduct)
            {
                return -1;
            }

            int saveResult = await _repository.CreateAsync(product);

            return saveResult;

          
        }

        public List<Product> GetAllProducts()
        {

            var products = _repository.GetAll();
            return products.AsQueryable().ToList();
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product;
        }

        //public async Task<int> UpdateProduct([FromBody]Product model, int id)
        //{
        //    var product = await _repository.UpdateAsync(model);

        //        //    public async Task<TEntity> Update(TEntity entity)
        //        //{
        //        //    context.Entry(entity).State = EntityState.Modified;
        //        //    await context.SaveChangesAsync();
        //        //    return entity;
        //        //}
        //    return product;
        //}
    }
}
