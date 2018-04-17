using System;

namespace EConsult_T.DAL.Entities
{
    public class DateRange
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
