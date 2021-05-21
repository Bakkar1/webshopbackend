using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;
using WPLClassLibraryTeam13.Entity;

namespace WPLClassLibraryTeam13.Query
{
    public class QueryOrders : QueryHelper
    {
        public DataTable GetData()
        {
            SQLCommandResult result = new SQLCommandResult();
            try
            {
                string command = $"SELECT * FROM {Orders.Database.TableName}";
                result = ExecuteQuery(command);
            }
            catch (Exception ex)
            {
                throw new Exception("GetRecord", ex);

            }
            return result.DataTable;
        }
        public DataTable getJoinData()
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM {Orders.Database.TableName} ");
            query.Append($"INNER JOIN {OrdersProducten.Database.TableName} ");
            query.Append($"ON { Orders.Database.TableName}.{ Orders.Database.Columns.OrderNummer} ");
            query.Append($"= { OrdersProducten.Database.TableName}.{ OrdersProducten.Database.Columns.OrderNummer} ");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        public DataTable getJoinData(int klantId)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM {Orders.Database.TableName} ");
            query.Append($"INNER JOIN {OrdersProducten.Database.TableName} ");
            query.Append($"ON { Orders.Database.TableName}.{ Orders.Database.Columns.OrderNummer} ");
            query.Append($"= { OrdersProducten.Database.TableName}.{ OrdersProducten.Database.Columns.OrderNummer} ");
            query.Append($"WHERE { Orders.Database.TableName}.{Orders.Database.Columns.KlantNummer} = {klantId}");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        public DataTable getJoinDataStatus(string status)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM {Orders.Database.TableName} ");
            query.Append($"INNER JOIN {OrdersProducten.Database.TableName} ");
            query.Append($"ON { Orders.Database.TableName}.{ Orders.Database.Columns.OrderNummer} ");
            query.Append($"= { OrdersProducten.Database.TableName}.{ OrdersProducten.Database.Columns.OrderNummer} ");
            query.Append($"WHERE lower({ Orders.Database.TableName}.{Orders.Database.Columns.OrderStatus}) = '{status.ToLower()}'");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        public DataTable GetJoinDataDate(DateTime date)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM {Orders.Database.TableName} ");
            query.Append($"INNER JOIN {OrdersProducten.Database.TableName} ");
            query.Append($"ON { Orders.Database.TableName}.{ Orders.Database.Columns.OrderNummer} ");
            query.Append($"= { OrdersProducten.Database.TableName}.{ OrdersProducten.Database.Columns.OrderNummer} ");
            query.Append($"WHERE { Orders.Database.TableName}.{Orders.Database.Columns.OrderDatum} = '{ConvertToSqlDate(date)}'");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        public DataTable getJoinDataStatus(string status, int id)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM {Orders.Database.TableName} ");
            query.Append($"INNER JOIN {OrdersProducten.Database.TableName} ");
            query.Append($"ON { Orders.Database.TableName}.{ Orders.Database.Columns.OrderNummer} ");
            query.Append($"= { OrdersProducten.Database.TableName}.{ OrdersProducten.Database.Columns.OrderNummer} ");
            query.Append($"WHERE lower({ Orders.Database.TableName}.{Orders.Database.Columns.OrderStatus}) = '{status.ToLower()}'");
            query.Append($"AND {Orders.Database.TableName}.{Orders.Database.Columns.KlantNummer} = {id}");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        public DataTable GetProductDetails(int productNummer)
        {
            StringBuilder command = new StringBuilder();
            SQLCommandResult resultCommand = new SQLCommandResult();
            command.Append($"SELECT {Productdetails.Database.Columns.Categorie} ");
            command.Append($"FROM {Productdetails.Database.TableName} ");
            command.Append($"WHERE {Productdetails.Database.Columns.Productnummer} = {productNummer}");
            resultCommand = ExecuteQuery(command.ToString());

            StringBuilder query = new StringBuilder();
            query.Append($"Select * from {Productdetails.Database.TableName}");

            if (resultCommand.DataTable.Rows[0][Productdetails.Database.Columns.Categorie].ToString() == "laptop")
            {
                query.Append($"JOIN {Laptop.Database.TableName}");
                query.Append($"ON {Laptop.Database.TableName}.{Laptop.Database.Columns.Productnummer}");
                
            }
            else if(resultCommand.DataTable.Rows[0][Productdetails.Database.Columns.Categorie].ToString() == "muis")
            {
                query.Append($"JOIN {Muis.Database.TableName}");
                query.Append($"ON {Muis.Database.TableName}.{Muis.Database.Columns.ProductNummer}");
            }
            else if (resultCommand.DataTable.Rows[0][Productdetails.Database.Columns.Categorie].ToString() == "toetsenbord") {
                query.Append($"JOIN {Toetsenbord.Database.TableName}");
                query.Append($"ON {Toetsenbord.Database.TableName}.{Toetsenbord.Database.Columns.ProductNummer}");
            }
            else if(resultCommand.DataTable.Rows[0][Productdetails.Database.Columns.Categorie].ToString() == "headset")
            {
                query.Append($"JOIN {Headset.Database.TableName}");
                query.Append($"ON {Headset.Database.TableName}.{Headset.Database.Columns.ProductNummer}");
            }

            query.Append($" = {Productdetails.Database.TableName}.{Productdetails.Database.Columns.Productnummer}");
            query.Append($"WHERE  {Productdetails.Database.TableName}.{Productdetails.Database.Columns.Productnummer} = {productNummer}");

            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }

        private string ConvertToSqlDate(DateTime date)
        {
            return $"{date.Year}-{date.Month}-{date.Day}";
        }
    }
}
