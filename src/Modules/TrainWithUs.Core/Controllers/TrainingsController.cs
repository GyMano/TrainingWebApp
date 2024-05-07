using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainWithUs.Core.Indexes;
using YesSql;


using OrchardCore.ContentFields.Fields;

using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Modules;
using OrchardCore.DisplayManagement.Shapes;
using TrainWithUs.Core.Models;
using Microsoft.AspNetCore.Authorization;
using TrainWithUs.Core.Permissions;
using OrchardCore.DisplayManagement;


namespace TrainWithUs.Core.Controllers
{
    public class TrainingsController : Controller
    {
        private readonly ISession _session;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly IContentManager _contentManager;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly IAuthorizationService _authorizationService;

        public TrainingsController(ISession session, IContentItemDisplayManager contentItemDisplayManager, IContentManager contentManager, IUpdateModelAccessor updateModelAccessor, IAuthorizationService authorizationService)
        {
            _session = session;
            _contentItemDisplayManager = contentItemDisplayManager;
            _contentManager = contentManager;
            _updateModelAccessor = updateModelAccessor;
            _authorizationService = authorizationService;
        }

        [Route("coachtrainings")]
        public async Task<IActionResult> ListCoachTrainings()
        {
            string userId = User?.Identity?.Name == null ? "" : User.Identity.Name;
            //check permission
            bool authorized = await _authorizationService.AuthorizeAsync(User, TrainingPermissions.WriteTrainingPermission);


            if (!authorized) 
            {
                return Redirect("/"); 
            }

            string coachId = userId;

            var trainingPosts = await _session
                .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "TrainingPost")
                .With<TrainingPostIndex>(trainingPostIndex => trainingPostIndex.Coach == coachId).ListAsync();

            List<OrchardCore.DisplayManagement.IShape> shapes = new List<OrchardCore.DisplayManagement.IShape>();

            foreach (var trainingPost in trainingPosts)
            {
                await _contentManager.LoadAsync(trainingPost);
                
                var shape = await _contentItemDisplayManager.BuildDisplayAsync(trainingPost, _updateModelAccessor.ModelUpdater, "Summary");
                if (shape is not null) shapes.Add(shape);
            }

            return View(shapes);
            

        }

        [Route("mytrainings")]
        public async Task<IActionResult> ListAthletesTrainings(string athleteId)
        {
            string userId = User?.Identity?.Name == null ? "" : User.Identity.Name;

            //check permission
            
            bool authorized = await _authorizationService.AuthorizeAsync(User, TrainingPermissions.ManageOwnSubscriptions);


            if (!authorized)
            {
                return Redirect("/");
            }

            //search subscribed trainings


            //get subscriptions
            var subscriptions = await _session
                .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "TrainingSubsription")
                .With<TrainingSubscriptionPartIndex>(subsc => subsc.AthleteUserId == userId).ListAsync();

            //load them
            foreach (var sub in subscriptions)
            {
                await _contentManager.LoadAsync(sub);
            }
            
            //get trainings
            var trainings = await _session
                .Query<ContentItem, ContentItemIndex>(idx => idx.ContentType == "TrainingPost").ListAsync();
            
            //load them
            foreach (var tr in trainings)
            {
                await _contentManager.LoadAsync(tr);
            }

            var filteredTrainingPosts = new List<ContentItem>();
            //filter trainings
            foreach (var sub in subscriptions)
            {

                var training = trainings
                    .Where(x => x.DisplayText == sub.As<TrainingSubscriptionPart>().TrainingId)
                    .FirstOrDefault();

                if (training != null) filteredTrainingPosts.Add(training);
            }
            
            List<OrchardCore.DisplayManagement.IShape> shapes = new List<OrchardCore.DisplayManagement.IShape>();

            foreach (var trainingPost in filteredTrainingPosts)
            {
                await _contentManager.LoadAsync(trainingPost);
                
                var shape = await _contentItemDisplayManager.BuildDisplayAsync(trainingPost, _updateModelAccessor.ModelUpdater, "Summary");

                if (shape is not null) shapes.Add(shape);
            }

            return View(shapes);
        }
    }
}
