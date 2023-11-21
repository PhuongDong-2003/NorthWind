using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.Web.Service
{
    public interface  ITokenProvider
    {
        public  Task<string> LoginAsync();
  
        
    }
}