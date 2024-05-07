using OrchardCore.ContentFields.Fields;
using OrchardCore.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Models;

namespace TrainWithUs.Core.ViewModels
{
    public class TrainingPartViewModel
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public SportEnum Sport { get; set; }

        [Required]
        public DateTime? DateUtc { get; set; }


        public string? AuthorCoachId { get; set; }

    }
}
