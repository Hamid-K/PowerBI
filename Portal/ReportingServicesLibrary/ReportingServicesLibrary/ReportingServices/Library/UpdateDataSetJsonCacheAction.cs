using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000100 RID: 256
	internal sealed class UpdateDataSetJsonCacheAction : UpdateCacheAction<DataSetCatalogItem>
	{
		// Token: 0x06000A59 RID: 2649 RVA: 0x000278DF File Offset: 0x00025ADF
		public UpdateDataSetJsonCacheAction(RSService service)
			: base("UpdateDataSetJsonCache", service)
		{
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x000278C4 File Offset: 0x00025AC4
		protected override void PerformItemTypeCheck(CatalogItem item)
		{
			item.ThrowIfWrongItemType(new ItemType[] { ItemType.DataSet });
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000278ED File Offset: 0x00025AED
		protected override CancelablePhaseBase ConstructExecutionPhase(DataSetCatalogItem item, JobType jobType)
		{
			return new CreateDataSetJsonCacheEntry(item, base.ActionParameters.ItemPath, jobType);
		}
	}
}
