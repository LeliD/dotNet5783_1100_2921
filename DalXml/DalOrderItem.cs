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
        public int Add(OrderItem orderItem)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            orderItem.ID = DataSource.Config.NextOrderItemNumber;
            orderItemsList.Add(orderItem);
            return orderItem.ID;
        }

        public void Delete(int id)
        {
            if (!DataSource.OrderItemsList.Exists(x => x?.ID == id))
                throw new DalMissingIdException(id, "OrderItem");
            DataSource.OrderItemsList.RemoveAll(x => x?.ID == id);
        }

        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
        {
            if (filter != null)
            {
                return from item in DataSource.OrderItemsList
                       where filter(item)
                       select item;
            }
            return from item in DataSource.OrderItemsList
                   select item;
        }

        public OrderItem GetBy2Identifiers(int productID, int orderID)
        {
            OrderItem orderItem = DataSource.OrderItemsList.Find(x => x?.ProductID == productID && x?.OrderID == orderID) ?? throw new DalMissingIdException(-1, "OrderItem");
            return orderItem;
        }

        public OrderItem GetById(int id)
        {
            OrderItem orderItem = DataSource.OrderItemsList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "OrderItem");
            return orderItem;
        }

        public IEnumerable<OrderItem?> GetItemsInOrder(int orderId)
        {
            return DataSource.OrderItemsList.FindAll(x => x?.OrderID == orderId);

        }

        public void Update(OrderItem item)
        {
            if (!DataSource.OrderItemsList.Exists(x => x?.ID == orderItem.ID))
                throw new DalMissingIdException(orderItem.ID, "OrderItem");
            DataSource.OrderItemsList.RemoveAll(x => x?.ID == orderItem.ID);
            DataSource.OrderItemsList.Add(orderItem);
        }
    }
}
