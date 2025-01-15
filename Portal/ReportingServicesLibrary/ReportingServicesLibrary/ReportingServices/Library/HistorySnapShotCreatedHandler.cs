using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000237 RID: 567
	internal sealed class HistorySnapShotCreatedHandler : IEventHandler, IExtension
	{
		// Token: 0x060014A7 RID: 5287 RVA: 0x000053DC File Offset: 0x000035DC
		public bool CanSubscribe(ICatalogQuery catalogQuery, string reportName)
		{
			return true;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000505E3 File Offset: 0x0004E7E3
		public void ValidateSubscriptionData(Subscription subscription, string subscriptionData, UserContext userContext)
		{
			if (subscriptionData != null && subscriptionData != "")
			{
				throw new InvalidParameterException("matchData");
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void CleanUp(Subscription subscription)
		{
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00050600 File Offset: 0x0004E800
		public void HandleEvent(ICatalogQuery catalogQuery, string eventType, string eventData)
		{
			if (RSTrace.ScheduleTracer.TraceInfo)
			{
				RSTrace.ScheduleTracer.Trace(TraceLevel.Info, "Creating notifications for snapshot creation for snapshot id {0}", new object[] { eventData });
			}
			string text = "CreateSnapShotNotifications";
			catalogQuery.ExecuteNonQuery(text, new Hashtable
			{
				{
					"@HistoryID",
					new Guid(eventData)
				},
				{
					"@LastRunTime",
					DateTime.Now
				}
			}, CommandType.StoredProcedure);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x00050673 File Offset: 0x0004E873
		public string LocalizedName
		{
			get
			{
				return RepLibRes.ReportHistorySnapShotCreatedHandler;
			}
		}
	}
}
