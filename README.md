# College Directory System
A college directory system using the MVC (Model-View-Controller) architecture, catering to four user types: Superadmin, Teacher, Student, and Anonymous.

The video demonstrates the Role of a "teacher":
https://github.com/user-attachments/assets/c4845097-e933-4c75-9220-71296fc5b319

## How It Works

1. **User Interaction (View)**  
   Users interact with the system through the **View**. Here, their inputs and requests are captured and routed to the appropriate components.

2. **Middleware Processing**  
   Before reaching the controller, requests pass through a middleware pipeline where critical functions like authentication, authorization, and error handling are performed. This ensures secure and seamless operations.

3. **User Credential Verification and Token Generation**  
   Based on the user's credentials, the system generates a secure token to authenticate the user session. This token-based authentication ensures that each user is verified and authorized to access specific parts of the system. Depending on the validated credentials and roles, users are directed to their respective workspaces. This step is essential for maintaining data security and role-based access control.

4. **Request Handling (Controller)**  
   The **Controller** acts as an intermediary, processing the user's request. It utilizes dependency injection to interact with the **Data Access Layer (DAL)**.

5. **Data Operations (Model)**  
   In the **DAL**, the business logic is executed. This layer communicates directly with the database using technologies like LINQ and MongoDB to retrieve or manipulate data as required.

6. **Response Cycle**  
   Once the data operations are complete, the results are returned to the controller, which then passes them through the middleware back to the view, displaying the desired output to the user.

## Project Highlights

- **Superadmin** can manage users with full CRUD (Create, Read, Update, Delete) capabilities.
- **Teachers** can enter and update marks for their respective sections.
- **Students** can view their marks and check their pass status.
- **Anonymous users** can access class lists.
- All tables in this project include features for sorting, searching, full CRUD operations, and a "fixMe" feature for flagging and addressing issues.

## Technologies and Tools

- **Backend:** C#, LINQ, MongoDB
- **Frontend:** HTML, CSS, JavaScript

## Easter Egg

- **Error Handling:** This project includes a special easter egg inspired by the CS240 class at Purdue.

