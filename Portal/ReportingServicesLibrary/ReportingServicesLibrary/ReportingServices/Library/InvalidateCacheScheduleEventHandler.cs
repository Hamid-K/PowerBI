using System;
using System.Collections;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200008C RID: 140
	internal sealed class InvalidateCacheScheduleEventHandler : ScheduleFireEventHandlerBase, IEventHandler, IExtension
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x00005BEF File Offset: 0x00003DEF
		public bool CanSubscribe(ICatalogQuery catalogQuery, string reportName)
		{
			return false;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void ValidateSubscriptionData(Subscription subscription, string subscriptionData, UserContext userContext)
		{
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void CleanUp(Subscription subscription)
		{
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00017B35 File Offset: 0x00015D35
		public void HandleEvent(ICatalogQuery catalogQuery, string eventType, string eventData)
		{
			base.HandleScheduleEvent(catalogQuery, eventData, new ScheduleFireEventHandlerBase.PerformEventActions(this.PerformActionHandler));
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000179E5 File Offset: 0x00015BE5
		public string LocalizedName
		{
			get
			{
				return RepLibRes.CacheInvalidateScheduleHanlder;
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00017B4C File Offset: 0x00015D4C
		private void PerformActionHandler(ICatalogQuery catalogQuery, ArrayList reportActions)
		{
			RSService rsservice = new RSService(false);
			using (rsservice.SetStreamFactory(new MemoryThenFileStreamFactory()))
			{
				CachedSystemProperties.InvalidateCache();
				foreach (object obj in reportActions)
				{
					ItemScheduleAction itemScheduleAction = (ItemScheduleAction)obj;
					RSService.EnsureItemTypeIsReportOrDataSet(itemScheduleAction.ItemType, itemScheduleAction.ItemPath.Value);
					rsservice.SetExternalRoot(itemScheduleAction.ItemPath);
					if (RSTrace.ScheduleTracer.TraceInfo)
					{
						RSTrace.ScheduleTracer.Trace("Invalidating report cache for report {0}", new object[] { itemScheduleAction.ItemPath });
					}
					FlushCacheAction flushCacheAction = rsservice.FlushCacheAction;
					flushCacheAction.BatchID = Guid.Empty;
					flushCacheAction.ActionParameters.ItemPath = rsservice.CatalogToExternal(itemScheduleAction.ItemPath).Value;
					flushCacheAction.Execute();
				}
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00017C50 File Offset: 0x00015E50
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
						ReportScheduleActions.InvalidateCache
					}
				}
			};
		}
	}
}
