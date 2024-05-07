using Microsoft.VisualBasic.FileIO;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainWithUs.Core.Models
{
    public class TrainingPart : ContentPart
    {
        public string? Title { get; set; }

        public SportEnum Sport { get; set; }

        public DateTime? DateUtc { get; set; }

        public TextField? Exercises { get; set; }

        public string? AuthorCoachId { get; set; }
    }

    public enum SportEnum
    {
        bicycle, running, swimming
    }
}
