using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BangazonAPI.Models;
using System.Threading.Tasks;

namespace BangazonAPI.Data
{
    // Class to seed our database with data for testing purposes.
    public static class DbInitializer
    {
        // Method runs on startup to initialize dummy data.
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BangazonAPIContext(serviceProvider.GetRequiredService<DbContextOptions<BangazonAPIContext>>()))
            {
                // Look for any Customers.
                if (context.Customer.Any())
                {
                    return;   // DB has been seeded, the rest of this method doesn't need to run.
                }
                // Creating new instances of Customer
                var customers = new Customer[]
                {
                    new Customer { 
                        FirstName = "Svetlana",
                        LastName = "Smith"

                    },
                    new Customer { 
                        FirstName = "Nigel",
                        LastName = "Thornberry"
                    },
                    new Customer { 
                        FirstName = "Sequina",
                        LastName = "Jones"
                    },
                };
                // Adds each new customer into the context
                foreach (Customer i in customers)
                {
                    context.Customer.Add(i);
                }
                // Saves the customers to the database
                context.SaveChanges();

                // Creating new instances of ProductType
                var productTypes = new ProductType[]
                {
                    new ProductType { 
                        Name = "Food"
                    },
                     new ProductType { 
                        Name = "Automobile"
                    },
                    new ProductType { 
                        Name = "Furniture"
                    },
                };

                // Adds each new product type into the context
                foreach (ProductType p in productTypes)
                {
                    context.ProductType.Add(p);
                }
                // Saves the customers to the database
                context.SaveChanges();

                // Creating new instances of payment type
                var paymentTypes = new PaymentType[]
                {
                    new PaymentType{
                        AccountNumber = 123459889,
                        Name = "Visa",
                        CustomerID = customers.Single(s => s.FirstName == "Svetlana").CustomerID
                    },
                    new PaymentType{
                        AccountNumber = 123400889,
                        Name = "Check",
                        CustomerID = customers.Single(s => s.FirstName == "Svetlana").CustomerID
                    },
                    new PaymentType{
                        AccountNumber = 555555555,
                        Name = "MasterCard",
                        CustomerID = customers.Single(c => c.FirstName == "Sequina").CustomerID
                    },
                    new PaymentType{
                        AccountNumber = 987654321,
                        Name = "SeaShells",
                        CustomerID = customers.Single(n => n.FirstName == "Nigel").CustomerID
                    },
                };
                // Adds each new payment type into the context
                foreach (PaymentType t in paymentTypes)
                {
                    context.PaymentType.Add(t);
                }
                // Saves the additions to the database
                context.SaveChanges();

                // Creating new instances of products
                var products = new Product[]
                {
                    new Product{
                        Title  = "Taco",
                        Description = "Delisious beef tacos in a hard corn tortia shell",
                        Price = 0.99,
                        ProductTypeID = productTypes.Single(p => p.Name == "Food").ProductTypeID,
                        CustomerID = customers.Single(c => c.FirstName == "Svetlana").CustomerID
                    },
                    new Product{
                        Title = "VW Beetle",
                        Description = "A classic hippy-mobile from 1967",
                        Price = 1289.99,
                        ProductTypeID = productTypes.Single(i => i.Name == "Automobile").ProductTypeID,
                        CustomerID = customers.Single(c => c.FirstName == "Svetlana").CustomerID
                    },
                    new Product{
                        Title = "Loveseat",
                        Description = "A comfortable coach that seats two",
                        Price = 199,
                        ProductTypeID = productTypes.Single(i => i.Name == "Furniture").ProductTypeID,
                        CustomerID = customers.Single(c => c.FirstName == "Nigel").CustomerID
                    }
                };
                // Adds each new product into the context
                foreach(Product p in products)
                {
                    context.Add(p);
                }
                // Saves the additions to the database
                context.SaveChanges();

                // Creating new instances of order
                var orders = new Order[]
                {
                    new Order{
                        CustomerID = customers.Single(c => c.FirstName == "Svetlana").CustomerID,
                        PaymentTypeID = paymentTypes.Single(s => s.Name == "Visa").PaymentTypeID
                    },
                    new Order{
                        CustomerID = customers.Single(c => c.FirstName == "Svetlana").CustomerID,
                        PaymentTypeID = paymentTypes.Single(s => s.Name == "Check").PaymentTypeID
                    },
                    new Order{
                        CustomerID = customers.Single(c => c.FirstName == "Svetlana").CustomerID
                    },
                    new Order{
                        CustomerID = customers.Single(c => c.FirstName == "Sequina").CustomerID
                    },
                    new Order{
                        CustomerID = customers.Single(c => c.FirstName == "Sequina").CustomerID
                    }

                };
                // Adds each new order into the context
                foreach(Order p in orders)
                {
                    context.Add(p);
                }
                context.SaveChanges();

                // Creating new instances of departments
                var departments = new Department[]
                {
                    new Department { 
                        Name = "Marketing",
                        ExpenseBudget = 200000
                    },
                    new Department { 
                        Name = "Accounting",
                        ExpenseBudget = 120000
                    },
                    new Department { 
                        Name = "IT",
                        ExpenseBudget = 150000
                    }
                };
                // Adds each new department into the context
                foreach (Department dept in departments)
                {
                    context.Department.Add(dept);
                }
                // Saves the additions to the database
                context.SaveChanges();

                // Creating new instances of computer
                var computers = new Computer[]
                {
                    new Computer { 
                        DatePurchased = new DateTime(1987, 03, 28),
                    },
                    new Computer { 
                        DatePurchased = new DateTime(1999, 12, 31),
                    },
                    new Computer { 
                        DatePurchased = new DateTime(2015, 6, 6),
                    }
                };
                // Adds each new computer into the context
                foreach (Computer comp in computers)
                {
                    context.Computer.Add(comp);
                }
                // Saves the additions to the database
                context.SaveChanges();

                // Creating new instances of training program
                var trainingPrograms = new TrainingProgram[]
                {
                    new TrainingProgram {
                        Name = "The correct pronunciation of Gif",
                        DateStart = new DateTime(2017, 07, 28),
                        DateEnd = new DateTime(2017, 08, 04),
                        MaxAttendees = 50
                    },
                    new TrainingProgram {
                        Name = "How to make a durn pot of coffee",
                        DateStart = new DateTime(2017, 01, 25),
                        DateEnd = new DateTime(2017, 02, 05),
                        MaxAttendees = 400
                    },
                    new TrainingProgram {
                        Name = "Fantastical Beasts and Where to Find Them",
                        DateStart = new DateTime(2017, 04, 03),
                        DateEnd = new DateTime(2017, 09,28),
                        MaxAttendees = 150

                    }
                };
                // Adds each new training program into the context
                foreach (TrainingProgram tp in trainingPrograms)
                {
                    context.TrainingProgram.Add(tp);
                }
                // Saves the additions to the database
                context.SaveChanges();

                // Creating new instances of employee
                var employees = new Employee[]
                {
                    new Employee { 
                        Name = "Joe Dirt",
                        DateStarted = new DateTime(1988,09,16),
                        JobTitle = "Graphic Designer",
                        IsSupervisor = 0,
                        DepartmentID = departments.Single(x => x.Name == "Marketing").DepartmentID
                    },
                    new Employee { 
                        Name = "Kevin Garvey",
                        DateStarted = new DateTime(1988,09,16),
                        JobTitle = "Head of Accounting",
                        IsSupervisor = 1,
                        DepartmentID = departments.Single(x => x.Name == "Accounting").DepartmentID
                    },
                    new Employee { 
                        Name = "Max Payne",
                        DateStarted = new DateTime(1988,09,16),
                        JobTitle = "Senior Developer",
                        IsSupervisor = 0,
                        DepartmentID = departments.Single(x => x.Name == "IT").DepartmentID
                    }
                };
                foreach (Employee emp in employees)
                {
                    context.Employee.Add(emp);
                }
                // Saves the additions to the database
                context.SaveChanges();

                // Creating new instances of product orders
                var ordersWithProducts = new ProductOrder[]
                {
                    new ProductOrder {
                        OrderID = 1 ,
                        ProductID = 2
                    },
                    new ProductOrder {
                        OrderID = 1 ,
                        ProductID = 1
                    },
                    new ProductOrder {
                        OrderID = 1 ,
                        ProductID = 1
                    }
                };
                // Adds each new product into the context
                foreach (ProductOrder product in ordersWithProducts)
                {
                    context.ProductOrder.Add(product);
                }
                // Saves the additions to the database
                context.SaveChanges();

            }
       }
    }
}
