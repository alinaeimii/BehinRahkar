using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helper
{
    public class UserJson
    {
        private static string userPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/user.json");
        public async static Task<string> GetUserJson()
        {
            return await File.ReadAllTextAsync(userPath);
        }
    }
}
