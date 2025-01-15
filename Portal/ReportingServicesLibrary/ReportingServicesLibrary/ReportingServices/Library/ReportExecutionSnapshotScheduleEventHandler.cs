using System;
using System.Collections;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200008B RID: 139
	internal sealed class ReportExecutionSnapshotScheduleEventHandler : ScheduleFireEventHandlerBase, IEventHandler, IExtension
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x00005BEF File Offset: 0x00003DEF
		public bool CanSubscribe(ICatalogQuery catalogQuery, string reportName)
		{
			return false;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void ValidateSubscriptionData(Subscription subscription, string subscriptionData, UserContext userContext)
		{
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void CleanUp(Subscription subscription)
		{
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000179CF File Offset: 0x00015BCF
		public void HandleEvent(ICatalogQuery catalogQuery, string eventType, string eventData)
		{
			base.HandleScheduleEvent(catalogQuery, eventData, new ScheduleFireEventHandlerBase.PerformEventActions(this.PerformActionHandler));
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x000179E5 File Offset: 0x00015BE5
		public string LocalizedName
		{
			get
			{
				return RepLibRes.CacheInvalidateScheduleHanlder;
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000179EC File Offset: 0x00015BEC
		private void PerformActionHandler(ICatalogQuery catalogQuery, ArrayList reportActions)
		{
			RSService rsservice = new RSService(false);
			CachedSystemProperties.InvalidateCache();
			using (rsservice.SetStreamFactory(new MemoryThenFileStreamFactory()))
			{
				foreach (object obj in reportActions)
				{
					ItemScheduleAction itemScheduleAction = (ItemScheduleAction)obj;
					RSService.EnsureItemTypeIsReportOrDataSet(itemScheduleAction.ItemType, itemScheduleAction.ItemPath.Value);
					rsservice.SetExternalRoot(itemScheduleAction.ItemPath);
					if (RSTrace.ScheduleTracer.TraceInfo)
					{
						RSTrace.ScheduleTracer.Trace("Updating report execution snapshot for report {0}", new object[] { itemScheduleAction.ItemPath });
					}
					UpdateExecutionSnapshotAction updateExecutionSnapshotAction = rsservice.UpdateExecutionSnapshotAction;
					updateExecutionSnapshotAction.ActionParameters.ReportPath = rsservice.CatalogToExternal(itemScheduleAction.ItemPath).Value;
					updateExecutionSnapshotAction.ActionParameters.JobType = JobType.SystemJobType;
					updateExecutionSnapshotAction.Execute();
				}
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00017AF4 File Offset: 0x00015CF4
		protected override ScheduleFireEventHandlerBase.RetrievalCommand ReportActionRetrievalCommand(string eventData)
		{
			return new ScheduleFireEventHandlerBase.RetrievalCommand
			{
				SqlCommand = "GetAReportsReportAction",
				Parameters = 
				{
					{ "@ReportID", eventData },
					{
						"@ReportAction",
						ReportScheduleActions.UpdateReportExecutionSnapshot
					}
				}
			};
		}
	}
}
