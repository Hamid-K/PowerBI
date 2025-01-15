using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000023 RID: 35
	internal sealed class Collation
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00004ED1 File Offset: 0x000030D1
		internal Collation(string culture, bool ignoreCase, bool ignoreNonSpace, bool ignoreKanaType, bool ignoreWidth, bool preferOrdinalStringEquality, bool useOrdinalStringKeyGeneration)
		{
			this._culture = culture;
			this._ignoreCase = ignoreCase;
			this._ignoreNonSpace = ignoreNonSpace;
			this._ignoreKanaType = ignoreKanaType;
			this._ignoreWidth = ignoreWidth;
			this._preferOrdinalStringEquality = preferOrdinalStringEquality;
			this._useOrdinalStringKeyGeneration = useOrdinalStringKeyGeneration;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004F0E File Offset: 0x0000310E
		internal string Culture
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00004F16 File Offset: 0x00003116
		internal bool IgnoreCase
		{
			get
			{
				return this._ignoreCase;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00004F1E File Offset: 0x0000311E
		internal bool IgnoreNonSpace
		{
			get
			{
				return this._ignoreNonSpace;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00004F26 File Offset: 0x00003126
		internal bool IgnoreKanaType
		{
			get
			{
				return this._ignoreKanaType;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004F2E File Offset: 0x0000312E
		internal bool IgnoreWidth
		{
			get
			{
				return this._ignoreWidth;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004F36 File Offset: 0x00003136
		internal bool PreferOrdinalStringEquality
		{
			get
			{
				return this._preferOrdinalStringEquality;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004F3E File Offset: 0x0000313E
		public bool UseOrdinalStringKeyGeneration
		{
			get
			{
				return this._useOrdinalStringKeyGeneration;
			}
		}

		// Token: 0x040000A4 RID: 164
		private readonly string _culture;

		// Token: 0x040000A5 RID: 165
		private readonly bool _ignoreCase;

		// Token: 0x040000A6 RID: 166
		private readonly bool _ignoreNonSpace;

		// Token: 0x040000A7 RID: 167
		private readonly bool _ignoreKanaType;

		// Token: 0x040000A8 RID: 168
		private readonly bool _ignoreWidth;

		// Token: 0x040000A9 RID: 169
		private readonly bool _preferOrdinalStringEquality;

		// Token: 0x040000AA RID: 170
		private readonly bool _useOrdinalStringKeyGeneration;
	}
}
