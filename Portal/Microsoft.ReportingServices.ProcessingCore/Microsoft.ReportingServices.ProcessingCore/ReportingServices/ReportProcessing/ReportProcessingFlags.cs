using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000641 RID: 1601
	[Flags]
	public enum ReportProcessingFlags
	{
		// Token: 0x04002E4A RID: 11850
		NotSet = 0,
		// Token: 0x04002E4B RID: 11851
		OnDemandEngine = 1,
		// Token: 0x04002E4C RID: 11852
		YukonEngine = 16,
		// Token: 0x04002E4D RID: 11853
		UpgradedYukonSnapshot = 2,
		// Token: 0x04002E4E RID: 11854
		YukonSnapshot = 32
	}
}
