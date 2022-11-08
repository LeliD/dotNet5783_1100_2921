﻿

using DO;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// create
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product product)
    {
        if(DataSource.ProductsList.Exists(x=>x.Value.ID== product.ID))
          {
            throw new Exception("Product ID already exists");
          }

        DataSource.ProductsList.Add(product);
        return product.ID;
    }
    public Product GetById(int id)
    {
        Product? product = DataSource.ProductsList.Find(x => x?.ID == id);
        if (product == null)
        {
            throw new Exception("Product doesn't exist");
        }
        return product.Value;
    }
    public void update(Product product)
    {
        if (!DataSource.ProductsList.Exists(x => x?.ID == product.ID))
        {
            throw new Exception("Product doesn't exist");
        }
        DataSource.OrdersList.RemoveAll(x => x?.ID == product.ID);
        DataSource.ProductsList.Add(product);
    }
    public IEnumerable<Product?> GetAll()
    {
        return DataSource.ProductsList;
    }
    public void delete(int id)
    {
        if (!DataSource.ProductsList.Exists(x => x?.ID == id))
        {
            throw new Exception("Product doesn't exist");
        }
        DataSource.ProductsList.RemoveAll(x => x?.ID == id);
    }
}
