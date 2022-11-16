using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        OrderItem GetBy2Identifiers(int productID, int orderID);
        IEnumerable<OrderItem> GetItemsInOrder(int orderId);
    }
}
