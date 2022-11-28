using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    public IEnumerable<ProductForList?> GetListedProductsForManager();
    public IEnumerable<ProductItem?> GetListedProductsForCustomer();
    public Product ProductDetailsForManager(int productID);
    public ProductItem ProductDetailsForCustomer(int productID, Cart c);//ghgjhgj
    public void AddProduct(Product boProduct);
    public void DeleteProduct(int productID);
    public void UpdateProduct(Product boProduct);
}
