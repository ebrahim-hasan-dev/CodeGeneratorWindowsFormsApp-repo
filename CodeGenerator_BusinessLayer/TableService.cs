
using CodeGenerator_DataAccessLayer;
using CodeGenerator_Modules;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CodeGenerator_BusinessLayer
{
    public class TableService
    {
        public static async Task<List<string>> GetAllTableNamesAsync(string ConnectionString)
        {
            if (!string.IsNullOrWhiteSpace(ConnectionString))
            {
                return await TableRepository.GetAllTableNamesAsync(ConnectionString);
            }
            else
            {
                return null;
            }
        }

        public static string ConvertToSingle(string TableName)
        {
            if (TableName.EndsWith("ses"))
                return TableName.Substring(0, TableName.Length - 2);
            else if (TableName.EndsWith("sses"))
                return TableName.Substring(0, TableName.Length - 2);
            else if (TableName.EndsWith("ches"))
                return TableName.Substring(0, TableName.Length - 2);
            else if (TableName.EndsWith("shes"))
                return TableName.Substring(0, TableName.Length - 2);
            else if (TableName.EndsWith("xes"))
                return TableName.Substring(0, TableName.Length - 2);
            else if (TableName.EndsWith("zes"))
                return TableName.Substring(0, TableName.Length - 2);
            else if (TableName.EndsWith("oes"))
                return TableName.Substring(0, TableName.Length - 2);
            else if (TableName.EndsWith("ies"))
                return TableName.Substring(0, TableName.Length - 3) + "y";
            else if (TableName.EndsWith("os"))
                return TableName.Substring(0, TableName.Length - 1);
            else if (TableName.EndsWith("as"))
                return TableName.Substring(0, TableName.Length - 1);
            else if (TableName.EndsWith("us"))
                return TableName.Substring(0, TableName.Length - 1);
            else if (TableName.EndsWith("is"))
                return TableName.Substring(0, TableName.Length - 1);
            else if (TableName.EndsWith("es"))
                return TableName.Substring(0, TableName.Length - 1);
            else if (TableName.EndsWith("s"))
                return TableName.Substring(0, TableName.Length - 1);
            else
                return TableName;
        }

    }
}
