
To set up and run the project, follow these steps:

1. Open the Package Manager Console in Visual Studio by navigating to `Tools > NuGet Package Manager > Package Manager Console`.

2. In the Package Manager Console, run the following command to apply migrations and update the database:

   ```
   update-database -context ApplicationDbContext
   ```

3. Run the Admin project. This will open the login page.

4. Change the URL from `account/login` to `account/createuser` to create three users:
   
   - Admin:
     - Email: "admin@roundpixel.com"
     - Password: "24528179###"
     
   - Customer 1:
     - Email: "Muhammad@roundpixel.com"
     - Password: "Mh1212"
     
   - Customer 2:
     - Email: "Ahmad@roundpixel.com"
     - Password: "Ah1212"

5. Log in to the admin dashboard using the admin account credentials. Here, you can check the CRUD operations for items.

6. Run the API project. You can try logging in using any of the customer or admin users' credentials. Additionally, explore other endpoints for currency retrieval and manipulation.
