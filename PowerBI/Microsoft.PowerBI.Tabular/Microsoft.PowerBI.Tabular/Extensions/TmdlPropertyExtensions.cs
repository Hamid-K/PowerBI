using System;
using Microsoft.AnalysisServices.Tabular.Tmdl;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001CF RID: 463
	internal static class TmdlPropertyExtensions
	{
		// Token: 0x06001BF7 RID: 7159 RVA: 0x000C3912 File Offset: 0x000C1B12
		public static TmdlProperty WithSourceLocation(this TmdlProperty property, TmdlSourceLocation location)
		{
			property.SourceLocation = location;
			return property;
		}
	}
}
