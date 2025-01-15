using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200008C RID: 140
	internal interface IAdomdBaseObject
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600089B RID: 2203
		AdomdConnection Connection { get; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600089C RID: 2204
		// (set) Token: 0x0600089D RID: 2205
		bool IsMetadata { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600089E RID: 2206
		// (set) Token: 0x0600089F RID: 2207
		object MetadataData { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060008A0 RID: 2208
		// (set) Token: 0x060008A1 RID: 2209
		IAdomdBaseObject ParentObject { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060008A2 RID: 2210
		string InternalUniqueName { get; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060008A3 RID: 2211
		string CubeName { get; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060008A4 RID: 2212
		SchemaObjectType SchemaObjectType { get; }
	}
}
