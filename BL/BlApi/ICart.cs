﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    Cart AddProductToCart(Cart cart, int productID);
    Cart UpdateAmountOfProductInCart(Cart cart, int productID, int newAmount);
    int MakeOrder(Cart cart);


}
