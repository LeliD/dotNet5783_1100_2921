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

    /// <summary>
    /// Gets User by its userName
    /// </summary>
    /// <param name="userName"></param>
    /// <returns>DO.User of the userName</returns>
    /// <exception cref="DO.DalMissingUserNameException">If userName isn't found</exception>
    public DO.User GetById(string userName)//returns a user by its username
    {
        DO.User user = DataSource.UsersList.Find(x => x?.UserName == userName) ?? throw new DO.DalMissingUserNameException(userName, "User name does not exist");
        return user;
    }
    /// <summary>
    /// Adds user to the system by its userName
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="DO.DalAlreadyExistUserNameException"></exception>
    public void Add(DO.User user)
    {
        if (DataSource.UsersList.Exists(x => x?.UserName == user.UserName))
            throw new DO.DalAlreadyExistUserNameException(user.UserName!, $"Username: {user.UserName} already exist");
        DataSource.UsersList.Add(user);
    }
   
}
