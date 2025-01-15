using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001F2 RID: 498
	internal interface INotifyObjectLineageTagChange
	{
		// Token: 0x06001CA2 RID: 7330
		void NotifyTagChanging(IMetadataObjectWithLineage obj, string newTag);

		// Token: 0x06001CA3 RID: 7331
		void NotifyTagChanged(IMetadataObjectWithLineage obj, string oldTag);
	}
}
