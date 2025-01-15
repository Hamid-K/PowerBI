using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200004C RID: 76
	internal abstract class EncodingAnalyzerBase
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00004E48 File Offset: 0x00003048
		protected EncodingAnalyzerBase(bool shouldFallback)
		{
			this.ShouldFallback = shouldFallback;
		}

		// Token: 0x040000D3 RID: 211
		protected readonly bool ShouldFallback;
	}
}
