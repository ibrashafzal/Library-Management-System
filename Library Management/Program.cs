using System;
using System.Linq;
using System.Windows.Forms;
using Library_Management.Admin;


namespace Library_Management
{
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ADMIN()
            {

            });

        }
    }
}
