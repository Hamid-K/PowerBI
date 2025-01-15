using System;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000012 RID: 18
	public class PbixDataSourceCheckResult
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003BEA File Offset: 0x00001DEA
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003BF2 File Offset: 0x00001DF2
		public bool IsSuccessful { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003BFB File Offset: 0x00001DFB
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003C03 File Offset: 0x00001E03
		public string ErrorMessage { get; set; }
	}
}
