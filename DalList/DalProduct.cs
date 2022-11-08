

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
}
