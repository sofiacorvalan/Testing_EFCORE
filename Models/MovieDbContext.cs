using Microsoft.EntityFrameworkCore;

namespace Testing.Models
{
    public partial class MovieDbContext : DbContext
    {
        public MovieDbContext()
        {
        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString ="server=localhost;database=moviesbd;user=root;password=SOFIApaola1997";
                optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("actor");

                entity.Property(e => e.Id).HasColumnName("actor_id");
                entity.Property(e => e.Actor_birthday).HasColumnName("actor_birthday");
                entity.Property(e => e.Actor_name)
                    .HasMaxLength(100)
                    .HasColumnName("actor_name");
                entity.Property(e => e.Actor_picture)
                    .HasColumnType("blob")
                    .HasColumnName("actor_picture");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("movie");

                entity.Property(e => e.Id).HasColumnName("movie_id");
                entity.Property(e => e.Movie_budget)
                    .HasPrecision(18, 2)
                    .HasColumnName("movie_budget");
                entity.Property(e => e.Movie_duration).HasColumnName("movie_duration");
                entity.Property(e => e.Movie_genre)
                    .HasMaxLength(50)
                    .HasColumnName("movie_genre");
                entity.Property(e => e.Movie_name)
                    .HasMaxLength(100)
                    .HasColumnName("movie_name");

                entity.HasMany(d => d.Actors).WithMany(p => p.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "movieactor",
                        r => r.HasOne<Actor>().WithMany()
                            .HasForeignKey("actor_id")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("movieactor_ibfk_2"),
                        l => l.HasOne<Movie>().WithMany()
                            .HasForeignKey("movie_id")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("movieactor_ibfk_1"),
                        j =>
                        {
                            j.HasKey("movie_id", "actor_id")
                                .HasName("PRIMARY")
                                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                            j.ToTable("movieactor");
                            j.HasIndex(new[] { "actor_id" }, "actor_id");
                            j.IndexerProperty<int>("movie_id").HasColumnName("movie_id");
                            j.IndexerProperty<int>("actor_id").HasColumnName("actor_id");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

