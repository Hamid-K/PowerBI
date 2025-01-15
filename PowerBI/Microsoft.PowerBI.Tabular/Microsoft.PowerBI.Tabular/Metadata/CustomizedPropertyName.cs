using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001E5 RID: 485
	internal class CustomizedPropertyName
	{
		// Token: 0x06001C62 RID: 7266 RVA: 0x000C6021 File Offset: 0x000C4221
		internal CustomizedPropertyName(string originalName, string customName)
		{
			this.OriginalName = originalName;
			this.CustomName = customName;
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x000C6037 File Offset: 0x000C4237
		// (set) Token: 0x06001C64 RID: 7268 RVA: 0x000C603F File Offset: 0x000C423F
		internal string OriginalName { get; set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x000C6048 File Offset: 0x000C4248
		// (set) Token: 0x06001C66 RID: 7270 RVA: 0x000C6050 File Offset: 0x000C4250
		internal string CustomName { get; set; }
	}
}
