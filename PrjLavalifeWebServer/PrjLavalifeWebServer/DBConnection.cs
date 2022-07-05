using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace PrjLavalifeWebServer
{
    public class DBConnection
    {
        public static OleDbConnection GetConnection()
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/Friendbook.mdb");

            OleDbConnection mycon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path);

            return mycon;
        }

    }
}