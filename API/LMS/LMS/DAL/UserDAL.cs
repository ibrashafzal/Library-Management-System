

using LMS.Entities;

namespace LMS
{
    public class UserDAL
    {
        public User GetAllUser(string username,string password)
        {
            using( var db = new LmsContext())
            {
                return db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            }
        }
    }
}
