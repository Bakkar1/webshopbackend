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
    public class QueryWinkelmand : QueryHelper
    {
        public List<DataTable> GetProductsForWinkelmand(List<int> ids)
        {
            List<DataTable> datalist = new List<DataTable>();
            StringBuilder command = new StringBuilder();
            SQLCommandResult result = new SQLCommandResult();
            foreach (int id in ids)
            {
                command.Clear();
                command.Append($"SELECT {Productdetails.Database.Columns.Categorie} ");
                command.Append($"FROM {Productdetails.Database.TableName} ");
                command.Append($"WHERE {Productdetails.Database.Columns.Productnummer} = {id}");
                result = ExecuteQuery(command.ToString());
                if (result.DataTable.Rows[0][Productdetails.Database.Columns.Categorie].ToString() == "laptop")
                {
                    command.Clear();
                    command.Append($"SELECT p.{Productdetails.Database.Columns.Productnummer}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Type}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Prijs}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Korting}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Images}, ");
                    command.Append($"l.{Laptop.Database.Columns.Processor} + '|' + ");
                    command.Append($"l.{Laptop.Database.Columns.Graphics} + '|' + ");
                    command.Append($"l.{Laptop.Database.Columns.Storage} \"pluspunten\" ");
                    command.Append($"FROM {Productdetails.Database.TableName} p ");
                    command.Append($"JOIN {Laptop.Database.TableName} l ");
                    command.Append($"ON p.{Productdetails.Database.Columns.Productnummer} = l.{Laptop.Database.Columns.Productnummer} ");
                    command.Append($"WHERE p.{Productdetails.Database.Columns.Productnummer} = {id}");
                    result = ExecuteQuery(command.ToString());
                    datalist.Add(result.DataTable);
                }
                else if (result.DataTable.Rows[0][Productdetails.Database.Columns.Categorie].ToString() == "muis")
                {
                    command.Clear();
                    command.Append($"SELECT p.{Productdetails.Database.Columns.Productnummer}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Type}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Prijs}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Korting}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Images}, ");
                    command.Append($"CONCAT('DPI: ', m.{Muis.Database.Columns.DPI}, '|', ");
                    command.Append($"m.{Muis.Database.Columns.AantalKnoppen}, ' knoppen',  ");
                    command.Append($"'|Verbinding: ' , m.{Muis.Database.Columns.Verbinding}) \"pluspunten\" ");
                    command.Append($"FROM {Productdetails.Database.TableName} p ");
                    command.Append($"JOIN {Muis.Database.TableName} m ");
                    command.Append($"ON p.{Productdetails.Database.Columns.Productnummer} = m.{Muis.Database.Columns.ProductNummer} ");
                    command.Append($"WHERE p.{Productdetails.Database.Columns.Productnummer} = {id}");
                    result = ExecuteQuery(command.ToString());
                    datalist.Add(result.DataTable);
                }
                else if (result.DataTable.Rows[0][Productdetails.Database.Columns.Categorie].ToString() == "toetsenbord")
                {
                    command.Clear();
                    command.Append($"SELECT p.{Productdetails.Database.Columns.Productnummer}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Type}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Prijs}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Korting}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Images}, ");
                    command.Append($"CONCAT(t.{Toetsenbord.Database.Columns.ModelAzerty} , '|' , ");
                    command.Append($"'Nummerklavier: ' , CASE t.{Toetsenbord.Database.Columns.NumeriekKlavier} ");
                    command.Append($"WHEN 'J' THEN 'Ja' ");
                    command.Append($"ELSE 'Nee' ");
                    command.Append($"END , '|' , ");
                    command.Append($"'Verbinding: ' , t.{Toetsenbord.Database.Columns.Verbinding}) \"pluspunten\" ");
                    command.Append($"FROM {Productdetails.Database.TableName} p ");
                    command.Append($"JOIN {Toetsenbord.Database.TableName} t ");
                    command.Append($"ON p.{Productdetails.Database.Columns.Productnummer} = t.{Toetsenbord.Database.Columns.ProductNummer} ");
                    command.Append($"WHERE p.{Productdetails.Database.Columns.Productnummer} = {id}");
                    result = ExecuteQuery(command.ToString());
                    datalist.Add(result.DataTable); 
                }
                else if (result.DataTable.Rows[0][Productdetails.Database.Columns.Categorie].ToString() == "headset")
                {
                    command.Clear();
                    command.Append($"SELECT p.{Productdetails.Database.Columns.Productnummer}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Type}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Prijs}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Korting}, ");
                    command.Append($"p.{Productdetails.Database.Columns.Images}, ");
                    command.Append($"h.{Headset.Database.Columns.Diameter_Drivers} + ' drivers' + '|' + ");
                    command.Append($"CASE h.{Headset.Database.Columns.InklapbareMic} ");
                    command.Append($"WHEN 'J' THEN 'Inklapbare microfoon' ");
                    command.Append($"ELSE 'Vaste microfoon' ");
                    command.Append($"END + '|' + ");
                    command.Append($"'Verbinding: ' + h.{Headset.Database.Columns.Verbinding} \"pluspunten\" ");
                    command.Append($"FROM {Productdetails.Database.TableName} p ");
                    command.Append($"JOIN {Headset.Database.TableName} h ");
                    command.Append($"ON p.{Productdetails.Database.Columns.Productnummer} = h.{Headset.Database.Columns.ProductNummer} ");
                    command.Append($"WHERE p.{Productdetails.Database.Columns.Productnummer} = {id}");
                    result = ExecuteQuery(command.ToString());
                    datalist.Add(result.DataTable);
                }
                
            }
            
           
            return datalist;
        }

            


    }
}
