using System.Data.Entity;

namespace MVCRealtimeSignalR.Models
{
    public class SignalRDbContext : DbContext
    {
        public SignalRDbContext() : base("name=SignalRDbContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Consultation> Consultations { get; set; }

        public DbSet<Patient> Patients { get; set; }
    }
}
