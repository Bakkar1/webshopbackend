using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;

namespace WPLClassLibraryTeam13.Entity
{
    public class Toetsenbord : ProjectFramework
    {
        public static class Database
        {
            public static string TableName = "tbltoetsenborddetails";
            public static string PrimaryKey = Columns.ToetsenbordID;

            public static class Columns
            {
                public static string ToetsenbordID = "toetsenbordid";
                public static string ProductNummer = "productnummer";
                public static string ModelAzerty = "modelazerty";
                public static string NumeriekKlavier = "numeriekklavier";
                public static string Verbinding = "verbinding";
                public static string Verlichting = "verlichting";
            }
        }

        public Toetsenbord() : base(Database.TableName, Database.PrimaryKey)
        {

        }

        #region Properties

        private int toetsenbordId;
        private int productNummer;
        private string modelAzerty;
        private string numeriekKlavier;
        private string verbinding;
        private string verlichting;


        public int ToetsenBordId
        {
            get { return toetsenbordId; }
            set
            {
                toetsenbordId = value;
                UpdateSqlParameters(Database.Columns.ToetsenbordID, toetsenbordId);
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

        public string ModelAzerty
        {
            get { return modelAzerty; }
            set
            {
                modelAzerty = value;
                UpdateSqlParameters(Database.Columns.ModelAzerty, modelAzerty);
            }
        }

        public string NumeriekKlavier
        {
            get { return numeriekKlavier; }
            set
            {
                numeriekKlavier = value;
                UpdateSqlParameters(Database.Columns.NumeriekKlavier, numeriekKlavier);
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

        public static Toetsenbord GetEntity(int id)
        {
            Toetsenbord toetsenbord = new Toetsenbord();
            var result = toetsenbord.Read(id);
            var dt = result.DataTable;
            if (dt.Rows.Count == 1)
            {
                toetsenbord = MapDataRow(dt.Rows[0]);
            }
            return toetsenbord;
        }

        private static Toetsenbord MapDataRow(DataRow dr)
        {
            Toetsenbord toetsenbord = new Toetsenbord();
            toetsenbord.ToetsenBordId = Convert.ToInt32(dr[Toetsenbord.Database.Columns.ToetsenbordID]);
            toetsenbord.ProductNummer = Convert.ToInt32(dr[Toetsenbord.Database.Columns.ProductNummer]);
            toetsenbord.ModelAzerty = dr[Toetsenbord.Database.Columns.ModelAzerty].ToString();
            toetsenbord.NumeriekKlavier = dr[Toetsenbord.Database.Columns.NumeriekKlavier].ToString();
            toetsenbord.Verbinding = dr[Headset.Database.Columns.Verbinding].ToString();
            toetsenbord.Verlichting = dr[Headset.Database.Columns.Verlichting].ToString();
            return toetsenbord;
        }
    }
}
