using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Library_Management.DAL;
using Library_Management.Model;


namespace Library_Management.BL
{
    public class UserRepo
    {
        public UserDAL userDAL = new UserDAL();
               
            public bool GetUsers(string username, string password)
        {
            var user = userDAL.GetAllUser(username, password);
            return user != null;
        }
        

        public void FormLocation(Form childForm)
        {
           
                if (childForm.MdiParent == null) return;

                MdiClient mdiClient = childForm.MdiParent.Controls
                    .OfType<MdiClient>()
                    .FirstOrDefault();

                if (mdiClient == null) return;

                Rectangle parentBounds = mdiClient.ClientRectangle;

                int newX = childForm.Left;
                int newY = childForm.Top;

                if (childForm.Left < 0) newX = 0;
                if (childForm.Top < 0) newY = 0;
                if (childForm.Right > parentBounds.Width) newX = parentBounds.Width - childForm.Width;
                if (childForm.Bottom > parentBounds.Height) newY = parentBounds.Height - childForm.Height;

                childForm.Location = new Point(newX, newY);
            }
        }

    }







