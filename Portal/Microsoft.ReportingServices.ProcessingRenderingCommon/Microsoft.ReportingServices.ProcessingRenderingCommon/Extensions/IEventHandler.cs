using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Extensions
{
	// Token: 0x02000010 RID: 16
	internal interface IEventHandler : IExtension
	{
		// Token: 0x06000088 RID: 136
		bool CanSubscribe(ICatalogQuery catalogQuery, string reportName);

		// Token: 0x06000089 RID: 137
		void ValidateSubscriptionData(Subscription subscription, string subscriptionData, UserContext userContext);

		// Token: 0x0600008A RID: 138
		void HandleEvent(ICatalogQuery catalogQuery, string eventType, string eventData);

		// Token: 0x0600008B RID: 139
		void CleanUp(Subscription subscription);
	}
}
