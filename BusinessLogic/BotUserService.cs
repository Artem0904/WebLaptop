using DataAccess.Data.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BotUserService : IBotUserService
    {
        private readonly IRepository<BotUser> botUserRepo;

        public BotUserService(IRepository<BotUser> botUserRepo)
        {
           this.botUserRepo = botUserRepo;
        }

        public void Create(BotUser botUser)
        {
            botUserRepo.Insert(botUser);
            botUserRepo.Save();
            
        }
    }
}
