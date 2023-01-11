using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal sealed class DalXml : IDal
{
   public IOrder Order { get; } = new DalOrder();
   public IOrderItem OrderItem { get; } = new DalOrderItem();
   public IProduct Product { get; } = new DalProduct();
   public IUser User { get; } = new DalUser();
   public static IDal Instance { get; } = new DalXml();
   private DalXml() { }
  
}
