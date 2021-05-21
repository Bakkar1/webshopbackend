using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPLClassLibraryTeam13.Data
{
    public interface IEntityHelper
    {
        SQLCommandResult Insert();
        SQLCommandResult Update();
        SQLCommandResult Delete(int id);
        SQLCommandResult Read();
    }
}
