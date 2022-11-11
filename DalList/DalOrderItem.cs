

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
        OrderItem orderItem = DataSource.OrderItemsList.Find(x => x?.ID == id)?? throw new Exception("OrderItem doesn't exist");
        return orderItem;
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
        IEnumerable<OrderItem?> list= DataSource.OrderItemsList;
        return list;
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
        OrderItem orderItem = DataSource.OrderItemsList.Find(x => x?.ProductID == productID && x?.OrderID == orderID)?? throw new Exception("OrderItem doesn't exist");
        return orderItem;
    }
    public IEnumerable<OrderItem?> GetItemsInOrder(int orderId)
    {
        return DataSource.OrderItemsList.FindAll(x => x?.OrderID == orderId);
    }
}
