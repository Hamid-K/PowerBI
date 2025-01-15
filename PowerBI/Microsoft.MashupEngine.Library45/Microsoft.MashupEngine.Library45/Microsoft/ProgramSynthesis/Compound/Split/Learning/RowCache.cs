using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x020009E5 RID: 2533
	internal class RowCache
	{
		// Token: 0x06003D2E RID: 15662 RVA: 0x000BFEE0 File Offset: 0x000BE0E0
		public RowCache(StringRegion region, RegularExpression[] prefixRegexes, HashSet<RegularExpression> symbolRegexes)
		{
			this.Region = region;
			this.PrefixRegexes = prefixRegexes;
			this.SymbolRegexes = symbolRegexes;
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000BFEFD File Offset: 0x000BE0FD
		// (set) Token: 0x06003D30 RID: 15664 RVA: 0x000BFF05 File Offset: 0x000BE105
		public StringRegion Region { get; private set; }

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x000BFF0E File Offset: 0x000BE10E
		// (set) Token: 0x06003D32 RID: 15666 RVA: 0x000BFF16 File Offset: 0x000BE116
		public RegularExpression[] PrefixRegexes { get; private set; }

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06003D33 RID: 15667 RVA: 0x000BFF1F File Offset: 0x000BE11F
		// (set) Token: 0x06003D34 RID: 15668 RVA: 0x000BFF27 File Offset: 0x000BE127
		public HashSet<RegularExpression> SymbolRegexes { get; private set; }
	}
}
