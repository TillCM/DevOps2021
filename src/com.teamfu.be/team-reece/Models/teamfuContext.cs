using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace team_reece.Models
{
    public partial class teamfuContext : DbContext
    {
        public teamfuContext()
        {
        }

        public teamfuContext(DbContextOptions<teamfuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ToDo> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(EnvironmentHelper.GenerateDBConnectionFromEnvMan());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ToDo>(entity =>
            {
                entity.HasKey(x=>x.PurchaseOrderId);

                entity.ToTable("ToDo");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
