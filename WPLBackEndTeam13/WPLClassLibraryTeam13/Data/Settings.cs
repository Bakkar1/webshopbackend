using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPLClassLibraryTeam13.Data
{
    static class Settings
    {
        public static class Database
        {
            public static string DatabaseName = "pxlteam13"; //team number

            //public static string Server = @"PF2CMF4Y\SQLEXPRESS"; //local server name   (PF2B1JTS\PXLDIGITAL) ((PF2CMF4Y\SQLEXPRESS)Dries) DESKTOP-5NQFRG2\SQLEXPRESS
            //public static string User = "sa";           //local
            //public static string Pwd = "pxl";           //local

            public static string Server = @"10.128.4.7";
            public static string User = "pxlteam13";  //Server
            public static string Pwd = "pxlteam13";   //Server



        }
    }
}
