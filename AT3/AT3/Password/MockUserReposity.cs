using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT3.Password
{
    class MockUserReposity
    {
        List<User> users = new List<User>();

        //add user to memory
        public void AddUser(User user)
        {
            users.Add(user);
        }

        //retrieve user based on id
        public User GetUser(string userid)
        {
            try
            {
                return users.Single(u => u.UserId == userid);
            }
            catch
            {
                return users.First();
            }
        }
    }
}
