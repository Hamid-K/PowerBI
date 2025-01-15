using System;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers
{
	// Token: 0x0200002F RID: 47
	public class SchedulesController : EntitySetReflectionODataController<Schedule>
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00008A64 File Offset: 0x00006C64
		protected ISystemService SystemService
		{
			get
			{
				return this._systemService;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008A6C File Offset: 0x00006C6C
		public SchedulesController(ISystemService systemService, ILogger logger)
			: base(logger)
		{
			if (systemService == null)
			{
				throw new ArgumentNullException("systemService");
			}
			this._systemService = systemService;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008A8A File Offset: 0x00006C8A
		protected override IQueryable<Schedule> GetEntitySet(string castName)
		{
			return this._systemService.GetSchedules(base.User, base.GetUtcOffsetInMinutes());
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008AA4 File Offset: 0x00006CA4
		protected override Schedule GetEntity(string key, string castName)
		{
			Guid guid;
			if (SchedulesController.TryParseGuid(key, out guid))
			{
				return this._systemService.GetSchedule(base.User, guid, base.GetUtcOffsetInMinutes());
			}
			return null;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008AD8 File Offset: 0x00006CD8
		protected override bool DeleteEntity(string key)
		{
			Guid guid;
			return SchedulesController.TryParseGuid(key, out guid) && this._systemService.DeleteSchedule(base.User, guid);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008B03 File Offset: 0x00006D03
		protected override bool AddEntity(Schedule entity)
		{
			return this._systemService.AddSchedule(base.User, entity);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008B18 File Offset: 0x00006D18
		protected override bool PutEntity(string key, Schedule entity)
		{
			Guid guid;
			return SchedulesController.TryParseGuid(key, out guid) && this._systemService.UpdateSchedule(base.User, guid, entity);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008B44 File Offset: 0x00006D44
		protected override bool PatchEntity(string key, Schedule entity, string[] delta)
		{
			throw new NotImplementedException("Path not emplemented");
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008B50 File Offset: 0x00006D50
		public virtual string GetDescription(Guid key)
		{
			return this.GetEntity(key.ToString(), null).Description;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00008B6B File Offset: 0x00006D6B
		[HttpPost]
		public virtual IHttpActionResult Pause(Guid key)
		{
			if (this._systemService.PauseSchedule(base.User, key))
			{
				return base.Ok();
			}
			return base.BadRequest();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008B8E File Offset: 0x00006D8E
		[HttpPost]
		public virtual IHttpActionResult Resume(Guid key)
		{
			if (this._systemService.ResumeSchedule(base.User, key))
			{
				return base.Ok();
			}
			return base.BadRequest();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008BB4 File Offset: 0x00006DB4
		[HttpPost]
		public virtual IHttpActionResult Describe(ODataActionParameters parameters)
		{
			if (parameters != null)
			{
				Schedule schedule = parameters["schedule"] as Schedule;
				return this.Ok<string>(this._systemService.GetScheduleDescription(schedule, base.GetUtcOffsetInMinutes()));
			}
			return base.BadRequest();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008BF4 File Offset: 0x00006DF4
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<Schedule>("Schedules");
			builder.EntityType<Schedule>().Action("Pause");
			builder.EntityType<Schedule>().Action("Resume");
			builder.EntityType<Schedule>().Collection.Action("Describe").Parameter<Schedule>("schedule");
			builder.ComplexType<MinuteRecurrence>();
			builder.ComplexType<DailyRecurrence>();
			builder.ComplexType<WeeklyRecurrence>();
			builder.ComplexType<MonthlyRecurrence>();
			builder.ComplexType<MonthlyDOWRecurrence>();
			builder.ComplexType<ScheduleDefinition>();
			builder.ComplexType<ScheduleReference>();
			builder.ComplexType<ScheduleRecurrence>();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008C87 File Offset: 0x00006E87
		private static bool TryParseGuid(string key, out Guid gKey)
		{
			return Guid.TryParse(key.Replace("'", string.Empty), out gKey);
		}

		// Token: 0x04000082 RID: 130
		private readonly ISystemService _systemService;
	}
}
