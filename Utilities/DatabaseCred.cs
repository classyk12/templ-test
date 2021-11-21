using System;
namespace tmpltest.Utilities
{
    public class DatabaseCred
    {
        public const string USERNAME = "sa";
        public const string PASSWORD = "Oluwakayode@12345";
        public const string DBNAME = "tmpldb";
        public const string SERVER = "localhost";
        public const string BASEURL = "https://yomomma-api.herokuapp.com/";

        public const string ConnectionString = "Data Source =" + SERVER + ";Initial Catalog =" + "DBNAME; User Id =" + USERNAME + " ; Password=" + PASSWORD;


    }
}

