﻿
using System.Collections.Generic;
using DalApi;
using DO;

namespace Dal;

internal class DalProduct : IProduct
{
    /// <summary>
    /// Adds product to ProductsList
    /// </summary>
    /// <param name="product">product is the object which being added</param>
    /// <returns>returns Id of profuct</returns>
    /// <exception cref="DalAlreadyExistIdException">Throw exception if id of product already exists</exception>
    public int Add(Product product)
    {
        if (DataSource.ProductsList.Exists(x => x?.ID == product.ID))
            throw new DalAlreadyExistIdException(product.ID, "Product");
        DataSource.ProductsList.Add(product);
        return product.ID;
    }
    /// <summary>
    /// Gets product by its Id
    /// </summary>
    /// <param name="id">id of product</param>
    /// <returns>returns the product</returns>
    /// <exception cref="DalMissingIdException">Throw exception if id of product doesn't exists</exception>
    public Product GetById(int id)
    {
        Product product = DataSource.ProductsList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Product");
        return product;
    }
    /// <summary>
    /// Update product that exsist in the list
    /// </summary>
    /// <param name="product">product is the object which being updated</param>
    /// <exception cref="DalMissingIdException">Throw exception if product doesn't exist</exception>
    public void Update(Product product)
    {
        if (!DataSource.ProductsList.Exists(x => x?.ID == product.ID))
        {
            throw new DalMissingIdException(product.ID, "Product");
        }
        DataSource.ProductsList.RemoveAll(x => x?.ID == product.ID);
        DataSource.ProductsList.Add(product);
    }
    /// <summary>
    /// Gets all the products in the list in case no function was transfered. Otherwize, returns the filter list. 
    /// </summary>
    /// <returns>return IEnumerable<Product?></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.ProductsList
                   where filter(item)
                   select item;
        }
        return from item in DataSource.ProductsList
               select item;
    }
    /// <summary>
    /// Deletes product from ProductsList
    /// </summary>
    /// <param name="id">id of product to delete</param>
    /// <exception cref="DalMissingIdException">Throw exception if product doesn't exist</exception>
    public void Delete(int id)
    {
        if (!DataSource.ProductsList.Exists(x => x?.ID == id))
        {
            throw new DalMissingIdException(id, "Product");
        }
        DataSource.ProductsList.RemoveAll(x => x?.ID == id);
    }
}
