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
        /// <summary>
        /// name of orderItems xml file 
        /// </summary>
        readonly string s_orderItems = "orderItems";
        /// <summary>
        /// Adds order item to orderItems xml file 
        /// </summary>
        /// <param name="orderItem">orderItem to add</param>
        /// <returns>returns Id of order item</returns>
        public int Add(OrderItem orderItem)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            //orderItem.ID = DataSource.Config.NextOrderItemNumber;
            orderItem.ID = Config.GetNextOrderItemID();
            orderItemsList.Add(orderItem);
            Config.SaveNextOrderItemID(orderItem.ID + 1);
            XMLTools.SaveListToXMLSerializer(orderItemsList, s_orderItems);
            return orderItem.ID;
        }
        /// <summary>
        /// Deletes order item from orderItems xml file
        /// </summary>
        /// <param name="id">id of order item to delete</param>
        /// <exception cref="DalMissingIdException">Throws exception if order item doesn't exist</exception>
        public void Delete(int id)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            if (!orderItemsList.Exists(x => x?.ID == id))
                throw new DalMissingIdException(id, "OrderItem");
            orderItemsList.RemoveAll(x => x?.ID == id);
            XMLTools.SaveListToXMLSerializer(orderItemsList, s_orderItems);
        }
        /// <summary>
        ///  Gets all the orderItems from orderItems xml file in case no function was transfered. Otherwize, returns the filter list. 
        /// </summary>
        /// <param name="filter">Func function</param>
        /// <returns>IEnumerable<OrderItem?></returns>
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
        /// <summary>
        /// Gets order item by its productID and orderID
        /// </summary>
        /// <param name="productID">productID of order item</param>
        /// <param name="orderID">orderID of order item</param>
        /// <returns>returns order item</returns>
        /// <exception cref="DalMissingIdException">Throw exception if order item doesn't exist</exception>
        public OrderItem GetBy2Identifiers(int productID, int orderID)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            OrderItem orderItem = orderItemsList.Find(x => x?.ProductID == productID && x?.OrderID == orderID) ?? throw new DalMissingIdException(-1, "OrderItem");
            return orderItem;
        }
        /// <summary>
        /// Gets order item by its Id
        /// </summary>
        /// <param name="id">id of order item</param>
        /// <returns>returns the order item</returns>
        /// <exception cref="DalMissingIdException">Throws an exception if id of order item doesn't exists</exception>
        public OrderItem GetById(int id)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            OrderItem orderItem = orderItemsList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "OrderItem");
            return orderItem;
        }
        /// <summary>
        ///  Gets items in order
        /// </summary>
        /// <param name="orderId">orderId of order items</param>
        /// <returns>returns list of order items of the order</returns>
        public IEnumerable<OrderItem?> GetItemsInOrder(int orderId)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
            return orderItemsList.FindAll(x => x?.OrderID == orderId);
        }
        /// <summary>
        /// Update order item that exists in orderItems' xml file 
        /// </summary>
        /// <param name="orderItem">orderItem is the object which being updated</param>
        /// <exception cref="DalMissingIdException">Throws an exception if order item doesn't exist</exception>
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
