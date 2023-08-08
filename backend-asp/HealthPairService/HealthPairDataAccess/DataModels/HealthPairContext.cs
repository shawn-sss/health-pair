using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace HealthPairDataAccess.DataModels
{
    [ExcludeFromCodeCoverage]
    public class HealthPairContext : DbContext
    {
        public HealthPairContext(DbContextOptions<HealthPairContext> options) : base(options)
        {
        }

        public DbSet<Data_Patient> Patients { get; set; }
        public DbSet<Data_Appointment> Appointments { get; set; }
        public DbSet<Data_Insurance> Insurances { get; set; }
        public DbSet<Data_Provider> Providers { get; set; }
        public DbSet<Data_Specialty> Specialties { get; set; }
        public DbSet<Data_Facility> Facilities { get; set; }
        public DbSet<Data_InsuranceProvider> InsuranceProviders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Data_Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId)
                    .IsRequired();
                entity.HasIndex(e => e.PatientId);
                entity.HasIndex(e => e.ProviderId);
                entity.Property(e => e.AppointmentDate)
                    .IsRequired();
                entity.HasOne(e => e.Patient)
                    .WithMany(e => e.Appointments)
                    .HasForeignKey(e => e.PatientId);
                entity.HasOne(e => e.Provider)
                    .WithMany(e => e.Appointments)
                    .HasForeignKey(e => e.ProviderId);
            });
            modelBuilder.Entity<Data_Appointment>().HasData(
                new Data_Appointment()
                {
                    AppointmentId = 1,
                    PatientId = 3,
                    ProviderId = 1,
                    AppointmentDate = new DateTime(2020, 4, 29)
                },
                new Data_Appointment()
                {
                    AppointmentId = 2,
                    PatientId = 2,
                    ProviderId = 3,
                    AppointmentDate = new DateTime(2020, 6, 29)
                },
                new Data_Appointment()
                {
                    AppointmentId = 3,
                    PatientId = 1,
                    ProviderId = 2,
                    AppointmentDate = new DateTime(2020, 5, 29)
                }
            );

            modelBuilder.Entity<Data_Facility>(entity =>
            {
                entity.Property(e => e.FacilityId)
                    .IsRequired();
                entity.Property(e => e.FacilityName)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.FacilityAddress1)
                    .IsRequired()
                    .HasMaxLength(120);
                entity.Property(e => e.FacilityCity)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.FacilityState)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.FacilityZipcode)
                    .IsRequired();
                entity.Property(e => e.FacilityPhoneNumber)
                    .IsRequired();
            });
            modelBuilder.Entity<Data_Facility>().HasData(
                new Data_Facility()
                {
                    FacilityId = 1,
                    FacilityName = "Houston Methodist San Jacinto Hospital Alexander Campus",
                    FacilityAddress1 = "1700 James Bowie Drive",
                    FacilityCity = "Baytown",
                    FacilityState = "TX",
                    FacilityZipcode = 77520,
                    FacilityPhoneNumber = 2814208765
                },
                new Data_Facility()
                {
                FacilityId = 2,
                FacilityName = "Providence Hospital of North Houston LLC",
                FacilityAddress1 = "16750 Red Oak Drive",
                FacilityCity = "Houston",
                FacilityState = "TX",
                FacilityZipcode = 77090,
                FacilityPhoneNumber = 2814537110
                },
                new Data_Facility()
                {
                FacilityId = 3,
                FacilityName = "McCallen Medical Center",
                FacilityAddress1 = "301 West Expressway 83",
                FacilityCity = "McAllen",
                FacilityState = "TX",
                FacilityZipcode = 78590,
                FacilityPhoneNumber = 9566324000
                },
                new Data_Facility()
                {
                FacilityId = 4,
                FacilityName = "Faith Community Hospital",
                FacilityAddress1 = "215 Chisholm Trail",
                FacilityCity = "Jacksboro",
                FacilityState = "TX",
                FacilityZipcode = 76458,
                FacilityPhoneNumber = 9405676633
                },
                new Data_Facility()
                {
                FacilityId = 5,
                FacilityName = "Covenant Medical Center – Lakeside",
                FacilityAddress1 = "4000 24th Street",
                FacilityCity = "Lubbock",
                FacilityState = "TX",
                FacilityZipcode = 79410,
                FacilityPhoneNumber = 8067250536
                },
                new Data_Facility()
                {
                FacilityId = 6,
                FacilityName = "Woodlands Specialty Hospital",
                FacilityAddress1 = "25440 I 45 North",
                FacilityCity = "The Woodlands",
                FacilityState = "TX",
                FacilityZipcode = 77386,
                FacilityPhoneNumber = 2816028160
                },
                new Data_Facility()
                {
                FacilityId = 7,
                FacilityName = "Christus Spohn Hospital Corpus Christi Shoreline",
                FacilityAddress1 = "600 Elizabeth Street",
                FacilityCity = "Corpus Christi",
                FacilityState = "TX",
                FacilityZipcode = 78404,
                FacilityPhoneNumber = 3619024690
                },
                new Data_Facility()
                {
                FacilityId = 8,
                FacilityName = "The Corpus Christi Medical Center – Northwest",
                FacilityAddress1 = "13725 Northwest Blvd",
                FacilityCity = "Corpus Christi",
                FacilityState = "TX",
                FacilityZipcode = 78410,
                FacilityPhoneNumber = 3617674300
                },
                new Data_Facility()
                {
                FacilityId = 9,
                FacilityName = "Reagan Memorial Hospital",
                FacilityAddress1 = "1300 N Main Ave",
                FacilityCity = "Big Lake",
                FacilityState = "TX",
                FacilityZipcode = 76932,
                FacilityPhoneNumber = 3258842561
                },
                new Data_Facility()
                {
                FacilityId = 10,
                FacilityName = "Baylor Emergency Medical Center",
                FacilityAddress1 = "1975 Alpha, Suite 100",
                FacilityCity = "Rockwall",
                FacilityState = "TX",
                FacilityZipcode = 75087,
                FacilityPhoneNumber = 2142946200
                },
                new Data_Facility()
                {
                FacilityId = 11,
                FacilityName = "Baylor Emergency Medical Center",
                FacilityAddress1 = "620 South Main, Suite 100",
                FacilityCity = "Keller",
                FacilityState = "TX",
                FacilityZipcode = 76248,
                FacilityPhoneNumber = 2142946100
                },
                new Data_Facility()
                {
                FacilityId = 12,
                FacilityName = "Baylor Emergency Medical Center",
                FacilityAddress1 = "12500 South Freeway, Suite 100",
                FacilityCity = "Burleson",
                FacilityState = "TX",
                FacilityZipcode = 76028,
                FacilityPhoneNumber = 2142946250
                },
                new Data_Facility()
                {
                FacilityId = 13,
                FacilityName = "Baylor Emergency Medical Center",
                FacilityAddress1 = "1776 North Us 287, Suite 100",
                FacilityCity = "Mansfield",
                FacilityState = "TX",
                FacilityZipcode = 76063,
                FacilityPhoneNumber = 2142946300
                },
                new Data_Facility()
                {
                FacilityId = 14,
                FacilityName = "Baylor Emergency Medical Center",
                FacilityAddress1 = "5500 Colleyville Boulevard",
                FacilityCity = "Colleyville",
                FacilityState = "TX",
                FacilityZipcode = 76034,
                FacilityPhoneNumber = 2142946350
                },
                new Data_Facility()
                {
                FacilityId = 15,
                FacilityName = "Medical Center of Alliance",
                FacilityAddress1 = "3101 North Tarrant Parkway",
                FacilityCity = "Fort Worth",
                FacilityState = "TX",
                FacilityZipcode = 76177,
                FacilityPhoneNumber = 8176391100
                }
            );

            modelBuilder.Entity<Data_Insurance>(entity =>
            {
                entity.Property(e => e.InsuranceId)
                    .IsRequired();
                entity.Property(e => e.InsuranceName)
                    .IsRequired()
                    .HasMaxLength(80);
            });
            modelBuilder.Entity<Data_Insurance>().HasData(
                new Data_Insurance()
                {
                    InsuranceId = 1,
                    InsuranceName = "Cigna"
                },
                new Data_Insurance()
                {
                    InsuranceId = 2,
                    InsuranceName = "Humana"
                },
                new Data_Insurance()
                {
                    InsuranceId = 3,
                    InsuranceName = "Community"
                },
                new Data_Insurance()
                {
                    InsuranceId = 4,
                    InsuranceName = "Molina"
                },
                new Data_Insurance()
                {
                    InsuranceId = 5,
                    InsuranceName = "FirstCare"
                },
                new Data_Insurance()
                {
                    InsuranceId = 6,
                    InsuranceName = "Superior"
                },
                new Data_Insurance()
                {
                    InsuranceId = 7,
                    InsuranceName = "Christus"
                },
                new Data_Insurance()
                {
                    InsuranceId = 8,
                    InsuranceName = "Celtic"
                },
                new Data_Insurance()
                {
                    InsuranceId = 9,
                    InsuranceName = "Sendero"
                },
                new Data_Insurance()
                {
                    InsuranceId = 10,
                    InsuranceName = "EmblemHealth"
                },
                new Data_Insurance()
                {
                    InsuranceId = 11,
                    InsuranceName = "Wellcare"
                },
                new Data_Insurance()
                {
                    InsuranceId = 12,
                    InsuranceName = "CVS"
                },
                new Data_Insurance()
                {
                    InsuranceId = 13,
                    InsuranceName = "Independence Health Group Inc."
                },
                new Data_Insurance()
                {
                    InsuranceId = 14,
                    InsuranceName = "Highmark Group"
                },
                new Data_Insurance()
                {
                    InsuranceId = 15,
                    InsuranceName = "None (Cash)"
                }
            );

            modelBuilder.Entity<Data_InsuranceProvider>(entity =>
            {
                entity.Property(e => e.IPId)
                    .IsRequired();
                entity.HasIndex(e => e.InsuranceId);
                entity.HasIndex(e => e.ProviderId);
                entity.HasOne(bc => bc.Insurance)
                    .WithMany(b => b.InsuranceProviders)
                    .HasForeignKey(bc => bc.InsuranceId);
                entity.HasOne(bc => bc.Provider)
                    .WithMany(b => b.InsuranceProviders)
                    .HasForeignKey(bc => bc.ProviderId);
            });
            modelBuilder.Entity<Data_InsuranceProvider>().HasData(
// ! insurance 1
                // new Data_InsuranceProvider()
                // {
                //     IPId = 1,
                //     InsuranceId = 1,
                //     ProviderId = 1
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 2,
                    InsuranceId = 1,
                    ProviderId = 2
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 3,
                //     InsuranceId = 1,
                //     ProviderId = 3
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 46,
                    InsuranceId = 1,
                    ProviderId = 4
                },
                new Data_InsuranceProvider()
                {
                    IPId = 47,
                    InsuranceId = 1,
                    ProviderId = 5
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 48,
                //     InsuranceId = 1,
                //     ProviderId = 6
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 49,
                    InsuranceId = 1,
                    ProviderId = 7
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 50,
                //     InsuranceId = 1,
                //     ProviderId = 8
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 51,
                //     InsuranceId = 1,
                //     ProviderId = 9
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 52,
                    InsuranceId = 1,
                    ProviderId = 10
                },
                new Data_InsuranceProvider()
                {
                    IPId = 53,
                    InsuranceId = 1,
                    ProviderId = 11
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 54,
                //     InsuranceId = 1,
                //     ProviderId = 12
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 55,
                    InsuranceId = 1,
                    ProviderId = 13
                },
                new Data_InsuranceProvider()
                {
                    IPId = 56,
                    InsuranceId = 1,
                    ProviderId = 14
                },
                new Data_InsuranceProvider()
                {
                    IPId = 57,
                    InsuranceId = 1,
                    ProviderId = 15
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 58,
                //     InsuranceId = 1,
                //     ProviderId = 16
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 59,
                    InsuranceId = 1,
                    ProviderId = 17
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 60,
                //     InsuranceId = 1,
                //     ProviderId = 18
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 61,
                //     InsuranceId = 1,
                //     ProviderId = 19
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 62,
                    InsuranceId = 1,
                    ProviderId = 20
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 63,
                //     InsuranceId = 1,
                //     ProviderId = 21
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 64,
                //     InsuranceId = 1,
                //     ProviderId = 22
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 65,
                //     InsuranceId = 1,
                //     ProviderId = 23
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 66,
                    InsuranceId = 1,
                    ProviderId = 24
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 67,
                //     InsuranceId = 1,
                //     ProviderId = 25
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 68,
                //     InsuranceId = 1,
                //     ProviderId = 26
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 69,
                    InsuranceId = 1,
                    ProviderId = 27
                },
                new Data_InsuranceProvider()
                {
                    IPId = 70,
                    InsuranceId = 1,
                    ProviderId = 28
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 71,
                //     InsuranceId = 1,
                //     ProviderId = 29
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 72,
                //     InsuranceId = 1,
                //     ProviderId = 30
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 73,
                    InsuranceId = 1,
                    ProviderId = 31
                },
                new Data_InsuranceProvider()
                {
                    IPId = 74,
                    InsuranceId = 1,
                    ProviderId = 32
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 75,
                //     InsuranceId = 1,
                //     ProviderId = 33
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 76,
                    InsuranceId = 1,
                    ProviderId = 34
                },
                new Data_InsuranceProvider()
                {
                    IPId = 77,
                    InsuranceId = 1,
                    ProviderId = 35
                },
                new Data_InsuranceProvider()
                {
                    IPId = 78,
                    InsuranceId = 1,
                    ProviderId = 36
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 79,
                //     InsuranceId = 1,
                //     ProviderId = 37
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 80,
                //     InsuranceId = 1,
                //     ProviderId = 38
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 81,
                //     InsuranceId = 1,
                //     ProviderId = 39
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 82,
                    InsuranceId = 1,
                    ProviderId = 40
                },
// ! Insurance 2
                new Data_InsuranceProvider()
                {
                    IPId = 4,
                    InsuranceId = 2,
                    ProviderId = 1
                },
                new Data_InsuranceProvider()
                {
                    IPId = 5,
                    InsuranceId = 2,
                    ProviderId = 2
                },
                new Data_InsuranceProvider()
                {
                    IPId = 6,
                    InsuranceId = 2,
                    ProviderId = 3
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 86,
                //     InsuranceId = 2,
                //     ProviderId = 4
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 87,
                //     InsuranceId = 2,
                //     ProviderId = 5
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 88,
                //     InsuranceId = 2,
                //     ProviderId = 6
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 89,
                //     InsuranceId = 2,
                //     ProviderId = 7
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 90,
                    InsuranceId = 2,
                    ProviderId = 8
                },
                new Data_InsuranceProvider()
                {
                    IPId = 91,
                    InsuranceId = 2,
                    ProviderId = 9
                },
                new Data_InsuranceProvider()
                {
                    IPId = 92,
                    InsuranceId = 2,
                    ProviderId = 10
                },
                new Data_InsuranceProvider()
                {
                    IPId = 93,
                    InsuranceId = 2,
                    ProviderId = 11
                },
                new Data_InsuranceProvider()
                {
                    IPId = 94,
                    InsuranceId = 2,
                    ProviderId = 12
                },
                new Data_InsuranceProvider()
                {
                    IPId = 95,
                    InsuranceId = 2,
                    ProviderId = 13
                },
                new Data_InsuranceProvider()
                {
                    IPId = 96,
                    InsuranceId = 2,
                    ProviderId = 14
                },
                new Data_InsuranceProvider()
                {
                    IPId = 97,
                    InsuranceId = 2,
                    ProviderId = 15
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 98,
                //     InsuranceId = 2,
                //     ProviderId = 16
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 99,
                //     InsuranceId = 2,
                //     ProviderId = 17
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 100,
                    InsuranceId = 2,
                    ProviderId = 18
                },
                new Data_InsuranceProvider()
                {
                    IPId = 101,
                    InsuranceId = 2,
                    ProviderId = 19
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 102,
                //     InsuranceId = 2,
                //     ProviderId = 20
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 103,
                //     InsuranceId = 2,
                //     ProviderId = 21
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 104,
                //     InsuranceId = 2,
                //     ProviderId = 22
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 105,
                //     InsuranceId = 2,
                //     ProviderId = 23
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 106,
                //     InsuranceId = 2,
                //     ProviderId = 24
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 107,
                    InsuranceId = 2,
                    ProviderId = 25
                },
                new Data_InsuranceProvider()
                {
                    IPId = 108,
                    InsuranceId = 2,
                    ProviderId = 26
                },
                new Data_InsuranceProvider()
                {
                    IPId = 109,
                    InsuranceId = 2,
                    ProviderId = 27
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 110,
                //     InsuranceId = 2,
                //     ProviderId = 28
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 111,
                //     InsuranceId = 2,
                //     ProviderId = 29
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 112,
                //     InsuranceId = 2,
                //     ProviderId = 30
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 113,
                //     InsuranceId = 2,
                //     ProviderId = 31
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 114,
                //     InsuranceId = 2,
                //     ProviderId = 32
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 115,
                //     InsuranceId = 2,
                //     ProviderId = 33
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 116,
                //     InsuranceId = 2,
                //     ProviderId = 34
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 117,
                    InsuranceId = 2,
                    ProviderId = 35
                },
                new Data_InsuranceProvider()
                {
                    IPId = 118,
                    InsuranceId = 2,
                    ProviderId = 36
                },
                new Data_InsuranceProvider()
                {
                    IPId = 119,
                    InsuranceId = 2,
                    ProviderId = 37
                },
                new Data_InsuranceProvider()
                {
                    IPId = 120,
                    InsuranceId = 2,
                    ProviderId = 38
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 121,
                //     InsuranceId = 2,
                //     ProviderId = 39
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 122,
                //     InsuranceId = 2,
                //     ProviderId = 40
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 7,
                    InsuranceId = 3,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 8,
                //     InsuranceId = 3,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 9,
                    InsuranceId = 3,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 10,
                    InsuranceId = 4,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 11,
                //     InsuranceId = 4,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 12,
                    InsuranceId = 4,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 13,
                    InsuranceId = 5,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 14,
                //     InsuranceId = 5,
                //     ProviderId = 2
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 15,
                //     InsuranceId = 5,
                //     ProviderId = 3
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 16,
                    InsuranceId = 6,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 17,
                //     InsuranceId = 6,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 18,
                    InsuranceId = 6,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 19,
                    InsuranceId = 7,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 20,
                //     InsuranceId = 7,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 21,
                    InsuranceId = 7,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 22,
                    InsuranceId = 8,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 23,
                //     InsuranceId = 8,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 24,
                    InsuranceId = 8,
                    ProviderId = 3
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 25,
                //     InsuranceId = 9,
                //     ProviderId = 1
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 26,
                    InsuranceId = 9,
                    ProviderId = 2
                },
                new Data_InsuranceProvider()
                {
                    IPId = 27,
                    InsuranceId = 9,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 28,
                    InsuranceId = 10,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 29,
                //     InsuranceId = 10,
                //     ProviderId = 2
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 30,
                //     InsuranceId = 10,
                //     ProviderId = 3
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 31,
                    InsuranceId = 11,
                    ProviderId = 1
                },
                new Data_InsuranceProvider()
                {
                    IPId = 32,
                    InsuranceId = 11,
                    ProviderId = 2
                },
                new Data_InsuranceProvider()
                {
                    IPId = 33,
                    InsuranceId = 11,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 34,
                    InsuranceId = 12,
                    ProviderId = 1
                },
                new Data_InsuranceProvider()
                {
                    IPId = 35,
                    InsuranceId = 12,
                    ProviderId = 2
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 36,
                //     InsuranceId = 12,
                //     ProviderId = 3
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 37,
                //     InsuranceId = 13,
                //     ProviderId = 1
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 38,
                    InsuranceId = 13,
                    ProviderId = 2
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 39,
                //     InsuranceId = 13,
                //     ProviderId = 3
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 40,
                //     InsuranceId = 14,
                //     ProviderId = 1
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 41,
                    InsuranceId = 14,
                    ProviderId = 2
                },
                new Data_InsuranceProvider()
                {
                    IPId = 42,
                    InsuranceId = 14,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 43,
                    InsuranceId = 15,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 44,
                //     InsuranceId = 15,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 45,
                    InsuranceId = 15,
                    ProviderId = 3
                },
// ! EDRFTVGHBJNOWSZEXDCFVGBNJMKL<HDXCFVGBHNJK<ML>
// ! EDRFTVGHBJNOWSZEXDCFVGBNJMKL<HDXCFVGBHNJK<ML>
// ! EDRFTVGHBJNOWSZEXDCFVGBNJMKL<HDXCFVGBHNJK<ML>
                new Data_InsuranceProvider()
                {
                    IPId = 83,
                    InsuranceId = 1,
                    ProviderId = 41
                },
                new Data_InsuranceProvider()
                {
                    IPId = 84,
                    InsuranceId = 1,
                    ProviderId = 42
                },
                new Data_InsuranceProvider()
                {
                    IPId = 85,
                    InsuranceId = 1,
                    ProviderId = 43
                }
            );

            modelBuilder.Entity<Data_Patient>(entity =>
            {
                entity.Property(e => e.PatientId)
                    .IsRequired();
                entity.HasIndex(e => e.InsuranceId);
                entity.Property(e => e.PatientFirstName)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.PatientLastName)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.PatientPassword)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.PatientAddress1)
                    .IsRequired()
                    .HasMaxLength(120);
                entity.Property(e => e.PatientCity)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.PatientState)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.PatientZipcode)
                    .IsRequired();
                entity.Property(e => e.PatientBirthDay)
                    .IsRequired();
                entity.Property(e => e.PatientPhoneNumber)
                    .IsRequired();
                entity.Property(e => e.PatientEmail)
                    .IsRequired()
                    .HasMaxLength(120);
                entity.Property(e => e.IsAdmin)
                    .IsRequired()
                    .HasDefaultValue(false);
            });
            modelBuilder.Entity<Data_Patient>().HasData(
                new Data_Patient()
                {
	                PatientId = 1,
	                InsuranceId = 1,
	                PatientFirstName = "Jared",
	                PatientLastName = "Blankenship",
	                PatientPassword = "Password1",
	                PatientAddress1 = "8401 Ronnie St",
	                PatientCity = "White Settlement",
	                PatientState = "TX",
	                PatientZipcode = 76108,
	                PatientBirthDay = new DateTime(2000, 4, 12),
	                PatientPhoneNumber = 7167787419,
	                PatientEmail = "ifawdry0@si.edu",
	                IsAdmin = true
                },
                new Data_Patient()
                {
                    PatientId = 2,
                    InsuranceId = 3,
                    PatientFirstName = "Geoffry",
                    PatientLastName = "Haggner",
                    PatientPassword = "Password2",
                    PatientAddress1 = "2500 Victory Ave",
                    PatientCity = "Dallas", PatientState = "TX",
                    PatientZipcode = 75201,
                    PatientBirthDay = new DateTime(1988,  11, 30),
                    PatientPhoneNumber = 3619033062,
                    PatientEmail = "wotimony1@shop-pro.jp",
                    IsAdmin = true
                },
                new Data_Patient()
                {
                    PatientId = 3,
                    InsuranceId = 5,
                    PatientFirstName = "Zack",
                    PatientLastName = "Tielde",
                    PatientPassword = "Password3",
                    PatientAddress1 = "5919 Peg Street",
                    PatientCity = "Houston",
                    PatientState = "TX",
                    PatientZipcode = 77092,
                    PatientBirthDay = new DateTime(1010, 10,  31),
                    PatientPhoneNumber = 2452488647,
                    PatientEmail = "lsiward2@cnbc.com",
                    IsAdmin = false
                },
                new Data_Patient()
                {
                    PatientId = 4,
                    InsuranceId = 7,
                    PatientFirstName = "Josh",
                    PatientLastName = "Rollins",
                    PatientPassword = "Password4",
                    PatientAddress1 = "10810 Spring Cypress Rd.",
                    PatientCity = "Tomball", PatientState = "TX",
                    PatientZipcode = 77375,
                    PatientBirthDay = new DateTime(1999, 1, 9),
                    PatientPhoneNumber = 8823701130,
                    PatientEmail = "mindgs3@microsoft.com",
                    IsAdmin = false
                },
                new Data_Patient()
                {
                    PatientId = 6,
                    InsuranceId = 1,
                    PatientFirstName = "TestPatientFirstName",
                    PatientLastName = "TestPatientLastName",
                    PatientPassword = "TestPassword",
                    PatientAddress1 = "123 Test Street",
                    PatientCity = "Test City",
                    PatientState = "Test State",
                    PatientZipcode = 12345,
                    PatientBirthDay = new DateTime(2000, 1, 1),
                    PatientPhoneNumber = 1234567890,
                    PatientEmail = "TestEmail@test.com",
                    IsAdmin = true
                }
            );

            modelBuilder.Entity<Data_Provider>(entity =>
            {
                entity.Property(e => e.ProviderId)
                    .IsRequired();
                entity.HasIndex(e => e.FacilityId);
                entity.HasIndex(e => e.SpecialtyId);
                entity.Property(e => e.ProviderFirstName)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.ProviderLastName)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.ProviderPhoneNumber)
                    .IsRequired();
                entity.HasOne(p => p.Facility)
                    .WithMany(p => p.Providers)
                    .HasForeignKey(p => p.FacilityId);
                entity.HasOne(p => p.Specialty)
                    .WithMany(p => p.Providers)
                    .HasForeignKey(p => p.SpecialtyId);
            });
            modelBuilder.Entity<Data_Provider>().HasData(
                new Data_Provider
                {
                    ProviderId = 1,
                    FacilityId = 1,
                    SpecialtyId = 6,
                    ProviderFirstName = "Josh",
                    ProviderLastName = "Rollins",
                    ProviderPhoneNumber = 7167787419
                },
                new Data_Provider
                {
                    ProviderId = 2,
                    FacilityId = 2,
                    SpecialtyId = 13,
                    ProviderFirstName = "Timmy",
                    ProviderLastName = "Harbringer",
                    ProviderPhoneNumber = 3619033062
                },
                new Data_Provider
                {
                    ProviderId = 3,
                    FacilityId = 3,
                    SpecialtyId = 5,
                    ProviderFirstName = "Joshua",
                    ProviderLastName = "Relnier",
                    ProviderPhoneNumber = 2452488647
                },
                new Data_Provider
                {
                    ProviderId = 4,
                    FacilityId = 4,
                    SpecialtyId = 8,
                    ProviderFirstName = "Maurice",
                    ProviderLastName = "Solste",
                    ProviderPhoneNumber = 8823701130
                },
                new Data_Provider
                {
                    ProviderId = 5,
                    FacilityId = 5,
                    SpecialtyId = 11,
                    ProviderFirstName = "Jared",
                    ProviderLastName = "Morris",
                    ProviderPhoneNumber = 9016226625
                },
                new Data_Provider
                {
                    ProviderId = 6,
                    FacilityId = 6,
                    SpecialtyId = 4,
                    ProviderFirstName = "Maury",
                    ProviderLastName = "Chitter",
                    ProviderPhoneNumber = 2577977019
                },
                new Data_Provider
                {
                    ProviderId = 7,
                    FacilityId = 7,
                    SpecialtyId = 8,
                    ProviderFirstName = "Philomena",
                    ProviderLastName = "Capon",
                    ProviderPhoneNumber = 2834986226
                },
                new Data_Provider
                {
                    ProviderId = 8,
                    FacilityId = 8,
                    SpecialtyId = 4,
                    ProviderFirstName = "Michail",
                    ProviderLastName= "Minkin",
                    ProviderPhoneNumber = 4487593448
                },
                new Data_Provider
                {
                    ProviderId = 9,
                    FacilityId = 9,
                    SpecialtyId = 2,
                    ProviderFirstName = "Boothe",
                    ProviderLastName = "Gurrado",
                    ProviderPhoneNumber = 9373233203
                },
                new Data_Provider
                {
                    ProviderId = 10,
                    FacilityId = 10,
                    SpecialtyId = 1,
                    ProviderFirstName = "Kendall",
                    ProviderLastName = "Mulqueen",
                    ProviderPhoneNumber = 9033544326
                },
                new Data_Provider
                {
                    ProviderId = 11,
                    FacilityId = 11,
                    SpecialtyId = 14,
                    ProviderFirstName = "Onfre",
                    ProviderLastName = "Grazier",
                    ProviderPhoneNumber = 8503233858
                },
                new Data_Provider
                {
                    ProviderId = 12,
                    FacilityId = 12,
                    SpecialtyId = 8,
                    ProviderFirstName = "Mauricio",
                    ProviderLastName = "Rowes",
                    ProviderPhoneNumber = 3259067361
                },
                new Data_Provider
                {
                    ProviderId = 13,
                    FacilityId = 13,
                    SpecialtyId = 6,
                    ProviderFirstName = "Elana",
                    ProviderLastName = "Dollman",
                    ProviderPhoneNumber = 8696441600
                },
                new Data_Provider
                {
                    ProviderId = 14,
                    FacilityId = 14,
                    SpecialtyId = 10,
                    ProviderFirstName = "Georg",
                    ProviderLastName = "Yakunchikov",
                    ProviderPhoneNumber = 3464020514
                },
                new Data_Provider
                {
                    ProviderId = 15,
                    FacilityId = 15,
                    SpecialtyId = 3,
                    ProviderFirstName = "Nanny",
                    ProviderLastName = "Stead",
                    ProviderPhoneNumber = 7705745366
                },
                new Data_Provider
                {
                    ProviderId = 16,
                    FacilityId = 1,
                    SpecialtyId = 8,
                    ProviderFirstName = "Jorie",
                    ProviderLastName = "Atwool",
                    ProviderPhoneNumber = 9363687510
                },
                new Data_Provider
                {
                    ProviderId = 17,
                    FacilityId = 2,
                    SpecialtyId = 11,
                    ProviderFirstName = "Alfons",
                    ProviderLastName = "Shee",
                    ProviderPhoneNumber = 6445427083
                },
                new Data_Provider
                {
                    ProviderId = 18,
                    FacilityId = 3,
                    SpecialtyId = 7,
                    ProviderFirstName = "Netta",
                    ProviderLastName = "Fincken",
                    ProviderPhoneNumber = 5252048919
                },
                new Data_Provider
                {
                    ProviderId = 19,
                    FacilityId = 4,
                    SpecialtyId = 14,
                    ProviderFirstName = "Melli",
                    ProviderLastName = "Hansford",
                    ProviderPhoneNumber = 5024459183
                },
                new Data_Provider
                {
                    ProviderId = 20,
                    FacilityId = 5,
                    SpecialtyId = 14,
                    ProviderFirstName = "Kathryne",
                    ProviderLastName = "Pawlaczyk",
                    ProviderPhoneNumber = 6981610466
                },
                new Data_Provider
                {
                    ProviderId = 21,
                    FacilityId = 6,
                    SpecialtyId = 13,
                    ProviderFirstName = "Dex",
                    ProviderLastName = "Rawstron",
                    ProviderPhoneNumber = 5243227555
                },
                new Data_Provider
                {
                    ProviderId = 22,
                    FacilityId = 7,
                    SpecialtyId = 4,
                    ProviderFirstName = "Dorie",
                    ProviderLastName = "O'Dreain",
                    ProviderPhoneNumber = 5123716488
                },
                new Data_Provider
                {
                    ProviderId = 23,
                    FacilityId = 8,
                    SpecialtyId = 10,
                    ProviderFirstName = "Leonora",
                    ProviderLastName = "Pitford",
                    ProviderPhoneNumber = 2138363970
                },
                new Data_Provider
                {
                    ProviderId = 24,
                    FacilityId = 9,
                    SpecialtyId = 1,
                    ProviderFirstName = "Whitney",
                    ProviderLastName = "Sevior",
                    ProviderPhoneNumber = 6174183997
                },
                new Data_Provider
                {
                    ProviderId = 25,
                    FacilityId = 10,
                    SpecialtyId = 8,
                    ProviderFirstName = "Zora",
                    ProviderLastName = "Paolucci",
                    ProviderPhoneNumber = 1909364291
                },
                new Data_Provider
                {
                    ProviderId = 26,
                    FacilityId = 11,
                    SpecialtyId = 10,
                    ProviderFirstName = "Jayson",
                    ProviderLastName = "Wookey",
                    ProviderPhoneNumber = 8982665744
                },
                new Data_Provider
                {
                    ProviderId = 27,
                    FacilityId = 12,
                    SpecialtyId = 8,
                    ProviderFirstName = "Rock",
                    ProviderLastName = "Sharville",
                    ProviderPhoneNumber = 5864186454
                },
                new Data_Provider
                {
                    ProviderId = 28,
                    FacilityId = 13,
                    SpecialtyId = 10,
                    ProviderFirstName = "Dani",
                    ProviderLastName = "Broadwell",
                    ProviderPhoneNumber = 4221047432
                },
                new Data_Provider
                {
                    ProviderId = 29,
                    FacilityId = 14,
                    SpecialtyId = 2,
                    ProviderFirstName = "Virginia",
                    ProviderLastName = "McAvinchey",
                    ProviderPhoneNumber = 8769325946
                },
                new Data_Provider
                {
                    ProviderId = 30,
                    FacilityId = 15,
                    SpecialtyId = 15,
                    ProviderFirstName = "Neala",
                    ProviderLastName = "Cianelli",
                    ProviderPhoneNumber = 4504879923
                },
                new Data_Provider
                {
                    ProviderId = 31,
                    FacilityId = 6,
                    SpecialtyId = 6,
                    ProviderFirstName = "Pooh",
                    ProviderLastName = "Florio",
                    ProviderPhoneNumber = 3317751153
                },
                new Data_Provider
                {
                    ProviderId = 32,
                    FacilityId = 14,
                    SpecialtyId = 14,
                    ProviderFirstName = "Portia",
                    ProviderLastName = "Treadwell",
                    ProviderPhoneNumber = 3609514708
                },
                new Data_Provider
                {
                    ProviderId = 33,
                    FacilityId = 4,
                    SpecialtyId = 4,
                    ProviderFirstName = "Tucky",
                    ProviderLastName = "Dreher",
                    ProviderPhoneNumber = 1313561327
                },
                new Data_Provider
                {
                    ProviderId = 34,
                    FacilityId = 15,
                    SpecialtyId = 15,
                    ProviderFirstName = "Gardie",
                    ProviderLastName = "Drakes",
                    ProviderPhoneNumber = 6921919943
                },
                new Data_Provider
                {
                    ProviderId = 35,
                    FacilityId = 7,
                    SpecialtyId = 7,
                    ProviderFirstName = "Phillip",
                    ProviderLastName = "Sharville",
                    ProviderPhoneNumber = 9529171778
                },
                new Data_Provider
                {
                    ProviderId = 36,
                    FacilityId = 8,
                    SpecialtyId = 8,
                    ProviderFirstName = "Byrle",
                    ProviderLastName = "Shuttleworth",
                    ProviderPhoneNumber = 2717398518
                },
                new Data_Provider
                {
                    ProviderId = 37,
                    FacilityId = 14,
                    SpecialtyId = 14,
                    ProviderFirstName = "Hy",
                    ProviderLastName = "Hamflett",
                    ProviderPhoneNumber = 1206169437
                },
                new Data_Provider
                {
                    ProviderId = 38,
                    FacilityId = 12,
                    SpecialtyId = 12,
                    ProviderFirstName = "Eadie",
                    ProviderLastName = "Taill",
                    ProviderPhoneNumber = 7997088826
                },
                new Data_Provider
                {
                    ProviderId = 39,
                    FacilityId = 2,
                    SpecialtyId = 2,
                    ProviderFirstName = "Timi",
                    ProviderLastName = "Kestian",
                    ProviderPhoneNumber = 2894355254
                },
                new Data_Provider
                {
                    ProviderId = 40,
                    FacilityId = 11,
                    SpecialtyId = 11,
                    ProviderFirstName = "Gearalt",
                    ProviderLastName = "Dows",
                    ProviderPhoneNumber = 7618424391
                },
                new Data_Provider
                {
                    ProviderId = 41,
                    FacilityId = 6,
                    SpecialtyId = 1,
                    ProviderFirstName = "Lisa",
                    ProviderLastName = "Simons",
                    ProviderPhoneNumber = 5821908432
                },
                new Data_Provider
                {
                    ProviderId = 42,
                    FacilityId = 12,
                    SpecialtyId = 1,
                    ProviderFirstName = "Leroy",
                    ProviderLastName = "Gerkins",
                    ProviderPhoneNumber = 2337452877
                },
                new Data_Provider
                {
                    ProviderId = 43,
                    FacilityId = 1,
                    SpecialtyId = 1,
                    ProviderFirstName = "Ki'Nir",
                    ProviderLastName = "Habadayah",
                    ProviderPhoneNumber = 4997874451
                },
                new Data_Provider
                {
                    ProviderId = 44,
                    FacilityId = 12,
                    SpecialtyId = 1,
                    ProviderFirstName = "James",
                    ProviderLastName = "Comey",
                    ProviderPhoneNumber = 3261558779
                },
                new Data_Provider
                {
                    ProviderId = 45,
                    FacilityId = 1,
                    SpecialtyId = 1,
                    ProviderFirstName = "Karina",
                    ProviderLastName = "Silkya",
                    ProviderPhoneNumber = 6674219116
                }
            );

            modelBuilder.Entity<Data_Specialty>(entity =>
            {
                entity.Property(e => e.SpecialtyId)
                    .IsRequired();
                entity.Property(e => e.Specialty)
                    .IsRequired()
                    .HasMaxLength(80);
            });
            modelBuilder.Entity<Data_Specialty>().HasData(
                new Data_Specialty()
                {
                    SpecialtyId = 1,
                    Specialty = "Ophthalmologist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 2,
                    Specialty = "Dermatologist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 3,
                    Specialty = "Cardiologist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 4,
                    Specialty = "Urologist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 5,
                    Specialty = "Gastroenterologist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 6,
                    Specialty = "Psychiatrist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 7,
                    Specialty = "Internist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 8,
                    Specialty = "Neurologist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 9,
                    Specialty = "Endocrinologist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 10,
                    Specialty = "Otolaryngologist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 11,
                    Specialty = "Orthopedist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 12,
                    Specialty = "Pediatrician"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 13,
                    Specialty = "Anesthesiologist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 14,
                    Specialty = "Pulmonologist"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 15,
                    Specialty = "Proctologist"
                }
            );
        }
    }
}
