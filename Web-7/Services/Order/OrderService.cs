using RESTwebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTwebAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new()
{
    new() { OrderId = 1, OrderName = "Электронная книга Amazon Kindle Paperwhite", TotalAmount = 120 },
    new() { OrderId = 2, OrderName = "Умные часы Apple Watch Series 7", TotalAmount = 399 },
    new() { OrderId = 3, OrderName = "Наушники Sony WH-1000XM4", TotalAmount = 350 },
    new() { OrderId = 4, OrderName = "Кофемашина De'Longhi Magnifica S", TotalAmount = 500 },
    new() { OrderId = 5, OrderName = "Фотоаппарат Canon EOS R5", TotalAmount = 3900 },
    new() { OrderId = 6, OrderName = "Планшет Apple iPad Pro 12.9\"", TotalAmount = 999 },
    new() { OrderId = 7, OrderName = "Видеокарта NVIDIA GeForce RTX 3080", TotalAmount = 699 },
    new() { OrderId = 8, OrderName = "Электросамокат Xiaomi Mi Electric Scooter Pro 2", TotalAmount = 530 },
    new() { OrderId = 9, OrderName = "Робот-пылесос Roborock S7", TotalAmount = 649 },
    new() { OrderId = 10, OrderName = "Игровая приставка Nintendo Switch", TotalAmount = 299 },
};


        public async Task<ResponseModel<Order>> AddOrderAsync(Order order)
        {
            order.OrderId = _orders.Any() ? _orders.Max(o => o.OrderId) + 1 : 1;
            _orders.Add(order);
            return new ResponseModel<Order> { Data = order, Success = true, Message = "Order added successfully." };
        }

        public async Task<ResponseModel<Order>> DeleteOrderAsync(int id)
        {
            var orderToRemove = _orders.FirstOrDefault(o => o.OrderId == id);
            if (orderToRemove == null)
                return new ResponseModel<Order> { Data = null, Success = false, Message = $"Order with id {id} not found." };

            _orders.Remove(orderToRemove);
            return new ResponseModel<Order> { Data = orderToRemove, Success = true, Message = $"Order with id {id} deleted successfully." };
        }

        public async Task<ResponseModel<IEnumerable<Order>>> GetAllOrdersAsync() =>
            new ResponseModel<IEnumerable<Order>> { Data = _orders, Success = true, Message = "Successfully retrieved all orders." };

        public async Task<ResponseModel<Order>> GetOrderAsync(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
                return new ResponseModel<Order> { Data = null, Success = false, Message = $"Order with id {id} not found." };

            return new ResponseModel<Order> { Data = order, Success = true, Message = $"Successfully retrieved order with id {id}." };
        }

        public async Task<ResponseModel<Order>> UpdateOrderAsync(int id, Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == id);
            if (existingOrder == null)
                return new ResponseModel<Order> { Data = null, Success = false, Message = $"Order with id {id} not found." };

            existingOrder.OrderName = order.OrderName;
            existingOrder.TotalAmount = order.TotalAmount;
            return new ResponseModel<Order> { Data = existingOrder, Success = true, Message = $"Order with id {id} updated successfully." };
        }
    }
}
