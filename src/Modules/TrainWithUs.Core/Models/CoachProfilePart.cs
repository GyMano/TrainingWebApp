using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainWithUs.Core.Models
{
    public class CoachProfilePart : ContentPart
    {
        public string? CoachId { get; set; }

        public string? Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public TextField? Introduction { get; set; }
    }
}
