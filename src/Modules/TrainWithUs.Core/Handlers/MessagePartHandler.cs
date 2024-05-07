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
    public class MessagePartHandler : ContentPartHandler<MessagePart>
    {
        public override Task UpdatedAsync(UpdateContentContext context, MessagePart part)
        {
            string datestr = "NoDateAvailable";

            if (part.SendingTimeUtc != null)
            {
                datestr = string.Concat(part.SendingTimeUtc?.ToShortDateString().Where(char.IsNumber));
            }

            context.ContentItem.DisplayText = datestr + "_" + part.FromUserId + "_" + part.ToUserId;

            return Task.CompletedTask;


        }


    }
}
