using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;

internal class User : IUser
{
    DalApi.IDal dal = DalApi.Factory.Get();
    public void Add(BO.User user)
    {
        try
        {
            dal.User.Add(new DO.User()
            {
                Name = user.Name,
                UserName = user.UserName,
                UserAddress = user.UserAddress,
                UserEmail = user.UserEmail,
                AdminAccess = user.AdminAccess,
                Passcode = user.Passcode
            });
        }
        catch (DO.DalAlreadyExistUserNameException ex)
        {
            throw new BO.BlAlreadyExistEntityException("User name already exist", ex);
        }

    }

    public BO.User GetById(string userName)
    {
        try
        {
            DO.User user = dal.User.GetById(userName);
            BO.User u=new BO.User() {
                Name = user.Name,
                UserName = user.UserName,
                UserAddress = user.UserAddress,
                UserEmail = user.UserEmail,
                AdminAccess = user.AdminAccess,
                Passcode = user.Passcode
            };
            return u;
        }
        catch (DO.DalMissingUserNameException ex)
        {
            throw new BO.BlMissingEntityException("User name doesn't exist", ex);
        }
    }
  
}
