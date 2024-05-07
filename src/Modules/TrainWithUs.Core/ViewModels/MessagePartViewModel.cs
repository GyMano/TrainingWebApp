using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainWithUs.Core.ViewModels
{
    public class MessagePartViewModel
    {
        public string? FromUserId { get; set; }
        public string? ToUserId { get; set; }


        private DateTime? _sendingTime;
        public DateTime SendingTimeUtc { 
            get 
            { 
                if (_sendingTime == null) _sendingTime = DateTime.UtcNow;
                return _sendingTime.Value;
            } 
            set 
            { 
                _sendingTime = value;
            } 
        }

        
        public List<SelectListItem>? UserIds { get; set; }

        public void SetList(IEnumerable<string> list)
        {
            List<SelectListItem> sl = new List<SelectListItem>();

            SelectListGroup slGroup = new SelectListGroup()
            {
                Disabled = false,
                Name = "UserIdListGroup"
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

            UserIds = sl;
        }
    }
}
