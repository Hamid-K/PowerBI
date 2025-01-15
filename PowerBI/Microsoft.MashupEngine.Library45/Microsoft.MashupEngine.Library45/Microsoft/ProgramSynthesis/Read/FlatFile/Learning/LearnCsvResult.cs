using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x020012CA RID: 4810
	public class LearnCsvResult : LearnResult
	{
		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x06009113 RID: 37139 RVA: 0x001E9309 File Offset: 0x001E7509
		// (set) Token: 0x06009114 RID: 37140 RVA: 0x001E9311 File Offset: 0x001E7511
		public string Delimiter { get; set; }

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x06009115 RID: 37141 RVA: 0x001E931A File Offset: 0x001E751A
		// (set) Token: 0x06009116 RID: 37142 RVA: 0x001E9322 File Offset: 0x001E7522
		public Optional<char> QuoteChar { get; set; }

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x06009117 RID: 37143 RVA: 0x001E932B File Offset: 0x001E752B
		// (set) Token: 0x06009118 RID: 37144 RVA: 0x001E9333 File Offset: 0x001E7533
		public Optional<char> EscapeChar { get; set; }

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x06009119 RID: 37145 RVA: 0x001E933C File Offset: 0x001E753C
		// (set) Token: 0x0600911A RID: 37146 RVA: 0x001E9344 File Offset: 0x001E7544
		public bool DoubleQuoteEscape { get; set; }
	}
}
