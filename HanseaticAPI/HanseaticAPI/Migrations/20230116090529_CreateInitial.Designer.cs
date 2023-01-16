﻿// <auto-generated />
using HanseaticAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HanseaticAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230116090529_CreateInitial")]
    partial class CreateInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HanseaticAPI.CityProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("actual_amount")
                        .HasColumnType("int");

                    b.Property<int>("buy_price")
                        .HasColumnType("int");

                    b.Property<int>("city_id")
                        .HasColumnType("int");

                    b.Property<int>("desired_amount")
                        .HasColumnType("int");

                    b.Property<double>("max_fluctation")
                        .HasColumnType("float");

                    b.Property<double>("min_fluctation")
                        .HasColumnType("float");

                    b.Property<int>("product_type")
                        .HasColumnType("int");

                    b.Property<int>("sell_price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CityProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
