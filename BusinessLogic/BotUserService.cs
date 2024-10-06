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

        public async Task Create(BotUser botUser)
        {
            if (botUserRepo.GetById(botUser.Id) == null)
            {
                 await botUserRepo.Insert(botUser);
                 await botUserRepo.Save();
            }
            
        }

        public async Task Delete(long id)
        {
            if (botUserRepo.GetById(id) != null)
            {
                await botUserRepo.Delete(id);
                await botUserRepo.Save();
            }
        }

        public async Task<BotUser?> Get(long id)
        {

            if (botUserRepo.GetById(id) != null)
            {
                return botUserRepo.GetById(id);
            }

            throw new Exception();
        }
    }
}
