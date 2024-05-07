using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using TrainWithUs.Core.Drivers;
using TrainWithUs.Core.Handlers;
using TrainWithUs.Core.Indexes;
using TrainWithUs.Core.Migrations;
using TrainWithUs.Core.Models;
using TrainWithUs.Core.Navigation;
using TrainWithUs.Core.Permissions;
using YesSql.Indexes;

namespace TrainWithUs.Core
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<TrainingPart>()
                .UseDisplayDriver<TrainingPartDisplayDriver>()
                .AddHandler<TrainingPartHandler>();

            services.AddContentPart<CoachProfilePart>()
                .UseDisplayDriver<CoachProfilePartDisplayDriver>()
                .AddHandler<CoachProfilePartHandler>();

            services.AddContentPart<TrainingSubscriptionPart>()
                .UseDisplayDriver<TrainingSubscriptionPartDisplayDriver>()
                .AddHandler<TrainingSubscriptionPartHandler>();

            services.AddContentPart<MessagePart>()
                .UseDisplayDriver<MessagePartDisplayDriver>()
                .AddHandler<MessagePartHandler>();

            services.AddScoped<IDataMigration, TrainingMigrations>();

            services.AddSingleton<IIndexProvider, TrainingPostIndexProvider>();
            services.AddSingleton<IIndexProvider, CoachProfilePartIndexProvider>();
            services.AddSingleton<IIndexProvider, TrainingSubscriptionPartIndexProvider>();
            services.AddSingleton<IIndexProvider, MessagePostIndexProvider>();


            services.AddScoped<INavigationProvider, AthleteAdminMenu>();
            services.AddScoped<INavigationProvider, CoachAdminMenu>();

            services.AddScoped<IPermissionProvider, TrainingPermissions>();








        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Home",
                areaName: "TrainWithUs.Core",
                pattern: "Home/Index",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
