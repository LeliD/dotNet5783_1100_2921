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
    /// <summary>
    /// Dal variable
    /// </summary>
    DalApi.IDal dal = DalApi.Factory.Get();
    /// <summary>
    /// The function adds product to cart
    /// </summary>
    /// <param name="cart">purchase cart</param>
    /// <param name="productID">ID of product to add to cart</param>
    /// <returns>Updated cart with the additional product</returns>
    /// <exception cref="BO.BlMissingEntityException">Catches dal exception and Throws if product to add to the cart doesn't exist</exception>
    /// <exception cref="BO.BlOutOfStockException">Throws exception if product to add to the cart is out of stock</exception>
    public BO.Cart AddProductToCart(BO.Cart cart, int productID)
    {
        DO.Product doProduct;
        try//Check if product exist
        {
            doProduct = dal.Product.GetById(productID);
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", ex);
        }

        if (doProduct.InStock == 0) //If product is out of Stock
            throw new BO.BlOutOfStockException(doProduct.ID, doProduct.Name!, "Product is out of Stock");
        if (cart.Items == null)//If items isn't initialized
            cart.Items = new List<BO.OrderItem>();
        BO.OrderItem? orderItemInCart = cart.Items.FirstOrDefault(x => x.ProductID == productID);//BO.OrderItems in cart which includes the product
        if (orderItemInCart == null)//product still doesn't exist in cart
        {
            var orderItemsList = cart.Items.ToList();//Convert cart.Items to List
            orderItemsList.Add(new BO.OrderItem()//Add new BO.OrderItem to cart 
            {
                Name = doProduct.Name,
                ProductID = doProduct.ID,
                Price = doProduct.Price,
                Amount = 1,
                TotalPrice = doProduct.Price,
                ImageRelativeName = doProduct.ImageRelativeName,

            });
            cart.Items = orderItemsList;
            cart.TotalPrice += doProduct.Price;//Calculate the TotalPrice of cart
        }
        else//product already exist in Cart
        {
            if (orderItemInCart.Amount + 1 > doProduct.InStock)
                throw new BO.BlOutOfStockException(doProduct.ID, doProduct.Name!, "Product is out of Stock");
            orderItemInCart.Amount++;//Increase amount in 1
            orderItemInCart.TotalPrice += orderItemInCart.Price;//Update totalPrice of OrderItem
            cart.TotalPrice += orderItemInCart.Price;
        }
        return cart;
    }
    
    /// <summary>
    /// The function closes an order by getting its cart and transfers its data to dal 
    /// </summary>
    /// <param name="cart">the cart of order to close</param>
    /// <returns>Order ID</returns>
    /// <exception cref="BO.BlNullPropertyException">Throw exceptions in case fields of cart are null</exception>
    /// <exception cref="BO.BlDetailInvalidException">Throw exceptions if details of cart are invalid</exception>
    /// <exception cref="BO.BlMissingEntityException">Catches and Throws exception of DO.GetById in case cart's products don't exist</exception>
    /// <exception cref="BO.BlOutOfStockException">Throws exception if demanded amount of product to add to the cart is out of stock</exception>

    public int MakeOrder(BO.Cart cart)
    {
        if ((cart.CustomerName ?? throw new BO.BlNullPropertyException("CustomerName is null")) == "")
            throw new BO.BlDetailInvalidException("CustomerName","Unvalid CustomerName");
        if (cart.CustomerAddress == null || cart.CustomerAddress == "")
            throw new BO.BlDetailInvalidException("CustomerAdress","Unvalid CustomerAddress");
        if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail ?? throw new BO.BlNullPropertyException("CustomerEmail is null")))
            throw new BO.BlDetailInvalidException("CustomerEmail", "Unvalid CustomerEmail");
        if (cart.Items == null || cart.Items.Count() == 0)
            throw new BO.BlDetailInvalidException("cart", "Unvalid Cart (No items)");
        cart.Items.ToList().ForEach(x =>
        {
            DO.Product doProduct;
            try { doProduct = dal.Product.GetById(x.ProductID); } catch (DO.DalMissingIdException ex) { throw new BO.BlMissingEntityException("product doen't exist", ex); }//Check if products exist
            if (x.Amount <= 0) throw new BO.BlDetailInvalidException("Amount", "Unvalid Amount of orderItem");//check if demanded amount is a positive number
            if (x.Amount>doProduct.InStock) throw new BO.BlOutOfStockException(doProduct.ID, doProduct.Name!, "Product is out of Stock in the demanded amount");//chech if demanded amount is in stock
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
        return orderId;
    }
    /// <summary>
    /// The function updates amount of product in cart
    /// </summary>
    /// <param name="cart">The cart to update</param>
    /// <param name="productID">the product in cart which has to be updated</param>
    /// <param name="newAmount">The new demanded amount of the product </param>
    /// <returns>Updated cart</returns>
    /// <exception cref="BO.BlNullPropertyException">Throw exceptions in case fields of cart are null</exception>
    /// <exception cref="BO.BlMissingEntityException">Catches and Throws exception of DO.GetById in case cart's products don't exist or OrderItem of product isn't found in cart</exception>
    /// <exception cref="BO.BlOutOfStockException">Throws exception if demanded amount of product to update in cart is out of stock</exception>
    public BO.Cart UpdateAmountOfProductInCart(BO.Cart cart, int productID, int newAmount)
    {
        
        if(cart.Items==null)//if items isn't initialized
        {
            throw new BO.BlNullPropertyException("There is no items in cart to update since its null");
        }
        BO.OrderItem? boOrderItem = cart.Items.FirstOrDefault(x => x?.ProductID == productID); //find the product to update its amount
        if(boOrderItem == null)//product wasn't found
            throw new BO.BlMissingEntityException("OrderItem of product to update in cart wasn't found");
        if (boOrderItem.Amount== newAmount)//If there is no need to update
        {
            return cart;
        }
       
        DO.Product doProduct;
        try//Check if product exist
        {
            doProduct = dal.Product.GetById(productID);//gets the product from dal to update its amount
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", ex);
        }

        double oldTotalPriceOfItem = boOrderItem.TotalPrice;//the total price of order item before the change 
        if (boOrderItem.Amount < newAmount)//if demanded amount increased
        {
            
            if(doProduct.InStock < newAmount) //if there isn't enough in stock
            {
                throw new BO.BlOutOfStockException(doProduct.ID, doProduct.Name!, "Product is out of Stock in the demanded amount");
            }
        }
        else //if demanded amount decreased
        {
           if(newAmount == 0) //in case there is a need to delete the order item from cart
            {
                var v = cart.Items.ToList();
                v.Remove(boOrderItem);//remove the order item
                cart.Items = v;
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
