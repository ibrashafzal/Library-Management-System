
using System.Drawing;
using LMS.Entities;


namespace LMS
{
    public class UserRepo
    {
        public UserDAL userDAL = new UserDAL();
               
            public bool GetUsers(string username, string password)
        {
            var user = userDAL.GetAllUser(username, password);
            return user != null;
        }
        

     
        }

    }







