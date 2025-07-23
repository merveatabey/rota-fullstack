using System;
using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
    public class TourActivityRepository : GenericRepository<TourActivity>, ITourActivityRepository
    {
        private readonly AppDbContext _context;

        public TourActivityRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

       
    }

}

