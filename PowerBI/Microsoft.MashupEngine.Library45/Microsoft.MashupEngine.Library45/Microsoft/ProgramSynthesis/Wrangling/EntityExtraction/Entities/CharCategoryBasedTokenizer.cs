using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001BB RID: 443
	public class CharCategoryBasedTokenizer : EntityBasedTokenizer
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x0001C7BC File Offset: 0x0001A9BC
		private static IEnumerable<CharCategoryBasedToken> _CharCategoryBasedTokenizer(string tokenizable, int start, int end)
		{
			if (start == end)
			{
				yield break;
			}
			UnicodeCategory unicodeCategory = tokenizable[start].GetUnicodeCategory();
			int num = start;
			int num2;
			for (int index = start + 1; index < end; index = num2)
			{
				char c = tokenizable[index];
				UnicodeCategory currentCategory = c.GetUnicodeCategory();
				if (CharCategoryBasedTokenizer.SingleCharCategories.Contains(currentCategory) || unicodeCategory != currentCategory)
				{
					yield return new CharCategoryBasedToken(tokenizable, num, index, unicodeCategory);
					unicodeCategory = currentCategory;
					num = index;
				}
				num2 = index + 1;
			}
			yield return new CharCategoryBasedToken(tokenizable, num, end, tokenizable[num].GetUnicodeCategory());
			yield break;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001C7DA File Offset: 0x0001A9DA
		public override IEnumerable<EntityToken> Tokenize(string sequence)
		{
			return CharCategoryBasedTokenizer._CharCategoryBasedTokenizer(sequence, 0, sequence.Length);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001C7E9 File Offset: 0x0001A9E9
		public IEnumerable<EntityToken> Tokenize(string sequence, int start, int end)
		{
			return CharCategoryBasedTokenizer._CharCategoryBasedTokenizer(sequence, start, end);
		}

		// Token: 0x0400049F RID: 1183
		private static readonly HashSet<UnicodeCategory> SingleCharCategories = new HashSet<UnicodeCategory>
		{
			UnicodeCategory.OpenPunctuation,
			UnicodeCategory.ClosePunctuation,
			UnicodeCategory.InitialQuotePunctuation,
			UnicodeCategory.FinalQuotePunctuation
		};
	}
}
