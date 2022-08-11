using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Learning_Diary_EL.Models
{
<<<<<<< HEAD:Learning_Diary_EL/Models/Learning_Diary_ELContext.cs
    public partial class Learning_Diary_ConsoleAppContext : DbContext
    {
        public Learning_Diary_ConsoleAppContext()
        {
        }

        public Learning_Diary_ConsoleAppContext(DbContextOptions<Learning_Diary_ConsoleAppContext> options)
=======
    public partial class Learning_DiaryContext : DbContext
    {
        public Learning_DiaryContext()
        {
        }

        public Learning_DiaryContext(DbContextOptions<Learning_DiaryContext> options)
>>>>>>> 9a8a0700e33facd885f56571a99e59f8ec1849aa:Learning_Diary_EL/Models/Learning_DiaryContext.cs
            : base(options)
        {
        }

        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // set this variable to your computer name so database creation and use works
            string computerName = "your-computer-name";

            if (!optionsBuilder.IsConfigured)
            {
<<<<<<< HEAD:Learning_Diary_EL/Models/Learning_Diary_ELContext.cs
                optionsBuilder.UseSqlServer($"Server={computerName}\\;Database=Learning_Diary_ConsoleApp;Trusted_Connection=True;");
=======
                optionsBuilder.UseSqlServer("Server=EETU\\;Database=Learning_Diary;Trusted_Connection=True;");
>>>>>>> 9a8a0700e33facd885f56571a99e59f8ec1849aa:Learning_Diary_EL/Models/Learning_DiaryContext.cs
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Note");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Note1)
                    .HasMaxLength(8000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("Topic");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompletionDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartLearningDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
