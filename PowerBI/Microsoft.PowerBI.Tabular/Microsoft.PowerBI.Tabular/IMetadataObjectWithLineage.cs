using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000FD RID: 253
	public interface IMetadataObjectWithLineage
	{
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001061 RID: 4193
		// (set) Token: 0x06001062 RID: 4194
		string LineageTag { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001063 RID: 4195
		// (set) Token: 0x06001064 RID: 4196
		string SourceLineageTag { get; set; }
	}
}
