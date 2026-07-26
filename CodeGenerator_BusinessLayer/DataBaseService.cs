
using CodeGenerator_DataAccessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CodeGenerator_BusinessLayer
{
    public class DataBaseService
    {
        public static async Task<List<string>> GetAllDatabases(string ConnectionString)
        {
            if (!string.IsNullOrWhiteSpace(ConnectionString))
            {
                return await DataBaseRepository.GetAllDatabases(ConnectionString);
            }
            else
            {
                return null;
            }
            
        }

        


    }
}
