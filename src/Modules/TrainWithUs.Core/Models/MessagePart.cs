using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainWithUs.Core.Models
{
    public class MessagePart : ContentPart
    {
        public string? FromUserId { get; set; }
        public string? ToUserId { get; set; }

        public DateTime? SendingTimeUtc { get; set; }

        public TextField? MessageText { get; set; }


    }
}
