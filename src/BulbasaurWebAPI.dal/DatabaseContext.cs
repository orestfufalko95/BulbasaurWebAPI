using BulbasaurWebAPI.entity;
using BulbasaurWebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace BulbasaurWebAPI.dal
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // do NOT delete this rows, just comment which are not needed!!!!!!

            /* 
             * "VITALIY-PC"= server name  - get it from Managment studio, when you just open it the connection window will contain server name      
             * Database="Bulbasaur"       - it must be the same for all
             *                          and do NOT forget to create DB with required tables!!!
            */

            //Vitalik Dorosh:
            //optionsBuilder.UseSqlServer(@"Server=VITALIY-PC;Database=Bulbasaur;Trusted_Connection=True;");

            //OrestFufalko:
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-31L3IML\SQLEXPRESS;Database=Bulbasaur;Trusted_Connection=True; MultipleActiveResultSets=true;");

            //Vitalik Khorobchuk
            //optionsBuilder.UseSqlServer(@"Server=VITALII;Database=Bulbasaur;Trusted_Connection=True; MultipleActiveResultSets=true;");

            //optionsBuilder.UseSqlServer(@"Server=WS-IF-CP1583;Database=Bulbasaur;Trusted_Connection=True; MultipleActiveResultSets=true;");

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserInfo> UsersInfo { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<UserHasRole> UsersRoles { get; set; }
        public virtual DbSet<Friendship> Friendships { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<UserHasGame> UserHasGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Properties mapping

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.Property(p => p.Name).HasColumnName("Name");
                entity.Property(p => p.SurName).HasColumnName("Surname");
                entity.Property(p => p.PasswordHash).HasColumnName("Password");
            });

            modelBuilder.Entity<UserInfo>(entity => 
            {
                entity.ToTable("UserInfo");
                entity.Property(p => p.SexId).HasColumnName("SexEnum");
                entity.Ignore(info => info.Sex);
                entity.HasKey(ui => ui.UserId);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");
                entity.Property(p => p.DateTime).HasColumnName("Timestamp");
            });
            
            modelBuilder.Entity<UserHasRole>(entity =>
            {
               entity.ToTable("UserRole");
               entity.HasKey(t => new { t.RoleId, t.UserId });
               entity.Ignore(p => p.RoleEnum);
            });
            
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.Ignore(p => p.RoleEnum);
                entity.HasKey(p => p._roleId);
            });

            modelBuilder.Entity<Friendship>(entity =>
            {
                entity.ToTable("Friend");
                //entity.Property(p => p.SubscriberId).HasColumnName("ActiveId");
                //entity.Property(p => p.ResponderId).HasColumnName("PassiveId");
                entity.HasKey(key => new {key.ResponderId, key.SubscriberId});
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");
                entity.Property(p => p.ImageUrl).HasColumnName("imageUrl");
            });

            modelBuilder.Entity<UserHasGame>(entity =>
            {
                entity.ToTable("UserGame");
                entity.HasKey(t => new { t.UserId, t.GameId });
            });
            // Reference mapping

            modelBuilder.Entity<User>()
                .HasOne(p => p.Info)
                .WithOne(p => p.User)
                .HasForeignKey<UserInfo>(u => u.UserId);
            
            modelBuilder.Entity<UserHasRole>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserHasRole>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(pt => pt.RoleId);

            modelBuilder.Entity<UserHasGame>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserGames)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserHasGame>()
                .HasOne(pt => pt.Game)
                .WithMany(t => t.UserGames)
                .HasForeignKey(pt => pt.GameId);
        }
    }
}
