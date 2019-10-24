using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AvioKompanija.Models
{
    public partial class AvioKompanijaContext : DbContext
    {
        public AvioKompanijaContext()
        {
        }

        public AvioKompanijaContext(DbContextOptions<AvioKompanijaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aerodrom> Aerodrom { get; set; }
        public virtual DbSet<Destinacija> Destinacija { get; set; }
        public virtual DbSet<Karta> Karta { get; set; }
        public virtual DbSet<Kompanija> Kompanija { get; set; }
        public virtual DbSet<Let> Let { get; set; }
        public virtual DbSet<Povlastice> Povlastice { get; set; }
        public virtual DbSet<Putnik> Putnik { get; set; }
        public virtual DbSet<PutnikPovlastice> PutnikPovlastice { get; set; }
        public virtual DbSet<Rezervacija> Rezervacija { get; set; }
        public virtual DbSet<Sluzbenik> Sluzbenik { get; set; }
        public virtual DbSet<Terminal> Terminal { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Aerodrom>(entity =>
            {
                entity.ToTable("aerodrom", "mydb");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Drzava)
                    .HasColumnName("drzava")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Grad)
                    .IsRequired()
                    .HasColumnName("grad")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnName("naziv")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Destinacija>(entity =>
            {
                entity.ToTable("destinacija", "mydb");

                entity.HasIndex(e => e.AerodromId)
                    .HasName("fk_Destinacija_Aerodrom1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.AerodromId)
                    .HasColumnName("Aerodrom_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Drzava)
                    .HasColumnName("drzava")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Grad)
                    .IsRequired()
                    .HasColumnName("grad")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Aerodrom)
                    .WithMany(p => p.Destinacija)
                    .HasForeignKey(d => d.AerodromId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Destinacija_Aerodrom1");
            });

            modelBuilder.Entity<Karta>(entity =>
            {
                entity.ToTable("karta", "mydb");

                entity.HasIndex(e => e.LetId)
                    .HasName("fk_Karta_Let1_idx");

                entity.HasIndex(e => e.PutnikId)
                    .HasName("fk_Karta_Putnik1_idx");

                entity.HasIndex(e => e.SluzbenikId)
                    .HasName("fk_Karta_sluzbenik1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BrojSjedista)
                    .IsRequired()
                    .HasColumnName("broj_sjedista")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Cijena)
                    .HasColumnName("cijena")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.DatumProdaje).HasColumnName("datum_prodaje");

                entity.Property(e => e.LetId)
                    .HasColumnName("Let_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Popust)
                    .HasColumnName("popust")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.PutnikId)
                    .HasColumnName("Putnik_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SluzbenikId)
                    .HasColumnName("Sluzbenik_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Storn)
                    .HasColumnName("storn")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Let)
                    .WithMany(p => p.Karta)
                    .HasForeignKey(d => d.LetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Karta_Let1");

                entity.HasOne(d => d.Putnik)
                    .WithMany(p => p.Karta)
                    .HasForeignKey(d => d.PutnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Karta_Putnik1");

                entity.HasOne(d => d.Sluzbenik)
                    .WithMany(p => p.Karta)
                    .HasForeignKey(d => d.SluzbenikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Karta_sluzbenik1");
            });

            modelBuilder.Entity<Kompanija>(entity =>
            {
                entity.ToTable("kompanija", "mydb");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnName("naziv")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Oznaka)
                    .IsRequired()
                    .HasColumnName("oznaka")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Sjediste)
                    .IsRequired()
                    .HasColumnName("sjediste")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Let>(entity =>
            {
                entity.ToTable("let", "mydb");

                entity.HasIndex(e => e.DestinacijaId)
                    .HasName("fk_Let_Destinacija1_idx");

                entity.HasIndex(e => e.KompanijaId)
                    .HasName("fk_Let_Avio-kompanija1_idx");

                entity.HasIndex(e => e.TerminalId)
                    .HasName("fk_Let_Terminal1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BrojLeta)
                    .HasColumnName("broj_leta")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BrojMjesta)
                    .HasColumnName("broj_mjesta")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DatumPolaska).HasColumnName("datum_polaska");

                entity.Property(e => e.DestinacijaId)
                    .HasColumnName("Destinacija_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.KompanijaId)
                    .HasColumnName("Kompanija_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TerminalId)
                    .HasColumnName("Terminal_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Povlastice>(entity =>
            {
                entity.ToTable("povlastice", "mydb");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Naziv)
                    .HasColumnName("naziv")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Procenat)
                    .HasColumnName("procenat")
                    .HasColumnType("decimal(10,0)");
            });

            modelBuilder.Entity<Putnik>(entity =>
            {
                entity.ToTable("putnik", "mydb");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BrojPasosa)
                    .IsRequired()
                    .HasColumnName("broj_pasosa")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DatumRodjenja).HasColumnName("datum_rodjenja");

                entity.Property(e => e.Ime)
                    .HasColumnName("ime")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Pol)
                    .HasColumnName("pol")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasColumnName("prezime")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PutnikPovlastice>(entity =>
            {
                entity.HasKey(e => new { e.PutnikId, e.PovlasticeId });

                entity.ToTable("putnik_povlastice", "mydb");

                entity.HasIndex(e => e.PovlasticeId)
                    .HasName("fk_Putnik_has_povlastice_povlastice1_idx");

                entity.HasIndex(e => e.PutnikId)
                    .HasName("fk_Putnik_has_povlastice_Putnik1_idx");

                entity.Property(e => e.PutnikId)
                    .HasColumnName("Putnik_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PovlasticeId)
                    .HasColumnName("Povlastice_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Povlastice)
                    .WithMany(p => p.PutnikPovlastice)
                    .HasForeignKey(d => d.PovlasticeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Putnik_has_povlastice_povlastice1");

                entity.HasOne(d => d.Putnik)
                    .WithMany(p => p.PutnikPovlastice)
                    .HasForeignKey(d => d.PutnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Putnik_has_povlastice_Putnik1");
            });

            modelBuilder.Entity<Rezervacija>(entity =>
            {
                entity.ToTable("rezervacija", "mydb");

                entity.HasIndex(e => e.KartaId)
                    .HasName("fk_Rezervacija_Karta1_idx");

                entity.HasIndex(e => e.LetId)
                    .HasName("fk_Rezervacija_Let1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.DatumRezervacije).HasColumnName("datum_rezervacije");

                entity.Property(e => e.KartaId)
                    .HasColumnName("Karta_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LetId)
                    .HasColumnName("Let_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Realizovana)
                    .HasColumnName("realizovana")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Storn)
                    .HasColumnName("storn")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.VazenjeRezervacije).HasColumnName("vazenje_rezervacije");

                entity.HasOne(d => d.Karta)
                    .WithMany(p => p.Rezervacija)
                    .HasForeignKey(d => d.KartaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Rezervacija_Karta1");

                entity.HasOne(d => d.Let)
                    .WithMany(p => p.Rezervacija)
                    .HasForeignKey(d => d.LetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Rezervacija_Let1");
            });

            modelBuilder.Entity<Sluzbenik>(entity =>
            {
                entity.ToTable("sluzbenik", "mydb");

                entity.HasIndex(e => e.KompanijaId)
                    .HasName("fk_sluzbenik_Avio-kompanija1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasColumnName("ime")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.KompanijaId)
                    .HasColumnName("Kompanija_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasColumnName("prezime")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.RadnoMjesto)
                    .HasColumnName("radno_mjesto")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Kompanija)
                    .WithMany(p => p.Sluzbenik)
                    .HasForeignKey(d => d.KompanijaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sluzbenik_Avio-kompanija1");
            });

            modelBuilder.Entity<Terminal>(entity =>
            {
                entity.ToTable("terminal", "mydb");

                entity.HasIndex(e => e.AerodromId)
                    .HasName("fk_Terminal_Aerodrom1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.AerodromId)
                    .HasColumnName("Aerodrom_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Naziv)
                    .HasColumnName("naziv")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Aerodrom)
                    .WithMany(p => p.Terminal)
                    .HasForeignKey(d => d.AerodromId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Terminal_Aerodrom1");
            });
        }
    }
}
