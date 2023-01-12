using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal class DalOrder : IOrder
    {
        readonly string s_orders = "orders";
        public int Add(Order order)
        {
            List<DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            order.ID = DataSource.Config.NextOrderNumber;
            ordersList.Add(order);
            XMLTools.SaveListToXMLSerializer(ordersList, s_orders);
            return order.ID;
        }

        public void Delete(int id)
        {
            List<DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            if (!ordersList.Exists(x => x?.ID == id))
            {
                throw new DalMissingIdException(id, "Order");
            }
            ordersList.RemoveAll(x => x?.ID == id);
            XMLTools.SaveListToXMLSerializer(ordersList, s_orders);
        }

        public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
        {
            List<DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            if (filter != null)
            {
                return from item in ordersList
                       where filter(item)
                       select item;
            }
            return from item in ordersList
                   select item;
        }

        public Order GetById(int id)
        {
            List<DO.Order?> Orderslist = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            Order order = Orderslist.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Order");
            return order;
        }

        public void Update(Order order)
        {
            List<DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

            if (!ordersList.Exists(x => x?.ID == order.ID))
            {
                throw new DalMissingIdException(order.ID, "Order");
            }
            ordersList.RemoveAll(x => x?.ID == order.ID);
            ordersList.Add(order);
            XMLTools.SaveListToXMLSerializer(ordersList, s_orders);
        }
    }
}
