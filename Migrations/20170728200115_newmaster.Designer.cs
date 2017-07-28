using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BangazonAPI.Data;

namespace BangazonAPI.Migrations
{
    [DbContext(typeof(BangazonAPIContext))]
    [Migration("20170728200115_newmaster")]
    partial class newmaster
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("BangazonAPI.Models.Computer", b =>
                {
                    b.Property<int>("ComputerID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateDecomissioned");

                    b.Property<DateTime?>("DatePurchased")
                        .IsRequired();

                    b.HasKey("ComputerID");

                    b.ToTable("Computer");
                });

            modelBuilder.Entity("BangazonAPI.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d')");

                    b.Property<DateTime?>("DateLastInteraction")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d')");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BangazonAPI.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("ExpenseBudget");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("DepartmentID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("BangazonAPI.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateStarted");

                    b.Property<int>("DepartmentID");

                    b.Property<int>("IsSupervisor");

                    b.Property<string>("JobTitle")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("EmployeeID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("BangazonAPI.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerID");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d')");

                    b.Property<int?>("PaymentTypeID");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("PaymentTypeID");

                    b.ToTable("Order");
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

            modelBuilder.Entity("BangazonAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerID");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<int>("ProductTypeID");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ProductID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("ProductTypeID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("BangazonAPI.Models.ProductOrder", b =>
                {
                    b.Property<int>("ProductOrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderID");

                    b.Property<int>("ProductID");

                    b.HasKey("ProductOrderID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductOrder");
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

            modelBuilder.Entity("BangazonAPI.Models.TrainingProgram", b =>
                {
                    b.Property<int>("TrainingProgramID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateEnd")
                        .IsRequired();

                    b.Property<DateTime?>("DateStart")
                        .IsRequired();

                    b.Property<int?>("MaxAttendees")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TrainingProgramID");

                    b.ToTable("TrainingProgram");
                });

            modelBuilder.Entity("BangazonAPI.Models.Employee", b =>
                {
                    b.HasOne("BangazonAPI.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BangazonAPI.Models.Order", b =>
                {
                    b.HasOne("BangazonAPI.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BangazonAPI.Models.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeID");
                });

            modelBuilder.Entity("BangazonAPI.Models.PaymentType", b =>
                {
                    b.HasOne("BangazonAPI.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BangazonAPI.Models.Product", b =>
                {
                    b.HasOne("BangazonAPI.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BangazonAPI.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BangazonAPI.Models.ProductOrder", b =>
                {
                    b.HasOne("BangazonAPI.Models.Order", "Order")
                        .WithMany("ProductOrders")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BangazonAPI.Models.Product", "Product")
                        .WithMany("ProductOrders")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
