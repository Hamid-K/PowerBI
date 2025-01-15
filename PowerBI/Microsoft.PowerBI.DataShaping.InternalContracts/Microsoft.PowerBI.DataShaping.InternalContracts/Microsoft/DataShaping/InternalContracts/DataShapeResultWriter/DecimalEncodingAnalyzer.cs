using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200004F RID: 79
	internal sealed class DecimalEncodingAnalyzer : EncodingAnalyzerBase
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00004ED2 File Offset: 0x000030D2
		internal DecimalEncodingAnalyzer(bool shouldFallback)
			: base(shouldFallback)
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004EDC File Offset: 0x000030DC
		public bool ShouldFallbackToSimple(decimal value, string strValue)
		{
			int num;
			return this.ShouldFallback || (strValue.Length > DecimalEncodingAnalyzer.MaxDecimalSignificantDigits && (!NumberUtils.TryCountSignificantDigits(value, out num) || num > DecimalEncodingAnalyzer.MaxDecimalSignificantDigits));
		}

		// Token: 0x040000D6 RID: 214
		private static readonly int MaxDecimalSignificantDigits = 15;
	}
}
