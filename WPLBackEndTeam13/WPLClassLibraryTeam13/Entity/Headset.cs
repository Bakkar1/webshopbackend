using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;

namespace WPLClassLibraryTeam13.Entity
{
    public class Headset : ProjectFramework
    {
        public static class Database
        {
            public static string TableName = "tblheadsetdetails";
            public static string PrimaryKey = Columns.HeadsetID;

            public static class Columns
            {
                public static string HeadsetID = "headsetid";
                public static string ProductNummer = "productnummer";
                public static string Diameter_Drivers = "diameter_drivers_mm";
                public static string InklapbareMic = "inklapbaremicro";
                public static string Verbinding = "verbinding";
                public static string Verlichting = "verlichting";
            }
        }

        public Headset() : base(Database.TableName, Database.PrimaryKey)
        {

        }

        #region Properties

        private int headsetId;
        private int productNummer;
        private string diameterDrivers;
        private string inklapbareMicro;
        private string verbinding;
        private string verlichting;


        public int HeadsetId
        {
            get { return headsetId; }
            set
            {
                headsetId = value;
                UpdateSqlParameters(Database.Columns.HeadsetID, headsetId);
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

        public string DiameterDrivers
        {
            get { return diameterDrivers; }
            set
            {
                diameterDrivers = value;
                UpdateSqlParameters(Database.Columns.Diameter_Drivers, diameterDrivers);
            }
        }

        public string InklapbareMicro
        {
            get { return inklapbareMicro; }
            set
            {
                inklapbareMicro = value;
                UpdateSqlParameters(Database.Columns.InklapbareMic, inklapbareMicro);
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

        public static Headset GetEntity(int id)
        {
            Headset headset = new Headset();
            var result = headset.Read(id);
            var dt = result.DataTable;
            if (dt.Rows.Count == 1)
            {
                headset = MapDataRow(dt.Rows[0]);
            }
            return headset;
        }

        private static Headset MapDataRow(DataRow dr)
        {
            Headset headset = new Headset();
            headset.HeadsetId = Convert.ToInt32(dr[Headset.Database.Columns.HeadsetID]);
            headset.ProductNummer = Convert.ToInt32(dr[Headset.Database.Columns.ProductNummer]);
            headset.DiameterDrivers = dr[Headset.Database.Columns.Diameter_Drivers].ToString();
            headset.InklapbareMicro = dr[Headset.Database.Columns.InklapbareMic].ToString();
            headset.Verbinding = dr[Headset.Database.Columns.Verbinding].ToString();
            headset.Verlichting = dr[Headset.Database.Columns.Verlichting].ToString();
            return headset;
        }



    }
}
