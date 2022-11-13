﻿
using System.Runtime.CompilerServices;
using DO;
namespace Dal;

public class DalOrder
{
    /// <summary>
    /// Adds an order to OrdersList
    /// </summary>
    /// <param name="order"></param>
    /// <returns>returns Id of order</returns>
    public int Add(Order order)
    {
        order.ID = DataSource.Config.NextOrderNumber;
        DataSource.OrdersList.Add(order);
        return order.ID;
    }

    /// <summary>
    /// Gets order by its Id
    /// </summary>
    /// <param name="id of order"></param>
    /// <returns>returns the order</returns>
    /// <exception cref="Exception">Throw exception if id of order doesn't exists</exception>

    public Order GetById(int id)
    {
        Order order = DataSource.OrdersList.Find(x => x?.ID == id)?? throw new Exception("Order doesn't exist");
        return order;
    }
    /// <summary>
    /// Update order that exsist in the list
    /// </summary>
    /// <param name="orderis the object which being updated"></param>
    /// <exception cref="Exception">Throw exception if order doesn't exist</exception>
    public void update(Order order)
    {
        if (!DataSource.OrdersList.Exists(x => x?.ID == order.ID))
        {
            throw new Exception("Order doesn't exist");
        }
        DataSource.OrdersList.RemoveAll(x => x?.ID == order.ID);
        DataSource.OrdersList.Add(order);
    }
    /// <summary>
    /// Gets all the orders in the list
    /// </summary>
    /// <returns>return OrdersList</returns>
    public IEnumerable<Order?> GetAll()
    {
        IEnumerable<Order?>list=DataSource.OrdersList;
        return list;
    }
    /// <summary>
    /// Deletes order from OrderItemsList
    /// </summary>
    /// <param name="id of order to delete"></param>
    /// <exception cref="Exception">Throw exception if order doesn't exist</exception>
    public void delete(int id)
    {
        if (!DataSource.OrdersList.Exists(x => x?.ID == id))
        {
            throw new Exception("Order doesn't exist");
        }
        DataSource.OrdersList.RemoveAll(x => x?.ID == id);
    }
}
