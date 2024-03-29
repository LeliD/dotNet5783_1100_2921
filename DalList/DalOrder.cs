﻿
using System.Runtime.CompilerServices;
using DalApi;
using DO;
namespace Dal;

internal class DalOrder : IOrder
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
    /// <param name="id">id of order</param>
    /// <returns>returns the order</returns>
    /// <exception cref="DalMissingIdException">Throw exception if id of order doesn't exists</exception>
    public Order GetById(int id)
    {
        Order order = DataSource.OrdersList.Find(x => x?.ID == id)?? throw new DalMissingIdException(id, "Order");
        return order;
    }
    /// <summary>
    /// Update order that exsist in the list
    /// </summary>
    /// <param name="order">order is the object which being updated</param>
    /// <exception cref="DalMissingIdException">Throw exception if order doesn't exist</exception>
    public void Update(Order order)
    {
        if (!DataSource.OrdersList.Exists(x => x?.ID == order.ID))
        {
            throw new DalMissingIdException(order.ID,"Order");
        }
        DataSource.OrdersList.RemoveAll(x => x?.ID == order.ID);
        DataSource.OrdersList.Add(order);
    }
    /// <summary>
    /// Gets all the orders in the list in case no function was transfered. Otherwize, returns the filter list. 
    /// </summary>
    /// <param name="filter">filter of category</param>
    /// <returns>return IEnumerable<Order?></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.OrdersList
                   where filter(item)
                   select item;
        }
        return from item in DataSource.OrdersList
               select item;
    }
    /// <summary>
    /// Deletes order from OrderItemsList
    /// </summary>
    /// <param name="id">id of order to delete</param>
    /// <exception cref="DalMissingIdException">Throw exception if order doesn't exist</exception>
    public void Delete(int id)
    {
        if (!DataSource.OrdersList.Exists(x => x?.ID == id))
        {
            throw new DalMissingIdException(id, "Order");
        }
        DataSource.OrdersList.RemoveAll(x => x?.ID == id);
    }


}
