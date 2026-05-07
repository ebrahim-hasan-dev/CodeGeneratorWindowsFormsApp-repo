
using CodeGenerator_DataAccessLayer;
using System.Collections.Generic;


namespace CodeGenerator_BusinessLayer
{
    public class ViewService
    {
        public static List<string> GetAllViewNames(string ConnectionString)
        {
            if (!string.IsNullOrWhiteSpace(ConnectionString))
            {
                return ViewRepository.GetAllViewNames(ConnectionString);
            }
            else
            {
                return null;
            }
        }





    }
}
