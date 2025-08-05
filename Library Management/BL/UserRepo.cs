using System;
using System.Collections.Generic;
using System.Linq;
using Library_Management.Model;
using Library_Management.PL;

namespace Library_Management.BL
{
    public class UserRepo
    {
        public Persistent Persistent = new Persistent();

        public void AddUser(UserModel user)
        {
            // Add user (ID is automatically generated in Persistent layer)
            Persistent.AddUser(user);
        }
        

        public List<UserModel> GetAll()
        {
            return Persistent.Users;
        }
        public List<UserModel> Search(string searchText)
        {
            // Ensure books are loaded from file
            Persistent.LoadUsers();
            List<UserModel> users = Persistent.Users;
            List<UserModel> filteredUsers = new List<UserModel>();

            if (string.IsNullOrWhiteSpace(searchText))
                return users;

            searchText = searchText.ToLower();

            // Traditional loop for filtering
            foreach (UserModel user in users)
            {
                if ((user.Name != null && user.Name.ToLower().Contains(searchText)) ||
                    (user.Name != null && user.Email.ToLower().Contains(searchText)) ||
                    (user.Class!= null && user.Class.ToLower().Contains(searchText)) ||
                    (user.Contact != null && user.Contact.ToLower().Contains(searchText)))


                {
                    filteredUsers.Add(user);
                }
            }

            return filteredUsers;
        }

        public void DeleteUser(int Id)
        {
            Persistent.Delete(Id);
        }
        
        public void EditUser(int Id, UserModel updatedUser)
        {
            Persistent.Edit(Id,updatedUser);
        }
       




        
    }

}





