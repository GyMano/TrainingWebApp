using OrchardCore.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainWithUs.Core.Permissions
{
    public class TrainingPermissions : IPermissionProvider
    {
        public static readonly Permission WriteTrainingPermission = new Permission(nameof(WriteTrainingPermission), "Can write trainings");
        public static readonly Permission SendMessagesPermission = new Permission(nameof(SendMessagesPermission), "Can send messages");
        public static readonly Permission ManageOwnSubscriptions = new Permission(nameof(ManageOwnSubscriptions), "Can manage own subscriptions");
        public static readonly Permission CreateCoachProfilePermission = new Permission(nameof(CreateCoachProfilePermission), "Can create Coach Profile");







        public Task<IEnumerable<Permission>> GetPermissionsAsync() =>
            Task.FromResult(new[]
            {
                WriteTrainingPermission, SendMessagesPermission, ManageOwnSubscriptions, CreateCoachProfilePermission
            }
            .AsEnumerable());

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() =>
            new[]
            {
                new PermissionStereotype()
                {
                    Name = "Administrator",
                    Permissions = new[] { WriteTrainingPermission, SendMessagesPermission, ManageOwnSubscriptions, CreateCoachProfilePermission},
                },
                new PermissionStereotype()
                {
                    Name = "Author",
                    Permissions = new[] { WriteTrainingPermission, SendMessagesPermission, ManageOwnSubscriptions, CreateCoachProfilePermission},
                },
                new PermissionStereotype()
                {
                    Name = "Authenticated",
                    Permissions = new[] { SendMessagesPermission, ManageOwnSubscriptions },
                }
            };
    }
}
