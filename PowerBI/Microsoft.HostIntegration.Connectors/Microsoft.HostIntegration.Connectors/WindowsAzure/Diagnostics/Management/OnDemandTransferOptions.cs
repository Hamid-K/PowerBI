using System;

namespace Microsoft.WindowsAzure.Diagnostics.Management
{
	// Token: 0x02000475 RID: 1141
	[Obsolete("This API is deprecated.")]
	public class OnDemandTransferOptions
	{
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x00077E07 File Offset: 0x00076007
		// (set) Token: 0x060027A6 RID: 10150 RVA: 0x00077E0F File Offset: 0x0007600F
		public DateTime From { get; set; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x00077E18 File Offset: 0x00076018
		// (set) Token: 0x060027A8 RID: 10152 RVA: 0x00077E20 File Offset: 0x00076020
		public DateTime To { get; set; }

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x00077E29 File Offset: 0x00076029
		// (set) Token: 0x060027AA RID: 10154 RVA: 0x00077E31 File Offset: 0x00076031
		public LogLevel LogLevelFilter { get; set; }

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00077E3A File Offset: 0x0007603A
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x00077E42 File Offset: 0x00076042
		public string NotificationQueueName { get; set; }
	}
}
