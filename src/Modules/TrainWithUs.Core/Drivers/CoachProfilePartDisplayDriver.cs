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
    public class CoachProfilePartDisplayDriver : ContentPartDisplayDriver<CoachProfilePart>
    {
        public override IDisplayResult Display(CoachProfilePart part, BuildPartDisplayContext context) =>
            Initialize<CoachProfilePartViewModel>(
                GetDisplayShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Detail", "Content:1")
            .Location("Summary", "Content:1");

        public override IDisplayResult Edit(CoachProfilePart part, BuildPartEditorContext context) =>
            Initialize<CoachProfilePartViewModel>(
                GetEditorShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Content:1");

        public override async Task<IDisplayResult> UpdateAsync(CoachProfilePart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var viewModel = new CoachProfilePartViewModel();

            await updater.TryUpdateModelAsync(viewModel, Prefix);

            // Populate part from view model here.
            part.Name = viewModel.Name;
            part.BirthDate = viewModel.BirthDate;
            part.CoachId = viewModel.CoachId;


            return await EditAsync(part, context);
        }

        private static void PopulateViewModel(CoachProfilePart part, CoachProfilePartViewModel viewModel)
        {
            // Populate view model from part here.
            viewModel.Name = part.Name;
            viewModel.BirthDate = part.BirthDate;
            viewModel.CoachId = part.CoachId;
        }
    }
}
