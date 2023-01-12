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
            //orderItem.ID = DataSource.Config.NextOrderItemNumber;
            orderItemsList.Add(orderItem);
            XMLTools.SaveListToXMLSerializer(orderItemsList, s_orderItems);
            return orderItem.ID;
        }

        public void Delete(int id)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            if (!orderItemsList.Exists(x => x?.ID == id))
                throw new DalMissingIdException(id, "OrderItem");
            orderItemsList.RemoveAll(x => x?.ID == id);
            XMLTools.SaveListToXMLSerializer(orderItemsList, s_orderItems);

        }

        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            if (filter != null)
            {
                return from item in orderItemsList
                       where filter(item)
                       select item;
            }
            return from item in orderItemsList
                   select item;
        }

        public OrderItem GetBy2Identifiers(int productID, int orderID)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            OrderItem orderItem = orderItemsList.Find(x => x?.ProductID == productID && x?.OrderID == orderID) ?? throw new DalMissingIdException(-1, "OrderItem");
            return orderItem;
        }

        public OrderItem GetById(int id)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            OrderItem orderItem = orderItemsList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "OrderItem");
            return orderItem;
        }

        public IEnumerable<OrderItem?> GetItemsInOrder(int orderId)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            return orderItemsList.FindAll(x => x?.OrderID == orderId);

        }

        public void Update(OrderItem orderItem)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            if (!orderItemsList.Exists(x => x?.ID == orderItem.ID))
                throw new DalMissingIdException(orderItem.ID, "OrderItem");
            orderItemsList.RemoveAll(x => x?.ID == orderItem.ID);
            orderItemsList.Add(orderItem);
        }
    }
}
