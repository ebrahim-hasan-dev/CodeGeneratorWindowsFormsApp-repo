
using CodeGenerator_DataAccessLayer;
using CodeGenerator_Modules;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Threading.Tasks;


namespace CodeGenerator_BusinessLayer
{
    public class ColumnService
    {
        public static async Task<List<clsColumnDataModulesLayer>> GetAllColumnsAsync(string TableName, string ConnectionString, bool IsGenerateModulesLayer)
        {
            if (!string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(ConnectionString))
            {
                List<clsColumnDataModulesLayer> ListOfColumns = await ColumnRepository.GetAllColumnsAsync(TableName, ConnectionString);

                if (ListOfColumns != null)
                {
                    for (short i = 0; i < ListOfColumns.Count; i++)
                    {
                        ListOfColumns[i].Type = MapSqlTypeToCSharpType(ListOfColumns[i].Type, ListOfColumns[i].IsNullable, IsGenerateModulesLayer);
                    }
                }

                return ListOfColumns;
            }
            else
            {
                return null;
            }
        }

        public static async Task<List<clsColumnDataDataAccessLayer>> GetAllColumnsDataAccessLayerAsync(string TableName, string ConnectionString, bool IsGenerateModulesLayer)
        {
            if (!string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(ConnectionString))
            {
                List<clsColumnDataDataAccessLayer> ListOfColumns = await ColumnRepository.GetAllColumnsDataAccessLayerAsync(TableName, ConnectionString);

                if (ListOfColumns != null)
                {
                    for (short i = 0; i < ListOfColumns.Count; i++)
                    {
                        ListOfColumns[i].Type = MapSqlTypeToCSharpType(ListOfColumns[i].Type, ListOfColumns[i].IsNullable, IsGenerateModulesLayer);
                    }
                }

                return ListOfColumns;
            }
            else
            {
                return null;
            }
        }

        public static async Task<List<clsColumnDataDataAccessLayer>> GetAllColumnsStoredProcedureScriptAsync(string TableName, string ConnectionString)
        {
            if (!string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(ConnectionString))
            {
                return await ColumnRepository.GetAllColumnsDataAccessLayerAsync(TableName, ConnectionString); ;
            }
            else
            {
                return null;
            }
        }

        public static async Task<List<clsColumnDataBusinessLayer>> GetAllColumnsBusinessLayerAsync(string TableName, string ConnectionString, bool IsGenerateModulesLayer)
        {
            if (!string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(ConnectionString))
            {
                List<clsColumnDataBusinessLayer> ListOfColumns = await ColumnRepository.GetAllColumnsBusinessLayerAsync(TableName, ConnectionString);

                if (ListOfColumns != null)
                {
                    for (short i = 0; i < ListOfColumns.Count; i++)
                    {
                        ListOfColumns[i].Type = MapSqlTypeToCSharpType(ListOfColumns[i].Type, false, IsGenerateModulesLayer);
                    }
                }

                return ListOfColumns;
            }
            else
            {
                return null;
            }
        }

        static string MapSqlTypeToCSharpType(string sqlType, bool isNullable, bool IsGenerateModulesLayer)
        {
            string cSharpType = "";

            switch (sqlType.ToLower())
            {
                case "bigint":
                    cSharpType = "long";
                    break;
                case "int":
                    cSharpType = "int";
                    break;
                case "smallint":
                    cSharpType = "short";
                    break;
                case "tinyint":
                    cSharpType = "byte";
                    break;
                case "bit":
                    cSharpType = "bool";
                    break;
                case "decimal":
                case "numeric":
                case "money":
                    cSharpType = "decimal";
                    break;
                case "float":
                case "smallmoney":
                    cSharpType = "float";
                    break;
                case "datetime":
                case "datetime2":
                case "date":
                case "smalldatetime":
                    cSharpType = "DateTime";
                    break;
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                    cSharpType = "string";
                    break;
                case "uniqueidentifier":
                    cSharpType = "Guid";
                    break;
                default:
                    cSharpType = "object";
                    break;
            }

            if (IsGenerateModulesLayer)
            {
                if (isNullable && cSharpType != "string" && cSharpType != "object")
                {
                    cSharpType += "?";
                }
            }

            return cSharpType;
        }

        public static async Task<List<clsColumnDataUILayer>> GetAllColumnsUILayerAsync(string TableName, string ConnectionString)
        {
            if (!string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(ConnectionString))
            {
                List<clsColumnDataUILayer> ListOfColumns = await ColumnRepository.GetAllColumnsUILayerAsync(TableName, ConnectionString);

                if (ListOfColumns != null)
                {
                    for (short i = 0; i < ListOfColumns.Count; i++)
                    {
                        ListOfColumns[i].ControlName = MapSqlTypeToControl(ListOfColumns[i].Type);
                    }
                }

                return ListOfColumns;
            }
            else
            {
                return null;
            }
        }

        static string MapSqlTypeToControl(string sqlType)
        {
            string ControlName = "";

            switch (sqlType.ToLower())
            {
                case "bigint":
                case "int":
                case "smallint":
                case "tinyint":
                case "float":
                case "uniqueidentifier":
                    ControlName = enControls.Label.ToString();
                    break;
                case "bit":
                    ControlName = enControls.CheckBox.ToString();
                    break;
                case "decimal":
                case "numeric":
                case "money":
                case "smallmoney":
                    ControlName = enControls.MaskedTextBox.ToString();
                    break;
                case "datetime":
                case "datetime2":
                case "date":
                case "smalldatetime":
                    ControlName = enControls.DateTimePicker.ToString();
                    break;
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                    ControlName = enControls.TextBox.ToString();
                    break;
                default:
                    ControlName = enControls.Label.ToString();
                    break;
            }

            return ControlName;
        }

        

    }
}
