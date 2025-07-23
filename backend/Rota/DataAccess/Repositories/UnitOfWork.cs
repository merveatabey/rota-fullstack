using System;
using System.Threading.Tasks;
using DataAccess;
using Entities;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly DapperContext _dapperContext;


        //tek repository üreterek bellek kullanımını sınırlandırıyoruz.


        // Private repository alanları
        private IGenericRepository<User>? _users;
        private IGenericRepository<Tour>? _tours;
        private IGenericRepository<TourDay>? _tourDays;
        private IGenericRepository<TourActivity>? _tourActivities;
        private IGenericRepository<Hotel>? _hotels;
        private IGenericRepository<Reservation>? _reservations;
        private IGenericRepository<Payment>? _payments;
        private IGenericRepository<Comment>? _comments;
        private IGenericRepository<FavoriteTour>? _favoriteTours;
        private IGenericRepository<Notification>? _notifications;
        private IGenericRepository<Message>? _messages;
        private ITourRepository? _customTours;
        private ICommentRepository? _customComments;
        private IUserRepository? _adminUsers;
        private IReportRepository? _reports;
        private IReservationRepository? _customReservation;
        private IFavoriteRepository? _customFavorite;
        private IPaymentRepository? _customPayment;
        public UnitOfWork(AppDbContext context, DapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }

        // Her repo yalnızca bir kez oluşturulur
        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);
        public IGenericRepository<Tour> Tours => _tours ??= new GenericRepository<Tour>(_context);
        public IGenericRepository<TourDay> TourDays => _tourDays ??= new GenericRepository<TourDay>(_context);
        public IGenericRepository<TourActivity> TourActivities => _tourActivities ??= new GenericRepository<TourActivity>(_context);
        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_context);
        public IGenericRepository<Reservation> Reservations => _reservations ??= new GenericRepository<Reservation>(_context);
        public IGenericRepository<Payment> Payments => _payments ??= new GenericRepository<Payment>(_context);
        public IGenericRepository<Comment> Comments => _comments ??= new GenericRepository<Comment>(_context);
        public IGenericRepository<FavoriteTour> FavoriteTours => _favoriteTours ??= new GenericRepository<FavoriteTour>(_context);
        public IGenericRepository<Notification> Notifications => _notifications ??= new GenericRepository<Notification>(_context);
        public IGenericRepository<Message> Messages => _messages ??= new GenericRepository<Message>(_context);

        public ITourRepository CustomTours => _customTours ??= new TourRepository(_context);

        public ICommentRepository CustomComments => _customComments ??= new CommentRepository(_context);

        public IUserRepository AdminUsers => _adminUsers ??= new UserRepository(_context);

        public IReportRepository Reports => _reports ??= new ReportRepository(_dapperContext);

        public IReservationRepository CustomReservation => _customReservation ??= new ReservationRepository(_context);

        public IFavoriteRepository CustomFavorite => _customFavorite ??= new FavoriteTourRepository(_context);

        public IPaymentRepository CustomPayment => _customPayment ??= new PaymentRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
