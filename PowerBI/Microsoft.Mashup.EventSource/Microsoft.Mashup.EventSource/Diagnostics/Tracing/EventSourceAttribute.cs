using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000019 RID: 25
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class EventSourceAttribute : Attribute
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00008A37 File Offset: 0x00006C37
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00008A3F File Offset: 0x00006C3F
		public string Name { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00008A48 File Offset: 0x00006C48
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00008A50 File Offset: 0x00006C50
		public string Guid { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00008A59 File Offset: 0x00006C59
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00008A61 File Offset: 0x00006C61
		public string LocalizationResources { get; set; }
	}
}
