using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Irudd.Expenses.Api.Datamodel;

public class ExpensesContext : IdentityDbContext<User>
{
    private readonly bool useInMemoryContext;

    public ExpensesContext(DbContextOptions<ExpensesContext> options) : base(options)
    {
        
    }

    private ExpensesContext(bool useInMemoryContext)
    {
        this.useInMemoryContext = useInMemoryContext;
    }

    /// <summary>
    /// Used for testing only.
    /// </summary>
    public static ExpensesContext CreateInMemoryContext() => new ExpensesContext(useInMemoryContext: true);

    public virtual DbSet<Expense> Expenses { get;set; }
    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && useInMemoryContext)
            optionsBuilder.UseInMemoryDatabase("TestDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        void Configure<TEntity>(Action<EntityTypeBuilder<TEntity>> withEntity) where TEntity : class => 
            withEntity(modelBuilder.Entity<TEntity>());

        Configure<Expense>(entity =>
        {
            entity.Property(x => x.Id).HasMaxLength(128);
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.User).WithMany(x => x.Expenses).HasForeignKey(x => x.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Category).WithMany(x => x.Expenses).HasForeignKey(x => x.CategoryCode).OnDelete(DeleteBehavior.SetNull);
            entity.Property(x => x.Amount).IsRequired().HasColumnType("money");
            entity.Property(x => x.Date).IsRequired();
            entity.Property(x => x.Description).IsRequired().HasMaxLength(500);
        });

        Configure<Category>(entity =>
        {
            entity.Property(x => x.Code).HasMaxLength(128);
            entity.HasKey(x => x.Code);
            entity.Property(x => x.DisplayName).HasMaxLength(128);
        });
    }
}
