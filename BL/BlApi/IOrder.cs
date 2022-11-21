using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IOrder
{
    IEnumerable<OrderForList> GetOrdersForManager();
    Order GetOrderByID(int orderID);
    Order UpdateShipDate(int orderID);
    Order UpdateDeliveryDate(int orderID);
    OrderTracking OrderTrack(int orderID);
    void UpdateOrder(int orderID);//bonus

}
