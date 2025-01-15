using System;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x020009E4 RID: 2532
	internal class Cache
	{
		// Token: 0x06003D29 RID: 15657 RVA: 0x000BFEA8 File Offset: 0x000BE0A8
		public Cache(QuotingConfiguration conf, RowCache[] rows)
		{
			this.Configuration = conf;
			this.Rows = rows;
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x000BFEBE File Offset: 0x000BE0BE
		// (set) Token: 0x06003D2B RID: 15659 RVA: 0x000BFEC6 File Offset: 0x000BE0C6
		public QuotingConfiguration Configuration { get; private set; }

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06003D2C RID: 15660 RVA: 0x000BFECF File Offset: 0x000BE0CF
		// (set) Token: 0x06003D2D RID: 15661 RVA: 0x000BFED7 File Offset: 0x000BE0D7
		public RowCache[] Rows { get; private set; }
	}
}
