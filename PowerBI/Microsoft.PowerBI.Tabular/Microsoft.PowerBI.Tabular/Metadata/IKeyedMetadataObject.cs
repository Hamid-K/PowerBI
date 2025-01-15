using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001E7 RID: 487
	internal interface IKeyedMetadataObject
	{
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001C6D RID: 7277
		object Key { get; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001C6E RID: 7278
		string LogicalPathElement { get; }
	}
}
