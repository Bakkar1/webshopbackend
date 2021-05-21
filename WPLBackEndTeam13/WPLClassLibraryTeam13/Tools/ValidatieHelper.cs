using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;

namespace WPLClassLibraryTeam13.Tools
{
    public static class ValidatieHelper
    {

        public static bool VType(string s)
        {
            bool v = false;
            QueryProductDetails qpd = new QueryProductDetails();
            if (s.Length > 0)
            {

                if (qpd.GetData(s).Count < 2)
                {
                    v = true ;
                }
                else
                {
                    v = false;
                    MessageBox.Show("Producttype bestaat al.");
                }
                
                
            }

            return v;
        }

    }
    public static class AdminHelper
    {
        public static void TestAdminData(Admin admin)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Id = {admin.AdminID}");
            sb.AppendLine($"Naam = {admin.AdminNaam}");
            sb.AppendLine($"Username = {admin.AdminUsername}");
            sb.AppendLine($"Paswoord(moet leeg blijven) = {admin.AdminPaswoord}");
            sb.AppendLine($"Afbeeldingpad = {admin.Afbeeldingen_wpf}");
            MessageBox.Show(sb.ToString());
        }
    }
}
