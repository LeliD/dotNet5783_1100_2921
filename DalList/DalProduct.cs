

using DO;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// Adds product to ProductsList
    /// </summary>
    /// <param name="product is the object which being added "></param>
    /// <returns></returns>
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
    public Product GetById(int id)
    {
        Product product = DataSource.ProductsList.Find(x => x?.ID == id) ?? throw new Exception("Product doesn't exist");

        return product;
    }
    public void update(Product product)
    {
        if (!DataSource.ProductsList.Exists(x => x?.ID == product.ID))
        {
            throw new Exception("Product doesn't exist");
        }
        DataSource.ProductsList.RemoveAll(x => x?.ID == product.ID);
       
        DataSource.ProductsList.Add(product);
    }
    public IEnumerable<Product?> GetAll()
    {
        IEnumerable<Product?> list = DataSource.ProductsList;
        return list;
      
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
