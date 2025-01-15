using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.Services.ODataExtensions;
using Model;

namespace Microsoft.ReportingServices.Portal.Services
{
	// Token: 0x0200001A RID: 26
	internal sealed class NotificationService : INotificationService
	{
		// Token: 0x06000177 RID: 375 RVA: 0x0000C250 File Offset: 0x0000A450
		internal NotificationService(IPowerBIIntegrationService powerBIIntegrationService, ISubscriptionService subscriptionService)
		{
			if (powerBIIntegrationService == null)
			{
				throw new ArgumentNullException("powerBIIntegrationService");
			}
			if (subscriptionService == null)
			{
				throw new ArgumentNullException("subscriptionService");
			}
			this._powerBiIntegrationService = powerBIIntegrationService;
			this._subscriptionService = subscriptionService;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000C282 File Offset: 0x0000A482
		public IEnumerable<Notification> GetNotifications(IPrincipal userPrincipal)
		{
			return this.CollectPowerBINotifications(userPrincipal).ToArray<Notification>();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000C290 File Offset: 0x0000A490
		private IEnumerable<Notification> CollectPowerBINotifications(IPrincipal userPrincipal)
		{
			if (this._powerBiIntegrationService.IsPowerBIEnabled())
			{
				PowerBIUserInfo userInfo = this._powerBiIntegrationService.GetUserInfo(userPrincipal);
				if (userInfo.Status != PowerBIUserStatus.SignedIn && (from item in this._subscriptionService.GetSubscriptions(userPrincipal)
					where item.IsActive
					select this._subscriptionService.GetSubscription(userPrincipal, item.Id)).Any((Subscription s) => (int)s.KnownAs() == 1))
				{
					IssueType issueType = ((userInfo.Status == PowerBIUserStatus.Expired) ? IssueType.PowerBITokenNeedsRenewal : IssueType.PowerBITokenNeeded);
					yield return new Notification
					{
						IssueType = issueType
					};
				}
			}
			yield break;
		}

		// Token: 0x04000077 RID: 119
		private readonly IPowerBIIntegrationService _powerBiIntegrationService;

		// Token: 0x04000078 RID: 120
		private readonly ISubscriptionService _subscriptionService;
	}
}
