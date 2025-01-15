using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001EA RID: 490
	internal interface IMetadataObjectCollection : INotifyObjectIdChange, ITxObject
	{
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001C7C RID: 7292
		ObjectType ItemType { get; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001C7D RID: 7293
		MetadataObject Owner { get; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001C7E RID: 7294
		int Count { get; }

		// Token: 0x06001C7F RID: 7295
		IEnumerable<MetadataObject> GetObjects();

		// Token: 0x06001C80 RID: 7296
		void Add(MetadataObject obj);

		// Token: 0x06001C81 RID: 7297
		void Remove(MetadataObject obj);
	}
}
