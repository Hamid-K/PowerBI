using System;

namespace AngleSharp.Attributes
{
	// Token: 0x0200040A RID: 1034
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class DomAccessorAttribute : Attribute
	{
		// Token: 0x060020FF RID: 8447 RVA: 0x000587F2 File Offset: 0x000569F2
		public DomAccessorAttribute(Accessors type)
		{
			this.Type = type;
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x00058801 File Offset: 0x00056A01
		// (set) Token: 0x06002101 RID: 8449 RVA: 0x00058809 File Offset: 0x00056A09
		public Accessors Type { get; private set; }
	}
}
