using OrchardCore.ContentManagement;
using OrchardCore.Indexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Models;
using YesSql.Indexes;

namespace TrainWithUs.Core.Indexes
{
    public class CoachProfilePartIndex : MapIndex
    {

        public string? ContentItemID { get; set; }

        public string? CoachId { get; set; }

        
    }

    public class CoachProfilePartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context) =>
            context.For<CoachProfilePartIndex>().When(ci => ci.Has<CoachProfilePart>()).Map(contentItem =>
            {
                var coachProf = contentItem.As<CoachProfilePart>();

                return coachProf == null
                ? null
                : new CoachProfilePartIndex()
                {
                    ContentItemID = contentItem.ContentItemId,
                    CoachId = coachProf.CoachId
                };
            });
    }
}
