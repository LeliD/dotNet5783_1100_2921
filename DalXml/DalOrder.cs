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
        /// <summary>
        /// name of orders xml file
        /// </summary>
        readonly string s_orders = "orders";
        /// <summary>
        /// Adds an order to orders xml file
        /// </summary>
        /// <param name="order"></param>
        /// <returns>returns Id of order</returns>
        public int Add(Order order)
        {
            List<DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            order.ID = Config.GetNextOrderID();
            ordersList.Add(order);
            Config.SaveNextOrderID(order.ID + 1);
            XMLTools.SaveListToXMLSerializer(ordersList, s_orders);
            return order.ID;
        }
        /// <summary>
        /// Deletes order from orders xml file
        /// </summary>
        /// <param name="id">id of order to delete</param>
        /// <exception cref="DalMissingIdException">Throw exception if order doesn't exist</exception>
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
        /// <summary>
        /// Gets all the orders in orders xml file in case no function was transfered. Otherwize, returns the filter orders xml file. 
        /// </summary>
        /// <param name="filter">filter of category</param>
        /// <returns>return IEnumerable<Order?></returns>
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
        /// <summary>
        /// Gets order by its Id
        /// </summary>
        /// <param name="id">id of order</param>
        /// <returns>returns the order</returns>
        /// <exception cref="DalMissingIdException">Throw exception if id of order doesn't exists</exception>
        public Order GetById(int id)
        {
            List<DO.Order?> Orderslist = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
            Order order = Orderslist.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Order");
            return order;
        }
        /// <summary>
        /// Update order that exsist in orders xml file
        /// </summary>
        /// <param name="order">order is the object which being updated</param>
        /// <exception cref="DalMissingIdException">Throw exception if order doesn't exist</exception>
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
