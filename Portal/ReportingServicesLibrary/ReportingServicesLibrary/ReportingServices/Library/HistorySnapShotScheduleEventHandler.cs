using System;
using System.Collections;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000238 RID: 568
	internal sealed class HistorySnapShotScheduleEventHandler : ScheduleFireEventHandlerBase, IEventHandler, IExtension
	{
		// Token: 0x060014AE RID: 5294 RVA: 0x00005BEF File Offset: 0x00003DEF
		public bool CanSubscribe(ICatalogQuery catalogQuery, string reportName)
		{
			return false;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void ValidateSubscriptionData(Subscription subscription, string subscriptionData, UserContext userContext)
		{
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void CleanUp(Subscription subscription)
		{
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0005067A File Offset: 0x0004E87A
		public void HandleEvent(ICatalogQuery catalogQuery, string eventType, string eventData)
		{
			base.HandleScheduleEvent(catalogQuery, eventData, new ScheduleFireEventHandlerBase.PerformEventActions(this.PerformActionHandler));
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00050690 File Offset: 0x0004E890
		public string LocalizedName
		{
			get
			{
				return RepLibRes.CanNotSubscribe;
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00050698 File Offset: 0x0004E898
		private void PerformActionHandler(ICatalogQuery catalogQuery, ArrayList reportActions)
		{
			RSService rsservice = new RSService(false);
			using (rsservice.SetStreamFactory(new MemoryThenFileStreamFactory()))
			{
				foreach (object obj in reportActions)
				{
					ItemScheduleAction itemScheduleAction = (ItemScheduleAction)obj;
					RSService.EnsureItemTypeIsReportOrDataSet(itemScheduleAction.ItemType, itemScheduleAction.ItemPath.Value);
					rsservice.SetExternalRoot(itemScheduleAction.ItemPath);
					CachedSystemProperties.InvalidateCache();
					if (RSTrace.ScheduleTracer.TraceInfo)
					{
						RSTrace.ScheduleTracer.Trace(TraceLevel.Info, "Creating report history snapshot for report {0}", new object[] { itemScheduleAction.ItemPath });
					}
					CreateSnapshotAction createSnapshotAction = rsservice.CreateSnapshotAction;
					createSnapshotAction.ActionParameters.ReportPath = rsservice.CatalogToExternal(itemScheduleAction.ItemPath).Value;
					createSnapshotAction.ActionParameters.JobType = JobType.SystemJobType;
					createSnapshotAction.Execute();
				}
			}
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000507A0 File Offset: 0x0004E9A0
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
						ReportScheduleActions.CreateReportHistorySnapshot
					}
				}
			};
		}
	}
}
