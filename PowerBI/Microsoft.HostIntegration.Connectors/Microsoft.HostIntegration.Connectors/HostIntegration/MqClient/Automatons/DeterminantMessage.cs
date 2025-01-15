using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AF1 RID: 2801
	internal class DeterminantMessage
	{
		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x060058AF RID: 22703 RVA: 0x0016CC27 File Offset: 0x0016AE27
		// (set) Token: 0x060058B0 RID: 22704 RVA: 0x0016CC2F File Offset: 0x0016AE2F
		public int QueueDeterminant { get; set; }

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x060058B1 RID: 22705 RVA: 0x0016CC38 File Offset: 0x0016AE38
		// (set) Token: 0x060058B2 RID: 22706 RVA: 0x0016CC40 File Offset: 0x0016AE40
		public int QueueManagerDeterminant { get; set; }
	}
}
