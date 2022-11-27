using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

internal class Product : IProduct
{
    DalApi.IDal dal = new Dal.DalList();
    /// <summary>
    /// The function returns the list of profucts for ProductForList
    /// </summary>
    /// <returns>BO.ProductForList?</returns>
    /// <exception cref="NullReferenceException"></exception>
    IEnumerable<BO.ProductForList?> GetListedProductsForManager()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductForList()
               {
                   ID = doProduct?.ID ?? throw new NullReferenceException("Missing ID"),
                   Name = doProduct?.Name ?? throw new NullReferenceException("Missing Name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("Missing Category"),
                   Price = doProduct?.Price ?? 0
               };
    }
    /// <summary>
    /// The function returns the list of profucts for ProductItem
    /// </summary>
    /// <returns>BO.ProductItem?</returns>
    /// <exception cref="NullReferenceException"></exception>
    IEnumerable<BO.ProductItem?> GetListedProductsForCustomer()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductItem()
               {
                   ID = doProduct?.ID ?? throw new NullReferenceException("Missing ID"),
                   Name = doProduct?.Name ?? throw new NullReferenceException("Missing Name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("Missing Category"),
                   Price = doProduct?.Price ?? 0,
                   InStock = doProduct?.InStock > 0 ? true : false,
                   Amount = doProduct?.InStock ?? throw new NullReferenceException("Missing InStock")
               };
    }
    /// <summary>
    /// Returns the details of product by it's ID for manager
    /// </summary>
    /// <param name="productID"></param>
    /// <returns>BO.Product</returns>
    BO.Product ProductDetailsForManager(int productID)
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
    BO.ProductItem ProductDetailsForCustomer(int productID)//ghgjhgj
    {
        DO.Product doProduct = dal.Product.GetById(productID);
        return new BO.ProductItem()
        {
            ID = doProduct.ID,
            Name = doProduct.Name,
            Category = (BO.Category)doProduct.Category,
            Price = doProduct.Price,
            InStock = doProduct.InStock > 0 ? true : false,
        };
    }
    /// <summary>
    /// Adds product to dal
    /// </summary>
    /// <param name="boProduct">The product to add</param>
    /// <exception cref="Exception"> Wrong details of boProduct or the product already exists</exception>
    void AddProduct(BO.Product boProduct)
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
    void DeleteProduct(int productID)
    {
       IEnumerable<DO.Order?> doOrder = dal.Order.GetAll();
       foreach (DO.Order item in doOrder) //item? or item   ??
        {
            IEnumerable<DO.OrderItem?> doOrderItems = dal.OrderItem.GetItemsInOrder(item.ID);
            foreach (DO.OrderItem? orderItem in doOrderItems)
            {
                if (orderItem?.ProductID == productID)
                    throw new Exception("Product exists in one or more orders");
            }
        
        }
        dal.Product.Delete(productID);
    }
    
    void UpdateProduct(BO.Product boProduct)
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
