using System.Data.Entity;
using DecoderServer.Model.Entities;

namespace DecoderServer.Model.utils
{
   
    public partial class MyContext : DbContext
    {
        public MyContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Cipher> Ciphers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cipher>()
                .Property(e => e.From)
                .IsUnicode(false);

            modelBuilder.Entity<Cipher>()
                .Property(e => e.To)
                .IsUnicode(false);
        }
    }
}
