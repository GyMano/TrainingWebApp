using Microsoft.AspNetCore.Authorization;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Indexes;
using TrainWithUs.Core.Models;
using TrainWithUs.Core.ViewModels;
using YesSql;

namespace TrainWithUs.Core.Drivers
{
    public class TrainingSubscriptionPartDisplayDriver : ContentPartDisplayDriver<TrainingSubscriptionPart>
    {
        private readonly ISession _session;

        private static List<string> trainingList = new List<string>();
        public static List<string> TrainingList
        {
            get
            {
                return trainingList;
            }
            set { trainingList = value; }
        }

        public TrainingSubscriptionPartDisplayDriver(ISession session, IAuthorizationService authorizationService)
        {

            _session = session;
        }

        public override IDisplayResult Display(TrainingSubscriptionPart part, BuildPartDisplayContext context)
        {
            RefreshTrainingDataAsync();


            return Initialize<TrainingSubscriptionPartViewModel>(
                GetDisplayShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Detail", "Content:1")
            .Location("Summary", "Content:1"); 
        }   

        public override IDisplayResult Edit(TrainingSubscriptionPart part, BuildPartEditorContext context)
        {
            RefreshTrainingDataAsync();


            return Initialize<TrainingSubscriptionPartViewModel>(
                GetEditorShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Content:1");
        }
        public override async Task<IDisplayResult> UpdateAsync(TrainingSubscriptionPart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var viewModel = new TrainingSubscriptionPartViewModel();

            await updater.TryUpdateModelAsync(viewModel, Prefix);

            // Populate part from view model here.
            part.AthleteUserId = viewModel.AthleteUserId;
            part.TrainingId = viewModel.TrainingId;

            return await EditAsync(part, context);
        }

        private static void PopulateViewModel(TrainingSubscriptionPart part, TrainingSubscriptionPartViewModel viewModel)
        {
            // Populate view model from part here.
            viewModel.AthleteUserId = part.AthleteUserId;
            viewModel.TrainingId = part.TrainingId;

            viewModel.SetList(TrainingList);
        }

        private async void RefreshTrainingDataAsync()
        {
            var trainings = await _session
                .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "TrainingPost")
                .With<TrainingPostIndex>().ListAsync();
            List<string> freshtrainingdata = new List<string>();

            foreach (var training in trainings)
            {
                freshtrainingdata.Add(training.DisplayText);
            }
        }
    }
}


