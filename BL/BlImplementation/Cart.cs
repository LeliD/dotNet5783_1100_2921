using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlImplementation;

internal class Cart : ICart
{
    DalApi.IDal dal = new Dal.DalList();
    Cart AddProductToCart(Cart cart)
    {
        throw new NotImplementedException();
    }
    Cart UpdateAmountOfProductInCart(Cart cart, int productID, int newAmount)
    {
        throw new NotImplementedException();
    }
    void MakeOrder(Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
    {
        throw new NotImplementedException();
    }

}
