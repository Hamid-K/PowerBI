using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000130 RID: 304
	internal static class TokenizerRuleCategoriesExtensions
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x0000AB06 File Offset: 0x00008D06
		internal static bool IsBasic(this TokenizerRuleCategories categories)
		{
			return categories.HasFlagFast(TokenizerRuleCategories.Basic);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0000AB0F File Offset: 0x00008D0F
		internal static bool IsQuotes(this TokenizerRuleCategories categories)
		{
			return categories.HasFlagFast(TokenizerRuleCategories.Quotes);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0000AB18 File Offset: 0x00008D18
		internal static bool IsDateTime(this TokenizerRuleCategories categories)
		{
			return categories.HasFlagFast(TokenizerRuleCategories.DateTime);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0000AB21 File Offset: 0x00008D21
		internal static bool HasFlagFast(this TokenizerRuleCategories categories, TokenizerRuleCategories flag)
		{
			return (categories & flag) == flag;
		}
	}
}
