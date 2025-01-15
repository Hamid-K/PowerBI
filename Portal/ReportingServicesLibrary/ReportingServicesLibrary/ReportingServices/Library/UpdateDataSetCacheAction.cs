using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000FF RID: 255
	internal sealed class UpdateDataSetCacheAction : UpdateCacheAction<DataSetCatalogItem>
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x000278B6 File Offset: 0x00025AB6
		public UpdateDataSetCacheAction(RSService service)
			: base("UpdateDataSetCache", service)
		{
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000278C4 File Offset: 0x00025AC4
		protected override void PerformItemTypeCheck(CatalogItem item)
		{
			item.ThrowIfWrongItemType(new ItemType[] { ItemType.DataSet });
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000278D6 File Offset: 0x00025AD6
		protected override CancelablePhaseBase ConstructExecutionPhase(DataSetCatalogItem item, JobType jobType)
		{
			return new CreateDataSetCacheEntry(item, jobType);
		}
	}
}
