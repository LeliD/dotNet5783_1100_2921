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
        readonly string s_Users = "users";
        public int Add(User item)
        {
            if (DataSource.UsersList.Exists(x => x?.UserName == user.UserName))
                throw new DO.DalAlreadyExistUserNameException(user.UserName!, $"Username: {user.UserName} already exist");
            DataSource.UsersList.Add(user);
            return 0;
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

        public User GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
