using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000133 RID: 307
	internal static class TokenizerRuleOptionsExtensions
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x0000AB5E File Offset: 0x00008D5E
		internal static bool PreserveDateTimeInQuotes(this TokenizerRuleOptions options)
		{
			return options.HasFlagFast(TokenizerRuleOptions.PreserveDateTimeInQuotes);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000AB67 File Offset: 0x00008D67
		internal static bool HasFlagFast(this TokenizerRuleOptions options, TokenizerRuleOptions flag)
		{
			return (options & flag) == flag;
		}
	}
}
