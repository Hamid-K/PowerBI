using System;

namespace AngleSharp.Attributes
{
	// Token: 0x0200040F RID: 1039
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	public sealed class DomInitDictAttribute : Attribute
	{
		// Token: 0x0600210A RID: 8458 RVA: 0x0005885A File Offset: 0x00056A5A
		public DomInitDictAttribute(int offset = 0, bool optional = false)
		{
			this.Offset = offset;
			this.IsOptional = optional;
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x00058870 File Offset: 0x00056A70
		// (set) Token: 0x0600210C RID: 8460 RVA: 0x00058878 File Offset: 0x00056A78
		public int Offset { get; private set; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x00058881 File Offset: 0x00056A81
		// (set) Token: 0x0600210E RID: 8462 RVA: 0x00058889 File Offset: 0x00056A89
		public bool IsOptional { get; private set; }
	}
}
