using System;
using Entities;

namespace Rota.Core.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{

        IGenericRepository<User> Users { get; }
        IGenericRepository<Tour> Tours { get; }
        IGenericRepository<TourDay> TourDays { get; }
        IGenericRepository<TourActivity> TourActivities { get; }
        IGenericRepository<Hotel> Hotels { get; }
        IGenericRepository<Reservation> Reservations { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Comment> Comments { get; }
        IGenericRepository<FavoriteTour> FavoriteTours { get; }
        IGenericRepository<Notification> Notifications { get; }
        IGenericRepository<Message> Messages { get; }
        ITourRepository CustomTours { get; }
        ICommentRepository CustomComments { get; }
        IUserRepository AdminUsers { get; }
        IReportRepository Reports { get; }
        IReservationRepository CustomReservation { get; }
        IFavoriteRepository CustomFavorite { get; }
        IPaymentRepository CustomPayment { get; }


        Task<bool> SaveAsync();
    }
}

