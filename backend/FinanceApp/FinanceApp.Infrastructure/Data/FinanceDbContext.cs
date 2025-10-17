using System;
using FinanceApp.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FinanceApp.Infrastructure.Data
{
    public partial class FinanceDbContext : DbContext
    {
        public FinanceDbContext()
        {
        }

        public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<CardType> CardTypes { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<DepositCertificate> DepositCertificates { get; set; }

        public virtual DbSet<CardReadOnlyView> CardDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.Property(e => e.BankId).HasColumnName("bank_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("updated_by");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.CardId).HasColumnName("card_id");

                entity.Property(e => e.BankId).HasColumnName("bank_id");

                entity.Property(e => e.CardTypeId).HasColumnName("card_type_id");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

                entity.Property(e => e.CutOffDay).HasColumnName("cut_off_day");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("date")
                    .HasColumnName("expiration_date");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("date")
                    .HasColumnName("issue_date");

                entity.Property(e => e.OwnerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("owner_name");

                entity.Property(e => e.PaymentDay).HasColumnName("payment_day");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("updated_by");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cards_Banks");

                entity.HasOne(d => d.CardType)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CardTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cards_Card_Types");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cards_Currencies");
            });

            modelBuilder.Entity<CardType>(entity =>
            {
                entity.ToTable("Card_Types");

                entity.HasIndex(e => e.CardTypeCode, "UQ__Card_Typ__8EAB05FD20855BAD")
                    .IsUnique();

                entity.Property(e => e.CardTypeId).HasColumnName("card_type_id");

                entity.Property(e => e.CardTypeCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("card_type_code");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("updated_by");
            });

            modelBuilder.Entity<DepositCertificate>(entity =>
            {
                entity.HasKey(e => e.CertificateId)
                    .HasName("PK__DepositC__E2256D31BB3CF1B0");

                entity.Property(e => e.CertificateId).HasColumnName("certificate_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.BankId).HasColumnName("bank_id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiration_date");

                entity.Property(e => e.InterestAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("interest_amount");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("owner_name");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("updated_by");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.DepositCertificates)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepositCertificates_Banks");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.DepositCertificates)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepositCertificates_Currencies");
            });

            modelBuilder.Entity<CardReadOnlyView>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
