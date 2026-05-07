
using CodeGenerator_DataAccessLayer;
using System.Collections.Generic;


namespace CodeGenerator_BusinessLayer
{
    public class TableService
    {
        public static List<string> GetAllTableNames(string ConnectionString)
        {
            if (!string.IsNullOrWhiteSpace(ConnectionString))
            {
                return TableRepository.GetAllTableNames(ConnectionString);
            }
            else
            {
                return null;
            }
        }




    }
}
