using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTwebAPI.Models;

namespace RESTwebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new()
{
    new() { Id = 1, Name = "Электронная книга Amazon Kindle Paperwhite", Price = 120 },
    new() { Id = 2, Name = "Наушники Apple AirPods Pro", Price = 200 },
    new() { Id = 3, Name = "Кофемашина De'Longhi Magnifica S", Price = 450 },
    new() { Id = 4, Name = "Робот-пылесос Roborock S7", Price = 500 },
    new() { Id = 5, Name = "Фитнес-браслет Xiaomi Mi Band 6", Price = 35 },
    new() { Id = 6, Name = "Умные весы Withings Body+", Price = 100 },
    new() { Id = 7, Name = "Умный домашний ассистент Amazon Echo Dot", Price = 50 },
    new() { Id = 8, Name = "Смартфон Google Pixel 6", Price = 600 },
    new() { Id = 9, Name = "Планшет Apple iPad Air", Price = 650 },
    new() { Id = 10, Name = "Игровая консоль Nintendo Switch", Price = 300 }
};


        public async Task<ResponseModel<Product>> AddProductAsync(Product product)
        {
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
            return new ResponseModel<Product> { Data = product, Success = true, Message = "Product added successfully." };
        }

        public async Task<ResponseModel<Product>> DeleteProductAsync(int id)
        {
            var productToRemove = _products.FirstOrDefault(p => p.Id == id);
            if (productToRemove == null)
                return new ResponseModel<Product> { Data = null, Success = false, Message = $"Product with id {id} not found." };

            _products.Remove(productToRemove);
            return new ResponseModel<Product> { Data = productToRemove, Success = true, Message = $"Product with id {id} deleted successfully." };
        }

        public async Task<ResponseModel<IEnumerable<Product>>> GetAllProductsAsync() =>
            new ResponseModel<IEnumerable<Product>> { Data = _products, Success = true, Message = "Successfully retrieved all products." };

        public async Task<ResponseModel<Product>> GetProductAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return new ResponseModel<Product> { Data = null, Success = false, Message = $"Product with id {id} not found." };

            return new ResponseModel<Product> { Data = product, Success = true, Message = $"Successfully retrieved product with id {id}." };
        }

        public async Task<ResponseModel<Product>> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
                return new ResponseModel<Product> { Data = null, Success = false, Message = $"Product with id {id} not found." };

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return new ResponseModel<Product> { Data = existingProduct, Success = true, Message = $"Product with id {id} updated successfully." };
        }
    }
}
