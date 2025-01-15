using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AF4 RID: 2804
	internal class QAttachFailed : DeterminantMessage
	{
		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x060058BF RID: 22719 RVA: 0x0016CCF9 File Offset: 0x0016AEF9
		// (set) Token: 0x060058C0 RID: 22720 RVA: 0x0016CD01 File Offset: 0x0016AF01
		public ReturnCode ReturnCode { get; set; }
	}
}
