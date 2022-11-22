using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation;

internal class Order: IOrder
{
    IEnumerable<OrderForList> GetOrdersForManager()
    {
        throw new NotImplementedException();
    }
    Order GetOrderByID(int orderID)
    {
        throw new NotImplementedException();
    }
    Order UpdateShipDate(int orderID)
    {
        throw new NotImplementedException();
    }
    Order UpdateDeliveryDate(int orderID)
    {
        throw new NotImplementedException();
    }
    OrderTracking OrderTrack(int orderID)
    {
        throw new NotImplementedException();
    }
    void UpdateOrder(int orderID)//bonus
    {
        throw new NotImplementedException();
    }
}
