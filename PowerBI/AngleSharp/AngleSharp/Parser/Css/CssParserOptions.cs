using System;

namespace AngleSharp.Parser.Css
{
	// Token: 0x0200007C RID: 124
	public struct CssParserOptions
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0001930A File Offset: 0x0001750A
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x00019312 File Offset: 0x00017512
		public bool IsIncludingUnknownRules { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0001931B File Offset: 0x0001751B
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00019323 File Offset: 0x00017523
		public bool IsIncludingUnknownDeclarations { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0001932C File Offset: 0x0001752C
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00019334 File Offset: 0x00017534
		public bool IsToleratingInvalidSelectors { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0001933D File Offset: 0x0001753D
		// (set) Token: 0x060003BA RID: 954 RVA: 0x00019345 File Offset: 0x00017545
		public bool IsToleratingInvalidValues { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0001934E File Offset: 0x0001754E
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00019356 File Offset: 0x00017556
		public bool IsToleratingInvalidConstraints { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0001935F File Offset: 0x0001755F
		// (set) Token: 0x060003BE RID: 958 RVA: 0x00019367 File Offset: 0x00017567
		public bool IsStoringTrivia { get; set; }
	}
}
