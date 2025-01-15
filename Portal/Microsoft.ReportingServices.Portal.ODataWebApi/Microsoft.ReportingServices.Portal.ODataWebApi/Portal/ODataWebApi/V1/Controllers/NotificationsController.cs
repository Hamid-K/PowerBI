using System;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers
{
	// Token: 0x0200002E RID: 46
	public class NotificationsController : EntitySetReflectionODataController<Notification>
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00008A11 File Offset: 0x00006C11
		protected INotificationService NotificationService
		{
			get
			{
				return this._notificationService;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008A19 File Offset: 0x00006C19
		public NotificationsController(INotificationService notificationService, ILogger logger)
			: base(logger)
		{
			if (notificationService == null)
			{
				throw new ArgumentNullException("notificationService");
			}
			this._notificationService = notificationService;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008A37 File Offset: 0x00006C37
		protected override IQueryable<Notification> GetEntitySet(string castName)
		{
			return this._notificationService.GetNotifications(base.User).AsQueryable<Notification>();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008A4F File Offset: 0x00006C4F
		protected override Notification GetEntity(string key, string castName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008A4F File Offset: 0x00006C4F
		protected override bool DeleteEntity(string key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00008A4F File Offset: 0x00006C4F
		protected override bool AddEntity(Notification entity)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008A4F File Offset: 0x00006C4F
		protected override bool PutEntity(string key, Notification entity)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008A4F File Offset: 0x00006C4F
		protected override bool PatchEntity(string key, Notification entity, string[] delta)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008A56 File Offset: 0x00006C56
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<Notification>("Notifications");
		}

		// Token: 0x04000081 RID: 129
		private readonly INotificationService _notificationService;
	}
}
