using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caramel.Pattern.Services.Application.Security
{
    public class AppSettings
    {
        public AppSettings() { }

        public char[] Secret(string appKey, string appSecret)
        {
            char[] retorno = ['a', 'b'];
            return retorno;
        }
    }
}
