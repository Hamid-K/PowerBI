using System;

namespace Model
{
	// Token: 0x02000010 RID: 16
	public class DeliveryExtension : Extension
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000022D0 File Offset: 0x000004D0
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000022D8 File Offset: 0x000004D8
		public bool IsImmutable { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000022E1 File Offset: 0x000004E1
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000022E9 File Offset: 0x000004E9
		public bool DefaultDeliveryExtension { get; set; }
	}
}
