using System;
using DataAccess;
using Entities;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class HotelRepository : GenericRepository<Hotel>
	{
		private readonly AppDbContext _context;

		public HotelRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
	}
}

