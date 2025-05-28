using HotelReservationSystem.Models;
using HotelReservationSystem.ViewModels;
using System.Collections.Generic;

namespace HotelReservationSystem.Repositories
{
    public interface IReservationInterface
    {
        IEnumerable<ReservationModel> GetAll();
        ReservationModel Get(int id);
        void Add(ReservationModel reservation);
        void Update(int id, ReservationModel reservation);
        void Delete(int id);

        List<ReservationViewModel> GetAllReservationsWithDetails(); //ReservationviewModel

        public ReservationViewModel GetDetailed(int id); // //ReservationviewModel

        public void Update2(int id, ReservationViewModel viewModel);

    }
}
