using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001DB RID: 475
	internal class EnclosedRegionTokenizer : EntityBasedTokenizer
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x0001FA8E File Offset: 0x0001DC8E
		public override IEnumerable<EntityToken> Tokenize(string sequence)
		{
			Stack<int> openTokenIndices = new Stack<int>();
			int num2;
			for (int i = 0; i < sequence.Length; i = num2)
			{
				UnicodeCategory unicodeCategory = sequence[i].GetUnicodeCategory();
				if (unicodeCategory == UnicodeCategory.OpenPunctuation || unicodeCategory == UnicodeCategory.InitialQuotePunctuation)
				{
					openTokenIndices.Push(i);
				}
				else if ((unicodeCategory == UnicodeCategory.ClosePunctuation || unicodeCategory == UnicodeCategory.FinalQuotePunctuation) && openTokenIndices.Count > 0)
				{
					int num = openTokenIndices.Pop();
					yield return new EnclosedRegionToken(sequence, num, i + 1);
				}
				num2 = i + 1;
			}
			yield break;
		}
	}
}
