using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;

namespace WPLClassLibraryTeam13.Entity
{
    public class Orders : ProjectFramework
    {
        public static class Database
        {
            public static string TableName = "orders";
            public static string PrimaryKey = Columns.OrderNummer;

            public static class Columns
            {
                public static string KlantNummer = "klantnummer";
                public static string OrderNummer = "ordernummer";
                public static string OrderDatum = "orderdatum";
                public static string StraatNaam = "straatnaam";
                public static string Postcode = "postcode";
                public static string LeveringMethod = "leveringmethod";
                public static string LeveringOpmerking = "leveringopmerking";
                public static string OrderStatus = "orderstatus";
            }
        }
        public Orders() : base(Database.TableName, Database.PrimaryKey)
        {

        }

        private int klantNummer;
        private int orderNummer;
        private DateTime orderDatum;
        private string straatNaam;
        private string postcode;
        private string leveringMethod;
        private string leveringOpmerking;
        private string orderStatus;

        public int KlantNummer
        {
            get { return klantNummer; }
            set
            {
                klantNummer = value;
                UpdateSqlParameters(Database.Columns.KlantNummer, klantNummer);
            }
        }
        public int OrderNummer
        {
            get { return orderNummer; }
            set
            {
                orderNummer = value;
                UpdateSqlParameters(Database.Columns.OrderNummer, orderNummer);
            }
        }
        public DateTime OrderDatum
        {
            get { return orderDatum; }
            set
            {
                orderDatum = value;
                UpdateSqlParameters(Database.Columns.OrderDatum, orderDatum);
            }
        }
        public string StraatNaam
        {
            get { return straatNaam; }
            set
            {
                straatNaam = value;
                UpdateSqlParameters(Database.Columns.StraatNaam, straatNaam);
            }
        }
        public string Postcode
        {
            get { return postcode; }
            set
            {
                postcode = value;
                UpdateSqlParameters(Database.Columns.Postcode, postcode);
            }
        }
        public string LeveringMethod
        {
            get { return leveringMethod; }
            set
            {
                leveringMethod = value;
                UpdateSqlParameters(Database.Columns.LeveringMethod, leveringMethod);
            }
        }
        public string LeveringOpmerking
        {
            get { return leveringOpmerking; }
            set
            {
                leveringOpmerking = value;
                UpdateSqlParameters(Database.Columns.LeveringOpmerking, leveringOpmerking);
            }
        }
        public string OrderStatus
        {
            get { return orderStatus; }
            set
            {
                orderStatus = value;
                UpdateSqlParameters(Database.Columns.OrderStatus, orderStatus);
            }
        }
    }
}
