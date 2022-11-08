
using System.Runtime.CompilerServices;
using DO;
namespace Dal;

public class DalOrder
{
    /// <summary>
    /// create
    /// </summary>
    public int Add(Order order)
    {
        order.ID = DataSource.Config.NextOrderNumber;
        DataSource.OrdersList.Add(order);
        return order.ID;
    }
    public Order GetById(int id)
    {
        Order? order=DataSource.OrdersList.Find(x => x?.ID == id);
        if (order == null)
        {
            throw new Exception("Order doesn't exist");
        }
        return order.Value;
    }
    public void update(Order order)
    {
        if (!DataSource.OrdersList.Exists(x => x?.ID == order.ID))
        {
            throw new Exception("Order doesn't exist");
        }
        DataSource.OrdersList.RemoveAll(x => x?.ID == order.ID);
        DataSource.OrdersList.Add(order);
    }
    public IEnumerable<Order?> GetAll()
    {
        return DataSource.OrdersList;
    }
    public void delete(int id)
    {
        if (!DataSource.OrdersList.Exists(x => x?.ID == id))
        {
            throw new Exception("Order doesn't exist");
        }
        DataSource.OrdersList.RemoveAll(x => x?.ID == id);
    }
}
