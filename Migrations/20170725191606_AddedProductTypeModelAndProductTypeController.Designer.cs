﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BangazonAPI.Data;

namespace BangazonAPI.Migrations
{
    [DbContext(typeof(BangazonAPIContext))]
    [Migration("20170725191606_AddedProductTypeModelAndProductTypeController")]
    partial class AddedProductTypeModelAndProductTypeController
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("BangazonAPI.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d')");

                    b.Property<DateTime>("DateLastInteraction");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BangazonAPI.Models.ProductType", b =>
                {
                    b.Property<int>("ProductTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ProductTypeID");

                    b.ToTable("ProductType");
                });
        }
    }
}