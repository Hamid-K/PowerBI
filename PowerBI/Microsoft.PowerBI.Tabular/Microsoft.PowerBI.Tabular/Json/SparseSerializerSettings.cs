using System;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001BE RID: 446
	internal class SparseSerializerSettings
	{
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x000C2EE8 File Offset: 0x000C10E8
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x000C2EF0 File Offset: 0x000C10F0
		public SparseSerializerSettings.SerializeObjectDelegate SerializeObject { get; set; }

		// Token: 0x02000420 RID: 1056
		// (Invoke) Token: 0x06002850 RID: 10320
		public delegate void SerializeObjectDelegate(MetadataObject metadataObject, JsonObject jsonObject, CompatibilityMode mode, int dbCompatibilityLevel);
	}
}
