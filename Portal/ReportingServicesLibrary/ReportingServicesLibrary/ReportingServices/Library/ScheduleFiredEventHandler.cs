using System;
using System.Collections;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200021E RID: 542
	internal sealed class ScheduleFiredEventHandler : ScheduleFireEventHandlerBase, IEventHandler, IExtension
	{
		// Token: 0x06001343 RID: 4931 RVA: 0x00005BEF File Offset: 0x00003DEF
		public bool CanSubscribe(ICatalogQuery catalogQuery, string reportName)
		{
			return false;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void ValidateSubscriptionData(Subscription subscription, string subscriptionData, UserContext userContext)
		{
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void CleanUp(Subscription subscription)
		{
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00045319 File Offset: 0x00043519
		public void HandleEvent(ICatalogQuery catalogQuery, string eventType, string eventData)
		{
			base.HandleScheduleEvent(catalogQuery, eventData, new ScheduleFireEventHandlerBase.PerformEventActions(this.PerformActionHandler));
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0004532F File Offset: 0x0004352F
		public string LocalizedName
		{
			get
			{
				return RepLibRes.ScheduleFiredEventHandlerName;
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00045338 File Offset: 0x00043538
		private void PerformActionHandler(ICatalogQuery catalogQuery, ArrayList reportActions)
		{
			foreach (object obj in reportActions)
			{
				ItemScheduleAction itemScheduleAction = (ItemScheduleAction)obj;
				string text = itemScheduleAction.ItemID.ToString();
				string text2;
				switch (itemScheduleAction.Action)
				{
				case ReportScheduleActions.UpdateReportExecutionSnapshot:
					text2 = InternalEvents.ReportExecutionUpdateSchedule.ToString();
					break;
				case ReportScheduleActions.CreateReportHistorySnapshot:
					text2 = InternalEvents.ReportHistorySchedule.ToString();
					break;
				case ReportScheduleActions.InvalidateCache:
					text2 = InternalEvents.CacheInvalidateSchedule.ToString();
					break;
				case ReportScheduleActions.TimedSubscription:
				case ReportScheduleActions.RefreshCache:
				case ReportScheduleActions.DataModelRefresh:
					text2 = itemScheduleAction.Action.ToString();
					text = itemScheduleAction.SubscriptionID.ToString();
					break;
				case ReportScheduleActions.SharedDatasetCacheUpdate:
				case ReportScheduleActions.CommentAddedAlert:
					continue;
				default:
					continue;
				}
				catalogQuery.ExecuteNonQuery("AddEvent", new Hashtable
				{
					{ "@EventType", text2 },
					{ "@EventData", text }
				}, CommandType.StoredProcedure);
				catalogQuery.Commit();
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00045470 File Offset: 0x00043670
		protected override ScheduleFireEventHandlerBase.RetrievalCommand ReportActionRetrievalCommand(string eventData)
		{
			return new ScheduleFireEventHandlerBase.RetrievalCommand
			{
				SqlCommand = "ListScheduledReports",
				Parameters = { { "@ScheduleID", eventData } }
			};
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00045493 File Offset: 0x00043693
		protected override Guid ScheduleID(ArrayList reportActions, string eventData)
		{
			if (reportActions.Count > 0)
			{
				return ((ItemScheduleAction)reportActions[0]).ScheduleID;
			}
			return new Guid(eventData);
		}
	}
}
