using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation;

internal class Product: IProduct
{
    IEnumerable<ProductForList?> GetListedProducts()
    {
        throw new NotImplementedException();
    }
    Product ProductDetailsForManager(int productID)
    {
        throw new NotImplementedException();
    }
    ProductItem ProductDetailsForCustomer(int productID, Cart cart)//ghgjhgj
    {
        throw new NotImplementedException();
    }
    void AddProduct(Product product)
    {
        throw new NotImplementedException();
    }
    void DeleteProduct(int productID)
    {
        throw new NotImplementedException();
    }
    void UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}
