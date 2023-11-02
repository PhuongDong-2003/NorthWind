using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public interface IAccountRepository
    {
        public IEnumerable<Account> GetAll();
       

    }
}