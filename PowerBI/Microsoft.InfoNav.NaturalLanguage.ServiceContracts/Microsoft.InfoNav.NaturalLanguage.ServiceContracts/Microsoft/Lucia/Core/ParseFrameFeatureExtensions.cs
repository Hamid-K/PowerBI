using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000F6 RID: 246
	public static class ParseFrameFeatureExtensions
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x00008D46 File Offset: 0x00006F46
		public static bool IsInstance(this ParseFrameFeature flags)
		{
			return flags.HasFlag(ParseFrameFeature.Instance);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00008D4F File Offset: 0x00006F4F
		public static bool IsFirstPersonPronoun(this ParseFrameFeature flags)
		{
			return flags.HasFlag(ParseFrameFeature.FirstPersonPronoun);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00008D58 File Offset: 0x00006F58
		public static bool IsSecondPersonPronoun(this ParseFrameFeature flags)
		{
			return flags.HasFlag(ParseFrameFeature.SecondPersonPronoun);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00008D61 File Offset: 0x00006F61
		public static bool IsThirdPersonPronoun(this ParseFrameFeature flags)
		{
			return flags.HasFlag(ParseFrameFeature.ThirdPersonPronoun);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00008D6A File Offset: 0x00006F6A
		public static bool IsDemonstrativePronoun(this ParseFrameFeature flags)
		{
			return flags.HasFlag(ParseFrameFeature.DemonstrativePronoun);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00008D74 File Offset: 0x00006F74
		public static bool IsPossessive(this ParseFrameFeature flags)
		{
			return flags.HasFlag(ParseFrameFeature.Possessive);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00008D7E File Offset: 0x00006F7E
		public static bool IsPlural(this ParseFrameFeature flags)
		{
			return flags.HasFlag(ParseFrameFeature.Plural);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00008D88 File Offset: 0x00006F88
		public static bool IsAggregated(this ParseFrameFeature flags)
		{
			return flags.HasFlag(ParseFrameFeature.Aggregated);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00008D95 File Offset: 0x00006F95
		public static bool IsCollection(this ParseFrameFeature flags)
		{
			return flags.HasFlag(ParseFrameFeature.Collection);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00008DA2 File Offset: 0x00006FA2
		private static bool HasFlag(this ParseFrameFeature frameFeature, ParseFrameFeature flag)
		{
			return (frameFeature & flag) == flag;
		}
	}
}
