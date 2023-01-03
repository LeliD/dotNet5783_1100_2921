using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation;

internal class User : IUser
{
    DalApi.IDal dal = DalApi.Factory.Get();
    /// <summary>
    /// Adds User to dal
    /// </summary>
    /// <param name="user">The product to add</param>
    /// <exception cref="BO.BlDetailInvalidException">Wrong details of user</exception>
    /// <exception cref="BO.BlAlreadyExistEntityException">Catches and Throws exception of DO.Add in case the userName of user to add already exists</exception>
    public void Add(BO.User user)
    {
        if (user.Name == null || user.Name == "")
            throw new BO.BlDetailInvalidException("Name", "Name is empty");
        
        if (user.UserName == null || user.UserName == "")
            throw new BO.BlDetailInvalidException("UserName", "UserName is empty");
        
        if (user.UserEmail == null || user.UserEmail == "")
            throw new BO.BlDetailInvalidException("UserEmail", "UserEmail is empty");
        
        if (user.UserAddress == null || user.UserAddress == "")
            throw new BO.BlDetailInvalidException("UserAddress", "UserAddress is empty");
        
        if (user.Passcode == null || user.Passcode == "")
            throw new BO.BlDetailInvalidException("Passcode", "Name is empty");


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
    /// <summary>
    /// The function gets userName and returns the details of the user in form of BO.User 
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlNullPropertyException">If userName is null</exception>
    /// <exception cref="BO.BlMissingEntityException">Catches and Throws exception of DO.GetById</exception>
    public BO.User GetById(string userName)
    {
        if (userName == null)
            throw new BO.BlNullPropertyException("UserName is empty");
        try
        {
            DO.User doUser = dal.User.GetById(userName);
            return new BO.User()
            {
                Name = doUser.Name,
                UserName = doUser.UserName,
                UserAddress = doUser.UserAddress,
                UserEmail = doUser.UserEmail,
                AdminAccess = doUser.AdminAccess,
                Passcode = doUser.Passcode
            };
        }
        catch (DO.DalMissingUserNameException ex)
        {
            throw new BO.BlMissingEntityException("User name doesn't exist", ex);
        }
    }
  
}
