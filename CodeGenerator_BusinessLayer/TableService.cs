
using CodeGenerator_DataAccessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CodeGenerator_BusinessLayer
{
    public class TableService
    {
        public static async Task<List<string>> GetAllTableNames(string ConnectionString)
        {
            if (!string.IsNullOrWhiteSpace(ConnectionString))
            {
                return await TableRepository.GetAllTableNames(ConnectionString);
            }
            else
            {
                return null;
            }
        }




    }
}
