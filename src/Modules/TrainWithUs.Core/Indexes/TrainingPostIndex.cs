using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Models;
using YesSql.Indexes;
using YesSql.Sql;

namespace TrainWithUs.Core.Indexes
{
    public class TrainingPostIndex : MapIndex
    {
        public string? ContentItemID { get; set; }

        public string? Coach { get; set; }
    }

    public class TrainingPostIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context) =>
            context.For<TrainingPostIndex>().When(contentItem => contentItem.Has<TrainingPart>()).Map(contentItem =>
            {
                var trainingPart = contentItem.As<TrainingPart>();

                return trainingPart == null
                ? null
                : new TrainingPostIndex()
                {
                    ContentItemID = contentItem.ContentItemId,
                    Coach = trainingPart.AuthorCoachId
                };
            });
    }
}
