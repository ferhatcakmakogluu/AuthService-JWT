using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Configurations
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }

        //hangi app hangi api ye istek atablilir belirlemek icin
        public List<String> Audiences { get; set; } 
    }
}
