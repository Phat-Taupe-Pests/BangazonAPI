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

* You can access a list of all customers by running a Get call to `http://localhost:5000/customer`
* You can get the information on a single customer by runnning a Get call to `http://localhost:5000/customer/{customerID}`
>Note you need to have a customers unique ID number to get the correct information
* You can update the info on a specific customer by running a Put call to `http://localhost:5000/customer/{customerID}`
    * You must Put the entire changed object, which will include the `customerID`, `firstName`, `lastName`, `dateCreated`, `dateLastInteraction`, and `isActive`. 
    * Example: `{"customerID": 1, "firstName": "Svetlana", "lastName": "Smith", "dateCreated": "2017-07-27T00:00:00", "dateLastInteraction": "2017-07-27T00:00:00", "isActive": 1}`
* You can post a new customer but running a Post call to `http://localhost:5000/customer`.
    * The post must have a FirstName and a LastName sent in.
    * Example: `{ "FirstName": "Harry", "LastName": "Potter" }`

### Products

* You can access a list of all products by running a Get call to `http://localhost:5000/product`
* You can get the information on a single product by runnning a Get call to `http://localhost:5000/product/{productID}`
>Note you need to have a products unique ID number to get the correct information

* You can update the info on a specific product by running a Put call to `http://localhost:5000/product/{productID}`
    * The Put must send in the complete object which will include a `productID`, `title`, `description`, `price`, `productTypeID`, `customerID`.
    * Example: `{"productID": 1, "title": "Taco", "description": "Delisious beef tacos in a hard corn tortia shell", "price": 0.99, "productTypeID": 1, "customerID": 1}`

* You can delete a product by running a Delete call to `http://localhost:5000/product{productID}`

### Product Types

* You can access a list of all product types by running a Get call to `http://localhost:5000/producttype`
* You can get the information on a single product type by runnning a Get call to `http://localhost:5000/producttype/{producttypeID}`
>Note you need to have a product types unique ID number to get the correct information

* You can update the info on a specific product type by running a Put call to `http://localhost:5000/producttype/{producttypeID}`

* You can delete a product type by running a Delete call to `http://localhost:5000/producttype{producttypeID}`

### Payment Types

* You can access a list of all payment types by running a Get call to `http://localhost:5000/paymenttype`
* You can get the information on a single payment type by runnning a Get call to `http://localhost:5000/paymenttype/{paymenttypeID}`
>Note you need to have a payment types unique ID number to get the correct information

* You can update the info on a specific payment type by running a Put call to `http://localhost:5000/paymenttype/{paymenttypeID}`

* You can delete a payment type by running a Delete call to `http://localhost:5000/paymenttype{paymenttypeID}`


### Order

* You can access a list of all payment types by running a Get call to `http://localhost:5000/paymenttype`
* You can get the information on a single payment type by runnning a Get call to `http://localhost:5000/paymenttype/{paymenttypeID}`
>Note you need to have a payment types unique ID number to get the correct information

* You can update the info on a specific payment type by running a Put call to `http://localhost:5000/paymenttype/{paymenttypeID}`

* You can delete a payment type by running a Delete call to `http://localhost:5000/paymenttype{paymenttypeID}`

### Employees

* You can access a list of all employees by running a Get call to `http://localhost:5000/employee`
* You can get the information on a single employee by runnning a Get call to `http://localhost:5000/employee/{employeeID}`
>Note you need to have a employee unique ID number to get the correct information

* You can update the info on a specific employee by running a Put call to `http://localhost:5000/employee/{employeeID}`



### Departments

* You can access a list of all departments by running a Get call to `http://localhost:5000/department`
* You can get the information on a single department by runnning a Get call to `http://localhost:5000/department/{departmentID}`
>Note you need to have a department unique ID number to get the correct information

* You can update the info on a specific department by running a Put call to `http://localhost:5000/department/{departmentID}`



### Computer

* You can access a list of all computers by running a Get call to `http://localhost:5000/computer`
* You can get the information on a single computer by runnning a Get call to `http://localhost:5000/computer/{computerID}`
>Note you need to have a computer unique ID number to get the correct information

* You can update the info on a specific computer by running a Put call to `http://localhost:5000/computer/{computerID}`

* You can delete a computer by running a Delete call to `http://localhost:5000/computer{computerID}`

### Training Programs

* You can access a list of all training programs by running a Get call to `http://localhost:5000/trainingprogram`
* You can get the information on a single training program by runnning a Get call to `http://localhost:5000/trainingprogram/{trainingprogramID}`
>Note you need to have a training program unique ID number to get the correct information

* You can update the info on a specific training program by running a Put call to `http://localhost:5000/trainingprogram/{trainingprogramID}`

* You can delete a training program by running a Delete call to `http://localhost:5000/trainingprogram{trainingprogramID}`
>Note - you can only delete a training program if the current date is before the start date of a program. You cannot delete programs that have already happened. 