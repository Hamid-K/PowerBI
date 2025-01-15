using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Common
{
	// Token: 0x02000114 RID: 276
	internal static class IncludeInStageExtensions
	{
		// Token: 0x06000A83 RID: 2691 RVA: 0x00028C0D File Offset: 0x00026E0D
		internal static bool IsIncludeInOutput(this SubtotalUsage value)
		{
			return value.IsIncludeInStage(SubtotalUsage.Output);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00028C16 File Offset: 0x00026E16
		internal static bool IsIncludeInStage(this SubtotalUsage value, SubtotalUsage expectedStage)
		{
			return value >= expectedStage;
		}
	}
}
