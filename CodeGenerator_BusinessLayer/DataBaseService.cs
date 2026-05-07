
using CodeGenerator_DataAccessLayer;
using System.Collections.Generic;


namespace CodeGenerator_BusinessLayer
{
    public class DataBaseService
    {
        public static List<string> GetAllDatabases(string ConnectionString)
        {
            if (!string.IsNullOrWhiteSpace(ConnectionString))
            {
                return DataBaseRepository.GetAllDatabases(ConnectionString);
            }
            else
            {
                return null;
            }
            
        }

        


    }
}
