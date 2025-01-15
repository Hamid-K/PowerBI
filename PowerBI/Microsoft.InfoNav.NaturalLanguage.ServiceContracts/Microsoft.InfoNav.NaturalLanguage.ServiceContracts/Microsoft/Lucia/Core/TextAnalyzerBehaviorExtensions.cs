using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200012E RID: 302
	internal static class TextAnalyzerBehaviorExtensions
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x0000AADA File Offset: 0x00008CDA
		internal static bool ShouldTokenize(this TextAnalyzerBehavior behavior)
		{
			return behavior.HasFlagFast(TextAnalyzerBehavior.Tokenize);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0000AAE3 File Offset: 0x00008CE3
		internal static bool ShouldSpellCorrect(this TextAnalyzerBehavior behavior)
		{
			return behavior.HasFlagFast(TextAnalyzerBehavior.SpellCorrect);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000AAEC File Offset: 0x00008CEC
		internal static bool ShouldPosTag(this TextAnalyzerBehavior behavior)
		{
			return behavior.HasFlagFast(TextAnalyzerBehavior.PosTag);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0000AAF5 File Offset: 0x00008CF5
		internal static bool ShouldStem(this TextAnalyzerBehavior behavior)
		{
			return behavior.HasFlagFast(TextAnalyzerBehavior.Stem);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0000AAFE File Offset: 0x00008CFE
		internal static bool HasFlagFast(this TextAnalyzerBehavior behavior, TextAnalyzerBehavior flag)
		{
			return (behavior & flag) == flag;
		}
	}
}
