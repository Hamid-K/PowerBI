using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000030 RID: 48
	[AttributeUsage(AttributeTargets.Property)]
	public class EventFieldAttribute : Attribute
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000B54E File Offset: 0x0000974E
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000B556 File Offset: 0x00009756
		public EventFieldTags Tags { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000B55F File Offset: 0x0000975F
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000B567 File Offset: 0x00009767
		internal string Name { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000B570 File Offset: 0x00009770
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000B578 File Offset: 0x00009778
		public EventFieldFormat Format { get; set; }
	}
}
