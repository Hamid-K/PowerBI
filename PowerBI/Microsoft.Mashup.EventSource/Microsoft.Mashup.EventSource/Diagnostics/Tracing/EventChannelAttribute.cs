using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200001C RID: 28
	[AttributeUsage(AttributeTargets.Field)]
	internal class EventChannelAttribute : Attribute
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00008B50 File Offset: 0x00006D50
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00008B58 File Offset: 0x00006D58
		public bool Enabled { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00008B61 File Offset: 0x00006D61
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00008B69 File Offset: 0x00006D69
		public EventChannelType EventChannelType { get; set; }
	}
}
