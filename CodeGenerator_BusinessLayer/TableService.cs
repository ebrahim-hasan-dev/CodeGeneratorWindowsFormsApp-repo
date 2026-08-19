
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


        
    }
}
