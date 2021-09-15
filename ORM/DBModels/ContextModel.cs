using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ORM.DBModels
{
    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=DefaultConnectionString")
        {
        }

        public virtual DbSet<Baller> Ballers { get; set; }
        public virtual DbSet<ForgetPWDRec> ForgetPWDRecs { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<UserChat> UserChats { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
        public virtual DbSet<UsersTaste> UsersTastes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Baller>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<Picture>()
                .Property(e => e.About)
                .IsUnicode(false);

            modelBuilder.Entity<UserChat>()
                .Property(e => e.About)
                .IsFixedLength();

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.PWD)
                .IsUnicode(false);
        }
    }
}
