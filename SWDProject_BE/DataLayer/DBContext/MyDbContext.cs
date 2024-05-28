using System;
using System.Collections.Generic;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DBContext;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BannedAccount> BannedAccounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Dispute> Disputes { get; set; }

    public virtual DbSet<Exchanged> Exchangeds { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost;database=SWD392_DB;User Id=sa;password=12345;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BannedAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Banned_A__3214EC079EE53316");

            entity.ToTable("Banned_Account");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.BannedAccounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Banned_Ac__User___412EB0B6");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC079AC626E3");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Dispute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dispute__3214EC07A5CEFFFF");

            entity.ToTable("Dispute");

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.Disputes)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dispute__Order_I__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.Disputes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dispute__User_Id__59063A47");
        });

        modelBuilder.Entity<Exchanged>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exchange__3214EC0792277B61");

            entity.ToTable("Exchanged");

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.PostId).HasColumnName("Post_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.Exchangeds)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exchanged__Order__5EBF139D");

            entity.HasOne(d => d.Post).WithMany(p => p.Exchangeds)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exchanged__Post___5DCAEF64");

            entity.HasOne(d => d.User).WithMany(p => p.Exchangeds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exchanged__User___5CD6CB2B");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07FC0D7876");

            entity.ToTable("Order");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.PaymentId).HasColumnName("Payment_Id");
            entity.Property(e => e.PostId).HasColumnName("Post_Id");
            entity.Property(e => e.TotalPrice).HasColumnName("Total_Price");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__Payment_I__5629CD9C");

            entity.HasOne(d => d.Post).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__Post_Id__5535A963");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__User_Id__5441852A");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC072DBB26AF");

            entity.ToTable("Payment");

            entity.Property(e => e.Method).HasMaxLength(50);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Post__3214EC0756D0A3C0");

            entity.ToTable("Post");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Img).HasColumnName("IMG");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionType_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.Posts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__Product_Id__4D94879B");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.Posts)
                .HasForeignKey(d => d.TransactionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__Transactio__4CA06362");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__User_Id__4BAC3F29");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC078961B1A9");

            entity.ToTable("Product");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.StockQuantity).HasColumnName("Stock_Quantity");
            entity.Property(e => e.UrlImg).HasColumnName("Url_IMG");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Categor__48CFD27E");

            entity.HasOne(d => d.User).WithMany(p => p.Products)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__User_Id__47DBAE45");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rating__3214EC07E2AACE25");

            entity.ToTable("Rating");

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE");
            entity.Property(e => e.ExchangeId).HasColumnName("Exchange_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Exchange).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ExchangeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rating__Exchange__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rating__User_Id__619B8048");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Report__3214EC07E16D065C");

            entity.ToTable("Report");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.PostId).HasColumnName("Post_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Post).WithMany(p => p.Reports)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__Post_Id__5165187F");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__User_Id__5070F446");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC0740734A5E");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Token__3214EC0755D0BE88");

            entity.ToTable("Token");

            entity.Property(e => e.Expiration).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Token__User_Id__3C69FB99");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC07232EC4DD");

            entity.ToTable("Transaction_Type");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0715DB0EF1");

            entity.ToTable("User");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasColumnType("date")
                .HasColumnName("Created_Date");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Field).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("date")
                .HasColumnName("Modified_Date");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.RatingCount).HasColumnName("Rating_Count");
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__Role_Id__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
