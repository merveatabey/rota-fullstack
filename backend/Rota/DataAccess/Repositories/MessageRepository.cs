using System;
using DataAccess;
using Entities;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class MessageRepository : GenericRepository<Message>
	{
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
       
	}
}

