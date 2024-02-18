using Reservoom.Exceptions;
using Reservoom.Services.ReservationConflictValidators;
using Reservoom.Services.ReservationCreators;
using Reservoom.Services.ReservationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;   //用于提供预订信息的服务
        private readonly IReservationCreator _reservationCreator;    //用于创建预订信息的服务
        private readonly IReservationConflictValidator _reservationConflictValidator;   //用于验证新预订是否与现有预订发生冲突的服务。
        //它可能包含比较新预订与现有预订时间、房间等信息的方法，以确定是否存在冲突。

        public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        /// <summary>
        /// Get all reservations.
        /// </summary>
        /// <returns>All reservations in the reservation book.</returns
       // 得到所有订单
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationProvider.GetAllReservations();
        }

        /// <summary>
        /// Add a reservation to the reservation book.
        /// </summary>
        /// <param name="reservation">The incoming reservation.</param>
        /// <exception cref="InvalidReservationTimeRangeException">Thrown if reservation start time is after end time.</exception>
        /// <exception cref="ReservationConflictException">Thrown if incoming reservation conflicts with existing reservation.</exception>
        public async Task AddReservation(Reservation reservation)
        {
            if(reservation.StartTime > reservation.EndTime)
            {
                throw new InvalidReservationTimeRangeException(reservation);
            }

            Reservation conflictingReservation = await _reservationConflictValidator.GetConflictingReservation(reservation);

            if(conflictingReservation != null)
            {
                throw new ReservationConflictException(conflictingReservation, reservation);
            }

            await _reservationCreator.CreateReservation(reservation);
            //少了三个类
        }
    }
}
