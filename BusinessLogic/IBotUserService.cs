using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IBotUserService
    {
        Task Create(BotUser botUser);
        Task Delete(long id);
        Task<BotUser?> Get(long id);
    }
}
