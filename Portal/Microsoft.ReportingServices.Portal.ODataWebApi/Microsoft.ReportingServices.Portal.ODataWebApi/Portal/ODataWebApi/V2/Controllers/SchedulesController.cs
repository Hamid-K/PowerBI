using System;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000023 RID: 35
	public class SchedulesController : SchedulesController
	{
		// Token: 0x060001CA RID: 458 RVA: 0x0000761A File Offset: 0x0000581A
		public SchedulesController(ISystemService systemService, ILogger logger)
			: base(systemService, logger)
		{
			this._systemService = base.SystemService;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007630 File Offset: 0x00005830
		protected override IQueryable<Schedule> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007639 File Offset: 0x00005839
		protected override Schedule GetEntity(string key, string castName)
		{
			return base.GetEntity(key, castName);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007643 File Offset: 0x00005843
		protected override bool DeleteEntity(string key)
		{
			return base.DeleteEntity(key);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000764C File Offset: 0x0000584C
		protected override bool AddEntity(Schedule entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007655 File Offset: 0x00005855
		protected override bool PutEntity(string key, Schedule entity)
		{
			return base.PutEntity(key, entity);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000765F File Offset: 0x0000585F
		protected override bool PatchEntity(string key, Schedule entity, string[] delta)
		{
			return base.PatchEntity(key, entity, delta);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000766A File Offset: 0x0000586A
		public override string GetDescription(Guid key)
		{
			return base.GetDescription(key);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007673 File Offset: 0x00005873
		[HttpPost]
		public override IHttpActionResult Pause(Guid key)
		{
			return base.Pause(key);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000767C File Offset: 0x0000587C
		[HttpPost]
		public override IHttpActionResult Resume(Guid key)
		{
			return base.Resume(key);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007685 File Offset: 0x00005885
		[HttpPost]
		public override IHttpActionResult Describe(ODataActionParameters parameters)
		{
			return base.Describe(parameters);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000768E File Offset: 0x0000588E
		public new static void RegisterModel(ODataConventionModelBuilder builder)
		{
			SchedulesController.RegisterModel(builder);
		}

		// Token: 0x0400006A RID: 106
		private readonly ISystemService _systemService;
	}
}
