
using System.Runtime.CompilerServices;
using DO;
namespace Dal;

public class DalOrder
{

    /// <summary>
    /// Adds an order to OrdersList
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public int Add(Order order)
    {
        order.ID = DataSource.Config.NextOrderNumber;
        DataSource.OrdersList.Add(order);
        return order.ID;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order GetById(int id)
    {
        Order order = DataSource.OrdersList.Find(x => x?.ID == id)?? throw new Exception("Order doesn't exist");
        return order;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="Exception"></exception>
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
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll()
    {
        IEnumerable<Order?>list=DataSource.OrdersList;
        return list;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void delete(int id)
    {
        if (!DataSource.OrdersList.Exists(x => x?.ID == id))
        {
            throw new Exception("Order doesn't exist");
        }
        DataSource.OrdersList.RemoveAll(x => x?.ID == id);
    }
}
