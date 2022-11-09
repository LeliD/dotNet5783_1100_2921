

using DO;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.Config.NextOrderItemNumber;
        DataSource.OrderItemsList.Add(orderItem);
        return orderItem.ID;
    }
    public OrderItem GetById(int id)
    {
        OrderItem? orderItem = DataSource.OrderItemsList.Find(x => x?.ID == id);
        if (orderItem == null)
        {
            throw new Exception("OrderItem doesn't exist");
        }
        return orderItem.Value;
    }
    public void update(OrderItem orderItem)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == orderItem.ID))
        {
            throw new Exception("OrderItem doesn't exist");
        }
        DataSource.OrderItemsList.RemoveAll(x => x?.ID == orderItem.ID);
        DataSource.OrderItemsList.Add(orderItem);
    }
    public IEnumerable<OrderItem?> GetAll()
    {
        return DataSource.OrderItemsList;
    }
    public void delete (int id)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == id))
        {
            throw new Exception("OrderItem doesn't exist");
        }
        DataSource.OrderItemsList.RemoveAll(x => x?.ID == id);
    }
    public OrderItem GetBy2Identifiers(int productID, int orderID)
    {
        OrderItem? orderItem = DataSource.OrderItemsList.Find(x => x?.ProductID == productID && x?.OrderID == orderID);
        if (orderItem == null)
        {
            throw new Exception("OrderItem doesn't exist");
        }
        return orderItem.Value;
    }
    public IEnumerable<OrderItem?> GetItemsInOrder(int orderId)
    {
        return DataSource.OrderItemsList.FindAll(x => x?.ID == orderId);
        
    }
}
