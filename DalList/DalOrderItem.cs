
using System.Runtime.Serialization;
using DalApi;
using DO;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    /// <summary>
    ///  Adds order item to OrderItemsList
    /// </summary>
    /// <param name="orderItem is the object which being added"></param>
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
    /// <param name="id of order item"></param>
    /// <returns>returns the order item</returns>
    /// <exception cref="Exception">Throw exception if id of order item doesn't exists</exception>
    public OrderItem GetById(int id)
    {
        OrderItem orderItem = DataSource.OrderItemsList.Find(x => x?.ID == id)?? throw new DalDoesNotExistException("OrderItem doesn't exist");
        return orderItem;
    }
    /// <summary>
    /// Update order item that exsist in the list
    /// </summary>
    /// <param name="orderItem is the object which being updated"></param>
    /// <exception cref="Exception">Throw exception if order item doesn't exist</exception>
    public void Update(OrderItem orderItem)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == orderItem.ID))
        {
            throw new DalDoesNotExistException("OrderItem doesn't exist");
        }
        DataSource.OrderItemsList.RemoveAll(x => x?.ID == orderItem.ID);
        DataSource.OrderItemsList.Add(orderItem);
    }
    /// <summary>
    /// Gets all the order items in the list
    /// </summary>
    /// <returns>return OrderItemsList</returns>
    public IEnumerable<OrderItem?> GetAll()
    {
        List<OrderItem?> list = new List<OrderItem?>();
        foreach (OrderItem? item in DataSource.OrderItemsList)
            list.Add(item);
        return list;
    }
    /// <summary>
    /// Deletes order item from OrderItemsList
    /// </summary>
    /// <param name="id of order item to delete"></param>
    /// <exception cref="Exception">Throw exception if order item doesn't exist</exception>
    public void Delete (int id)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("OrderItem doesn't exist");
        }
        DataSource.OrderItemsList.RemoveAll(x => x?.ID == id);
    }
    /// <summary>
    /// Gets order item by its productID and orderID
    /// </summary>
    /// <param name="productID of order item"></param>
    /// <param name="orderID of order item"></param>
    /// <returns>returns order item</returns>
    /// <exception cref="Exception">Throw exception if order item doesn't exist</exception>
    public OrderItem GetBy2Identifiers(int productID, int orderID)
    {
        OrderItem orderItem = DataSource.OrderItemsList.Find(x => x?.ProductID == productID && x?.OrderID == orderID)?? throw new DalDoesNotExistException("OrderItem doesn't exist");
        return orderItem;
    }
    /// <summary>
    ///  Gets items in order
    /// </summary>
    /// <param name="orderId of order items "></param>
    /// <returns>return list of order items of the order</returns>
    public IEnumerable<OrderItem?> GetItemsInOrder(int orderId)
    {
        return DataSource.OrderItemsList.FindAll(x => x?.OrderID == orderId);
    }
}
