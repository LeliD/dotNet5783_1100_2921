using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BlApi;
//using BO;
using DO;

namespace BlImplementation;

internal class Order: IOrder
{
    DalApi.IDal dal = new Dal.DalList();

    public BO.Order GetOrderByID(int orderID)
    {
        DO.Order doOrder;
        if (orderID < 0)
            throw new Exception("Wrong (Negative) ID");
        try
        {
            doOrder = dal.Order.GetById(orderID);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw ex;
        }
        IEnumerable<DO.OrderItem?> doOrderItems = dal.OrderItem.GetItemsInOrder(orderID);
        var boOrderItems = from item in doOrderItems
                select new BO.OrderItem()
                {
                    ID = item?.ID ?? throw new Exception("Missing ID"),
                    Name = dal.Product.GetById(item?.ProductID ?? throw new Exception("Missing Product ID")).Name,
                    ProductID = item?.ProductID ?? throw new Exception("Missing Product ID"),
                    Price = item?.Price ?? throw new Exception("Missing Price"),
                    Amount = item?.Amount ?? throw new Exception("Missing Price"),
                    TotalPrice = (item?.Price ?? throw new Exception("Missing Price")) * (item?.Amount ?? throw new Exception("Missing Price"))
                };
        return new BO.Order()
        {
            ID = doOrder.ID,
            CustomerName = doOrder.CustomerName,
            CustomerEmail = doOrder.CustomerEmail,
            CustomerAddress = doOrder.CustomerAddress,
            Status = orderStatus(doOrder),
            OrderDate = doOrder.OrderDate,
            ShipDate = doOrder.ShipDate,
            DeliveryDate = doOrder.DeliveryDate,
            Items = boOrderItems,
            TotalPrice = boOrderItems.Sum(x => x?.TotalPrice ?? throw new Exception("Missing Price"))
        };
        //throw new NotImplementedException();
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

    public BO.OrderTracking OrderTrack(int orderID)
    {
        DO.Order doOrder;
        try
        {
            doOrder = dal.Order.GetById(orderID);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw ex;
        }
        List<Tuple<DateTime, string>>? tracking = new List<Tuple<DateTime, string>>();
        if (doOrder.OrderDate != null)
        {
            tracking.Add(new Tuple<DateTime, string>(doOrder.OrderDate ?? throw new Exception("There is no order date"), "Ordered"));
        }
        if (doOrder.ShipDate != null)
        {
            tracking.Add(new Tuple<DateTime, string>(doOrder.ShipDate ?? throw new Exception("There is no ship date"),"Shipped"));
        }
        if (doOrder.DeliveryDate != null)
        {
            tracking.Add(new Tuple<DateTime, string>(doOrder.DeliveryDate ?? throw new Exception("There is no Delivery Date"), "Delivered"));
        }

        return new BO.OrderTracking()
        {
            ID = doOrder.ID,
            Status = orderStatus(doOrder),
            Tracking = tracking
        };
       
    }

    public BO.Order UpdateDeliveryDate(int orderID)
    {
        DO.Order doOrder;
        try
        {
            doOrder = dal.Order.GetById(orderID);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw ex;
        }
        if (doOrder.ShipDate == null)
            throw new Exception("ShipDateDoesn'tExist");

        if (doOrder.DeliveryDate != null)
            throw new Exception("DeliveryDateAlreadyExist");

        doOrder.DeliveryDate = DateTime.Now;
        try
        {
            return this.GetOrderByID(orderID);
        }
        catch (BO.DalDoesNotExistException ex)
        {
            throw ex;
        }

    }

    public void UpdateOrder(int orderID)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateShipDate(int orderID)
    {
        DO.Order doOrder;
        try
        {
            doOrder = dal.Order.GetById(orderID);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw ex;
        }
        if (doOrder.ShipDate != null)
        {
            throw new Exception("ShipDateAlreadyExist");
        }
        doOrder.ShipDate = DateTime.Now;
        try
        {
            return this.GetOrderByID(orderID);
        }
        catch (Exception ex)
        {
            throw ex;
        }

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
