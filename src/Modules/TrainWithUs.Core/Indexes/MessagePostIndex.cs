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
    public class MessagePostIndex : MapIndex
    {
        public string ContentItemID { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public DateTime SendingTimeUtc { get; set; }
    }

    public class MessagePostIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context) =>
            context.For<MessagePostIndex>().When(contIt => contIt.Has<MessagePart>()).Map(contentItem =>
            {
                var message = contentItem.As<MessagePart>();
                if(message != null)
                {
                    return new MessagePostIndex()
                    {
                        ContentItemID = contentItem.ContentItemId,
                        FromUserId = message.FromUserId,
                        ToUserId = message.ToUserId,
                        SendingTimeUtc = message.SendingTimeUtc != null ? message.SendingTimeUtc.Value : DateTime.UtcNow
                    };

                }
                return null;
            });
    }
}
