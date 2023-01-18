using System;
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
        public IOrder Order { get; } = new DalOrder();
        public IOrderItem OrderItem { get; } = new DalOrderItem();
        public IProduct Product { get; } = new DalProduct();
        public IUser User { get; } = new DalUser();
        public static IDal Instance { get; } = new DalList(); //singelton
        private DalList() { }
    }
}
