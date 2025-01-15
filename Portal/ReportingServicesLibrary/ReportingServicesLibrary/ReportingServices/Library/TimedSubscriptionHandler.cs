using System;
using System.Collections;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200025A RID: 602
	internal sealed class TimedSubscriptionHandler : ScheduleFireEventHandlerBase, IEventHandler, IExtension
	{
		// Token: 0x060015E5 RID: 5605 RVA: 0x000053DC File Offset: 0x000035DC
		public bool CanSubscribe(ICatalogQuery catalogQuery, string reportName)
		{
			return true;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void ValidateSubscriptionData(Subscription subscription, string subscriptionData, UserContext userContext)
		{
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000576D8 File Offset: 0x000558D8
		public void CleanUp(Subscription subscription)
		{
			SchedulingDBInterface schedulingDBInterface = new SchedulingDBInterface();
			schedulingDBInterface.ConnectionManager = ((SubscriptionImpl)subscription).m_connectionManager;
			Task timeBasedSubscriptionSchedule = schedulingDBInterface.GetTimeBasedSubscriptionSchedule(subscription.ID);
			if (timeBasedSubscriptionSchedule != null)
			{
				if (timeBasedSubscriptionSchedule.Type == ScheduleType.Shared)
				{
					schedulingDBInterface.RemoveReportFromSchedule(timeBasedSubscriptionSchedule.ID, subscription.ItemID, subscription.ID, ReportScheduleActions.TimedSubscription);
					return;
				}
				schedulingDBInterface.DeleteTimeBasedSubscriptionSchedule(subscription.ID);
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0005773A File Offset: 0x0005593A
		public void HandleEvent(ICatalogQuery catalogQuery, string eventType, string eventData)
		{
			base.HandleScheduleEvent(catalogQuery, eventData, new ScheduleFireEventHandlerBase.PerformEventActions(this.PerformActionHandler));
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00057750 File Offset: 0x00055950
		public string LocalizedName
		{
			get
			{
				return RepLibRes.TimedSubscriptions;
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00057758 File Offset: 0x00055958
		private void PerformActionHandler(ICatalogQuery catalogQuery, ArrayList reportActions)
		{
			foreach (object obj in reportActions)
			{
				ItemScheduleAction itemScheduleAction = (ItemScheduleAction)obj;
				RSService.EnsureItemTypeIsReportOrDataSet(itemScheduleAction.ItemType, itemScheduleAction.ItemPath.Value);
				if (RSTrace.ScheduleTracer.TraceInfo)
				{
					RSTrace.ScheduleTracer.Trace("Creating Time based subscription notification for subscription: {0}", new object[] { itemScheduleAction.SubscriptionID });
				}
				TimedSubscriptionHandler.CallCreateNotification(catalogQuery, itemScheduleAction);
			}
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000577F4 File Offset: 0x000559F4
		protected override ScheduleFireEventHandlerBase.RetrievalCommand ReportActionRetrievalCommand(string eventData)
		{
			return new ScheduleFireEventHandlerBase.RetrievalCommand
			{
				SqlCommand = "GetTimeBasedSubscriptionReportAction",
				Parameters = { { "@SubscriptionID", eventData } }
			};
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00057818 File Offset: 0x00055A18
		internal static void CallCreateNotification(ICatalogQuery catalogQuery, ItemScheduleAction reportAction)
		{
			RSTrace.SubscriptionTracer.Assert(reportAction.Action == ReportScheduleActions.TimedSubscription);
			string text = "CreateTimeBasedSubscriptionNotification";
			catalogQuery.ExecuteNonQuery(text, new Hashtable
			{
				{ "@SubscriptionID", reportAction.SubscriptionID },
				{
					"@LastRunTime",
					DateTime.Now
				},
				{
					"@LastStatus",
					RepLibRes.SubscriptionPending
				}
			}, CommandType.StoredProcedure);
		}
	}
}
