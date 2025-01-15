using System;
using System.Globalization;

namespace Microsoft.DataShaping.SemanticQueryTranslation.SparklineData
{
	// Token: 0x02000020 RID: 32
	internal sealed class GuidSourceNameGenerator : ISourceNameGenerator
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000055B4 File Offset: 0x000037B4
		public string GenerateUniqueName()
		{
			return Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
		}
	}
}
