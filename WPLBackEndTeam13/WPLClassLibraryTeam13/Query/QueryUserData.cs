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
    public class QueryUserData : QueryHelper
    {
        //public DataTable GetUserData(string email, string pwd)
        //{

        //    var dt = EntityData.Userdata_1.Get(email, pwd);
        //    return dt;
        //}
        public SQLCommandResult GetData()
        {
            SQLCommandResult result = new SQLCommandResult();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"SELECT * FROM {UserData.Database.TableName} ");
                result = ExecuteQuery(sb.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("GetRecord", ex);

            }
            return result;
        }
        public SQLCommandResult GetData(string email, string pwd)
        {
            SQLCommandResult result = new SQLCommandResult();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"SELECT * FROM {UserData.Database.TableName} ");
                sb.Append($"WHERE {UserData.Database.Columns.emailAdres} = '{email}'");
                sb.Append($" and {UserData.Database.Columns.wachtwoord} = '{pwd}'");
                result = ExecuteQuery(sb.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("GetRecord", ex);

            }
            return result;
        }
        public DataTable getID(int id)
        {
            SQLCommandResult result = new SQLCommandResult();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"SELECT * FROM {UserData.Database.TableName} ");
                sb.Append($"WHERE {UserData.Database.PrimaryKey} = '{id}'");
                result = ExecuteQuery(sb.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("GetRecord", ex);

            }
            return result.DataTable;
        }
    }
}
