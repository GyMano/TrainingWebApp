using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Permissions;

namespace TrainWithUs.Core.Navigation
{
    public class AthleteAdminMenu : INavigationProvider
    {
        
        private readonly IStringLocalizer T;

        public AthleteAdminMenu(IStringLocalizer<AthleteAdminMenu> localizer) => T = localizer;

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase)) return Task.CompletedTask;

            builder.Add(T["Subscribed trainings"], "1",
                menuitem => menuitem
                .Action("ListAthletesTrainings", "Trainings", new { Area = "TrainWithUs.Core" })
                .Permission(TrainingPermissions.ManageOwnSubscriptions));

            builder.Add(T["My messages"], "1",
                menuitem => menuitem
                .Action("ListUserMessages", "Messages", new { Area = "TrainWithUs.Core" })
                .Permission(TrainingPermissions.SendMessagesPermission));

            return Task.CompletedTask;
        }
        
    }
}
