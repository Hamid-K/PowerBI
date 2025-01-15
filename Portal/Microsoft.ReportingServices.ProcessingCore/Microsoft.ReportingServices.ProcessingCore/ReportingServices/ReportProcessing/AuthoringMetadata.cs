using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000650 RID: 1616
	public class AuthoringMetadata
	{
		// Token: 0x1700201B RID: 8219
		// (get) Token: 0x060057BA RID: 22458 RVA: 0x0016FEBC File Offset: 0x0016E0BC
		// (set) Token: 0x060057BB RID: 22459 RVA: 0x0016FEC4 File Offset: 0x0016E0C4
		public AuthoringToolProperty CreatedBy { get; set; }

		// Token: 0x1700201C RID: 8220
		// (get) Token: 0x060057BC RID: 22460 RVA: 0x0016FECD File Offset: 0x0016E0CD
		// (set) Token: 0x060057BD RID: 22461 RVA: 0x0016FED5 File Offset: 0x0016E0D5
		public AuthoringToolProperty UpdatedBy { get; set; }

		// Token: 0x1700201D RID: 8221
		// (get) Token: 0x060057BE RID: 22462 RVA: 0x0016FEDE File Offset: 0x0016E0DE
		// (set) Token: 0x060057BF RID: 22463 RVA: 0x0016FEE6 File Offset: 0x0016E0E6
		public string LastModifiedTimestamp { get; set; }
	}
}
