# EmailSendingApp

To bring up this application, follow these steps in order:

Go to the AppDbContext class and enter your connection string in the specified area within the method as shown below

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseNpgsql("YOUR_CONNECTION_STRING_HERE");
}

Run the command Update-Database in the Package Manager Console.
Right-click on the MVC project, select Build Dependencies => Project Dependencies.
In the Projects section, make sure your MVC project is selected, and in the ‘depends on’ section, choose our API application and click Ok.
In the MVC project, navigate to the files in the service folder and update the following line:

private const string BaseUrl = "YOUR_HOST_HERE/api/....";
Replace "YOUR_HOST_HERE" with your actual host address.
Do the same thing for the connection string which is in appsettings.json file.

Finally, right-click on the solution, choose Configure Startup Projects, and select the option to run both applications simultaneously.
 
