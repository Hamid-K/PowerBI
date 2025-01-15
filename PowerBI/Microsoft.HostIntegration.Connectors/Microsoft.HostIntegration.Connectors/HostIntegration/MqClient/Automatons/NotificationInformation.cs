using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AF5 RID: 2805
	public class NotificationInformation
	{
		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x060058C2 RID: 22722 RVA: 0x0016CD0A File Offset: 0x0016AF0A
		// (set) Token: 0x060058C3 RID: 22723 RVA: 0x0016CD12 File Offset: 0x0016AF12
		public int ObjectHandle { get; set; }

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x060058C4 RID: 22724 RVA: 0x0016CD1B File Offset: 0x0016AF1B
		// (set) Token: 0x060058C5 RID: 22725 RVA: 0x0016CD23 File Offset: 0x0016AF23
		public NotificationType Code { get; set; }

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x060058C6 RID: 22726 RVA: 0x0016CD2C File Offset: 0x0016AF2C
		// (set) Token: 0x060058C7 RID: 22727 RVA: 0x0016CD34 File Offset: 0x0016AF34
		public ReturnCode ReasonCode { get; set; }
	}
}
