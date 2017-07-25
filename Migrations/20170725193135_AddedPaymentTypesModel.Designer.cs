using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BangazonAPI.Data;

namespace BangazonAPI.Migrations
{
    [DbContext(typeof(BangazonAPIContext))]
    [Migration("20170725193135_AddedPaymentTypesModel")]
    partial class AddedPaymentTypesModel
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

            modelBuilder.Entity("BangazonAPI.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountNumber");

                    b.Property<int>("CustomerID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("PaymentTypeID");

                    b.HasIndex("CustomerID");

                    b.ToTable("PaymentType");
                });

            modelBuilder.Entity("BangazonAPI.Models.PaymentType", b =>
                {
                    b.HasOne("BangazonAPI.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
