using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x02000266 RID: 614
	public class TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> : IPrefixSearchTreeEdge<TSequenceable, TSequence, TSubSequence, TrieNode<TSequenceable, TSequence, TSubSequence, TValue>, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>, TValue> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence>
	{
		// Token: 0x06000D24 RID: 3364 RVA: 0x00026C59 File Offset: 0x00024E59
		public TrieEdge(TrieNode<TSequenceable, TSequence, TSubSequence, TValue> parent, TSubSequence matchedPrefix)
		{
			this.Parent = parent;
			this.MatchedPrefix = matchedPrefix;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00026C6F File Offset: 0x00024E6F
		public TSubSequence MatchedPrefix { get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00026C77 File Offset: 0x00024E77
		public TrieNode<TSequenceable, TSequence, TSubSequence, TValue> Parent { get; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00026C7F File Offset: 0x00024E7F
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x00026C87 File Offset: 0x00024E87
		public TrieNode<TSequenceable, TSequence, TSubSequence, TValue> Child { get; internal set; }

		// Token: 0x06000D29 RID: 3369 RVA: 0x00026C90 File Offset: 0x00024E90
		public override string ToString()
		{
			return this.MatchedPrefix.ToString();
		}
	}
}
