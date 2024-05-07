using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesSql;
using OrchardCore.ContentManagement.Records;
using TrainWithUs.Core.Indexes;
using TrainWithUs.Core.Permissions;

namespace TrainWithUs.Core.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ISession _session;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly IContentManager _contentManager;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly IAuthorizationService _authorizationService;

        public MessagesController(ISession session, IContentItemDisplayManager contentItemDisplayManager, IContentManager contentManager, IUpdateModelAccessor updateModelAccessor, IAuthorizationService authorizationService)
        {
            _session = session;
            _contentItemDisplayManager = contentItemDisplayManager;
            _contentManager = contentManager;
            _updateModelAccessor = updateModelAccessor;
            _authorizationService = authorizationService;
        }


        [Route("mymessages")]
        public async Task<IActionResult> ListUserMessages()
        {
            string userId = User?.Identity?.Name == null ? "" : User.Identity.Name;

            //check permission
            bool authorized = await _authorizationService.AuthorizeAsync(User, TrainingPermissions.SendMessagesPermission);


            if (!authorized)
            {
                return Redirect("/");
            }



            var messagePosts = await _session
                .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "MessagePost")
                .With<MessagePostIndex>(messagePostIndex => messagePostIndex.FromUserId == userId || messagePostIndex.ToUserId == userId).ListAsync();

            List<OrchardCore.DisplayManagement.IShape> shapes = new List<OrchardCore.DisplayManagement.IShape>();

            foreach (var messagePost in messagePosts)
            {
                await _contentManager.LoadAsync(messagePost);

                var shape = await _contentItemDisplayManager.BuildDisplayAsync(messagePost, _updateModelAccessor.ModelUpdater, "Summary");
                if (shape is not null) shapes.Add(shape);
            }

            return View(shapes);


        }
    }
}
