using NLog.Web;
using TicketManagementSystem.Middleware;
using TicketManagementSystem.Repositories;
using TicketManagementSystem.Services;

namespace TicketManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            builder.Services.AddTransient<IEventRepository, EventRepository>();
            builder.Services.AddTransient<IEventService, EventService>();

            builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();
            builder.Services.AddTransient<IOrdersService, OrdersService>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }


}