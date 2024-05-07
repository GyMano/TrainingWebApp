using OrchardCore.ContentManagement.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Models;

namespace TrainWithUs.Core.Handlers
{
    public class CoachProfilePartHandler : ContentPartHandler<CoachProfilePart>
    {
        public override Task UpdatedAsync(UpdateContentContext context, CoachProfilePart part)
        {
            context.ContentItem.DisplayText = part.Name;

            return Task.CompletedTask;
        }



    }
}
