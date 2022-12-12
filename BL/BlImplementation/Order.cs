using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BlApi;

namespace BlImplementation;

internal class Order: IOrder
{
    /// <summary>
    /// Dal variable
    /// </summary>
    DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// The function gets orderID and returns the details of order  by it's ID  in form of BO.order
    /// </summary>
    /// <param name="orderID">The ID of the order to get its details</param>
    /// <returns>BO.order of the transferred ID</returns>
    /// <exception cref="BO.BlDetailInvalidException">Throws exception if orderID is negative</exception>
    /// <exception cref="BO.BlNullPropertyException">Throws exception if one of the orders is null</exception>
    /// <exception cref="BO.BlMissingEntityException">Throws exception of the dal function GetById</exception>
    public BO.Order GetOrderByID(int orderID)
    {
        try
        {

            if (orderID < 0) //if orderID is negative-invalid 
                throw new BO.BlDetailInvalidException("ID", "Negative Order ID");
            DO.Order doOrder = dal.Order.GetById(orderID); //gets the order from dal
            IEnumerable<DO.OrderItem?> doOrderItems = dal.OrderItem.GetItemsInOrder(orderID);
            var boOrderItems = from item in doOrderItems//gets the orderItems of the transferred order from dal
                               select new BO.OrderItem()
                               {
                                   ID = item?.ID ?? throw new BO.BlNullPropertyException("Null order"),
                                   Name = dal.Product.GetById(item?.ProductID ?? throw new BO.BlNullPropertyException("Null order")).Name,//jjijiijjjjjjjjjjjj
                                   ProductID = item?.ProductID ?? throw new BO.BlNullPropertyException("Null order"),
                                   Price = item?.Price ?? throw new BO.BlNullPropertyException("Null order"),
                                   Amount = item?.Amount ?? throw new BO.BlNullPropertyException("Null order"),
                                   TotalPrice = (item?.Price ?? throw new BO.BlNullPropertyException("Null order")) * (item?.Amount ?? throw new BO.BlNullPropertyException("Null order"))
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
                TotalPrice = boOrderItems.Sum(x => x?.TotalPrice ?? throw new BO.BlNullPropertyException("Null order"))
            };
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Doesn't exist", ex);
        }

     
    }

    /// <summary>
    ///  The function brings the list of orders from dal and returns it in form of BO.OrderForList? (For Manager)
    /// </summary>
    /// <returns>list of products in form of BO.ProductForList?</returns>
    /// <exception cref="BO.BlNullPropertyException">Throws exception if one of the orders is null</exception>
    public IEnumerable<BO.OrderForList?> GetOrdersForManager()
    {
        var listOfOrders = from item in dal.Order.GetAll()
                           select new BO.OrderForList()
                           {
                               ID = item?.ID ?? throw new BO.BlNullPropertyException("Null order"),
                               CustomerName = item?.CustomerName,
                               Status = orderStatus(item),
                               AmountOfItems = dal.OrderItem.GetItemsInOrder(item?.ID ?? throw new BO.BlNullPropertyException("Null order")).Count(),
                               TotalPrice= dal.OrderItem.GetItemsInOrder(item?.ID ?? throw new BO.BlNullPropertyException("Null order")).Sum(x=>x?.Price * x?.Amount)?? throw new BO.BlNullPropertyException("Null Price")
                           };
        return listOfOrders;
    }
    /// <summary>
    /// The function tracks the order status
    /// </summary>
    /// <param name="orderID">The ID of order to track</param>
    /// <returns>BO.OrderTracking</returns>
    /// <exception cref="BO.BlMissingEntityException">Throws exception if one of the orders is null</exception>
    /// <exception cref="BO.BlNullPropertyException">Throws exception if one of the property of order is null</exception>
    public BO.OrderTracking OrderTrack(int orderID)
    {
        DO.Order doOrder;
        try
        {
            doOrder = dal.Order.GetById(orderID);//gets order from dal by its id
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Order doesn't exist", ex);
        }
        List<Tuple<DateTime, string>>? tracking = new List<Tuple<DateTime, string>>();
        if (doOrder.OrderDate != null) //if there is an order date
        {
            tracking.Add(new Tuple<DateTime, string>(doOrder.OrderDate ?? throw new BO.BlNullPropertyException("order date is null"), "Ordered"));
        }
        if (doOrder.ShipDate != null)//if there is a ship date
        {
            tracking.Add(new Tuple<DateTime, string>(doOrder.ShipDate ?? throw new BO.BlNullPropertyException("ship date is null"),"Shipped"));
        }
        if (doOrder.DeliveryDate != null)//if there is delivery date
        {
            tracking.Add(new Tuple<DateTime, string>(doOrder.DeliveryDate ?? throw new BO.BlNullPropertyException("delivery Date is null"), "Delivered"));
        }

        return new BO.OrderTracking()
        {
            ID = doOrder.ID,
            Status = orderStatus(doOrder),
            Tracking = tracking
        };
       
    }
    /// <summary>
    /// The function updates delivery date
    /// </summary>
    /// <param name="orderID">The ID of order to update its delivery date</param>
    /// <returns>BO.Order after the update</returns>
    /// <exception cref="BO.BlInCorrectDatesException">Throws exception if there is no ship date or delivery date already exists </exception>
    /// <exception cref="BO.BlMissingEntityException">Throws exception of the dal function "GetById"</exception>
    public BO.Order UpdateDeliveryDate(int orderID)
    {
        try
        {
            DO.Order doOrder = dal.Order.GetById(orderID);//gets order from dal by its id

            if (doOrder.ShipDate == null)//if there is no ship date
                throw new BO.BlInCorrectDatesException("Ship date doesn't exist");

            if (doOrder.DeliveryDate != null)//if delivery date already exists
                throw new BO.BlInCorrectDatesException("Delivery date already exists");

            doOrder.DeliveryDate = DateTime.Now; //updates delivery date
            dal.Order.Update(doOrder); //updates order after the change

            return this.GetOrderByID(orderID); //returns order after the change 

        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Order doen't exist", ex);
        }
    }

    public void UpdateOrder(int orderID)//Bonus
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// The function updates an order ship date
    /// </summary>
    /// <param name="orderID">The ID of order to update its ship date</param>
    /// <returns>BO.Order after the update</returns>
    /// <exception cref="BO.BlInCorrectDatesException">Throws exception if ship date already exists </exception>
    /// <exception cref="BO.BlMissingEntityException">Throws exception of the dal function GetById</exception>
    public BO.Order UpdateShipDate(int orderID)
    {
       try
       {
            DO.Order doOrder = dal.Order.GetById(orderID);//gets order from dal by its id

            if (doOrder.ShipDate != null)//if ship date already exists
            {
                throw new BO.BlInCorrectDatesException("Ship date already exists");
            }
            doOrder.ShipDate = DateTime.Now; //updates ship date
            dal.Order.Update(doOrder);//updates order after the change
            return this.GetOrderByID(orderID);//returns order after the change 

        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Order doen't exist", ex);
        }
    }

    /// <summary>
    /// The function returns the status of its order
    /// </summary>
    /// <param name="doOrder">The order to get its sataus</param>
    /// <returns></returns>
    /// <exception cref="BO.BlNullPropertyException">Throws exception if the transferred doOrder is null</exception>
    private BO.OrderStatus orderStatus(DO.Order? doOrder)
    {
        if (doOrder == null)
            throw new BO.BlNullPropertyException("Null order");
        if (doOrder?.DeliveryDate == null && doOrder?.OrderDate == null && doOrder?.ShipDate == null)
            return BO.OrderStatus.Initiated;
        if(doOrder?.OrderDate != null)//If OrderDate exists
        {
            if (doOrder?.ShipDate == null && doOrder?.DeliveryDate == null )
                return BO.OrderStatus.Ordered;
            if (doOrder?.ShipDate != null && doOrder?.DeliveryDate == null)
                return BO.OrderStatus.Shipped;
            if (doOrder?.DeliveryDate != null)
                return BO.OrderStatus.Delivered;
        }
        return BO.OrderStatus.Initiated;
    }
}
