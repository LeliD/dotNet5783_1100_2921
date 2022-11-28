using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

internal class Cart : ICart
{
    DalApi.IDal dal = new Dal.DalList();

    public BO.Cart AddProductToCart(BO.Cart cart)
    {
        throw new NotImplementedException();
    }

    public void MakeOrder(BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
    {
        throw new NotImplementedException();
    }

    public BO.Cart UpdateAmountOfProductInCart(BO.Cart cart, int productID, int newAmount)
    {
        throw new NotImplementedException();
    }
}
