using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000204 RID: 516
	internal interface ICompiledTextRunInstance
	{
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06001348 RID: 4936
		// (set) Token: 0x06001349 RID: 4937
		ICompiledStyleInstance Style { get; set; }

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600134A RID: 4938
		// (set) Token: 0x0600134B RID: 4939
		string Value { get; set; }

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600134C RID: 4940
		// (set) Token: 0x0600134D RID: 4941
		string Label { get; set; }

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x0600134E RID: 4942
		// (set) Token: 0x0600134F RID: 4943
		string ToolTip { get; set; }

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001350 RID: 4944
		// (set) Token: 0x06001351 RID: 4945
		MarkupType MarkupType { get; set; }

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001352 RID: 4946
		// (set) Token: 0x06001353 RID: 4947
		IActionInstance ActionInstance { get; set; }
	}
}
