using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001EC RID: 492
	internal interface IMetadataObjectCollectionBody : ITxObjectBody
	{
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001C83 RID: 7299
		IEnumerable<MetadataObject> AllObjects { get; }
	}
}
