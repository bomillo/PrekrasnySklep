using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Util
{
    internal class InMemoryDbConecttion
    {
        private readonly SqliteConnection dbConnection;

        public SqliteConnection DbConnection => dbConnection;

        public InMemoryDbConecttion() { 
            dbConnection = new SqliteConnection($"Data Source=:memory:;");
            dbConnection.Open();
        }

        ~InMemoryDbConecttion()
        {
            dbConnection.Close();
        }
    }
}
