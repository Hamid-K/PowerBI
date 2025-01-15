using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000CF RID: 207
	public sealed class StemmerSuggestion
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x000081DD File Offset: 0x000063DD
		public StemmerSuggestion(string stem, PosTagKind posTagKind, string disambiguator = null, bool isSpellCorrected = false)
		{
			this.StemValue = stem;
			this.PosTagKind = posTagKind;
			this.Disambiguator = disambiguator;
			this.IsSpellCorrected = isSpellCorrected;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00008202 File Offset: 0x00006402
		public string StemValue { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000820A File Offset: 0x0000640A
		public PosTagKind PosTagKind { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00008212 File Offset: 0x00006412
		public string Disambiguator { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000821A File Offset: 0x0000641A
		public bool IsSpellCorrected { get; }
	}
}
