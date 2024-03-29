﻿
using System.Runtime.Serialization;
using DalApi;
using DO;

namespace Dal;
internal class DalOrderItem : IOrderItem
{
    /// <summary>
    ///  Adds order item to OrderItemsList
    /// </summary>
    /// <param name="orderItem"> is the object which being added</param>
    /// <returns>returns Id of order item</returns>
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.Config.NextOrderItemNumber;
        DataSource.OrderItemsList.Add(orderItem);
        return orderItem.ID;
    }
    /// <summary>
    /// Gets order item by its Id
    /// </summary>
    /// <param name="id">id of order item</param>
    /// <returns>returns the order item</returns>
    /// <exception cref="DalMissingIdException">Throws an exception if id of order item doesn't exists</exception>
    public OrderItem GetById(int id)
    {
        OrderItem orderItem = DataSource.OrderItemsList.Find(x => x?.ID == id)?? throw new DalMissingIdException(id, "OrderItem");
        return orderItem;
    }
    /// <summary>
    /// Update order item that exists in the orders' list
    /// </summary>
    /// <param name="orderItem">orderItem is the object which being updated></param>
    /// <exception cref="DalMissingIdException">Throws an exception if order item doesn't exist</exception>
    public void Update(OrderItem orderItem)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == orderItem.ID))
            throw new DalMissingIdException(orderItem.ID, "OrderItem");
        DataSource.OrderItemsList.RemoveAll(x => x?.ID == orderItem.ID);
        DataSource.OrderItemsList.Add(orderItem);
    }

    /// <summary>
    ///  Gets all the orderItems in the list in case no function was transfered. Otherwize, returns the filter list. 
    /// </summary>
    /// <param name="filter">Func function</param>
    /// <returns>IEnumerable<OrderItem?></returns>
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
    /// <summary>
    /// Deletes order item from OrderItemsList
    /// </summary>
    /// <param name="id">id of order item to delete</param>
    /// <exception cref="DalMissingIdException">Throws exception if order item doesn't exist</exception>

    public void Delete (int id)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == id))
            throw new DalMissingIdException(id, "OrderItem");
        DataSource.OrderItemsList.RemoveAll(x => x?.ID == id);
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
        OrderItem orderItem = DataSource.OrderItemsList.Find(x => x?.ProductID == productID && x?.OrderID == orderID)?? throw new DalMissingIdException(-1, "OrderItem");
        return orderItem;
    }
    /// <summary>
    ///  Gets items in order
    /// </summary>
    /// <param name="orderId">orderId of order items</param>
    /// <returns>returns list of order items of the order</returns>
    public IEnumerable<OrderItem?> GetItemsInOrder(int orderId)
    {
        return DataSource.OrderItemsList.FindAll(x => x?.OrderID == orderId);
    }
}
