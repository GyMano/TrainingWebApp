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

namespace TrainWithUs.Core.Controllers
{
    public class CoachController : Controller
    {
        private readonly ISession _session;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly IContentManager _contentManager;
        private readonly IUpdateModelAccessor _updateModelAccessor;

        public CoachController(ISession session, IContentItemDisplayManager contentItemDisplayManager, IContentManager contentManager, IUpdateModelAccessor updateModelAccessor)
        {
            _session = session;
            _contentItemDisplayManager = contentItemDisplayManager;
            _contentManager = contentManager;
            _updateModelAccessor = updateModelAccessor;
        }

        [Route("coaches")]
        public async Task<IActionResult> ListCoaches()
        {
            

            var caochpostS = await _session
                .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "CoachProfilePost")
                .With<CoachProfilePartIndex>().ListAsync();

            List<OrchardCore.DisplayManagement.IShape> shapes = new List<OrchardCore.DisplayManagement.IShape>();

            foreach (var coachpost in caochpostS)
            {
                await _contentManager.LoadAsync(coachpost);

                var shape = await _contentItemDisplayManager.BuildDisplayAsync(coachpost, _updateModelAccessor.ModelUpdater, "Summary");
                if (shape is not null) shapes.Add(shape);
            }

            return View(shapes);


        }
    }
}
