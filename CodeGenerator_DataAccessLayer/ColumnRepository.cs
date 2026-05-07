using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using CodeGenerator_Modules;


namespace CodeGenerator_DataAccessLayer
{
    public class ColumnRepository
    {
        public static List<clsColumnDataModulesLayer> GetAllColumns(string TableName, string ConnectionString)
        {
            List<clsColumnDataModulesLayer> ListOfColumns = null;

            if (!string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(ConnectionString))
            {
                SqlConnection Connection = null;
                SqlCommand Command = null;
                SqlDataReader Reader = null;

                try
                {
                    Connection = new SqlConnection(ConnectionString);

                    string Query = @"SELECT c.COLUMN_NAME, c.DATA_TYPE, c.IS_NULLABLE, 
                                CASE WHEN k.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IsPrimaryKey 
                                FROM INFORMATION_SCHEMA.COLUMNS c LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE k 
                                ON c.TABLE_NAME = k.TABLE_NAME AND c.COLUMN_NAME = k.COLUMN_NAME AND
                                OBJECTPROPERTY(OBJECT_ID(k.CONSTRAINT_SCHEMA + '.' + k.CONSTRAINT_NAME), 'IsPrimaryKey') = 1 
                                WHERE c.TABLE_NAME = @TableName;";

                    Command = new SqlCommand(Query, Connection);

                    Command.Parameters.AddWithValue("@TableName", TableName);

                    Connection.Open();

                    Reader = Command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        ListOfColumns = new List<clsColumnDataModulesLayer>();

                        while (Reader.Read())
                        {
                            ListOfColumns.Add(new clsColumnDataModulesLayer
                            {
                                Name = Reader["COLUMN_NAME"] as string ?? "",
                                Type = Reader["DATA_TYPE"] as string ?? "",
                                IsPrimaryKey = Reader["IsPrimaryKey"].ToString() == "1",
                                IsNullable = Reader["IS_NULLABLE"].ToString() == "YES"
                            }
                            );
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

            return ListOfColumns;
        }

        public static List<clsColumnDataDataAccessLayer> GetAllColumnsDataAccessLayer(string TableName, string ConnectionString)
        {
            List<clsColumnDataDataAccessLayer> ListOfColumns = null;

            if (!string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(ConnectionString))
            {
                SqlConnection Connection = null;
                SqlCommand Command = null;
                SqlDataReader Reader = null;

                try
                {
                    Connection = new SqlConnection(ConnectionString);

                    string Query = @"SELECT c.COLUMN_NAME, c.DATA_TYPE, c.IS_NULLABLE, 
                                CASE WHEN k.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IsPrimaryKey 
                                FROM INFORMATION_SCHEMA.COLUMNS c LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE k 
                                ON c.TABLE_NAME = k.TABLE_NAME AND c.COLUMN_NAME = k.COLUMN_NAME AND
                                OBJECTPROPERTY(OBJECT_ID(k.CONSTRAINT_SCHEMA + '.' + k.CONSTRAINT_NAME), 'IsPrimaryKey') = 1 
                                WHERE c.TABLE_NAME = @TableName;";

                    Command = new SqlCommand(Query, Connection);

                    Command.Parameters.AddWithValue("@TableName", TableName);

                    Connection.Open();

                    Reader = Command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        ListOfColumns = new List<clsColumnDataDataAccessLayer>();

                        while (Reader.Read())
                        {
                            ListOfColumns.Add(new clsColumnDataDataAccessLayer
                            {
                                Name = Reader["COLUMN_NAME"] as string ?? "",
                                IsNullable = Reader["IS_NULLABLE"].ToString() == "YES",
                                IsPrimaryKey = Reader["IsPrimaryKey"].ToString() == "1",
                                Type = Reader["DATA_TYPE"] as string ?? ""
                            }
                            );
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

            return ListOfColumns;
        }

        public static List<clsColumnDataBusinessLayer> GetAllColumnsBusinessLayer(string TableName, string ConnectionString)
        {
            List<clsColumnDataBusinessLayer> ListOfColumns = null;

            if (!string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(ConnectionString))
            {
                SqlConnection Connection = null;
                SqlCommand Command = null;
                SqlDataReader Reader = null;

                try
                {
                    Connection = new SqlConnection(ConnectionString);

                    string Query = @"SELECT c.COLUMN_NAME, c.DATA_TYPE, 
                                CASE WHEN k.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IsPrimaryKey 
                                FROM INFORMATION_SCHEMA.COLUMNS c LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE k 
                                ON c.TABLE_NAME = k.TABLE_NAME AND c.COLUMN_NAME = k.COLUMN_NAME AND
                                OBJECTPROPERTY(OBJECT_ID(k.CONSTRAINT_SCHEMA + '.' + k.CONSTRAINT_NAME), 'IsPrimaryKey') = 1 
                                WHERE c.TABLE_NAME = @TableName;";

                    Command = new SqlCommand(Query, Connection);

                    Command.Parameters.AddWithValue("@TableName", TableName);

                    Connection.Open();

                    Reader = Command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        ListOfColumns = new List<clsColumnDataBusinessLayer>();

                        while (Reader.Read())
                        {
                            ListOfColumns.Add(new clsColumnDataBusinessLayer
                            {
                                Name = Reader["COLUMN_NAME"] as string ?? "",
                                Type = Reader["DATA_TYPE"] as string ?? "",
                                IsPrimaryKey = Reader["IsPrimaryKey"].ToString() == "1"
                            }
                            );
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

            return ListOfColumns;
        }

        public static List<clsColumnDataUILayer> GetAllColumnsUILayer(string TableName, string ConnectionString)
        {
            List<clsColumnDataUILayer> ListOfColumns = null;

            if (!string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(ConnectionString))
            {
                SqlConnection Connection = null;
                SqlCommand Command = null;
                SqlDataReader Reader = null;

                try
                {
                    Connection = new SqlConnection(ConnectionString);

                    string Query = @"SELECT c.COLUMN_NAME, c.DATA_TYPE, c.IS_NULLABLE, 
                                CASE WHEN Pk.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IsPrimaryKey,
                                CASE WHEN FK.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IsForeignKey
                                FROM INFORMATION_SCHEMA.COLUMNS c LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE Pk 
                                ON c.TABLE_NAME = Pk.TABLE_NAME AND c.COLUMN_NAME = Pk.COLUMN_NAME AND
                                OBJECTPROPERTY(OBJECT_ID(Pk.CONSTRAINT_SCHEMA + '.' + Pk.CONSTRAINT_NAME), 'IsPrimaryKey') = 1
                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE FK
                                ON c.TABLE_NAME = FK.TABLE_NAME AND c.COLUMN_NAME = FK.COLUMN_NAME AND
                                OBJECTPROPERTY(OBJECT_ID(FK.CONSTRAINT_SCHEMA + '.' + FK.CONSTRAINT_NAME), 'IsForeignKey') = 1 
                                WHERE c.TABLE_NAME = @TableName;";


                    Command = new SqlCommand(Query, Connection);

                    Command.Parameters.AddWithValue("@TableName", TableName);

                    Connection.Open();

                    Reader = Command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        ListOfColumns = new List<clsColumnDataUILayer>();

                        while (Reader.Read())
                        {
                            ListOfColumns.Add(new clsColumnDataUILayer
                            {
                                Name = Reader["COLUMN_NAME"] as string ?? "",
                                IsNullable = Reader["IS_NULLABLE"].ToString() == "YES",
                                IsPrimaryKey = Reader["IsPrimaryKey"].ToString() == "1",
                                Type = Reader["DATA_TYPE"] as string ?? "",
                                IsForeignKey = Reader["IsForeignKey"].ToString() == "1"
                            }
                            );
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

            return ListOfColumns;
        }

       





    }
}
