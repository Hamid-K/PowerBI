using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001F1 RID: 497
	internal interface INotifyObjectIdChange
	{
		// Token: 0x06001CA0 RID: 7328
		void NotifyIdChanging(MetadataObject obj, ObjectId newId);

		// Token: 0x06001CA1 RID: 7329
		void NotifyIdChanged(MetadataObject obj, ObjectId oldId);
	}
}
