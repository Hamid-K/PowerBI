using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000032 RID: 50
	internal class GlobalConfiguration
	{
		// Token: 0x04000066 RID: 102
		public static int SignatureGeneratorPoolSize = 256;

		// Token: 0x04000067 RID: 103
		public static int ComparerPoolSize = 256;

		// Token: 0x04000068 RID: 104
		public static int RecordContextBuilderPoolSize = 256;

		// Token: 0x04000069 RID: 105
		public static int TemporalObjectPoolTimeout = 10000;
	}
}
