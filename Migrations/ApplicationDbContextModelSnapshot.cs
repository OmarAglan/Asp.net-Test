﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Roshta.Data;

#nullable disable

namespace Roshta.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("Roshta.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactEmail")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactPhone")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSubscribed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LicenseNumber")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Specialization")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Roshta.Models.Medication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Dosage")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Form")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Medications");
                });

            modelBuilder.Entity("Roshta.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactInfo")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasOutstandingBalance")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastVisitDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("VisitCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Roshta.Models.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateIssued")
                        .HasColumnType("TEXT");

                    b.Property<int>("DoctorId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("NextAppointmentDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("Roshta.Models.PrescriptionItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Dosage")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Duration")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Frequency")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<int>("MedicationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int>("PrescriptionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("Refills")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MedicationId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("PrescriptionItems");
                });

            modelBuilder.Entity("Roshta.Models.Prescription", b =>
                {
                    b.HasOne("Roshta.Models.Doctor", "Doctor")
                        .WithMany("Prescriptions")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Roshta.Models.Patient", "Patient")
                        .WithMany("Prescriptions")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Roshta.Models.PrescriptionItem", b =>
                {
                    b.HasOne("Roshta.Models.Medication", "Medication")
                        .WithMany("PrescriptionItems")
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Roshta.Models.Prescription", "Prescription")
                        .WithMany("PrescriptionItems")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medication");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("Roshta.Models.Doctor", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("Roshta.Models.Medication", b =>
                {
                    b.Navigation("PrescriptionItems");
                });

            modelBuilder.Entity("Roshta.Models.Patient", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("Roshta.Models.Prescription", b =>
                {
                    b.Navigation("PrescriptionItems");
                });
#pragma warning restore 612, 618
        }
    }
}
