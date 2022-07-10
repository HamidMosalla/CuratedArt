//using CuratedArt.Data;
//using CuratedArt.Data.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using Microsoft.Extensions.Logging;
//using Microsoft.VisualStudio.TestPlatform.TestHost;

//namespace CuratedArt.IntegrationTests.Setup
//{
//    internal abstract class IntegrationTestBase : WebApplicationFactory<Program>
//    {
//        protected override void ConfigureWebHost(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) =>
//            {
//                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CuratedArtDbContext>));

//                if (descriptor != null) services.Remove(descriptor);

//                services.AddDbContext<CuratedArtDbContext>(options =>
//                {
//                    options.UseSqlServer(context.Configuration.GetConnectionString("IntegrationTestsConnection"));
//                });

//                var sp = services.BuildServiceProvider();

//                using (var scope = sp.CreateScope())
//                {
//                    var scopedServices = scope.ServiceProvider;
//                    var db = scopedServices.GetRequiredService<CuratedArtDbContext>();
//                    var logger = scopedServices.GetRequiredService<ILogger<IntegrationTestBase>>();

//                    if (!db.Database.CanConnect())
//                    {
//                        db.Database.EnsureCreated();
//                    }

//                    try
//                    {
//                        db.ArtWorks.Add(new ArtWork
//                        {
//                            Id = Guid.NewGuid(),
//                            Name = "crom inte",
//                            Desc = "sdf",
//                            DateReleased = DateTimeOffset.MaxValue,
//                            Type = ArtWorkType.Movie
//                        });
//                        db.SaveChanges();
//                    }
//                    catch (Exception ex)
//                    {
//                        logger.LogError(ex, "An error occurred seeding the " +
//                                            "database with test messages. Error: {Message}", ex.Message);
//                    }
//                }
//            });
//        }
//    }
//}
