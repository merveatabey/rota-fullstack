using System;
using DataAccess;
using Entities;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class TourDayRepository : GenericRepository<TourDay>
	{

        private readonly AppDbContext _context;

        public TourDayRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        
	}
}

