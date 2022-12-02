using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BlApi;
using BO;
//using BO;
using DO;

namespace BlImplementation;

internal class Order: IOrder
{
    /// <summary>
    /// Dal variable
    /// </summary>
    DalApi.IDal dal = new Dal.DalList();


    /// <summary>
    /// The function gets orderID and returns the details of order  by it's ID  in form of BO.order
    /// </summary>
    /// <param name="orderID">The ID of the order to get its details</param>
    /// <returns>BO.order of the transferred ID</returns>
    /// <exception cref="BlDetailInvalidException">Throws exception if orderID is negative</exception>
    /// <exception cref="BlNullPropertyException">Throws exception if one of the orders is null</exception>
    /// <exception cref="BO.BlMissingEntityException">Throws exception of the dal function GetById</exception>
    public BO.Order GetOrderByID(int orderID)
    {
        try
        {

            if (orderID < 0) //if orderID is negative-invalid 
                throw new BlDetailInvalidException("ID");
            DO.Order doOrder = dal.Order.GetById(orderID); //gets the order from dal
            IEnumerable<DO.OrderItem?> doOrderItems = dal.OrderItem.GetItemsInOrder(orderID);
            var boOrderItems = from item in doOrderItems
                               select new BO.OrderItem()
                               {
                                   ID = item?.ID ?? throw new BlNullPropertyException("Null order"),
                                   Name = dal.Product.GetById(item?.ProductID ?? throw new BlNullPropertyException("Null order")).Name,//jjijiijjjjjjjjjjjj
                                   ProductID = item?.ProductID ?? throw new BlNullPropertyException("Null order"),
                                   Price = item?.Price ?? throw new BlNullPropertyException("Null order"),
                                   Amount = item?.Amount ?? throw new BlNullPropertyException("Null order"),
                                   TotalPrice = (item?.Price ?? throw new BlNullPropertyException("Null order")) * (item?.Amount ?? throw new BlNullPropertyException("Null order"))
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
                TotalPrice = boOrderItems.Sum(x => x?.TotalPrice ?? throw new BlNullPropertyException("Null order"))
            };
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("doen't exist", ex);
        }

     
    }

    /// <summary>
    ///  The function brings the list of orders from dal and returns it in form of BO.OrderForList? (For Manager)
    /// </summary>
    /// <returns>list of products in form of BO.ProductForList?</returns>
    /// <exception cref="BlNullPropertyException">Throws exception if one of the orders is null</exception>
    public IEnumerable<BO.OrderForList?> GetOrdersForManager()
    {
        var listOfOrders = from item in dal.Order.GetAll()
                           select new BO.OrderForList()
                           {
                               ID = item?.ID ?? throw new BlNullPropertyException("Null order"),
                               CustomerName = item?.CustomerName,
                               Status = orderStatus(item),
                               AmountOfItems = dal.OrderItem.GetItemsInOrder(item?.ID ?? throw new BlNullPropertyException("Null order")).Count()//x is OrderItem?
                           };
        return listOfOrders;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public BO.OrderTracking OrderTrack(int orderID)
    {
        DO.Order doOrder;
        try
        {
            doOrder = dal.Order.GetById(orderID);
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("doen't exist", ex);
        }
        List<Tuple<DateTime, string>>? tracking = new List<Tuple<DateTime, string>>();
        if (doOrder.OrderDate != null)
        {
            tracking.Add(new Tuple<DateTime, string>(doOrder.OrderDate ?? throw new BlInCorrectDatesException("order date is null"), "Ordered"));
        }
        if (doOrder.ShipDate != null)
        {
            tracking.Add(new Tuple<DateTime, string>(doOrder.ShipDate ?? throw new BlNullPropertyException("There is no ship date"),"Shipped"));
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
        try
        {
            DO.Order doOrder = dal.Order.GetById(orderID);

            if (doOrder.ShipDate == null)
                throw new BlInCorrectDatesException("ShipDate Doesn't Exist");

            if (doOrder.DeliveryDate != null)
                throw new BlInCorrectDatesException("DeliveryDate Already Exist");

            doOrder.DeliveryDate = DateTime.Now;
            dal.Order.Update(doOrder);

            return this.GetOrderByID(orderID);

        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("doen't exist", ex);
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
        dal.Order.Update(doOrder);
        //try
        //{
        //    return this.GetOrderByID(orderID);
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        return this.GetOrderByID(orderID);

    }
    /// <summary>
    /// The function returns the status of its order
    /// </summary>
    /// <param name="doOrder">The order to get its sataus</param>
    /// <returns>BO.OrderStatus</returns>
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
