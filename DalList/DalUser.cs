using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalUser : IUser
{

   
    public DO.User GetById(string userName)//returns a user by its username
    {
        DO.User user = DataSource.UsersList.Find(x => x?.UserName == userName) ?? throw new DO.DalMissingUserNameException(userName, "This user name does not exist");
        return user;
    }
    public void Add(DO.User user)
    {
        if (DataSource.UsersList.Exists(x => x?.UserName == user.UserName))
            throw new DO.DalAlreadyExistUserNameException(user.UserName, $"the username: {user.UserName} is already exist");
        DataSource.UsersList.Add(user);
        return;
       
    }
   
}
