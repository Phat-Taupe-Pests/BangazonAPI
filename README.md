# BangazonAPI

This is an API for Bangazon INC. This API will allow user to GET/POST/PUT and (sometimes) DELETE items from the Bangazon Database. Before you can utilize the database, there are a few things you need to make sure you have installed. 

## Installing

As of now, the database is going to be hosted on your local computer. There are a few things you need to make sure are in place before the database can be up and running.
 1. Fork and clone the repo on to you local machine. 
 2. Run `dotnet restore`
 3. Run `dotnet ef migrations add bangazonapi` 
 >This will create all the migrations needed for Entity Framework to post items to the database based on the models in the Models/ directory
 3. Run `dotnet ef database update` 
 4. Run `dotnet run` 
 > This will compile and run everything as well as initalizing the database with some data to get started

## Using the API
For now, all calls to the API will be made from `http://localhost:5000` as the domain. All calls will be made from here. 
>EX you can get a list of all the customers by making a get call to `http://localhost:5000/customer`

### Customers

* GET You can access a list of all customers by running a Get call to `http://localhost:5000/customer`
* GET one. You can get the information on a single customer by runnning a Get call to `http://localhost:5000/customer/{customerID}`
>Note you need to have a customers unique ID number to get the correct information
* PUT You can update the info on a specific customer by running a Put call to `http://localhost:5000/customer/{customerID}`
    * You must Put the entire changed object, which will include the `customerID`, `firstName`, `lastName`, `dateCreated`, `dateLastInteraction`, and `isActive`. 
    * Example: `{"customerID": 1, "firstName": "Svetlana", "lastName": "Smith", "dateCreated": "2017-07-27T00:00:00", "dateLastInteraction": "2017-07-27T00:00:00", "isActive": 1}`
* POST You can post a new customer but running a Post call to `http://localhost:5000/customer`.
    * The post must have a FirstName and a LastName sent in.
    * Example: `{ "FirstName": "Harry", "LastName": "Potter" }`

### Products

* GET You can access a list of all products by running a Get call to `http://localhost:5000/product`
* GET one. You can get the information on a single product by runnning a Get call to `http://localhost:5000/product/{productID}`
>Note you need to have a products unique ID number to get the correct information

* PUT You can update the info on a specific product by running a Put call to `http://localhost:5000/product/{productID}`
    * The Put must send in the complete object which will include a `productID`, `title`, `description`, `price`, `productTypeID`, `customerID`.
    * Example: `{"productID": 1, "title": "Phoenix Wand", "description": "Phoenix feathers are capable of the greatest range of magic, though they may take longer than either unicorn or dragon cores to reveal this.", "price": 10.99, "productTypeID": 1, "customerID": 1}`
>Note you need to have a product, customer and productType unique IDs number to put correctly

* DELETE You can delete a product by running a Delete call to `http://localhost:5000/product{productID}`

* POST You can add a product by running a Post call to `http://localhost:5000/product`
    * You must submit a `ProductTypeID`, `Price`, `Title`, `Description` and `CustomerID`.
    * Example: `{"ProductTypeID": 1, "Price": 10.50, "Title": "Dragon Wand", "Description": "As a rule, dragon heartstrings produce wands with the most power, and which are capable of the most flamboyant spells.", "CustomerID": 1}`

### Product Types

* GET You can access a list of all product types by running a Get call to `http://localhost:5000/producttype`
* GET one. You can get the information on a single product type by runnning a Get call to `http://localhost:5000/producttype/{producttypeID}`
>Note you need to have a product types unique ID number to get the correct information

* PUT You can update the info on a specific product type by running a Put call to `http://localhost:5000/producttype/{producttypeID}`
    * Running a put requires that you submit the entire object.
    * Example: `{ "productTypeID": 1, "name": "Wand" }`

* DELETE You can delete a product type by running a Delete call to `http://localhost:5000/producttype{producttypeID}`

* POST You can enter a new product type by running a Post call to `http://localhost:5000/producttype`
    * You must put a `name` with a post.
    * Example: `{ "name": "Spell Books" }`

### Payment Types

* GET You can access a list of all payment types by running a Get call to `http://localhost:5000/paymenttype`
* GET one. You can get the information on a single payment type by runnning a Get call to `http://localhost:5000/paymenttype/{paymenttypeID}`
>Note you need to have a payment types unique ID number to get the correct information

* PUT You can update the info on a specific payment type by running a Put call to `http://localhost:5000/paymenttype/{paymenttypeID}`
    * Running a Put requires that you submit the entire object.
    * Example: `{ "paymentTypeID": 1, "name": "Galleon", "accountNumber": 123459889, "customerID": 1 }`

* DELETE You can delete a payment type by running a Delete call to `http://localhost:5000/paymenttype{paymenttypeID}`

* POST You can enter a new payment type by running a Post call to `http://localhost:5000/paymenttype`
    * You must put a `name`, `accountNumber`, and `customerID` with a Post.
    * Example: `{ "name": "Knut", "accountNumber": 123459889, "customerID": 1 }`


### Order

* GET You can access a list of all orders by running a Get call to `http://localhost:5000/order`
* GET one. You can get the information on a single order by runnning a Get call to `http://localhost:5000/order/{orderID}`
>Note you need to have a order unique ID number to get the correct information

* PUT You can update the info on a specific order by running a Put call to `http://localhost:5000/order/{orderID}`
    * Running a Put requires that you submit the entire object.
    * Example: `{ "orderID": 1, "dateCreated": "2017-07-28T00:00:00", "customerID": 3 }`

* DELETE You can delete an order by running a Delete call to `http://localhost:5000/order{orderID}`

* POST You can enter a new payment type by running a Post call to `http://localhost:5000/order`
    * You must put a `customerID` with a Post.
    * Example: `{ "customerID": 1 }`

### Employees

* GET You can access a list of all employees by running a Get call to `http://localhost:5000/employee`
* GET one You can get the information on a single employee by runnning a Get call to `http://localhost:5000/employee/{employeeID}`
>Note you need to have a employee unique ID number to get the correct information

* PUT You can update the info on a specific employee by running a Put call to `http://localhost:5000/employee/{employeeID}`
    * Running a Put requires that you submit the entire object.
    * Example: `{ "employeeID": 1, "name": "Minerva McGonagall", "jobTitle": "Professor", "dateStarted": "0001-01-01T00:00:00", "isSupervisor": 1, "departmentID": 1}`

* POST You can enter a new payment type by running a Post call to `http://localhost:5000/employee`
    * You must put a `name`, `jobTitle`, `dateStarted`, and `departmentID` with a Post.
    * Example: `{ "name": "Minerva McGonagall", "jobTitle": "Professor", "dateStarted": "12-01-1956", "departmentID": 1}`
    >isSupervisor is an autogenerated field that will set every employee to 0 (not a supervisor) 
    >If you hire a supervisor you can add `"isSupervisor":1` to the POST or change `"isSupervisor":1` in a put later on. 

### Departments

* GET You can access a list of all departments by running a Get call to `http://localhost:5000/department`
* GET one. You can get the information on a single department by runnning a Get call to `http://localhost:5000/department/{departmentID}`
>Note you need to have a department unique ID number to get the correct information

* PUT You can update the info on a specific department by running a Put call to `http://localhost:5000/department/{departmentID}`
    * Running a Put requires that you submit the entire object.
    * Example: `{ "departmentID": 1, "name": "Transfiguration", "expenseBudget": 200000 }`

* POST You can enter a new payment type by running a Post call to `http://localhost:5000/department`
    * You must put a `name` and `expenseBudget` with a Post.
    * Example: `{ "name": "Transfiguration" "expenseBudget": 100 }`

### Computer

* GET You can access a list of all computers by running a Get call to `http://localhost:5000/computer`
* GET one. You can get the information on a single computer by runnning a Get call to `http://localhost:5000/computer/{computerID}`
>Note you need to have a computer unique ID number to get the correct information

* PUT You can update the info on a specific computer by running a Put call to `http://localhost:5000/computer/{computerID}`
    * Running a Put requires that you submit the entire object.
    * Example: `{ "computerID": 1, "datePurchased": "0001-01-01T00:00:00", "dateDecomissioned": "12-13-2017" }`

* DELETE You can delete a computer by running a Delete call to `http://localhost:5000/computer{computerID}`

* POST You can enter a new computer by running a Post call to `http://localhost:5000/computer`
    * You must put a `datePurchased` with a Post.
    * Example: `{ "datePurchased": "0001-01-01T00:00:00" }`

### Training Programs

* GET You can access a list of all training programs by running a Get call to `http://localhost:5000/trainingprogram`
* GET one. You can get the information on a single training program by runnning a Get call to `http://localhost:5000/trainingprogram/{trainingprogramID}`
>Note you need to have a training program unique ID number to get the correct information

* PUT You can update the info on a specific training program by running a Put call to `http://localhost:5000/trainingprogram/{trainingprogramID}`
    * Running a Put requires that you submit the entire object.
    * Example: `{ "trainingProgramID": 1 "dateStart": "02-14-2018", "dateEnd": "02-15-2018", "maxAttendees": 50 }`

* DELETE You can delete a training program by running a Delete call to `http://localhost:5000/trainingprogram{trainingprogramID}`
>Note - you can only delete a training program if the current date is before the start date of a program. You cannot delete programs that have already happened. 

* POST You can enter a new payment type by running a Post call to `http://localhost:5000/trainingProgram`
    * You must put a `dateStart`, `dateEnd`, and `maxAttendees` with a Post.
    * Example: `{ "dateStart": "02-14-2018", "dateEnd": "10-15-2018", "maxAttendees": 50 }`