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
    public class QueryLaptop : QueryHelper
    {
        public DataTable GetData()
        {
            SQLCommandResult result = new SQLCommandResult();
            try
            {
                string command = $"SELECT * FROM {Laptop.Database.TableName}";
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
            query.Append($"SELECT * FROM {Productdetails.Database.TableName} ");
            query.Append($"INNER JOIN {Laptop.Database.TableName} ");
            query.Append($"ON { Productdetails.Database.TableName}.{ Productdetails.Database.Columns.Productnummer} ");
            query.Append($"= { Laptop.Database.TableName}.{ Laptop.Database.Columns.Productnummer} ");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }

        public DataTable getSingelJoin(int productnummer)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM { Productdetails.Database.TableName } ");
            query.Append($"INNER JOIN { Laptop.Database.TableName } ");
            query.Append($"ON { Productdetails.Database.TableName}.{ Productdetails.Database.Columns.Productnummer } ");
            query.Append($"= { Laptop.Database.TableName}.{ Laptop.Database.Columns.Productnummer } ");
            query.Append($"WHERE { Laptop.Database.TableName}.{ Laptop.Database.Columns.Productnummer } = { productnummer }");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        public DataTable ViewLaptopDetails()
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT {Productdetails.Database.TableName}.{Productdetails.Database.Columns.Productnummer} as 'Productnummer' , " +
                    $"{Laptop.Database.TableName}.{Laptop.Database.Columns.LaptopID} as 'LaptopID' , " +
                    $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Type} as 'Producttype' , " +

                //prijzen
                $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} AS 'Basisprijs', " +                          //basisprijs opgeslagen op de server
                $"ROUND({Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} * 0.79 ,2) AS 'Basisprijs ex BTW' , " + //basisprijs exclusief btw
                $"ROUND(({Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} * 0.21),2) AS BTW , " +                //BTW
                $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Korting} AS Korting, " +                             //Korting
                $"ROUND(({Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} - Korting), 2) AS 'Totaalprijs', " +   //Totaalprijs

                    $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Kleur} as Kleur, " +
                    $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Voorraad} as Voorraad, " +

                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Os}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Processor}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Memory}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Graphics}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Storage}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Display}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Keyboard}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Connectivity}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Battery_and_adaptor}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Touchpad}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Input_and_output}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Audio}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Additional_features}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Finish}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Dimensions}, " +
                    //$"{Laptop.Database.TableName}.{Laptop.Database.Columns.Weight}, " +
                    $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Categorie} as Categorie " +
                    $"FROM {Productdetails.Database.TableName} " +
                    $"INNER JOIN {Laptop.Database.TableName} " +
                    $"ON {Productdetails.Database.TableName}.{Productdetails.Database.Columns.Productnummer} " +
                    $"= {Laptop.Database.TableName}.{Laptop.Database.Columns.Productnummer}");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }

        //public SQLCommandResult GetData(string email, string pwd)
        //{
        //    SQLCommandResult result = new SQLCommandResult();
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append($"SELECT * FROM {Laptop.Database.TableName} ");
        //        sb.Append($"WHERE {UserData.Database.Columns.emailAdres} = '{email}'");
        //        sb.Append($" and {UserData.Database.Columns.wachtwoord} = '{pwd}'");
        //        result = ExecuteQuery(sb.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetRecord", ex);

        //    }
        //    return result;
        //}
    }
}
