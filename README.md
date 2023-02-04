# Family Budget Tracker
Simple web application, created for tracking family budget.

## Application details
- Frontend: ASP.NET Core MVC with .NET 6
- Backend: ASP.NET Core Web API with .NET 6
- Documentation of the available API endpoints: [Swagger UI](https://family-budget-tracker-api.herokuapp.com/index.html)
- Database: PostgreSQL
- ORM: Entity Framework Core

## Specifics
- One user can add as many payments, both incomes and expenses, as they want.  
- Each payment category (expenses and incomes) has different types (e.g salary, gift, etc for the incomes and car payment, tuition, etc for the expenses).
- Other than those categories, everything else works the same.
- Each payment can be marked as recurring upon creation, meaning that every month on a specific date, the user will be charged the appropriate amount.
- If the payment date is set for a future date, no current charges are made.
- All characteristics of the payments can be edited with the exception of their recurrence (a.k.a a recurring payment cannot be turned to a one-time payment and vice versa).
- Every user has access only to their payments and can delete and edit only them.

## Ways to check out the application
Login credentials: ***username: test.user & password: test***
### 1. Access the deployed version at [Family Budget Tracker](https://fbt-mvc.azurewebsites.net/) (Currently unavailable)
### 2. Download the source code as a zip or clone it:
- Prerequisites: [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Navigate to _FBT.MVC/_
- Open a terminal of your choice and run `dotnet run`
- Go to localhost:5001 in a browser of your choice
### 3. Family Budget Tracker Guide: 
_The screenshots have been taken on 4th of August, 2022. This is an important information regarding the payment dates._

- Login page  
If a user is not logged in, this is the initial page that loads.

![Login](https://user-images.githubusercontent.com/43497483/183283366-794d7ca9-001f-4db2-8cfd-057b580d5f82.png)

- Register page  
If you're new to this app, you can go to the register page, following the link in the upper right and sign up with your info. After successfully filling up the info,
you'll be redirected to the login page once again to input your credentials.

![Register](https://user-images.githubusercontent.com/43497483/183283452-f7ae7c79-24ec-4b9c-ac5b-acdf020b11c8.png)

- Home page  
As a newly registered user, this is the home page you'll see. No expenses or incomes have been added, so the budget is 0.

![Home - initial](https://user-images.githubusercontent.com/43497483/183283353-d8cb4150-12fd-45a0-901f-aefbb6e273fe.png)

- Expense page  
This is the initial look of the expense tab, when there haven't been any expenses added.

![Expenses - initial](https://user-images.githubusercontent.com/43497483/183283582-7676faaa-272e-4748-89e0-2649b61af77c.png)

- Adding an expense  
Upon clicking on the _Create_ link from the previous screenshot, this is the form that loads.
Now you're prompted to specify an amount, a type, a payment date and whether the expense is recurring in order to add it in your account.

![Create Expense](https://user-images.githubusercontent.com/43497483/183283627-7d162458-2102-4379-8cfd-744df6df7213.png)

- Expenses page (updated)  
Now the newly added expense shows up in the expense page. The total amount is equal to 0.00 because the specified payment date is after the current date so no charges have been made (the screenshot is taken on August 4th).

![Expenses - second](https://user-images.githubusercontent.com/43497483/183283736-2296bccb-bfc9-47da-b018-0bdb5cf07cdd.png)

- Editing an expense

![Edit Expense](https://user-images.githubusercontent.com/43497483/183283928-bbd110dd-eb54-4d9b-8f4b-0032ec6b56a5.png)

- The expense page once again  
Now that the date of the expense has been moved to 03-Jul-2022, the total amount is 24.00 because the payment is labeled as recurring and two months have passed since the initial date, which means the user will be charged twice.

![Expenses - third](https://user-images.githubusercontent.com/43497483/183283936-1d8aff4f-a06c-4f7f-9bdc-dbe1a9eac06c.png)

- The Incomes tab  
Once again, this is the look of the page before any incomes have been added.

![Incomes - initial](https://user-images.githubusercontent.com/43497483/183284252-3d0ef641-0ffd-4f89-a6b7-698870297bb3.png)

- Creating an income  
The process is the exact same as with the expenses. The only difference is the types of payments.

![Create Income](https://user-images.githubusercontent.com/43497483/183284270-1e5489e0-d48b-4ded-b429-3190a538db05.png)

- Updated Incomes page

![Incomes - second](https://user-images.githubusercontent.com/43497483/183284330-c94cfa61-d008-4c63-9eac-03a0b83af579.png)

- Updated Home page  
After adding an expense, as well as an income, the home page will look a little different. This is the newly calculated budget of the current user.

![Home - calculated](https://user-images.githubusercontent.com/43497483/183284370-82955a9b-7ece-4f85-a321-763d197f3bd1.png)
