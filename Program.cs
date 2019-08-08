using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ContosoUniversity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<SchoolContext>();

                    // EnsureCreated позволяет проверить существование базы данных для контекста.
                    // Если контекст существует, никаких действий не предпринимается. 
                    // Если контекст не существует, создаются база данных и вся ее схема.
                    // EnsureCreated не использует миграции для создания базы данных.
                    // Созданную с помощью EnsureCreated базу данных впоследствии нельзя обновить,
                    // используя миграции.
                    // EnsureCreated удобно использовать на ранних стадиях разработки, 
                    // когда схема часто меняется. Далее в этом учебнике база данных удаляется 
                    // и используются миграции.
                    //context.Database.EnsureCreated();

                    // Используется для инициализации первичных данных 
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Произошла ошибка при создании БД.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
