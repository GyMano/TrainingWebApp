using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Users.Indexes;
using OrchardCore.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Models;
using TrainWithUs.Core.ViewModels;
using YesSql;

namespace TrainWithUs.Core.Drivers
{
    public class MessagePartDisplayDriver : ContentPartDisplayDriver<MessagePart>
    {
        private readonly ISession _session;

        public MessagePartDisplayDriver(ISession session)
        {
            _session = session;
        }

        public static IEnumerable<string>? UserIds { get; set; }

        public override IDisplayResult Display(MessagePart part, BuildPartDisplayContext context)
        {
            RefreshUserIdsDataAsync();


            return Initialize<MessagePartViewModel>(
                GetDisplayShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Detail", "Content:1")
            .Location("Summary", "Content:1");

        }
            

        public override IDisplayResult Edit(MessagePart part, BuildPartEditorContext context)
        {
            RefreshUserIdsDataAsync();

            return Initialize<MessagePartViewModel>(
                GetEditorShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Content:1");
        }
            

        public override async Task<IDisplayResult> UpdateAsync(MessagePart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var viewModel = new MessagePartViewModel();

            await updater.TryUpdateModelAsync(viewModel, Prefix);

            // Populate part from view model here.
            part.FromUserId = viewModel.FromUserId;
            part.ToUserId = viewModel.ToUserId;
            part.SendingTimeUtc = DateTime.UtcNow;

            return await EditAsync(part, context);
        }

        private static void PopulateViewModel(MessagePart part, MessagePartViewModel viewModel)
        {
            // Populate view model from part here.
            viewModel.FromUserId = part.FromUserId;
            viewModel.ToUserId = part.ToUserId;

            viewModel.SetList(UserIds);

        }

        private async void RefreshUserIdsDataAsync()
        {
            List<string> refreshedUsersData = new List<string>();

            var queriedusers = await _session.Query<User, UserIndex>().ListAsync();

            foreach (var user in queriedusers)
            {
                if (user != null) refreshedUsersData.Add(user.NormalizedUserName.ToLower());
            }

            UserIds = refreshedUsersData;
        }
    }
}
