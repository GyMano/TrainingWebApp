using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Models;
using TrainWithUs.Core.ViewModels;

namespace TrainWithUs.Core.Drivers
{
    public class TrainingPartDisplayDriver : ContentPartDisplayDriver<TrainingPart>
    {
        public override IDisplayResult Display(TrainingPart part, BuildPartDisplayContext context) =>
            Initialize<TrainingPartViewModel>(
                GetDisplayShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Detail", "Content:1")
            .Location("Summary", "Content:1");

        public override IDisplayResult Edit(TrainingPart part, BuildPartEditorContext context) =>
            Initialize<TrainingPartViewModel>(
                GetEditorShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Content:1");

        public override async Task<IDisplayResult> UpdateAsync(TrainingPart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var viewModel = new TrainingPartViewModel();

            await updater.TryUpdateModelAsync(viewModel, Prefix);

            part.Title = viewModel.Title;
            part.Sport = viewModel.Sport;
            part.DateUtc = viewModel.DateUtc;
            part.AuthorCoachId = viewModel.AuthorCoachId;

            return await EditAsync(part, context);
        }

        private static void PopulateViewModel(TrainingPart part, TrainingPartViewModel viewModel)
        {
            viewModel.Title = part.Title;
            viewModel.Sport = part.Sport;
            viewModel.DateUtc = part.DateUtc;
            viewModel.AuthorCoachId = part.AuthorCoachId;
        }
    }
}
