using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Models;
using YesSql.Indexes;

namespace TrainWithUs.Core.Indexes
{
    public class TrainingSubscriptionPartIndex : MapIndex
    {
        public string ContentItemID { get; set; }

        public string AthleteUserId { get; set; }
    }

    public class TrainingSubscriptionPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context) =>
            context.For<TrainingSubscriptionPartIndex>().When(ci => ci.Has<TrainingSubscriptionPart>()).Map(contentItem =>
            {

                var item = contentItem.As<TrainingSubscriptionPart>();

                if (item != null)
                {
                    return new TrainingSubscriptionPartIndex()
                    {
                        ContentItemID = contentItem.ContentItemId,
                        AthleteUserId = item.AthleteUserId
                    };
                }
                else return null;
                
            });
    }
}
