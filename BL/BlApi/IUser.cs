using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IUser
{
    public BO.User GetById(string userName);
    public void Add(BO.User user);
}
