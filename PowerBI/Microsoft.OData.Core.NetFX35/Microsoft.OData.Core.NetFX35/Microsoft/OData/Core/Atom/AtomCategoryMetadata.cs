using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200000F RID: 15
	public sealed class AtomCategoryMetadata : ODataAnnotatable
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002BCD File Offset: 0x00000DCD
		public AtomCategoryMetadata()
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002BD5 File Offset: 0x00000DD5
		internal AtomCategoryMetadata(AtomCategoryMetadata other)
		{
			if (other == null)
			{
				return;
			}
			this.Term = other.Term;
			this.Scheme = other.Scheme;
			this.Label = other.Label;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002C05 File Offset: 0x00000E05
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002C0D File Offset: 0x00000E0D
		public string Term { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002C16 File Offset: 0x00000E16
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002C1E File Offset: 0x00000E1E
		public string Scheme { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002C27 File Offset: 0x00000E27
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002C2F File Offset: 0x00000E2F
		public string Label { get; set; }
	}
}
