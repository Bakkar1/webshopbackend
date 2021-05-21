using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;
using WPLClassLibraryTeam13.Entity;

namespace WPLClassLibraryTeam13.Query
{
    public class QueryAdmin : QueryHelper
    {
        // Zoek username in database
        public SQLCommandResult LogIn(string username)
        {
            SQLCommandResult result = new SQLCommandResult();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM {Admin.Database.TableName} ");
            sb.Append($"WHERE {Admin.Database.Columns.AdminUsername} = '{username}'");
            result = ExecuteQuery(sb.ToString());
            return result;
        }



        //zoek data van alle admins voor in datagrid
        public SQLCommandResult GetAdmindata()
        {
            SQLCommandResult result = new SQLCommandResult();
            StringBuilder sb = new StringBuilder();
            sb.Append($"SELECT * FROM {Admin.Database.TableName} ");
            result = ExecuteQuery(sb.ToString());
            return result;
        }
        public SQLCommandResult SetAdminAfbeeldingenFolder(int id, string pad)
        {
            SQLCommandResult result = new SQLCommandResult();
            StringBuilder sb = new StringBuilder();
            sb.Append($"UPDATE {Admin.Database.TableName} SET {Admin.Database.Columns.Afbeeldingen_wpf} = '{pad}' " +
                $"WHERE {Admin.Database.Columns.AdminId} = '{id}'");
            result = ExecuteQuery(sb.ToString());
            return result;
        }
        public void LogIn(object content)
        {
            throw new NotImplementedException();
        }


    }
}
