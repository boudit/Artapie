namespace Dal
{
    using System.Data.Entity;

    using DalContract.Models;

    public class ModelContext : DbContext
    {
        private const string ConnctionStringBase = @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";

        public ModelContext(string attachDbFileName)
            : base(GetConnectionStringWithAttachDbFileName(attachDbFileName))
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Cotation> Cotations { get; set; }

        public DbSet<Fiche> Fiches { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Seance> Seances { get; set; }

        private static string GetConnectionStringWithAttachDbFileName(string attachDbFileName)
        {
            return ConnctionStringBase + @";AttachDbFilename=" + attachDbFileName;
        }
    }
}
