
using System.Collections.Generic;
using System.Data.SqlClient;


namespace CodeGenerator_DataAccessLayer
{
    public class DataBaseRepository
    {
        public static List<string> GetAllDatabases(string ConnectionString)
        {
            List<string> ListOfDataBaseNames = null;

            if (!string.IsNullOrWhiteSpace(ConnectionString))
            {
                SqlConnection Connection = null;
                SqlCommand Command = null;
                SqlDataReader Reader = null;

                try
                {
                    Connection = new SqlConnection(ConnectionString);

                    string Query = "SELECT name FROM sys.databases WHERE database_id > 4";

                    Command = new SqlCommand(Query, Connection);

                    Connection.Open();

                    Reader = Command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        ListOfDataBaseNames = new List<string>();

                        while (Reader.Read())
                        {
                            ListOfDataBaseNames.Add(Reader["name"].ToString());
                        }
                    }
                }
                finally
                {
                    if (Reader != null)
                    {
                        Reader.Close();
                        Reader.Dispose();
                    }

                    if (Command != null)
                    {
                        Command.Dispose();
                    }

                    if (Connection != null)
                    {
                        Connection.Close();
                        Connection.Dispose();
                    }
                }
            }

            return ListOfDataBaseNames;
        }





    }
}
