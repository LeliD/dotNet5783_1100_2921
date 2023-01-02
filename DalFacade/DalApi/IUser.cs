using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IUser 
    {
        public DO.User GetById(string userName);
        public void Add(DO.User user);
    }
}
