using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;

namespace WPLClassLibraryTeam13.Entity
{
    public class Muis : ProjectFramework
    {
        public static class Database
        {
            public static string TableName = "tblmuizendetails";
            public static string PrimaryKey = Columns.MuisID;

            public static class Columns
            {
                public static string MuisID = "muisid";
                public static string ProductNummer = "productnummer";
                public static string DPI = "DPI";
                public static string Instelbaarh_DPI = "instelbaarheiddpi";
                public static string AantalKnoppen = "aantalknoppen";
                public static string Verbinding = "verbinding";
                public static string Verlichting = "verlichting";
            }
        }

        public Muis() : base(Database.TableName, Database.PrimaryKey)
        {

        }

        #region Properties

        private int muisId;
        private int productNummer;
        private int dpi;
        private string instelbaarh_dpi;
        private int aantalKnoppen;
        private string verbinding;
        private string verlichting;


        public int MuisId
        {
            get { return muisId; }
            set
            {
                muisId = value;
                UpdateSqlParameters(Database.Columns.MuisID, muisId);
            }
        }


        public int ProductNummer
        {
            get { return productNummer; }
            set
            {
                productNummer = value;
                UpdateSqlParameters(Database.Columns.ProductNummer, productNummer);
            }
        }

        public int Dpi
        {
            get { return dpi; }
            set
            {
                dpi = value;
                UpdateSqlParameters(Database.Columns.DPI, dpi);
            }
        }

        public string Instelbaarh_dpi
        {
            get { return instelbaarh_dpi; }
            set
            {
                instelbaarh_dpi = value;
                UpdateSqlParameters(Database.Columns.Instelbaarh_DPI, instelbaarh_dpi);
            }
        }

        public int AantalKnoppen
        {
            get { return aantalKnoppen; }
            set
            {
                aantalKnoppen = value;
                UpdateSqlParameters(Database.Columns.AantalKnoppen, aantalKnoppen);
            }
        }

        public string Verbinding
        {
            get { return verbinding; }
            set
            {
                verbinding = value;
                UpdateSqlParameters(Database.Columns.Verbinding, verbinding);
            }
        }

        public string Verlichting
        {
            get { return verlichting; }
            set
            {
                verlichting = value;
                UpdateSqlParameters(Database.Columns.Verlichting, verlichting);
            }
        }

        #endregion

        public static Muis GetEntity(int id)
        {
            Muis muis = new Muis();
            var result = muis.Read(id);
            var dt = result.DataTable;
            if (dt.Rows.Count == 1)
            {
                muis = MapDataRow(dt.Rows[0]);
            }
            return muis;
        }

        private static Muis MapDataRow(DataRow dr)
        {
            Muis muis = new Muis();
            muis.MuisId = Convert.ToInt32(dr[Muis.Database.Columns.MuisID]);
            muis.ProductNummer = Convert.ToInt32(dr[Muis.Database.Columns.ProductNummer]);
            muis.Dpi = Convert.ToInt32(dr[Muis.Database.Columns.DPI]);
            muis.Instelbaarh_dpi = dr[Muis.Database.Columns.Instelbaarh_DPI].ToString();
            muis.AantalKnoppen = Convert.ToInt32(dr[Muis.Database.Columns.AantalKnoppen]);
            muis.Verbinding = dr[Muis.Database.Columns.Verbinding].ToString();
            muis.Verlichting = dr[Muis.Database.Columns.Verlichting].ToString();
            return muis;
        }

    }
}
