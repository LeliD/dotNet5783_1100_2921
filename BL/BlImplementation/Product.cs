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
    public IEnumerable<BO.ProductForList?> GetListedProductsForManager()
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
    public IEnumerable<BO.ProductItem?> GetListedProductsForCustomer()
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
    /// <param name="productID">The id of the product to get its details</param>
    /// <returns>BO.Product</returns>
    public BO.Product ProductDetailsForManager(int productID)
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
    /// <summary>
    /// Returns the details of product by it's ID for customer
    /// </summary>
    /// <param name="productID">The id of the product to get its details</param>
    /// <returns></returns>
    public BO.ProductItem ProductDetailsForCustomer(int productID,BO.Cart c)//ghgjhgj
    {
        DO.Product doProduct = dal.Product.GetById(productID);
        return new BO.ProductItem()
        {
            ID = doProduct.ID,
            Name = doProduct.Name,
            Category = (BO.Category)doProduct.Category,
            Price = doProduct.Price,
            InStock = doProduct.InStock > 0,
            Amount = c.Items.Where(x => x?.ProductID == productID).Count() != 0 ? 2 : 0
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
