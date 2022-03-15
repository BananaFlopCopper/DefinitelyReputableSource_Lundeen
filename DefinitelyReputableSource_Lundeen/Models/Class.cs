using System;
using System.Collections.Generic;

namespace DefinitelyReputableSource_Lundeen.Models
{
    public partial class Class
    {
        public Class()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal? DailyCost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        // probably should've had a description

        public virtual ICollection<Student> Students { get; set; }
    }
}
