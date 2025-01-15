using System;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000020 RID: 32
	public class NotificationsController : NotificationsController
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00006D4F File Offset: 0x00004F4F
		public NotificationsController(INotificationService notificationService, ILogger logger)
			: base(notificationService, logger)
		{
			this._notificationService = base.NotificationService;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006D65 File Offset: 0x00004F65
		protected override IQueryable<Notification> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006D6E File Offset: 0x00004F6E
		protected override Notification GetEntity(string key, string castName)
		{
			return base.GetEntity(key, castName);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006D78 File Offset: 0x00004F78
		protected override bool DeleteEntity(string key)
		{
			return base.DeleteEntity(key);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006D81 File Offset: 0x00004F81
		protected override bool AddEntity(Notification entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006D8A File Offset: 0x00004F8A
		protected override bool PutEntity(string key, Notification entity)
		{
			return base.PutEntity(key, entity);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006D94 File Offset: 0x00004F94
		protected override bool PatchEntity(string key, Notification entity, string[] delta)
		{
			return base.PatchEntity(key, entity, delta);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006D9F File Offset: 0x00004F9F
		public new static void RegisterModel(ODataConventionModelBuilder builder)
		{
			NotificationsController.RegisterModel(builder);
		}

		// Token: 0x04000065 RID: 101
		private readonly INotificationService _notificationService;
	}
}
