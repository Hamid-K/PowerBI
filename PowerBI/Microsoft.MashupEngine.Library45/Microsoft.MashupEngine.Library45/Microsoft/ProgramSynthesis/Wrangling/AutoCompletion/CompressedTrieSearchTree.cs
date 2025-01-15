using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion
{
	// Token: 0x02000243 RID: 579
	public class CompressedTrieSearchTree : CompressedTrie<char, string, StringSubSequence, List<CompletionInfo>>, IAutoCompleteSearchTree, IPrefixSearchTree<char, string, StringSubSequence, List<CompletionInfo>>
	{
		// Token: 0x06000C5E RID: 3166 RVA: 0x00025466 File Offset: 0x00023666
		public CompressedTrieSearchTree(bool caseInsensitiveLookups = true)
		{
			this.CaseInsensitiveLookups = caseInsensitiveLookups;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00025475 File Offset: 0x00023675
		protected override StringSubSequence EmptySubSequence
		{
			get
			{
				return StringSubSequence.Empty;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0002547C File Offset: 0x0002367C
		public bool CaseInsensitiveLookups { get; }

		// Token: 0x06000C61 RID: 3169 RVA: 0x00025484 File Offset: 0x00023684
		public void Add(string key, CompletionInfo value)
		{
			base.GetOrCreate(key, () => new List<CompletionInfo>()).Add(value);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000254B2 File Offset: 0x000236B2
		protected override StringSubSequence TraversalSubSequenceForKey(string key)
		{
			if (!this.CaseInsensitiveLookups)
			{
				return StringSubSequence.Create(key.Normalize());
			}
			return StringSubSequence.Create(key.Normalize().ToLowerInvariant());
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000254D8 File Offset: 0x000236D8
		protected override StringSubSequence SubSequenceForSequence(string sequence)
		{
			return StringSubSequence.Create(sequence.Normalize());
		}
	}
}
