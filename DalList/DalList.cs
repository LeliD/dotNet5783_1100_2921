﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    internal sealed class DalList : IDal
    {
        public IOrder Order => new DalOrder();
        public IOrderItem OrderItem => new DalOrderItem();
        public IProduct Product => new DalProduct();
        public static IDal Instance { get; } = new DalList();
        private DalList(){ }
    }
}
