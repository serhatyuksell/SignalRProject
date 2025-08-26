using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfBookingDal : GenericRepository<Booking>, IBookingDal
    {
        public EfBookingDal(SignalRContext context) : base(context)
        {
        }

        public void BookingStatusApproved(int id)
        {
            using (var context = new SignalRContext())
            {
                var booking = context.Bookings.Find(id);
                if (booking != null)
                {
                    booking.Description = "Rezervasyon Onaylandı"; // Assuming Status is a string property
                    context.SaveChanges();
                }
            }
        }

        public void BookingStatusCancelled(int id)
        {
            using (var context = new SignalRContext())
            {
                var booking = context.Bookings.Find(id);
                if (booking != null)
                {
                    booking.Description = "Rezervasyon İptal Edildi"; // Assuming Status is a string property
                    context.SaveChanges();
                }
            }
        }
    }
}
