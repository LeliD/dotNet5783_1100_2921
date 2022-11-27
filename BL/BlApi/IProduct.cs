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
    IEnumerable<ProductItem?> GetListedProductsForCustomer();
    Product ProductDetailsForManager(int productID);
    ProductItem ProductDetailsForCustomer(int productID);//ghgjhgj
    void AddProduct(Product boProduct);
    void DeleteProduct(int productID);
    void UpdateProduct(Product boProduct);
}
