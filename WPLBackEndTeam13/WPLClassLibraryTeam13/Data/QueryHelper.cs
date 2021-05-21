using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPLClassLibraryTeam13.Data
{
    public class QueryHelper
    {
        private Database database { get; set; }
        private Dictionary<string, object> SqlParameters;
        public QueryHelper()
        {
            database = new Database();
            SqlParameters = new Dictionary<string, object>();
        }
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
        public SQLCommandResult ExecuteQuery(string sqlCommand)
        {
            var result = new SQLCommandResult();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlCommand;
                result = database.ExecuteCommand(command, EntityCommand.Read, SqlParameters);
            }
            catch(Exception ex)
            {
                throw new Exception("GetRecords", ex);
            }
            return result;
        }
        public SQLCommandResult ExecuteQuerySearch(string sqlCommand,string searchVeld)
        {
            var result = new SQLCommandResult();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlCommand;
                command.Parameters.AddWithValue("@SearchValue", searchVeld + "%");
                result = database.ExecuteCommand(command, EntityCommand.Read, SqlParameters);
            }
            catch (Exception ex)
            {
                throw new Exception("GetRecords", ex);
            }
            return result;
        }
    }
}
