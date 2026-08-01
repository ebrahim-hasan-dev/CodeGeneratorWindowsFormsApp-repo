
using DLMApp_ModulesLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace CodeGenerator_DataAccessLayer
{
    public class TableRepository
    {
        public static async Task<List<string>> GetAllTableNamesAsync(string ConnectionString)
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

                    await Connection.OpenAsync();

                    Reader = await Command.ExecuteReaderAsync();

                    if (Reader.HasRows)
                    {
                        ListOfTableNames = new List<string>();

                        while (await Reader.ReadAsync())
                        {
                            ListOfTableNames.Add(Reader["TABLE_NAME"] as string ?? "");
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
                    ListOfTableNames = null;
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

            return ListOfTableNames;
        }




    }
}
