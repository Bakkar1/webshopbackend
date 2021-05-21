using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;

namespace WPLClassLibraryTeam13.Entity
{
    public class Admin : ProjectFramework
    {

        public static class Database
        {
            public static string TableName = "tbladmindata";
            public static string PrimaryKey = Columns.AdminId;

            public static class Columns
            {
                public static string AdminId = "adminid";
                public static string AdminNaam = "adminnaam";
                public static string AdminUsername = "adminusername";
                public static string AdminPaswoord = "adminpaswoord";
                public static string FullAccess = "fullaccess";
                public static string Afbeeldingen_wpf = "afbeeldingenwpf";
            }
        }

        public Admin() : base(Database.TableName, Database.PrimaryKey)
        {

        }

        private int adminID;
        private string adminNaam;
        private string adminUsername;
        private string adminPaswoord;
        private string fullAcces;
        private string afbeeldingen_wpf;

        public int AdminID
        {
            get { return adminID; }
            set
            {
                adminID = value;
                UpdateSqlParameters(Database.Columns.AdminId, adminID);
            }
        }

        public string AdminNaam
        {
            get { return adminNaam; }
            set
            {
                adminNaam = value;
                UpdateSqlParameters(Database.Columns.AdminNaam, adminNaam);
            }
        }

        public string AdminUsername
        {
            get { return adminUsername; }
            set
            {
                adminUsername = value;
                UpdateSqlParameters(Database.Columns.AdminUsername, adminUsername);
            }
        }

        public string AdminPaswoord
        {
            get { return adminPaswoord; }
            set
            {
                adminPaswoord = value;
                UpdateSqlParameters(Database.Columns.AdminPaswoord, adminPaswoord);
            }
        }

        public string FullAcces
        {
            get { return fullAcces; }
            set
            {
                fullAcces = value;
                UpdateSqlParameters(Database.Columns.FullAccess, fullAcces);
            }
        }
        public string Afbeeldingen_wpf
        {
            get { return afbeeldingen_wpf; }
            set
            {
                afbeeldingen_wpf = value;
                UpdateSqlParameters(Database.Columns.Afbeeldingen_wpf, afbeeldingen_wpf);
            }

        }

        public static Admin GetEntity(int id)
        {
            Admin admin = new Admin();
            var result = admin.GetRecords(id);
            var dt = result.DataTable;
            if (dt.Rows.Count == 1)
            {
                admin = MapDataRow(dt.Rows[0]);
            }
            return admin;
        }
        private static Admin MapDataRow(DataRow dr)
        {
            Admin admin = new Admin();
            admin.AdminID = Convert.ToInt32(dr[Admin.Database.Columns.AdminId]);
            admin.AdminNaam = dr[Admin.Database.Columns.AdminNaam].ToString();
            admin.AdminUsername = dr[Admin.Database.Columns.AdminUsername].ToString();
            admin.AdminPaswoord = dr[Admin.Database.Columns.AdminPaswoord].ToString();
            admin.Afbeeldingen_wpf = dr[Admin.Database.Columns.Afbeeldingen_wpf].ToString();
            return admin;
        }
    }
}
