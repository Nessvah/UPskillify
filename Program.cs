using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UPskillify_Forum.Data;

var builder = WebApplication.CreateBuilder(args);

// build the connection string for the db using secrets
var connectionStrBuilder = new SqlConnectionStringBuilder(
    builder.Configuration.GetConnectionString("UPskillify"));

connectionStrBuilder.UserID = builder.Configuration["DB_USER"];
connectionStrBuilder.Password = builder.Configuration["DB_PWD"];
string connection = connectionStrBuilder.ConnectionString;


// Add services to the container.
builder.Services.AddRazorPages();
// Add the database context to the application
builder.Services.AddDbContext<UpskillifyDbContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); 
}
// The order in which these are configured is very important!
app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseAuthorization();

app.MapRazorPages();

app.Run();
app.UseRouting();
