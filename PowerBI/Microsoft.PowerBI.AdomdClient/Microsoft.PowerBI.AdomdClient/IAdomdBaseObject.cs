using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200008C RID: 140
	internal interface IAdomdBaseObject
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600088E RID: 2190
		AdomdConnection Connection { get; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600088F RID: 2191
		// (set) Token: 0x06000890 RID: 2192
		bool IsMetadata { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000891 RID: 2193
		// (set) Token: 0x06000892 RID: 2194
		object MetadataData { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000893 RID: 2195
		// (set) Token: 0x06000894 RID: 2196
		IAdomdBaseObject ParentObject { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000895 RID: 2197
		string InternalUniqueName { get; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000896 RID: 2198
		string CubeName { get; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000897 RID: 2199
		SchemaObjectType SchemaObjectType { get; }
	}
}
