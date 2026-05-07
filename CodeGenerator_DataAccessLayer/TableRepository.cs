
using System.Collections.Generic;
using System.Data.SqlClient;


namespace CodeGenerator_DataAccessLayer
{
    public class TableRepository
    {
        public static List<string> GetAllTableNames(string ConnectionString)
        {
            List<string> ListOfTableNames = null;

            if (!string.IsNullOrWhiteSpace(ConnectionString))
            {
                SqlConnection Connection = null;
                SqlCommand Command = null;
                SqlDataReader Reader = null;

                try
                {
                    Connection = new SqlConnection(ConnectionString);

                    string Query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME;";

                    Command = new SqlCommand(Query, Connection);

                    Connection.Open();

                    Reader = Command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        ListOfTableNames = new List<string>();

                        while (Reader.Read())
                        {
                            ListOfTableNames.Add(Reader["TABLE_NAME"] as string ?? "");
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

            return ListOfTableNames;
        }




    }
}
