using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    internal class DalOrderItem : IOrderItem
    {
        readonly string s_orderItems = "orderItems";
        public int Add(OrderItem item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public OrderItem GetBy2Identifiers(int productID, int orderID)
        {
            throw new NotImplementedException();
        }

        public OrderItem GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem?> GetItemsInOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderItem item)
        {
            throw new NotImplementedException();
        }
    }
}
