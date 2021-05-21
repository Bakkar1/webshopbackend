using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WPLClassLibraryTeam13.Data
{
    internal class Database
    {
        private SqlConnection connection;
        public Database()
        {
            SqlConnectionStringBuilder sqlSB = new SqlConnectionStringBuilder();
            sqlSB.InitialCatalog = Settings.Database.DatabaseName;
            sqlSB.DataSource = Settings.Database.Server;
            sqlSB.UserID = Settings.Database.User;
            sqlSB.Password = Settings.Database.Pwd;
            connection = new SqlConnection(sqlSB.ConnectionString);
            //string cs = $"data source = {Settings.Database.Server}; database = {Settings.Database.DatabaseName}; User id = {Settings.Database.User}; Password ={Settings.Database.Pwd}";
            //connection = new SqlConnection(cs);
        }
        public DataTable GetData(string query)
        {
            try
            {
                DataTable dt = new DataTable();
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(dt);
                connection.Close();
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public void ExecuteNonquery(SqlCommand command)
        {
            try
            {
                connection.Open();
                command.Connection = connection;
                int recordsAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
            finally
            {
                connection.Close();
            }
        }
        public SQLCommandResult ExecuteCommand(SqlCommand sqlCommand, EntityCommand entityCommand, Dictionary<string, object> sqlParameters)
        {
            SQLCommandResult result = new SQLCommandResult();
            try
            {
                result.EntityCommand = entityCommand;
                connection.Open();
                sqlCommand.Connection = connection;
                foreach (KeyValuePair<string, object> sqlParam in sqlParameters)
                {
                    sqlCommand.Parameters.AddWithValue($"@{sqlParam.Key}", sqlParam.Value);
                    //@voornaam = dict[voornaam]
                    //insert into tbluser (klantnummern, voornaam) values (1, 'test') delete * from tbluser;'
                }
                if (entityCommand == EntityCommand.Read)
                {
                    SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                    sda.Fill(result.DataTable);
                    result.Count = result.DataTable.Rows.Count;
                }
                else
                {
                    //result.Count = sqlCommand.ExecuteNonQuery();
                    //if (entityCommand == EntityCommand.Insert)
                    //{
                    //    result.NewId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    //}
                    if (entityCommand == EntityCommand.Insert) 
                    { 
                        result.NewId = Convert.ToInt32(sqlCommand.ExecuteScalar()); 
                    } 
                    else 
                    { 
                        result.Count = sqlCommand.ExecuteNonQuery();
                        //result.Query = sqlCommand.CommandText;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}
