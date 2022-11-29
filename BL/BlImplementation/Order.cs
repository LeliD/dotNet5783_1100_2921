using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class Order: IOrder
{
    DalApi.IDal dal = new Dal.DalList();

    public BO.Order GetOrderByID(int orderID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.OrderForList?> GetOrdersForManager()
    {
        var listOfOrders = from item in dal.Order.GetAll()
                    //let a = dal.OrderItem.GetItemsInOrder((int)item?.ID)
                select new BO.OrderForList()
                {
                    ID = item?.ID ?? throw new NullReferenceException("Missing ID"),
                    CustomerName = item?.CustomerName,
                    Status = orderStatus(item),
                    AmountOfItems = dal.OrderItem.GetItemsInOrder(item?.ID ?? throw new NullReferenceException("Missing ID")).Count()//x is OrderItem?
                };
        return listOfOrders;
    }

    public OrderTracking OrderTrack(int orderID)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateDeliveryDate(int orderID)
    {
        throw new NotImplementedException();
    }

    public void UpdateOrder(int orderID)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateShipDate(int orderID)
    {
        throw new NotImplementedException();
    }
    private BO.OrderStatus orderStatus(DO.Order? doOrder)
    {
        if (doOrder?.DeliveryDate == null && doOrder?.OrderDate == null && doOrder?.ShipDate == null)
            return BO.OrderStatus.Initiated;
        if (doOrder?.OrderDate != null && doOrder?.DeliveryDate == null && doOrder?.ShipDate == null)
            return BO.OrderStatus.Ordered;
        if (doOrder?.ShipDate != null && doOrder?.DeliveryDate == null)
            return BO.OrderStatus.Shipped;
        if (doOrder?.DeliveryDate != null)
            return BO.OrderStatus.Delivered;
       
        return BO.OrderStatus.Initiated;
    }
}
