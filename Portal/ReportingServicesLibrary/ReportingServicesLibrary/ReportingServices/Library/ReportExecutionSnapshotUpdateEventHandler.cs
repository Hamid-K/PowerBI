using System;
using System.Collections;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200008A RID: 138
	internal sealed class ReportExecutionSnapshotUpdateEventHandler : IEventHandler, IExtension
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x000053DC File Offset: 0x000035DC
		public bool CanSubscribe(ICatalogQuery catalogQuery, string reportName)
		{
			return true;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00017941 File Offset: 0x00015B41
		public void ValidateSubscriptionData(Subscription subscription, string subscriptionData, UserContext userContext)
		{
			if (subscriptionData != null && subscriptionData != "")
			{
				throw new InvalidParameterException("matchData");
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void CleanUp(Subscription subscription)
		{
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00017960 File Offset: 0x00015B60
		public void HandleEvent(ICatalogQuery catalogQuery, string eventType, string eventData)
		{
			if (RSTrace.ScheduleTracer.TraceInfo)
			{
				RSTrace.ScheduleTracer.Trace("Creating notifications for report execution snapshot update event for report {0}", new object[] { eventData });
			}
			string text = "CreateCacheUpdateNotifications";
			catalogQuery.ExecuteNonQuery(text, new Hashtable
			{
				{ "@ReportID", eventData },
				{
					"@LastRunTime",
					DateTime.Now
				}
			}, CommandType.StoredProcedure);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x000179C8 File Offset: 0x00015BC8
		public string LocalizedName
		{
			get
			{
				return RepLibRes.ReportExecutionSnapshotUpdateHandler;
			}
		}
	}
}
