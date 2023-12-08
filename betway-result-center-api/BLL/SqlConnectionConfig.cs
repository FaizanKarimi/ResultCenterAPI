using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace betway_result_center_api.BLL
{
    public class SqlConnectionConfig
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["SportsDataDB"].ConnectionString;

        public static string getConnectionString()
        {
            return _connectionString;
        }
    }
}