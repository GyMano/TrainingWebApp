using OrchardCore.ContentManagement.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Models;

namespace TrainWithUs.Core.Handlers
{
    public class TrainingSubscriptionPartHandler : ContentPartHandler<TrainingSubscriptionPart>
    {
        public override Task UpdatedAsync(UpdateContentContext context, TrainingSubscriptionPart part)
        {

            context.ContentItem.DisplayText = part.AthleteUserId + "_" + part.TrainingId;

            return Task.CompletedTask;
        }
    }
}
