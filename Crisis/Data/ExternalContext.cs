using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Crisis.Models.ExternalData;

namespace Crisis.Data
{
    public partial class ExternalContext : DbContext
    {
        public ExternalContext() 
        {
        }
        public ExternalContext(DbContextOptions<ExternalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VwSrmAhmSupplier> VwSrmAhmSuppliers { get; set; }
        public virtual DbSet<VwSrmDeliveryPrimary> VwSrmDeliveryPrimaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VwSrmAhmSupplier>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_srm_ahm_suppliers");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AhmDealerNo)
                    .HasColumnName("AHM_Dealer_No")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.AhmDeptCode)
                    .HasColumnName("AHM_Dept_Code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AhmLocCd)
                    .HasColumnName("AHM_Loc_Cd")
                    .HasColumnType("ntext");

                entity.Property(e => e.AhmSupplierNm)
                    .HasColumnName("AHM_Supplier_Nm")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AhmSupplierNo)
                    .IsRequired()
                    .HasColumnName("AHM_Supplier_No")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.OemSupplierNo)
                    .HasColumnName("OEM_Supplier_No")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierDeliveryEmail)
                    .HasColumnName("Supplier_Delivery_Email")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierDeliveryName)
                    .HasColumnName("Supplier_Delivery_Name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierDeliveryPhone)
                    .HasColumnName("Supplier_Delivery_Phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwSrmDeliveryPrimary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_srm_delivery_primary");

                entity.Property(e => e.AhmSupplierNo)
                    .HasColumnName("AHM_Supplier_No")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.PortalLoginId)
                    .HasColumnName("Portal_Login_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleAdminGroup)
                    .HasColumnName("Role_Admin_Group")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleAdminSubGroup)
                    .HasColumnName("Role_Admin_Sub_Group")
                    .HasMaxLength(7)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
