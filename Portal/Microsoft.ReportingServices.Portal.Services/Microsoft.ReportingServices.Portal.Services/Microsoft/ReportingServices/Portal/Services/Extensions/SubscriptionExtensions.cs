using System;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Models.Impl;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x02000068 RID: 104
	internal static class SubscriptionExtensions
	{
		// Token: 0x0600031B RID: 795 RVA: 0x0001505C File Offset: 0x0001325C
		public static Subscription ToWebApiSubscription(this SubscriptionImpl librarySubscription)
		{
			if (librarySubscription == null)
			{
				throw new ArgumentNullException("librarySubscription");
			}
			return new Subscription
			{
				Id = librarySubscription.ID,
				Description = librarySubscription.Description,
				IsActive = librarySubscription.IsActive(),
				EventType = librarySubscription.EventType,
				LastRunTime = librarySubscription.LastRunTime,
				LastStatus = librarySubscription.LastStatus
			};
		}
	}
}
