using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPLClassLibraryTeam13.Data
{
    public class EntityHelper
    {
        public EntityHelper(string tableName, string pkey)
        {
            TableName = tableName;
            PrimaryKey = pkey;
            Database = new Database();
            SqlParameters = new Dictionary<string, object>();
            if (tableName == null || tableName.Length == 0)
            {
                throw new Exception("TableName is missing -- Create static string in your Entity class and pass by your constructor!");
            }
            if (PrimaryKey == null || PrimaryKey.Length == 0)
            {
                throw new Exception("PrimaryKey is missing -- Create static string in your Entity class and pass by your constructor!");
            }
        }

        private Database Database { get; set; }
        private Dictionary<string, object> SqlParameters;
        protected string TableName { get; set; }
        protected string PrimaryKey { get; set; }

        public void UpdateSqlParameters(string key, object parameter)
        {
            if (SqlParameters.ContainsKey(key))
            {
                SqlParameters[key] = parameter;
            }
            else
            {
                SqlParameters.Add(key, parameter);
            }
        } 
        
        #region insert
        private string InsertFields { get; set; }
        private string InsertParams { get; set; }
        private void InsertHelper()
        {
            InsertFields = "";
            InsertParams = "";
            foreach(KeyValuePair<string, object> kvp in SqlParameters)
            {
                if (!(kvp.Key == PrimaryKey))
                {
                    if (InsertFields.Length > 0)
                    {
                        InsertFields += ",";
                        InsertParams += ",";
                    }
                    InsertFields += kvp.Key;
                    InsertParams += $"@{kvp.Key}";
                    //Insert into tbluser (klantnummer, voornaam) values (@klantnummer, @voornaam)
                }
            }
        }
      
        protected SQLCommandResult InsertRecord()
        {
            var result = new SQLCommandResult();
            try
            {
                InsertHelper();
                //var email = SqlParameters["Emailadres"]; 
                var insertCommando = $"INSERT INTO {TableName} ({InsertFields}) VALUES ({InsertParams});SELECT scope_identity();";
                SqlCommand command = new SqlCommand();
                command.CommandText = insertCommando;
                result = Database.ExecuteCommand(command, EntityCommand.Insert, SqlParameters);
            }
            catch (Exception ex)
            {
                var sqlException = ex.InnerException as System.Data.SqlClient.SqlException;

                //als de record al bestaat op de database
                if (sqlException.Number == 2601 || sqlException.Number == 2627)
                {
                    //result = null;
                    result.Count = -1;
                }
                else
                {
                    throw new Exception("InsertRecord", ex);
                }
            }
            return result;
        }
        #endregion
        #region Update
        private void UpdateHelper()
        {
            UpdateParams = "";
            foreach (KeyValuePair<string, object> kvp in SqlParameters)
            {
                if (!(kvp.Key == PrimaryKey))
                {
                    if (UpdateParams.Length > 0)
                    {
                        UpdateParams += ",";
                    }
                    UpdateParams += $"{kvp.Key}=@{kvp.Key}";
                }
            }
        }
        private string UpdateParams { get; set; }
        protected SQLCommandResult UpdateRecord()
        {
            var result = new SQLCommandResult();
            try
            {
                UpdateHelper();
                var updateCommando = $"UPDATE {TableName} SET {UpdateParams} WHERE {PrimaryKey} = {SqlParameters[PrimaryKey]}";
                SqlCommand command = new SqlCommand();
                command.CommandText = updateCommando;
                result = Database.ExecuteCommand(command, EntityCommand.Update, SqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateRecord", ex);
            }
            return result;
        }
        #endregion
        #region read

        //===============================================
        // ALS ONDERSTAANDE METHODE NOG GEBRUIKT WORDT MOET DEZE IN EEN QUERY KLASSE KOMEN
        //===============================================

        //protected SQLCommandResult GetRecords()
        //{
        //    var result = new SQLCommandResult();
        //    try
        //    {
        //        var selectCommando = $"SELECT * FROM {TableName} WHERE Emailadres = {SqlParameters["Emailadres"]}";
        //        SqlCommand command = new SqlCommand();
        //        command.CommandText = selectCommando;
        //        result = Database.ExecuteCommand(command, EntityCommand.Read, SqlParameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetRecord", ex);

        //    }
        //    return result;
        //}

        protected SQLCommandResult GetRecords()
        {
            var result = new SQLCommandResult();
            try
            {
                var selectCommando = $"SELECT * FROM {TableName}";
                SqlCommand command = new SqlCommand();
                command.CommandText = selectCommando;
                result = Database.ExecuteCommand(command, EntityCommand.Read, SqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("GetRecord", ex);

            }
            return result;
        }

        protected SQLCommandResult GetRecords(int id)
        {
            var result = new SQLCommandResult();
            try
            {
                var selectCommando = $"SELECT * FROM {TableName} WHERE {PrimaryKey} = {id}";
                SqlCommand command = new SqlCommand();
                command.CommandText = selectCommando;
                result = Database.ExecuteCommand(command, EntityCommand.Read, SqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("GetRecord", ex);

            }
            return result;
        }
        //protected DataTable GetEamil(string email)
        //{
        //    var result = new DataTable();
        //    try
        //    {
                
        //        var selectCommando = $"SELECT * FROM {TableName} WHERE Emailadres = \"{email}\"";
        //        result = Database.GetData(selectCommando);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetRecord", ex);

        //    }
        //    return result;
        //}
        #endregion
        protected SQLCommandResult DeleteRecord(int id)
        {
            var result = new SQLCommandResult();
            try
            {
                var deleteCommando = $"DELETE from {TableName} WHERE {PrimaryKey} = {id}";
                SqlCommand command = new SqlCommand();
                command.CommandText = deleteCommando;
                Database.ExecuteNonquery(command);
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteRecord", ex);

            }
            return result;
        }

        protected SQLCommandResult DeleteRecord()
        {
            var result = new SQLCommandResult();
            try
            {
                var deleteCommando = $"DELETE * from {TableName}";
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteRecord", ex);

            }
            return result;
        }
    }

    public enum EntityCommand
    {
        Insert = 0,
        Update = 1,
        Delete = 2,
        Read = 3
    }
}
