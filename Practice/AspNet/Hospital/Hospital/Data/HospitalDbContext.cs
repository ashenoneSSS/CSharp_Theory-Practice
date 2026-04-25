using Microsoft.EntityFrameworkCore;
using Hospital.Models;

namespace Hospital.Data
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public HospitalDbContext(DbContextOptions general_options) : base(general_options)
        {
        }
    }
}
