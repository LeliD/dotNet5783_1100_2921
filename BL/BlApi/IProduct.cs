using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    IEnumerable<ProductForList> GetListedProducts();
    Product ProductDetailsForManager(int productID);
    ProductItem ProductDetailsForCustomer(int productID, Cart cart);//ghgjhgj
    void AddProduct(Product product);
    void DeleteProduct(int productID);
    void UpdateProduct(Product product);
}
