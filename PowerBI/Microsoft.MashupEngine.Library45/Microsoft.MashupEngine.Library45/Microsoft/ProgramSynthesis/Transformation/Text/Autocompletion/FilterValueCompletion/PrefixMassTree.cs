using System;
using System.Collections;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.FilterValueCompletion
{
	// Token: 0x02001E29 RID: 7721
	internal class PrefixMassTree : CompressedTrie<char, string, StringSubSequence, BitArray>
	{
		// Token: 0x17002ACF RID: 10959
		// (get) Token: 0x060101F1 RID: 66033 RVA: 0x00025475 File Offset: 0x00023675
		protected override StringSubSequence EmptySubSequence
		{
			get
			{
				return StringSubSequence.Empty;
			}
		}

		// Token: 0x060101F2 RID: 66034 RVA: 0x00375762 File Offset: 0x00373962
		protected override StringSubSequence SubSequenceForSequence(string sequence)
		{
			return StringSubSequence.Create(sequence);
		}
	}
}
