using System;
using System.Collections.Generic;

namespace DefinitelyReputableSource_Lundeen.Models
{
    public partial class Student
    {
        public Student()
        {
            Classes = new HashSet<Class>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEnd { get; set; }
        public decimal TotalMoney { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
