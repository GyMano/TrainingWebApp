using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Drivers;
using TrainWithUs.Core.Indexes;
using TrainWithUs.Core.Models;
using YesSql;

namespace TrainWithUs.Core.Handlers
{
    public class TrainingPartHandler : ContentPartHandler<TrainingPart>
    {
        private readonly ISession _session;

        public TrainingPartHandler(ISession session)
        {
            _session = session;
        }



        public async override Task UpdatedAsync(UpdateContentContext context, TrainingPart part)
        {
            string datestr = "NoDateAvailable";
            
            if (part.DateUtc != null)
            {
                datestr = string.Concat(part.DateUtc?.ToShortDateString().Where(char.IsNumber));
            }
            
            context.ContentItem.DisplayText = datestr + "_" + part.Sport + "_" + part.Title;

            var trainings = await _session
                .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "TrainingPost")
                .With<TrainingPostIndex>().ListAsync();

            List<string> freshtrainingdata = new List<string>();

            foreach (var training in trainings)
            {
                freshtrainingdata.Add(training.DisplayText);
            }

            TrainingSubscriptionPartDisplayDriver.TrainingList = freshtrainingdata;
            return;


        }

        
    }
}
