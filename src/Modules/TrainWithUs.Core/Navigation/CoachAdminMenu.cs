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
    public class CoachAdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer T;

        public CoachAdminMenu(IStringLocalizer<CoachAdminMenu> localizer) => T = localizer;

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase)) return Task.CompletedTask;

            builder.Add(T["Written trainings"], "1",
                menuitem => menuitem
                .Action("ListCoachTrainings", "Trainings", new { Area = "TrainWithUs.Core" })
                .Permission(TrainingPermissions.WriteTrainingPermission));

            builder.Add(T["My messages"], "1",
                menuitem => menuitem
                .Action("ListUserMessages", "Messages", new { Area = "TrainWithUs.Core" })
                .Permission(TrainingPermissions.SendMessagesPermission));

            return Task.CompletedTask;
        }
    }
}
