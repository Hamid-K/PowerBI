using System;
using System.Globalization;

namespace Microsoft.DataShaping.Processing.Utils
{
	// Token: 0x02000019 RID: 25
	internal sealed class ProcessingCompareInfo
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003048 File Offset: 0x00001248
		internal ProcessingCompareInfo(string cultureName, CompareOptions compareOptions, bool nullAsBlank, bool useOrdinalStringKeyGeneration)
		{
			this._compareOptions = compareOptions;
			this._compareInfo = CompareInfo.GetCompareInfo(cultureName);
			this._nullAsBlank = nullAsBlank;
			this._useOrdinalStringKeyGeneration = useOrdinalStringKeyGeneration;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003072 File Offset: 0x00001272
		internal CompareOptions CompareOptions
		{
			get
			{
				return this._compareOptions;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000307A File Offset: 0x0000127A
		internal CompareInfo CompareInfo
		{
			get
			{
				return this._compareInfo;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003082 File Offset: 0x00001282
		internal bool NullAsBlank
		{
			get
			{
				return this._nullAsBlank;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000308A File Offset: 0x0000128A
		internal bool UseOrdinalStringKeyGeneration
		{
			get
			{
				return this._useOrdinalStringKeyGeneration;
			}
		}

		// Token: 0x0400007B RID: 123
		private readonly CompareOptions _compareOptions;

		// Token: 0x0400007C RID: 124
		private readonly CompareInfo _compareInfo;

		// Token: 0x0400007D RID: 125
		private readonly bool _nullAsBlank;

		// Token: 0x0400007E RID: 126
		private readonly bool _useOrdinalStringKeyGeneration;
	}
}
