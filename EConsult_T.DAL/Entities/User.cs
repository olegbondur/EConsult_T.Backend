using System.Collections.Generic;

namespace EConsult_T.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public ICollection<DateRange> DateRanges { get; set; }
    }
}
