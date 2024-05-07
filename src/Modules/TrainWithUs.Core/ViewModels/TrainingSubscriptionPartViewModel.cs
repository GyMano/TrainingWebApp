using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainWithUs.Core.ViewModels
{
    public class TrainingSubscriptionPartViewModel
    {
        public string? AthleteUserId { get; set; }

        public string? TrainingId { get; set; }

        public IEnumerable<SelectListItem>? TrainingIdList { get; set; }

        public void SetList(IEnumerable<string> list)
        {
            List<SelectListItem> sl = new List<SelectListItem>();

            SelectListGroup slGroup = new SelectListGroup()
            {
                Disabled = false,
                Name = "TrainingIdListGroup"
            };

            foreach (var item in list)
            {
                sl.Add(new SelectListItem()
                {
                    Disabled = false,
                    Group = slGroup,
                    Selected = false,
                    Text = item,
                    Value = item

                });
            }

            TrainingIdList = sl;
        }
    }
}
