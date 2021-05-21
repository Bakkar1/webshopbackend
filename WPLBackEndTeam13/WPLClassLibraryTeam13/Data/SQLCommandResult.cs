using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPLClassLibraryTeam13.Data
{
    public class SQLCommandResult
    {
        public SQLCommandResult()
        {
            Count = 0;
            NewId = -1;
            DataTable = new DataTable();
        }
        public int NewId { get; set; }
        public int Count { get; set; }
        public EntityCommand EntityCommand { get; set; }
        public DataTable DataTable { get; set; }
        public string Query { get; set; }
    }
}
