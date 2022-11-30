using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

internal class Cart : ICart
{
    DalApi.IDal dal = new Dal.DalList();

    public BO.Cart AddProductToCart(BO.Cart cart, int productID)
    {
        DO.Product doProduct;
        try//Check if product exist
        {
            doProduct = dal.Product.GetById(productID);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw ex;
        }

        if (doProduct.InStock == 0) //If product isn't in Stock
            throw new Exception("Product isn't in stock");
        if (cart.Items == null)
            cart.Items = new List<BO.OrderItem>();
        BO.OrderItem orderItemInCart = cart.Items.FirstOrDefault(x => x.ProductID == productID);//The orderItem in cart
        if (orderItemInCart == null)//product doesn't exist in Cart
        {
            //var v=(List<BO.OrderItem>)cart.Items.Add;
            cart.Items.ToList().Add(new BO.OrderItem()
            {
                //ID = cart.Items.Count() + 1,//??????????
                Name = doProduct.Name,
                ProductID = doProduct.ID,
                Price = doProduct.Price,
                Amount = 1,
                TotalPrice = doProduct.Price,

            });
            cart.TotalPrice += doProduct.Price;
        }
        else//product already exist in Cart
        {
            orderItemInCart.Amount++;//Increase amount in 1
            orderItemInCart.TotalPrice += orderItemInCart.Price;//Update totalPrice of OrderItem
            cart.TotalPrice += orderItemInCart.Price;
        }
        return cart;
    }

    public void MakeOrder(BO.Cart cart)
    {
        if ((cart.CustomerName ?? throw new Exception("CustomerName is null")) == "")
            throw new Exception("Unvalid CustomerName");
        //if (cart.CustomerName == null || cart.CustomerName == "")
        //    throw new Exception("Unvalid CustomerName");
        if (cart.CustomerAddress == null || cart.CustomerAddress == "")
            throw new Exception("Unvalid CustomerAddress");
        if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail ?? throw new Exception("CustomerEmail is null")))
            throw new Exception("Unvalid CustomerEmail");
        if (cart.Items == null)
            throw new Exception("Unvalid Cart (No items)");
        cart.Items.ToList().ForEach(x =>
        {
            DO.Product doProduct;
            try { doProduct = dal.Product.GetById(x.ProductID); } catch (DO.DalDoesNotExistException ex) { throw ex; }//Check if products exist
            if (x.Amount <= 0) throw new Exception("Unvalid Amount");//chech if demanded amount is a positive number
            if(x.Amount>doProduct.InStock) throw new Exception("Out Of Stock");//chech if demanded amount is in stock
        });
        DO.Order doOrder = new DO.Order()//Create DO.Order
        {
            CustomerName = cart.CustomerName,
            CustomerEmail = cart.CustomerEmail,
            CustomerAddress = cart.CustomerAddress,
            OrderDate = DateTime.Now,
            ShipDate = null,
            DeliveryDate = null
        };
       
        int orderId=dal.Order.Add(doOrder);//Add DO.Order
        var doOrderItems = from  BO.OrderItem boOrderItem in cart.Items//v is list
                select  new DO.OrderItem()
                {
                    OrderID= orderId,
                    ProductID= boOrderItem.ProductID,
                    Price = boOrderItem.Price,
                    Amount= boOrderItem.Amount,
                };
        doOrderItems.ToList().ForEach(x => dal.OrderItem.Add(x));//Add DO.orderItems of order to dal
        doOrderItems.ToList().ForEach(x => {DO.Product p=dal.Product.GetById(x.ProductID) ; p.InStock -= x.Amount; dal.Product.Update(p); });//Update the InStock of products
    }

    public BO.Cart UpdateAmountOfProductInCart(BO.Cart cart, int productID, int newAmount)
    {
        
        if(cart.Items==null)//if items isn't initialized
        {
            throw new Exception("There is no items in cart to update");
        }
        BO.OrderItem boOrderItem = cart.Items.FirstOrDefault(x => x?.ProductID == productID); //find the product to update its amount
        if(boOrderItem == null)//product wasn't found
            throw new Exception("Product to update wasn't found");
        if (boOrderItem.Amount== newAmount)//???? to throw?
        {
            throw new Exception("Same amount");
        }
       
        DO.Product doProduct;
        try//Check if product exist
        {
            doProduct = dal.Product.GetById(productID);//gets the product from dal to update its amount
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw ex;
        }

        double oldTotalPriceOfItem = boOrderItem.TotalPrice;//the total price of order item before the change 
        if (boOrderItem.Amount < newAmount)//if amount increased
        {
            
            if(doProduct.InStock < newAmount) //if there isn't enough in stock
            {
                throw new Exception("Not enough in stock");
            }
        }
        else //if amount decreased
        {
           if(newAmount == 0) //in case there is a need to delete the order item from cart
            {
                cart.Items.ToList().Remove(boOrderItem);//remove the order item
                cart.TotalPrice -= oldTotalPriceOfItem; //update total price of cart
                return cart;
            }
           
        }
        boOrderItem.Amount = newAmount; //update the amount
        boOrderItem.TotalPrice = newAmount * boOrderItem.Price;//update the total price of order item
        cart.TotalPrice -= oldTotalPriceOfItem;//update total price of cart to be minus old total price of item
        cart.TotalPrice += boOrderItem.TotalPrice;//update total price of cart to be plus new total price of item
        return cart;

    }
}
