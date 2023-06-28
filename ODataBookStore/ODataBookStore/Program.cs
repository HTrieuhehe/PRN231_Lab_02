using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using ODataBookStore;
using ODataBookStore.Models;

#region Old Code
//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookDb")));
////odata
//ODataConventionModelBuilder modelBuilder = new();
//modelBuilder.EntitySet<Book>("Book");
//modelBuilder.EntitySet<Press>("Presses");
//builder.Services.AddControllers().AddOData(option => option.AddRouteComponents("odata", modelBuilder.GetEdmModel())
//        .Select()
//        .Filter()
//        .OrderBy()
//        .SetMaxTop(20)
//        .Count()
//        .Expand());
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c=>
//    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()));


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//#region Firebase
//var pathToKey = Path.Combine(Directory.GetCurrentDirectory(), "Keys", "firebase.json");
//FirebaseApp.Create(new AppOptions
//{
//    Credential = GoogleCredential.FromFile(pathToKey)
//});
//#endregion

//app.UseODataBatching();
//app.UseRouting();

//app.UseHttpsRedirection();
//app.UseAuthorization();

//app.UseEndpoints(endpoints => endpoints.MapControllers());

//app.Run();
#endregion 

namespace ODataBookStore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
