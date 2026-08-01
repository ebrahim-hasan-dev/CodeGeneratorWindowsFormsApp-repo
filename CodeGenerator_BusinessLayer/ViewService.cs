
using CodeGenerator_DataAccessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CodeGenerator_BusinessLayer
{
    public class ViewService
    {
        public static async Task<List<string>> GetAllViewNamesAsync(string ConnectionString)
        {
            if (!string.IsNullOrWhiteSpace(ConnectionString))
            {
                return await ViewRepository.GetAllViewNamesAsync(ConnectionString);
            }
            else
            {
                return null;
            }
        }





    }
}
