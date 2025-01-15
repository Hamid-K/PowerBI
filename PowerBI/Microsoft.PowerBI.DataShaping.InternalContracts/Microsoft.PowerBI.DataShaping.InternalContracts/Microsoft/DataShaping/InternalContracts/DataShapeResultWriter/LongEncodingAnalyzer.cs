using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200004D RID: 77
	internal sealed class LongEncodingAnalyzer : EncodingAnalyzerBase
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00004E57 File Offset: 0x00003057
		internal LongEncodingAnalyzer(bool shouldFallback)
			: base(shouldFallback)
		{
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004E60 File Offset: 0x00003060
		public bool ShouldFallbackToSimple(long value)
		{
			return this.ShouldFallback || value > 9007199254740991L || value < -9007199254740991L;
		}

		// Token: 0x040000D4 RID: 212
		private const long MaxRoundtripValue = 9007199254740991L;
	}
}
