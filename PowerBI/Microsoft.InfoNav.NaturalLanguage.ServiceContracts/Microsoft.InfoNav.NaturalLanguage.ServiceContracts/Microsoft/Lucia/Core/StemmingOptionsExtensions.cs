using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000124 RID: 292
	public static class StemmingOptionsExtensions
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x0000A9D5 File Offset: 0x00008BD5
		internal static bool IsNoun(this StemmingOptions option)
		{
			return StemmingOptionsExtensions.HasFlag(option, StemmingOptions.Noun);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000A9DE File Offset: 0x00008BDE
		internal static bool IsVerb(this StemmingOptions option)
		{
			return StemmingOptionsExtensions.HasFlag(option, StemmingOptions.Verb);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000A9E7 File Offset: 0x00008BE7
		internal static bool IsAdjective(this StemmingOptions option)
		{
			return StemmingOptionsExtensions.HasFlag(option, StemmingOptions.Adjective);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		internal static bool IsOther(this StemmingOptions option)
		{
			return StemmingOptionsExtensions.HasFlag(option, StemmingOptions.Other);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000A9F9 File Offset: 0x00008BF9
		private static bool HasFlag(StemmingOptions option, StemmingOptions flag)
		{
			return (option & flag) == flag;
		}
	}
}
