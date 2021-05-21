using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;
using System.Data;

namespace WPLClassLibraryTeam13.Entity
{
    public class UserData : ProjectFramework
    {
        public static class Database
        {
            public static string TableName = "tblUserdata";
            public static string PrimaryKey = Columns.klantNummer;
            public static class Columns
            {
                public static string klantNummer = "Klantnummer";
                public static string voorNaam = "Voornaam";
                public static string naam = "Naam";
                public static string emailAdres = "Emailadres";
                public static string telefoon = "Telefoon";
                public static string straatNaam = "Straatnaam";
                public static string huisNummer = "Huisnummer";
                public static string postCode = "Postcode";
                //public static string plaats = "Plaats";
                //public static string land = "Land";
                public static string wachtwoord = "Wachtwoord";
                public static string aangemaakt = "Aangemaakt";
            }
        }
        public UserData() : base(Database.TableName, Database.PrimaryKey)
        {
            TableName = Database.TableName;
            PrimaryKey = Database.PrimaryKey;

        }
        #region Properties
        private int klantNummer;
        private string voorNaam;
        private string naam;
        private string emailAdres;
        private string telefoon;
        private string straatNaam;
        private string huisNummer;
        private string postCode;
        //private string plaats;
        //private string land;
        private string wachtwoord;
        private DateTime aangemaakt;

        //hier word de sqlparameter ingevuld
        public int KlantNummer
        {
            get
            {
                return klantNummer;
            }
            set
            {
                klantNummer = value;
                //hier word de sqlparameter ingevuld
                UpdateSqlParameters(Database.Columns.klantNummer, klantNummer);
            }
        }
        public string VoorNaam
        {
            get
            {
                return voorNaam;
            }
            set
            {
                voorNaam = value;
                UpdateSqlParameters(Database.Columns.voorNaam, voorNaam);
            }
        }
        public string Naam
        {
            get
            {
                return naam;
            }
            set
            {
                naam = value;
                UpdateSqlParameters(Database.Columns.naam, naam);
            }
        }
        public string EmailAdres
        {
            get
            {
                return emailAdres;
            }
            set
            {
                emailAdres = value;
                UpdateSqlParameters(Database.Columns.emailAdres, emailAdres);
            }
        }
        public string Telefoon
        {
            get
            {
                return telefoon;
            }
            set
            {
                telefoon = value;
                UpdateSqlParameters(Database.Columns.telefoon, telefoon);
            }
        }
        public string StraatNaam
        {
            get
            {
                return straatNaam;
            }
            set
            {
                straatNaam = value;
                UpdateSqlParameters(Database.Columns.straatNaam, straatNaam);
            }
        }
        public string HuisNummer
        {
            get
            {
                return huisNummer;
            }
            set
            {
                huisNummer = value;
                UpdateSqlParameters(Database.Columns.huisNummer, huisNummer);
            }
        }
        public string PostCode
        {
            get
            {
                return postCode;
            }
            set
            {
                postCode = value;
                UpdateSqlParameters(Database.Columns.postCode, postCode);
            }
        }
        //public string Plaats
        //{
        //    get
        //    {
        //        return plaats;
        //    }
        //    set
        //    {
        //        plaats = value;
        //        UpdateSqlParameters(Database.Columns.plaats, plaats);
        //    }
        //}
        //public string Land
        //{
        //    get
        //    {
        //        return land;
        //    }
        //    set
        //    {
        //        land = value;
        //        UpdateSqlParameters(Database.Columns.land, land);
        //    }
        //}
        public string Wachtwoord
        {
            get
            {
                return wachtwoord;
            }
            set
            {
                wachtwoord = value;
                UpdateSqlParameters(Database.Columns.wachtwoord, wachtwoord);
            }
        }
        public DateTime Aangemaakt
        {
            get
            {
                return aangemaakt;
            }
            set
            {
                aangemaakt = value;
                UpdateSqlParameters(Database.Columns.aangemaakt, aangemaakt);
            }
        }
        #endregion
        public static UserData GetEntity(int id)
        {
            UserData tocd = null;
            var result = tocd.GetRecords(id);
            var dt = result.DataTable;
            if (dt.Rows.Count == 1)
            {
                tocd = MapDataRow(dt.Rows[0]);
            }
            return tocd;
        }
        private static UserData MapDataRow(DataRow dr)
        {
            UserData tocd = new UserData();
            tocd.klantNummer = Convert.ToInt32(dr[UserData.Database.Columns.klantNummer].ToString());

            return tocd;
        }
        //public bool CheckEmail(string email)
        //{
        //    UserData tocd = new UserData();
        //    var result = tocd.GetEamil(email);
        //    if (result.Rows.Count == 1)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
