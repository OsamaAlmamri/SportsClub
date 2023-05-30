using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SportsClub.Models
{
    public partial class SportsClubContext : DbContext
    {
        public SportsClubContext()
        {
        }

        public SportsClubContext(DbContextOptions<SportsClubContext> options)
            : base(options)
        {
        }


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }

      
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<ServiceTime> ServiceTimes { get; set; }
        public virtual DbSet<PaymentGatway> PaymentGatways { get; set; }

        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }
        
        public virtual DbSet<UserSubscriptionService> UserSubscriptionServices { get; set; }
        public virtual DbSet<UserSubscriptionPaymentGatway> UserServicePaymentGatways { get; set; }


        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
*/                optionsBuilder.UseSqlServer("Server=.\\;Database=sportsClub;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");



            modelBuilder.Entity<User>(entity =>
            {

                entity.HasIndex(e => e.Email, "users_email_unique")
                    .IsUnique();
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(255);

                entity.HasOne(d => d.UserDetail)
                .WithOne(p => p.User)
                .HasForeignKey<UserDetail>(e => e.UserId)

              
                .HasConstraintName("UserSetails_user_id_foreign");
            });


            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_UserDetailUserId");


                modelBuilder.Entity<UserDetail>()
                  .HasOne(e => e.User)
                  .WithOne(e => e.UserDetail)
                  .HasForeignKey<UserDetail>(e => e.UserId)
                  .IsRequired();

            });




            modelBuilder.Entity<ServiceType>(entity =>
            {
            });



            modelBuilder.Entity<ServiceTime>(entity =>
            {
            });

            modelBuilder.Entity<PaymentGatway>(entity =>
            {
            });


            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => e.ServiceTypeId, "IX_ServicesServiceTypeId");

                entity.Property(e => e.CreatedAt)
                             .HasDefaultValueSql("(getdate())");



                entity.HasOne(d => d.ServiceType)
                 .WithMany(p => p.Services)
                 .HasForeignKey(d => d.ServiceTypeId)
                 .HasConstraintName("services_service_type_id_foreign");

                entity.HasOne(d => d.ServiceTime)
               .WithMany(p => p.Services)
               .HasForeignKey(d => d.ServiceTimeId)
               .HasConstraintName("services_service_time_id_foreign");

            });



            modelBuilder.Entity<UserSubscription>(entity =>
            {
              
                entity.HasIndex(e => e.UserId, "IX_UserSubscriptionId");

                entity.Property(e => e.CreatedAt)
                             .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User)
                 .WithMany(p => p.UserSubscriptions)
                 .HasForeignKey(d => d.UserId)
                 .HasConstraintName("UserSubscriptions_user_id_foreign");


            });

            modelBuilder.Entity<UserSubscriptionService>(entity =>
            {
                entity.HasIndex(e => e.ServiceId, "IX_SUserSubscriptionServiceServiceId");
                entity.HasIndex(e => e.UserId, "IX_UserSubscriptionServiceUserID");


                entity.HasOne(d => d.Service)
                 .WithMany(p => p.UserServices)
                 .HasForeignKey(d => d.ServiceId)
                 .HasConstraintName("UserService_services_type_id_foreign");

                entity.HasOne(d => d.User)
                 .WithMany(p => p.UserSubscriptionServices)
                 .HasForeignKey(d => d.UserId)
                 .HasConstraintName("UserSubscriptionServices_user_id_foreign");


            });



            modelBuilder.Entity<UserSubscriptionPaymentGatway>(entity =>
            {
                entity.HasIndex(e => e.UserSubscriptionId, "IX_UserSubscriptionPaymentGatwayUserSubscriptionId");
                entity.HasIndex(e => e.PaymentGatwayId, "IX_PaymentGatway");

                entity.Property(e => e.CreatedAt)
                             .HasDefaultValueSql("(getdate())");



                entity.HasOne(d => d.UserSubscription)
                 .WithMany(p => p.UserServicePaymentGatways)
                 .HasForeignKey(d => d.UserSubscriptionId)
                 .HasConstraintName("UserServicePaymentGatwayUserService_id_foreign");

                entity.HasOne(d => d.PaymentGatway)
                 .WithMany(p => p.UserServicePaymentGatways)
                 .HasForeignKey(d => d.PaymentGatwayId)
                 .HasConstraintName("UserServicePaymentGatwayPaymentGatway_id_foreign");


            });



         



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
