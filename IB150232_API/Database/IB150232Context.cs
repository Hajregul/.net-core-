using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IB150232_API.Database
{
    public partial class IB150232Context : DbContext
    {
        public IB150232Context()
        {
        }

        public IB150232Context(DbContextOptions<IB150232Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Klijenti> Klijenti { get; set; }
        public virtual DbSet<Komentari> Komentari { get; set; }
        public virtual DbSet<Korisnici> Korisnici { get; set; }
        public virtual DbSet<KorisniciUloge> KorisniciUloge { get; set; }
        public virtual DbSet<Lokacije> Lokacije { get; set; }
        public virtual DbSet<Ocjene> Ocjene { get; set; }
        public virtual DbSet<Rezervacije> Rezervacije { get; set; }
        public virtual DbSet<RezervacijeStavke> RezervacijeStavke { get; set; }
        public virtual DbSet<Stanovi> Stanovi { get; set; }
        public virtual DbSet<Uloge> Uloge { get; set; }
        public virtual DbSet<ZahtjeviZaRezervaciju> ZahtjeviZaRezervaciju { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-S0I4096\\SQLEXPRESS;Database=IB150232;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Klijenti>(entity =>
            {
                entity.HasKey(e => e.KlijentId);

                entity.Property(e => e.KlijentId).HasColumnName("KlijentID");

                entity.Property(e => e.Adresa).HasMaxLength(50);

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.KorisnickoIme)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LozinkaHash)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LozinkaSalt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Komentari>(entity =>
            {
                entity.HasKey(e => e.KomenatarId);

                entity.Property(e => e.KomenatarId).HasColumnName("KomenatarID");

                entity.Property(e => e.Datum).HasColumnType("date");

                entity.Property(e => e.KlijentId).HasColumnName("KlijentID");

                entity.Property(e => e.Komentar).IsRequired();

                entity.Property(e => e.StanId).HasColumnName("StanID");

                entity.HasOne(d => d.Klijent)
                    .WithMany(p => p.Komentari)
                    .HasForeignKey(d => d.KlijentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Komentari__Klije__18EBB532");

                entity.HasOne(d => d.Stan)
                    .WithMany(p => p.Komentari)
                    .HasForeignKey(d => d.StanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Komentari__StanI__17F790F9");
            });

            modelBuilder.Entity<Korisnici>(entity =>
            {
                entity.HasKey(e => e.KorisnikId);

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.Property(e => e.Adresa).HasMaxLength(100);

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.KorisnickoIme).HasMaxLength(50);

                entity.Property(e => e.LozinkaHash).HasMaxLength(50);

                entity.Property(e => e.LozinkaSalt).HasMaxLength(50);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<KorisniciUloge>(entity =>
            {
                entity.HasKey(e => e.KorisnikUlogaId);

                entity.Property(e => e.DatumIzmjene).HasColumnType("datetime");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.KorisniciUloge)
                    .HasForeignKey(d => d.KorisnikId)
                    .HasConstraintName("FK__Korisnici__Koris__2A164134");

                entity.HasOne(d => d.Uloga)
                    .WithMany(p => p.KorisniciUloge)
                    .HasForeignKey(d => d.UlogaId)
                    .HasConstraintName("FK__Korisnici__Uloga__29221CFB");
            });

            modelBuilder.Entity<Lokacije>(entity =>
            {
                entity.HasKey(e => e.LokacijaId);

                entity.Property(e => e.LokacijaId).HasColumnName("LokacijaID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ocjene>(entity =>
            {
                entity.HasKey(e => e.OcjenaId);

                entity.Property(e => e.OcjenaId).HasColumnName("OcjenaID");

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.Property(e => e.KlijentId).HasColumnName("KlijentID");

                entity.Property(e => e.StanId).HasColumnName("StanID");

                entity.HasOne(d => d.Klijent)
                    .WithMany(p => p.Ocjene)
                    .HasForeignKey(d => d.KlijentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ocjene__KlijentI__151B244E");

                entity.HasOne(d => d.Stan)
                    .WithMany(p => p.Ocjene)
                    .HasForeignKey(d => d.StanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ocjene__StanID__14270015");
            });

            modelBuilder.Entity<Rezervacije>(entity =>
            {
                entity.HasKey(e => e.RezervacijaId);

                entity.Property(e => e.RezervacijaId).HasColumnName("RezervacijaID");

                entity.Property(e => e.BrojRezervacije)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Datum).HasColumnType("date");

                entity.Property(e => e.KlijentId).HasColumnName("KlijentID");

                entity.HasOne(d => d.Klijent)
                    .WithMany(p => p.Rezervacije)
                    .HasForeignKey(d => d.KlijentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rezervaci__Klije__1BC821DD");
            });

            modelBuilder.Entity<RezervacijeStavke>(entity =>
            {
                entity.HasKey(e => e.RezervacijaStavkeId);

                entity.Property(e => e.RezervacijaStavkeId).HasColumnName("RezervacijaStavkeID");

                entity.Property(e => e.RezervacijaId).HasColumnName("RezervacijaID");

                entity.Property(e => e.StanId).HasColumnName("StanID");

                entity.HasOne(d => d.Rezervacija)
                    .WithMany(p => p.RezervacijeStavke)
                    .HasForeignKey(d => d.RezervacijaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rezervaci__Rezer__1F98B2C1");

                entity.HasOne(d => d.Stan)
                    .WithMany(p => p.RezervacijeStavke)
                    .HasForeignKey(d => d.StanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rezervaci__StanI__1EA48E88");
            });

            modelBuilder.Entity<Stanovi>(entity =>
            {
                entity.HasKey(e => e.StanId);

                entity.Property(e => e.StanId).HasColumnName("StanID");

                entity.Property(e => e.Adresa)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Cijena).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.Property(e => e.LokacijaId).HasColumnName("LokacijaID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tv).HasColumnName("TV");

                entity.Property(e => e.Velicina).HasMaxLength(1);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Stanovi)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stanovi__Korisni__10566F31");

                entity.HasOne(d => d.Lokacija)
                    .WithMany(p => p.Stanovi)
                    .HasForeignKey(d => d.LokacijaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stanovi__Lokacij__114A936A");
            });

            modelBuilder.Entity<Uloge>(entity =>
            {
                entity.HasKey(e => e.UlogaId);

                entity.Property(e => e.UlogaId).HasColumnName("UlogaID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ZahtjeviZaRezervaciju>(entity =>
            {
                entity.Property(e => e.ZahtjeviZaRezervacijuId).HasColumnName("ZahtjeviZaRezervacijuID");

                entity.Property(e => e.Datum).HasColumnType("date");

                entity.Property(e => e.KlijentId).HasColumnName("KlijentID");

                entity.Property(e => e.StanId).HasColumnName("StanID");

                entity.HasOne(d => d.Klijent)
                    .WithMany(p => p.ZahtjeviZaRezervaciju)
                    .HasForeignKey(d => d.KlijentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ZahtjeviZ__Klije__236943A5");

                entity.HasOne(d => d.Stan)
                    .WithMany(p => p.ZahtjeviZaRezervaciju)
                    .HasForeignKey(d => d.StanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ZahtjeviZ__StanI__22751F6C");
            });
        }
    }
}
