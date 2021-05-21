using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;
using System.Data;

namespace WPLClassLibraryTeam13.Entity
{
    public class Productdetails : ProjectFramework
    {
        public static class Database
        {
            public static string TableName = "Productdetails";
            public static string PrimaryKey = Columns.Productnummer;
            public static class Columns
            {
                public static string Productnummer = "Productnummer";
                //public static string Merk = "merk";
                public static string Prijs = "prijs";
                public static string Type = "producttype";
                public static string Korting = "korting";
                public static string Beschrijving = "kleine_beschrijving";
                public static string Uitgebreide_beschrijving = "uitgebr_beschrijving";
                public static string Kleur = "kleur";
                public static string Voorraad = "voorraad";
                public static string Images = "images";
                public static string Categorie = "Categorie";
            }
        }
        public Productdetails() : base(Database.TableName, Database.PrimaryKey)
        {
            TableName = Database.TableName;
            PrimaryKey = Database.PrimaryKey;

        }
        #region Properties
        private int productnummer;
        //private string merk;
        private string type;
        private double prijs;
        private double korting;
        private string beschrijving;
        private string uitgebreide_beschrijving;
        private string kleur;
        private int voorraad;
        private string images;
        private string categorie;



        //hier word de sqlparameter ingevuld
        public int Productnummer
        {
            get
            {
                return productnummer;
            }
            set
            {
                productnummer = value;
                //hier word de sqlparameter ingevuld
                UpdateSqlParameters(Database.Columns.Productnummer, productnummer);
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                UpdateSqlParameters(Database.Columns.Type, type);
            }
        }
        public double Korting
        {
            get
            {
                return korting;
            }
            set
            {
                korting = value;
                UpdateSqlParameters(Database.Columns.Korting, korting);
            }
        }
        public string Beschrijving
        {
            get
            {
                return beschrijving;
            }
            set
            {
                beschrijving = value;
                UpdateSqlParameters(Database.Columns.Beschrijving, beschrijving);
            }
        }
        public string Uitgebreide_beschrijving
        {
            get
            {
                return uitgebreide_beschrijving;
            }
            set
            {
                uitgebreide_beschrijving = value;
                UpdateSqlParameters(Database.Columns.Uitgebreide_beschrijving, uitgebreide_beschrijving);
            }
        }

        
        public int Voorraad
        {
            get
            {
                return voorraad;
            }
            set
            {
                voorraad = value;
                UpdateSqlParameters(Database.Columns.Voorraad, voorraad);
            }
        }
        public string Images
        {
            get
            {
                return images;
            }
            set
            {
                images = value;
                UpdateSqlParameters(Database.Columns.Images, images);
            }
        }

        public string Categorie
        {
            get
            {
                return categorie;
            }
            set
            {
                categorie = value;
                UpdateSqlParameters(Database.Columns.Categorie, categorie);
            }
        }

        //public string Pluspunten
        //{
        //    get
        //    {
        //        return pluspunten;
        //    }
        //    set
        //    {
        //        pluspunten = value;
        //        UpdateSqlParameters(Database.Columns.Pluspunten, pluspunten);
        //    }
        //}
        public double Prijs
        {
            get
            {
                return prijs;
            }
            set
            {
                prijs = value;
                UpdateSqlParameters(Database.Columns.Prijs, prijs);
            }
        }
        public string Kleur
        {
            get
            {
                return kleur;
            }
            set
            {
                kleur = value;
                UpdateSqlParameters(Database.Columns.Kleur, kleur);
            }
        }

        #endregion
        public static Productdetails GetEntity(int id)
        {
            Productdetails pd = new Productdetails();
            var result = pd.Read(id);
            var dt = result.DataTable;
            if (dt.Rows.Count == 1)
            {
                pd = MapDataRow(dt.Rows[0]);
            }
            return pd;
        }
        private static Productdetails MapDataRow(DataRow dr)
        {
            Productdetails pd = new Productdetails();
            pd.Productnummer = Convert.ToInt32(dr[Productdetails.Database.Columns.Productnummer]);
            //pd.Merk = dr[Productdetails.Database.Columns.Merk].ToString();
            pd.Type = dr[Productdetails.Database.Columns.Type].ToString();
            pd.Prijs = Convert.ToDouble(dr[Productdetails.Database.Columns.Prijs]);
            pd.Korting = Convert.ToDouble(dr[Productdetails.Database.Columns.Korting]);
            pd.Beschrijving = dr[Productdetails.Database.Columns.Beschrijving].ToString();
            pd.Uitgebreide_beschrijving = dr[Productdetails.Database.Columns.Uitgebreide_beschrijving].ToString();
            pd.Kleur = dr[Productdetails.Database.Columns.Kleur].ToString();
            pd.Voorraad = Convert.ToInt32(dr[Productdetails.Database.Columns.Voorraad]);
            pd.Images = dr[Productdetails.Database.Columns.Images].ToString();
            pd.Categorie = dr[Productdetails.Database.Columns.Categorie].ToString();
            //pd.Pluspunten = dr[Productdetails.Database.Columns.Pluspunten].ToString();
            return pd;
        }
    }
}
