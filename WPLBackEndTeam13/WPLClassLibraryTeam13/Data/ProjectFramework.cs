using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPLClassLibraryTeam13.Data
{
    public class ProjectFramework : EntityHelper, IEntityHelper
    {
        public ProjectFramework(string tableName, string pkey) : base(tableName, pkey)
        {

        }
        public SQLCommandResult Insert()
        {
            return InsertRecord();
        }
        public SQLCommandResult Update()
        {
            return UpdateRecord();
        }
        public SQLCommandResult Delete(int id)
        {
            return DeleteRecord(id);
        }
        public SQLCommandResult Read()
        {
            return GetRecords();
        }
        public SQLCommandResult Read(int id)
        {
            return GetRecords(id);
        }
    }
}
