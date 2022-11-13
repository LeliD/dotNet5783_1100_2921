

using DO;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// Adds product to ProductsList
    /// </summary>
    /// <param name="product is the object which being added "></param>
    /// <returns>returns Id of profuct</returns>
    /// <exception cref="Exception">Throw exception if id of product already exists</exception>
    public int Add(Product product)
    {
        if(DataSource.ProductsList.Exists(x=>x?.ID== product.ID))
          {
            throw new Exception("Product ID already exists");
          }

        DataSource.ProductsList.Add(product);
        return product.ID;
    }
    /// <summary>
    /// Gets product by its Id
    /// </summary>
    /// <param name="id of product"></param>
    /// <returns>returns the product</returns>
    /// <exception cref="Exception">Throw exception if id of product doesn't exists</exception>
    public Product GetById(int id)
    {
        Product product = DataSource.ProductsList.Find(x => x?.ID == id) ?? throw new Exception("Product doesn't exist");

        return product;
    }
    /// <summary>
    /// Update product that exsist in the list
    /// </summary>
    /// <param name="product is the object which being updated"></param>
    /// <exception cref="Exception">Throw exception if product doesn't exist</exception>

    public void update(Product product)
    {
        if (!DataSource.ProductsList.Exists(x => x?.ID == product.ID))
        {
            throw new Exception("Product doesn't exist");
        }
        DataSource.ProductsList.RemoveAll(x => x?.ID == product.ID);
       
        DataSource.ProductsList.Add(product);
    }
    /// <summary>
    /// Gets all the products in the list
    /// </summary>
    /// <returns>return ProductsList</returns>
    public IEnumerable<Product?> GetAll()
    {
        IEnumerable<Product?> list = DataSource.ProductsList;
        return list;
      
    }
    /// <summary>
    /// Deletes product from ProductsList
    /// </summary>
    /// <param name="id of product to delete"></param>
    /// <exception cref="Exception">Throw exception if product doesn't exist</exception>
    public void delete(int id)
    {
        if (!DataSource.ProductsList.Exists(x => x?.ID == id))
        {
            throw new Exception("Product doesn't exist");
        }
        DataSource.ProductsList.RemoveAll(x => x?.ID == id);
    }
}
