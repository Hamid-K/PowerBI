using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200013E RID: 318
	internal static class TokenMatchTypeExtensions
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x0000B233 File Offset: 0x00009433
		internal static bool IsSpellCorrectedOrStemSpellCorrected(this TokenMatchType tokenMatchType)
		{
			return tokenMatchType == TokenMatchType.SpellCorrectedMatch || tokenMatchType == TokenMatchType.StemmedSpellCorrectedMatch;
		}
	}
}
