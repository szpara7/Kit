using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kit.Data.DatabaseLogic
{

    public class User : IEntity
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public byte[] TimeStamp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

        public static void check()
        {


        }
    }

    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .UseSqlServerIdentityColumn();

            builder.Property(t => t.PublicId);

            builder.Property(t => t.TimeStamp)
                .IsRequired()
                .IsRowVersion();

            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
