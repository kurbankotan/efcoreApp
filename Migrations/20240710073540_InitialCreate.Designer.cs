﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using efcoreApp.Data;

#nullable disable

namespace efcoreApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240710073540_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("efcoreApp.Data.Kurs", b =>
                {
                    b.Property<int>("KursId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KursId"));

                    b.Property<string>("Baslik")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KursId");

                    b.ToTable("Kurslar");
                });

            modelBuilder.Entity("efcoreApp.Data.KursKayit", b =>
                {
                    b.Property<int>("KayitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KayitId"));

                    b.Property<DateTime>("KayitTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("KursId")
                        .HasColumnType("int");

                    b.Property<int>("OgrenciId")
                        .HasColumnType("int");

                    b.HasKey("KayitId");

                    b.ToTable("KursKayitlari");
                });

            modelBuilder.Entity("efcoreApp.Data.Ogrenci", b =>
                {
                    b.Property<int>("OgrenciId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OgrenciId"));

                    b.Property<string>("Eposta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OgrenciAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ogrencisoyad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OgrenciId");

                    b.ToTable("Ogrenciler");
                });
#pragma warning restore 612, 618
        }
    }
}
