using System;
using System.Collections.Generic;
using System.Linq;
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
                ID = cart.Items.Count() + 1,//??????????
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

    public void MakeOrder(BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
    {
        throw new NotImplementedException();
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
