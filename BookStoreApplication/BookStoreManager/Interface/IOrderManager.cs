using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IOrderManager
    {
        public PlaceOrderModel PlaceOrder(PlaceOrderModel placeOrderModel);
        public List<GetOrdersModel> GetAllOrders(int UserID);
        public bool DeleteOrder(int OrderID, int UserID);
    }
}
