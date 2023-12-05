using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.Shop.Service
{
    public interface  ITokenProvider
    {
        public  Task<string> LoginAsync();
  
        
    }
}