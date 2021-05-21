using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;
using WPLClassLibraryTeam13.Entity;

namespace WPLClassLibraryTeam13.Query
{
    public class QueryHeadset : QueryHelper
    {
        public SQLCommandResult getLatestProductDetail()
        {
            SQLCommandResult result = new SQLCommandResult();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT MAX ({ Productdetails.Database.Columns.Productnummer }) FROM { Productdetails.Database.TableName }");
            result = ExecuteQuery(query.ToString());
            return result;
        }
        public DataTable getSingelJoin(int productnummer)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM { Productdetails.Database.TableName } ");
            query.Append($"INNER JOIN { Headset.Database.TableName } ");
            query.Append($"ON { Productdetails.Database.TableName}.{ Productdetails.Database.Columns.Productnummer } ");
            query.Append($"= { Headset.Database.TableName}.{ Headset.Database.Columns.ProductNummer } ");
            query.Append($"WHERE { Headset.Database.TableName}.{ Headset.Database.Columns.ProductNummer }= { productnummer }");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        public SQLCommandResult getSingelJoinResult(int productnummer)
        {
            SQLCommandResult result = new SQLCommandResult();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM { Productdetails.Database.TableName } ");
            query.Append($"INNER JOIN { Headset.Database.TableName } ");
            query.Append($"ON { Productdetails.Database.TableName}.{ Productdetails.Database.Columns.Productnummer } ");
            query.Append($"= { Headset.Database.TableName}.{ Headset.Database.Columns.ProductNummer } ");
            query.Append($"WHERE { Headset.Database.TableName}.{ Headset.Database.Columns.ProductNummer } = { productnummer }");
            result = ExecuteQuery(query.ToString());
            return result;
        }
        public DataTable getJoinData()
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM { Productdetails.Database.TableName} ");
            query.Append($"INNER JOIN { Headset.Database.TableName } ");
            query.Append($"ON { Productdetails.Database.TableName }.{ Productdetails.Database.Columns.Productnummer } ");
            query.Append($"= { Headset.Database.TableName }.{ Headset.Database.Columns.ProductNummer} ");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        public DataTable ViewHeadsetDetails()
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT {Productdetails.Database.TableName}.{Productdetails.Database.Columns.Productnummer} AS 'Productnummer' , " +
                    $"{Headset.Database.TableName}.{Headset.Database.Columns.HeadsetID} as 'HeadsetID' , " +
                    $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Type} as 'Producttype' , " +

                //prijzen
                $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} AS 'Basisprijs', " +                          //basisprijs opgeslagen op de server
                $"ROUND({Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} * 0.79 ,2) AS 'Basisprijs ex BTW' , " + //basisprijs exclusief btw
                $"ROUND(({Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} * 0.21),2) AS BTW , " +                //BTW
                $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Korting} AS Korting, " +                             //Korting
                $"ROUND(({Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} - Korting), 2) AS 'Totaalprijs', " +   //Totaalprijs

                    $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Kleur} as 'Kleur', " +
                    $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Voorraad} as 'Voorraad' , " +

                    $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Categorie} as 'Categorie'  " +
                    $"FROM {Productdetails.Database.TableName} " +
                    $"INNER JOIN {Headset.Database.TableName} " +
                $"ON {Productdetails.Database.TableName}.{Productdetails.Database.Columns.Productnummer} " +
                $"= {Headset.Database.TableName}.{Headset.Database.Columns.ProductNummer}");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
       
    }
}
