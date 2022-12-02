using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation;

internal class Product : IProduct
{
    DalApi.IDal dal = new Dal.DalList();
    /// <summary>
    /// The function brings the list of products from dal and returns it in form of BO.ProductForList? (For Manager)
    /// </summary>
    /// <returns>list of products in form of BO.ProductForList?</returns>
    /// <exception cref="NullReferenceException">Throws exception if one of the products is null</exception>
    public IEnumerable<BO.ProductForList?> GetListedProductsForManager()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductForList()
               {
                   ID = doProduct?.ID ?? throw new BlNullPropertyException("Null Product"),
                   Name = doProduct?.Name ?? throw new BlNullPropertyException("Null Product"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new BlNullPropertyException("Null Product"),
                   Price = doProduct?.Price ?? throw new BlNullPropertyException("Null Product")
               };
    }
    /// <summary>
    /// The function brings the list of products from dal and returns it in form of BO.ProductItem? (For Customer) 
    /// </summary>
    /// <returns>list of products in form of BO.ProductItem?</returns>
    /// <exception cref="NullReferenceException">Throws exception if one of the products is null</exception>
    public IEnumerable<BO.ProductItem?> GetListedProductsForCustomer()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductItem()
               {
                   ID = doProduct?.ID ?? throw new BlNullPropertyException("Null Product"),
                   Name = doProduct?.Name ?? throw new NullReferenceException("Null Product"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("Null Product"),
                   Price = doProduct?.Price ?? throw new NullReferenceException("Null Product"),
                   InStock = doProduct?.InStock != null ? doProduct?.InStock > 0 : throw new NullReferenceException("Null Product"),
                   Amount = doProduct?.InStock ?? throw new NullReferenceException("Null Product")
               };
    }
    /// <summary>
    /// The function gets productID and returns the details of product in form of BO.Product (For Manager)
    /// </summary>
    /// <param name="productID">The ID of the product to get its details</param>
    /// <returns>BO.Product of the transferred ID </returns>
    public BO.Product ProductDetailsForManager(int productID)
    {
        try
        {
            DO.Product doProduct = dal.Product.GetById(productID);
            return new BO.Product()
            {
                ID = doProduct.ID,
                Name = doProduct.Name,
                Category = (BO.Category)doProduct.Category,
                Price = doProduct.Price,
                InStock = doProduct.InStock
            };
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product already exist", ex);
        }
    }
    /// <summary>
    /// The function gets productID and customer's cart and returns the details of product in form of BO.ProductItem (For Customer)
    /// </summary>
    /// <param name="productID">The ID of the product to get its details</param>
    /// <returns></returns>
    public BO.ProductItem ProductDetailsForCustomer(int productID,BO.Cart c)//ghgjhgj
    {
        DO.Product doProduct;
        try 
        {
            doProduct = dal.Product.GetById(productID);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw ex;
        }
        int amount;
        if(c.Items == null) 
            amount=0;   
        else
        {
            BO.OrderItem boProductItem = c.Items.FirstOrDefault(x => x?.ProductID == productID);
            if(boProductItem == null)
            {
                amount = 0;
            }
            else
                amount = boProductItem.Amount;
        }
        //IEnumerable<BO.OrderItem> items = c.Items?? throw new Exception("Missing items in cart");
         
        return new BO.ProductItem()
        {
            ID = doProduct.ID,
            Name = doProduct.Name,
            Category = (BO.Category)doProduct.Category,
            Price = doProduct.Price,
            InStock = doProduct.InStock > 0,
            //let a= c.Items.FirstOrDefault(x => x?.ProductID == productID)
            Amount = amount//boProductItem!=null? boProductItem.Amount:0//c.Items.FirstOrDefault(x => x?.ProductID == productID).Amount
        };
    }
    /// <summary>
    /// Adds product to dal
    /// </summary>
    /// <param name="boProduct">The product to add</param>
    /// <exception cref="Exception"> Wrong details of boProduct or the product already exists</exception>
    /// 
     public void AddProduct(BO.Product boProduct)
    {
        if (boProduct.ID < 0)
            throw new Exception("Negative ID");
        if(boProduct.Name == "")//??nul
            throw new Exception("Missing Name");
        if (boProduct.Price <=0)
            throw new Exception("Negative price");
        if(boProduct.InStock<0)
            throw new Exception("Negative amount in stock");

        try
        {
            dal.Product.Add(new DO.Product()
            {
                ID = boProduct.ID,
                Name = boProduct.Name,
                Price = boProduct.Price,
                Category = (DO.Category)boProduct.Category,
                InStock = boProduct.InStock
            });
        }
        catch(DO.DalAlreadyExistsException ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Delete Product from dal by its ID if it doesn't appear in orders. Otherwize, throws exceptions.
    /// </summary>
    /// <param name="productID">The id of product to delete</param>
    /// <exception cref="Exception">In case the product appear in orders or doesn't exist at all </exception>
    public void DeleteProduct(int productID)
    {
        if(dal.OrderItem.GetAll(x=>x?.ID== productID).Count()!=0)
            throw new Exception("Product exists in one or more orders");

        try
        {
            dal.Product.Delete(productID);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw ex;
        }

    }
    /// <summary>
    /// Update product in dal by getting BO.Product
    /// </summary>
    /// <param name="boProduct">product to update</param>
    /// <exception cref="Exception">Throw exceptions either the details of product aren't correct or the product doesn't exist at all</exception>
    public void UpdateProduct(BO.Product boProduct)
    {
        if (boProduct.ID < 0)
            throw new Exception("Negative ID");
        if (boProduct.Name == "")//??nul
            throw new Exception("Missing Name");
        if (boProduct.Price <= 0)
            throw new Exception("Negative price");
        if (boProduct.InStock < 0)
            throw new Exception("Negative amount in stock");

        try
        {
            dal.Product.Update(new DO.Product()
            {
                ID = boProduct.ID,
                Name = boProduct.Name,
                Price = boProduct.Price,
                Category = (DO.Category)boProduct.Category,
                InStock = boProduct.InStock
            });
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw ex;
        }
    }
}
