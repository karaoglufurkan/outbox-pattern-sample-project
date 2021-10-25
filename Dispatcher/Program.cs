using Dispatcher.Data;
using Dispatcher.Extentions;
using Dispatcher.Jobs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace Dispatcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // services.AddHostedService<Worker>();
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionJobFactory();

                        // Create a "key" for the job                    
                        q.AddJobAndTrigger<OutboxJob>(hostContext.Configuration);
                    });

                    // Add the Quartz.NET hosted service

                    services.AddQuartzHostedService(
                        q => q.WaitForJobsToComplete = true);

                    services.ConfigureMassTransit(hostContext.Configuration);

                    services.AddDbContext<DispatcherDbContext>(builder => {
                        builder.UseSqlServer(hostContext.Configuration.GetConnectionString("DemoDb"));
                    });
                });
                // .ConfigureWebHostDefaults(configure => {
                    
                // });
    }
}
