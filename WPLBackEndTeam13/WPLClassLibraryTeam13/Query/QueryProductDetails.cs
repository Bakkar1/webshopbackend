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
    public class QueryProductDetails : QueryHelper
    {
        public SQLCommandResult GetData()
        {
            SQLCommandResult result = new SQLCommandResult();
            try
            {
                string command = $"SELECT * FROM {Productdetails.Database.TableName}";
                result = ExecuteQuery(command);
            }
            catch (Exception ex)
            {
                throw new Exception("GetRecord", ex);

            }
            return result;
        }
        public SQLCommandResult GetData(string type)
        {
            SQLCommandResult result = new SQLCommandResult();
            try
            {
                string command = $"SELECT * FROM {Productdetails.Database.TableName} WHERE {Productdetails.Database.Columns.Type} = '{type}'";
                result = ExecuteQuery(command);
            }
            catch (Exception ex)
            {
                throw new Exception("GetRecord", ex);

            }
            return result;
        }


        public DataTable GetImages(int id)
        {
            //Selecteer de images van productdetails van de meegegeven id
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT {Productdetails.Database.Columns.Images} FROM {Productdetails.Database.TableName} WHERE {Productdetails.Database.Columns.Productnummer} = '{id}'");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        public DataTable GetDataSearch(string productType)
        {
            // om de sql injection attack te vermijden moeten we de parameterized query gebruiken
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT { Productdetails.Database.Columns.Type},{ Productdetails.Database.Columns.Productnummer},{ Productdetails.Database.Columns.Categorie}   FROM {Productdetails.Database.TableName} ");
            query.Append($"where {Productdetails.Database.Columns.Type} like @SearchValue");
            var result = ExecuteQuerySearch(query.ToString(), productType);
            return result.DataTable;
        }
        public DataTable getJoinData()
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM {Productdetails.Database.TableName} ");
            query.Append($"INNER JOIN { Laptop.Database.TableName} ");
            query.Append($"ON { Productdetails.Database.TableName}.{ Productdetails.Database.Columns.Productnummer} ");
            query.Append($"= { Laptop.Database.TableName}.{ Laptop.Database.Columns.Productnummer} ");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        public DataTable getJoinData(int id)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT * FROM {Productdetails.Database.TableName} ");
            query.Append($"INNER JOIN { Laptop.Database.TableName} ");
            query.Append($"ON { Productdetails.Database.TableName}.{ Productdetails.Database.Columns.Productnummer} ");
            query.Append($"= { Laptop.Database.TableName}.{ Laptop.Database.Columns.Productnummer} ");
            query.Append($"and { Productdetails.Database.TableName}.{ Productdetails.Database.Columns.Productnummer} = {id} ");
            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        public DataTable ViewProductDetails()
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"SELECT {Productdetails.Database.TableName}.{Productdetails.Database.Columns.Productnummer} AS Productnummer, " +
                $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Type} AS Type, " +

                //prijzen
                $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} AS 'Basisprijs', " +                          //basisprijs opgeslagen op de server
                $"ROUND({Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} * 0.79 ,2) AS 'Basisprijs ex BTW' , " + //basisprijs exclusief btw
                $"ROUND(({Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} * 0.21),2) AS BTW , " +                //BTW
                $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Korting} AS Korting, " +                             //Korting
                $"ROUND(({Productdetails.Database.TableName}.{Productdetails.Database.Columns.Prijs} - Korting), 2) AS 'Totaalprijs', " +   //Totaalprijs

                $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Kleur} AS Kleur, " +
                $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Voorraad} AS Voorraad, " +
                $"{Productdetails.Database.TableName}.{Productdetails.Database.Columns.Categorie} AS Categorie " +
                $"FROM {Productdetails.Database.TableName}");

            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }
        //        alter table tblheadsetdetails
        //nocheck constraint hd_pn_fk;

        //        delete from productdetails
        //        where productnummer = 1
        //delete from tblheadsetdetails
        //where productnummer = 1

        //alter table tblheadsetdetails
        //check constraint hd_pn_fk;
        public SQLCommandResult DeleteRecord(int id, string constraint, string tabelnaam)
        {
            var result = new SQLCommandResult();
            try
            {
                var deleteCommando = $"ALTER TABLE {tabelnaam} " +
                    $"NOCHECK CONSTRAINT {constraint} " +
                    $"DELETE FROM {Productdetails.Database.TableName} " +
                    $"WHERE {Productdetails.Database.Columns.Productnummer} = {id} " +
                    $"DELETE FROM {tabelnaam} " +
                    $"WHERE {tabelnaam}.Productnummer = {id}" +
                    $"ALTER TABLE {tabelnaam} " +
                    $"CHECK CONSTRAINT {constraint} ;";
                result = ExecuteQuery(deleteCommando);

            }
            catch (Exception ex)
            {
                throw new Exception("DeleteRecord", ex);

            }
            return result;
        }

        public SQLCommandResult DeleteAllRecords()
        {
            var result = new SQLCommandResult();
            try
            {
                var deleteCommando = $"ALTER TABLE {Laptop.Database.TableName} " +
                    $"NOCHECK CONSTRAINT ld_pn_fk " +
                    $"ALTER TABLE {Headset.Database.TableName} " +
                    $"NOCHECK CONSTRAINT hd_pn_fk " +
                    $"ALTER TABLE {Muis.Database.TableName} " +
                    $"NOCHECK CONSTRAINT md_pn_fk " +
                    $"ALTER TABLE {Toetsenbord.Database.TableName} " +
                    $"NOCHECK CONSTRAINT td_pn_fk " +

                    $"DELETE FROM {Productdetails.Database.TableName} " +
                    $"DELETE FROM {Laptop.Database.TableName} " +
                    $"DELETE FROM {Headset.Database.TableName} " +
                    $"DELETE FROM {Muis.Database.TableName} " +
                    $"DELETE FROM {Toetsenbord.Database.TableName} " +

                    $"ALTER TABLE {Laptop.Database.TableName} " +
                    $"CHECK CONSTRAINT ld_pn_fk " +
                    $"ALTER TABLE {Headset.Database.TableName} " +
                    $"CHECK CONSTRAINT hd_pn_fk " +
                    $"ALTER TABLE {Muis.Database.TableName} " +
                    $"CHECK CONSTRAINT md_pn_fk " +
                    $"ALTER TABLE {Toetsenbord.Database.TableName} " +
                    $"CHECK CONSTRAINT td_pn_fk ;";

                result = ExecuteQuery(deleteCommando);
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteRecord", ex);

            }
            return result;
        }
        public DataTable SelectAllRecords()
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append($"select " +
                $"p.{Productdetails.Database.Columns.Productnummer} as Productnummer, " +
                $"p.{Productdetails.Database.Columns.Type} as Producttype, " +
                $"p.{Productdetails.Database.Columns.Prijs} as Basisprijs, " +
                $"p.{Productdetails.Database.Columns.Korting} as Korting, " +
                $"p.{Productdetails.Database.Columns.Beschrijving} as 'Korte beschrijving', " +
                $"p.{Productdetails.Database.Columns.Uitgebreide_beschrijving} as 'Uitgebreide beschrijving', " +
                $"p.{Productdetails.Database.Columns.Kleur} as Kleur, " +
                $"p.{Productdetails.Database.Columns.Voorraad} as Voorraad, " +
                $"p.{Productdetails.Database.Columns.Images} as Afbeeldingen, " +
                $"h.{Headset.Database.Columns.HeadsetID} as HeadsetID, " +
                $"h.{Headset.Database.Columns.Diameter_Drivers} as 'Diameter drivers', " +
                $"h.{Headset.Database.Columns.InklapbareMic} as 'Inklapbare micro', " +
                $"h.{Headset.Database.Columns.Verbinding} as 'Verbinding headset', " +
                $"h.{Headset.Database.Columns.Verlichting} as Verlichting, " +
                $"l.{Laptop.Database.Columns.LaptopID} as LaptopID, " +
                $"l.{Laptop.Database.Columns.Os} as Besturingssysteem, " +
                $"l.{Laptop.Database.Columns.Processor} as Processor, " +
                $"l.{Laptop.Database.Columns.Memory} as Geheugen, " +
                $"l.{Laptop.Database.Columns.Graphics} as Videokaart, " +
                $"l.{Laptop.Database.Columns.Storage} as Opslagruimte, " +
                $"l.{Laptop.Database.Columns.Display} as Scherm, " +
                $"l.{Laptop.Database.Columns.Keyboard} as Toetsenbord, " +
                $"l.{Laptop.Database.Columns.Connectivity} as Connectiviteit, " +
                $"l.{Laptop.Database.Columns.Battery_and_adaptor} as 'Accu en adapter', " +
                $"l.{Laptop.Database.Columns.Touchpad} as Touchpad, " +
                $"l.{Laptop.Database.Columns.Input_and_output} as Poorten, " +
                $"l.{Laptop.Database.Columns.Audio} as Audio, " +
                $"l.{Laptop.Database.Columns.Additional_features} as 'Bijkomende kenmerken', " +
                $"l.{Laptop.Database.Columns.Finish} as Finish, " +
                $"l.{Laptop.Database.Columns.Dimensions} as Afmetingen, " +
                $"l.{Laptop.Database.Columns.Weight} as Gewicht, " +
                $"m.{Muis.Database.Columns.MuisID} as MuidID, " +
                $"m.{Muis.Database.Columns.DPI} as DPI, " +
                $"m.{Muis.Database.Columns.Instelbaarh_DPI} as 'Instelbaarheid DPI', " +
                $"m.{Muis.Database.Columns.AantalKnoppen} as 'Aantal knoppen', " +
                $"m.{Muis.Database.Columns.Verbinding} as 'Verbinding muis', " +
                $"m.{Muis.Database.Columns.Verlichting} as Verlichting, " +
                $"t.{Toetsenbord.Database.Columns.ToetsenbordID} as ToetsenbordID, " +
                $"t.{Toetsenbord.Database.Columns.ModelAzerty} as 'Model (Azerty/Qwerty)', " +
                $"t.{Toetsenbord.Database.Columns.NumeriekKlavier} as 'Numeriek klavier', " +
                $"t.{Toetsenbord.Database.Columns.Verbinding} as 'Verbinding toetsenbord',  " +
                $"t.{Toetsenbord.Database.Columns.Verlichting} as Verlichting, " +
                $"p.{Productdetails.Database.Columns.Categorie} as Categorie " +

                $" from {Productdetails.Database.TableName} p full outer join {Headset.Database.TableName} h on p.{Productdetails.Database.Columns.Productnummer} = h.{Headset.Database.Columns.ProductNummer} " +
            $"full outer join {Laptop.Database.TableName} l on p.{Productdetails.Database.Columns.Productnummer} = l.{Laptop.Database.Columns.Productnummer} " +
            $"full outer join {Muis.Database.TableName} m on p.{Productdetails.Database.Columns.Productnummer} = m.{Muis.Database.Columns.ProductNummer} " +
            $"full outer join {Toetsenbord.Database.TableName} t on p.{Productdetails.Database.Columns.Productnummer} = t.{Toetsenbord.Database.Columns.ProductNummer}");


            var result = ExecuteQuery(query.ToString());
            return result.DataTable;
        }


    }
}
