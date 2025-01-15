using System;
using System.Globalization;

namespace Microsoft.InfoNav
{
	// Token: 0x02000016 RID: 22
	public sealed class ConceptualCollation
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002440 File Offset: 0x00000640
		public ConceptualCollation(string culture, CompareOptions compareOptions, bool preferOrdinalStringEquality)
		{
			this.Culture = culture;
			this.CompareOptions = compareOptions;
			this.PreferOrdinalStringEquality = preferOrdinalStringEquality;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000245D File Offset: 0x0000065D
		public string Culture { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002465 File Offset: 0x00000665
		public CompareOptions CompareOptions { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000246D File Offset: 0x0000066D
		public bool PreferOrdinalStringEquality { get; }

		// Token: 0x0400005A RID: 90
		public static readonly ConceptualCollation Default = new ConceptualCollation(null, CompareOptions.None, false);
	}
}
