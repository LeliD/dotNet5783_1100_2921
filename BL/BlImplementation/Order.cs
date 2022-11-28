using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
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
        IEnumerable<DO.Order?> doOrder = dal.Order.GetAll();
        foreach(DO.Order item in doOrder)
        {
            dal.OrderItem.GetItemsInOrder(item.ID);

        }
        var v = from item in dal.Order.GetAll()
                let a = dal.OrderItem.GetItemsInOrder((int)item?.ID)
                select new BO.OrderForList()
                {
                    ID = (int)item?.ID,
                    CustomerName = item?.CustomerName,
                    if(item?.DeliveryDate!=null)
                       {

                       }



              }

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
}
