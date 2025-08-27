using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management.DAL
{
    public class UserDAL
    {
        public User GetAllUser(string username,string password)
        {
            using( var db = new LMSEntities2())
            {
                return db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            }
        }
    }
}
