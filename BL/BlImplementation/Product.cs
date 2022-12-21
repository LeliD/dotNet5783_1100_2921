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
    /// <summary>
    /// Dal variable
    /// </summary>
    DalApi.IDal dal = DalApi.Factory.Get();
    /// <summary>
    /// The function brings the list of products from dal and returns it filtered in form of BO.ProductForList? (For Manager)
    /// </summary>
    /// <param name="filter">The function in which to filter the products</param>
    /// <returns>Filtered list of products in form of BO.ProductForList?</returns>
    /// <exception cref="BO.BlNullPropertyException">Throws exception if one of the products is null</exception>
    public IEnumerable<BO.ProductForList?> GetListedProductsForManager(Func<BO.ProductForList?, bool>? filter = null)
    {
        var x = from DO.Product? doProduct in dal.Product.GetAll()
                select new BO.ProductForList()
                {
                    ID = doProduct?.ID ?? throw new BO.BlNullPropertyException("Null Product"),
                    Name = doProduct?.Name ?? throw new BO.BlNullPropertyException("Null Product"),
                    Category = (BO.Category?)doProduct?.Category ?? throw new BO.BlNullPropertyException("Null Product"),
                    Price = doProduct?.Price ?? throw new BO.BlNullPropertyException("Null Product")
                };

        if (filter == null)
            return x;

        return from productForList in x
               where filter(productForList)
               select productForList;
              
        
    }
    

    /// <summary>
    /// The function brings the list of products from dal and returns it in form of BO.ProductItem? (For Customer) 
    /// </summary>
    /// <returns>list of products in form of BO.ProductItem?</returns>
    /// <exception cref="BO.BlNullPropertyException">Throws exception if one of the products is null</exception>
    public IEnumerable<BO.ProductItem?> GetListedProductsForCustomer()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductItem()
               {
                   ID = doProduct?.ID ?? throw new BO.BlNullPropertyException("Null Product"),
                   Name = doProduct?.Name ?? throw new BO.BlNullPropertyException("Null Product"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new BO.BlNullPropertyException("Null Product"),
                   Price = doProduct?.Price ?? throw new BO.BlNullPropertyException("Null Product"),
                   InStock = doProduct?.InStock != null ? doProduct?.InStock > 0 : throw new BO.BlNullPropertyException("Null Product"),
                   Amount = doProduct?.InStock ?? throw new BO.BlNullPropertyException("Null Product")
               };
    }
    /// <summary>
    /// The function gets productID and returns the details of product in form of BO.Product (For Manager)
    /// </summary>
    /// <param name="productID">The ID of the product to get its details</param>
    /// <returns>BO.Product of the transferred ID </returns>
    /// <exception cref="BO.BlMissingEntityException">Catches and Throws exception of DO.GetById</exception>
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
            throw new BO.BlMissingEntityException("Product doesn't exist", ex);
        }
    }
    /// <summary>
    /// The function gets productID and customer's cart and returns the details of product in form of BO.ProductItem (For Customer)
    /// </summary>
    /// <param name="productID">The ID of the product to get its details</param>
    /// <param name="c">Customer's cart</param>
    /// <returns></returns>
    /// <exception cref="BO.BlMissingEntityException">Catches and Throws exception of DO.GetById</exception>
    public BO.ProductItem ProductDetailsForCustomer(int productID,BO.Cart c)//ghgjhgj
    {
      try 
        {
            DO.Product doProduct = dal.Product.GetById(productID);
            int amount; // Amount of "doProduct" in "c"
            if (c.Items == null)//If Items of cart isn't initialized
                amount = 0;
            else
            {
                BO.OrderItem boProductItem = c.Items.FirstOrDefault(x => x?.ProductID == productID);//Finds the BO.OrderItem which includes "doProduct"
                if (boProductItem == null)//If "doProduct" isn't in c
                {
                    amount = 0;
                }
                else //"doProduct" exist in c
                    amount = boProductItem.Amount;
            }
            return new BO.ProductItem()
            {
                ID = doProduct.ID,
                Name = doProduct.Name,
                Category = (BO.Category)doProduct.Category,
                Price = doProduct.Price,
                InStock = doProduct.InStock > 0,
                Amount = amount
            };
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", ex);
        }
    }
    /// <summary>
    /// Adds product to dal
    /// </summary>
    /// <param name="boProduct">The product to add</param>
    /// <exception cref="BO.BlDetailInvalidException">Wrong details of boProduct</exception>
    /// <exception cref="BO.BlAlreadyExistEntityException">Catches and Throws exception of DO.Add in case the product to add already exists</exception>
    /// <exception cref ="BO.BlWrongCategoryException" >Throws exception if categiry isn't correct</ exception >

    public void AddProduct(BO.Product boProduct)
    {
        if (boProduct.ID < 0)
            throw new BO.BlDetailInvalidException("ID", "Negative ID");
        if (boProduct.Name == null || boProduct.Name == "")
            throw new BO.BlDetailInvalidException("ID", "Name is empty");
        if (boProduct.Price <= 0)
            throw new BO.BlDetailInvalidException("Price", "wrong Price");
        if (boProduct.InStock < 0)
            throw new BO.BlDetailInvalidException("InStock", "Negative Amount in Stock");
        if (boProduct.Category != BO.Category.BATHROOM && boProduct.Category != BO.Category.BEDROOM && boProduct.Category != BO.Category.LIVING_ROOM && boProduct.Category != BO.Category.KITCHEN && boProduct.Category != BO.Category.KIDS)
            throw new BO.BlWrongCategoryException("Wrong Category");

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
        catch(DO.DalAlreadyExistIdException ex)
        {
            throw new BO.BlAlreadyExistEntityException("Product already exist", ex);
        }
    }
    /// <summary>
    /// Delete Product from dal by its ID if it doesn't appear in orders. Otherwize, throws exceptions.
    /// </summary>
    /// <param name="productID">The product to delete</param>
    /// <exception cref="BO.BlAlreadyExistEntityException">Throws if orders of this product exist</exception>
    /// <exception cref="BO.BlMissingEntityException">Catches and Throws exception of DO.Add in case the product to delete is missing</exception>
    public void DeleteProduct(int productID)
    {
        if (dal.OrderItem.GetAll(x => x?.ProductID == productID).Count() != 0)
            throw new BO.BlAlreadyExistEntityException("Product exists in one or more orders");
        try
        {
            dal.Product.Delete(productID);
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", ex);
        }
    }
    /// <summary>
    /// Updates product in dal by getting BO.Product
    /// </summary>
    /// <param name="boProduct">product to update</param>
    /// <exception cref="BO.BlDetailInvalidException">Wrong details of boProduct</exception>
    /// <exception cref="BO.BlMissingEntityException">Catches and Throws exception of DO.Update in case the product to update is missing</exception>
    public void UpdateProduct(BO.Product boProduct)
    {
        
        if (boProduct.ID < 0)
            throw new BO.BlDetailInvalidException("ID", "Negative ID");
        if (boProduct.Name == null || boProduct.Name == "")
            throw new BO.BlDetailInvalidException("ID", "Name is empty");
        if (boProduct.Price <= 0)
            throw new BO.BlDetailInvalidException("Price", "Negative Price");
        if (boProduct.InStock < 0)
            throw new BO.BlDetailInvalidException("InStock", "Negative Amount in Stock");
        if (boProduct.Category != BO.Category.BATHROOM && boProduct.Category != BO.Category.BEDROOM && boProduct.Category != BO.Category.LIVING_ROOM && boProduct.Category != BO.Category.KITCHEN && boProduct.Category != BO.Category.KIDS)
            throw new BO.BlWrongCategoryException("Wrong Category");
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
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", ex);
        }
    }
}
