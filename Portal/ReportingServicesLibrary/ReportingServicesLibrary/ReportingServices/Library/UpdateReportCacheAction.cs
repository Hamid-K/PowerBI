using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001CA RID: 458
	internal sealed class UpdateReportCacheAction : UpdateCacheAction<BaseReportCatalogItem>
	{
		// Token: 0x0600100E RID: 4110 RVA: 0x00038F87 File Offset: 0x00037187
		public UpdateReportCacheAction(RSService service)
			: base("UpdateReportCache", service)
		{
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00038F95 File Offset: 0x00037195
		protected override CancelablePhaseBase ConstructExecutionPhase(BaseReportCatalogItem item, JobType jobType)
		{
			return new CreateReportCacheEntry(item, jobType);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00038F9E File Offset: 0x0003719E
		protected override void PerformItemTypeCheck(CatalogItem item)
		{
			item.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport
			});
		}
	}
}
