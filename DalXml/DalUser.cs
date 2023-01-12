using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    internal class DalUser : IUser
    {
        /// <summary>
        /// name of users xml file
        /// </summary>
        readonly string s_Users = "users";
        /// <summary>
        /// Adds user to users xml file by its userName
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="DO.DalAlreadyExistUserNameException"></exception>
        public int Add(User user)
        {
            List<DO.User?> usersList = XMLTools.LoadListFromXMLSerializer<DO.User>(s_Users);
            if (usersList.Exists(x => x?.UserName == user.UserName))
                throw new DO.DalAlreadyExistUserNameException(user.UserName!, $"Username: {user.UserName} already exist");
            usersList.Add(user);
            XMLTools.SaveListToXMLSerializer(usersList, s_Users);
            return 0;
        }
        /// <summary>
        /// Gets User from users xml file by its userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>DO.User of the userName</returns>
        /// <exception cref="DO.DalMissingUserNameException">If userName isn't found</exception>
        public User GetByUserName(string userName)
        {
            List<DO.User?> usersList = XMLTools.LoadListFromXMLSerializer<DO.User>(s_Users);
            User user = usersList.Find(x => x?.UserName == userName) ?? throw new DO.DalMissingUserNameException(userName, "User name does not exist");
            return user;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User?> GetAll(Func<User?, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
