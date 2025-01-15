using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200004E RID: 78
	internal sealed class DoubleEncodingAnalyzer : EncodingAnalyzerBase
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00004E86 File Offset: 0x00003086
		internal DoubleEncodingAnalyzer(bool shouldFallback)
			: base(shouldFallback)
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004E8F File Offset: 0x0000308F
		public bool ShouldFallbackToSimple(double value, string strValue)
		{
			return this.ShouldFallback || double.IsInfinity(value) || double.IsNaN(value) || (strValue.Length > 15 && !DoubleEncodingAnalyzer.ConvertibleToSingle(value));
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004EC2 File Offset: 0x000030C2
		private static bool ConvertibleToSingle(double value)
		{
			return value == Convert.ToDouble(Convert.ToSingle(value));
		}

		// Token: 0x040000D5 RID: 213
		private const int MaxDoubleSignificantDigits = 15;
	}
}
