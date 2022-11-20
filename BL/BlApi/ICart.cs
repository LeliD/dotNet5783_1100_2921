﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface ICart
{
    Cart AddProductToCart(Cart cart);
    Cart UpdateAmountOfProductInCart(Cart cart,int productID, int newAmount);    
    void MakeOrder (Cart cart,string CustomerName, string CustomerEmail, string CustomerAddress)
    
}
