using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository orderRepository;
        public OrderManager(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public PlaceOrderModel PlaceOrder(PlaceOrderModel placeOrderModel)
        {
            try
            {
                return this.orderRepository.PlaceOrder(placeOrderModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<GetOrdersModel> GetAllOrders(int UserID)
        {
            try
            {
                return this.orderRepository.GetAllOrders(UserID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteOrder(int OrderID, int UserID)
        {
            try
            {
                return this.orderRepository.DeleteOrder(OrderID, UserID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
