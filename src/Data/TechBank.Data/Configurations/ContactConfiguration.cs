using TechBank.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechBank.Data.Configurations
{    
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            //Table configurations
            builder.ToTable("contact", "contact");

            builder.HasKey(e => e.Id);

            // Entity configurations
            builder.Property(e => e.Id)
                .HasColumnName("contact_id")
                .IsRequired()
                .HasColumnOrder(1);

            #region IPublicKeyEntity
            builder.Property(e => e.EntityPublicKey)
                .HasColumnName("public_key")
                .IsRequired()
                .HasColumnOrder(2);
            #endregion            

            #region IAuditEntity
            builder.Property(e => e.Created)
                .HasColumnName("created")
                .IsRequired()
                .HasColumnOrder(4);

            builder.Property(e => e.CreatedBy)
                .HasColumnName("created_by")
                .IsRequired()
                .HasColumnOrder(5);

            builder.Property(e => e.Modified)
                .HasColumnName("modified")
                .IsRequired()
                .HasColumnOrder(6);

            builder.Property(e => e.ModifiedBy)
                .HasColumnName("modified_by")
                .IsRequired()
                .HasColumnOrder(7);

            builder.Property(e => e.Deleted)
                .HasColumnName("deleted")
                .IsRequired(false)
                .HasColumnOrder(8);

            builder.Property(e => e.DeletedBy)
                .HasColumnName("deleted_by")
                .IsRequired(false)
                .HasColumnOrder(9);

            builder.Property(e => e.Locked)
                .HasColumnName("locked")
                .IsRequired(false)
                .HasColumnOrder(10);

            builder.Property(e => e.LockedBy)
                .HasColumnName("locked_by")
                .IsRequired(false)
                .HasColumnOrder(11);
            #endregion

            #region ContactProperties
            builder.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired(false)
                .HasMaxLength(256)
                .HasColumnOrder(16);

            builder.Property(e => e.FirstName)
                .HasColumnName("first_name")
                .IsRequired(false)
                .HasMaxLength(256)
                .HasColumnOrder(17);

            builder.Property(e => e.MiddleName)
                .HasColumnName("middle_name")
                .IsRequired(false)
                .HasMaxLength(256)
                .HasColumnOrder(18);

            builder.Property(e => e.FirstLastName)
                .HasColumnName("first_last_name")
                .IsRequired(false)
                .HasMaxLength(256)
                .HasColumnOrder(19);

            builder.Property(e => e.SecondLastName)
                .HasColumnName("second_last_name")
                .IsRequired(false)
                .HasMaxLength(256)
                .HasColumnOrder(20);            
            #endregion

            builder.Ignore(e => e.User);
            builder.Ignore(e => e.FullName);

        }
    }
}
