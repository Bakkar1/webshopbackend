using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;

namespace WPLClassLibraryTeam13.Entity
{
    public class OrdersProducten : ProjectFramework
    {
        public static class Database
        {
            public static string TableName = "ordersproducten";
            public static string PrimaryKey = $"{Columns.OrderNummer},{Columns.ProductNummer}";
            public static class Columns
            {
                public static string OrderNummer = "ordernummer";
                public static string ProductNummer = "productnummer";
                public static string AantalProducten = "aantalproducten";
                public static string Prijs = "prijs";
            }
        }
        public OrdersProducten() : base(Database.TableName, Database.PrimaryKey)
        {

        }

        private int orderNummer;
        private int productNummer;
        private int aantalProducten;
        private float prijs;


        public int OrderNummer
        {
            get { return orderNummer; }
            set
            {
                orderNummer = value;
                UpdateSqlParameters(Database.Columns.OrderNummer, orderNummer);
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
        public int AantalProducten
        {
            get { return aantalProducten; }
            set
            {
                aantalProducten = value;
                UpdateSqlParameters(Database.Columns.AantalProducten, aantalProducten);
            }
        }
        public float Prijs
        {
            get { return prijs; }
            set
            {
                prijs = value;
                UpdateSqlParameters(Database.Columns.Prijs, prijs);
            }
        }

    }
}
