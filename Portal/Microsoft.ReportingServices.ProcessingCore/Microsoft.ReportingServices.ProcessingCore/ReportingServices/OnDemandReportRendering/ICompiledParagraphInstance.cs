using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000207 RID: 519
	internal interface ICompiledParagraphInstance
	{
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06001390 RID: 5008
		// (set) Token: 0x06001391 RID: 5009
		IList<ICompiledTextRunInstance> CompiledTextRunInstances { get; set; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06001392 RID: 5010
		// (set) Token: 0x06001393 RID: 5011
		ICompiledStyleInstance Style { get; set; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06001394 RID: 5012
		// (set) Token: 0x06001395 RID: 5013
		ListStyle ListStyle { get; set; }

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06001396 RID: 5014
		// (set) Token: 0x06001397 RID: 5015
		int ListLevel { get; set; }

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06001398 RID: 5016
		// (set) Token: 0x06001399 RID: 5017
		ReportSize LeftIndent { get; set; }

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600139A RID: 5018
		// (set) Token: 0x0600139B RID: 5019
		ReportSize RightIndent { get; set; }

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600139C RID: 5020
		// (set) Token: 0x0600139D RID: 5021
		ReportSize HangingIndent { get; set; }

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x0600139E RID: 5022
		// (set) Token: 0x0600139F RID: 5023
		ReportSize SpaceBefore { get; set; }

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060013A0 RID: 5024
		// (set) Token: 0x060013A1 RID: 5025
		ReportSize SpaceAfter { get; set; }
	}
}
