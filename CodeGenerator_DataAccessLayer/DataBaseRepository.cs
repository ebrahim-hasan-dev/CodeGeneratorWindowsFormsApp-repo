
using DLMApp_ModulesLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace CodeGenerator_DataAccessLayer
{
    public class DataBaseRepository
    {
        public static async Task<List<string>> GetAllDatabasesAsync(string ConnectionString)
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

                    await Connection.OpenAsync();

                    Reader = await Command.ExecuteReaderAsync();

                    if (Reader.HasRows)
                    {
                        ListOfDataBaseNames = new List<string>();

                        while (await Reader.ReadAsync())
                        {
                            ListOfDataBaseNames.Add(Reader["name"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
                    ListOfDataBaseNames = null;
                    throw;
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
