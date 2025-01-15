using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x02000265 RID: 613
	public abstract class Trie<TSequenceable, TSequence, TSubSequence, TValue> : PrefixSearchTree<TSequenceable, TSequence, TSubSequence, TValue, TrieNode<TSequenceable, TSequence, TSubSequence, TValue>, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence>
	{
		// Token: 0x06000D22 RID: 3362 RVA: 0x00026C40 File Offset: 0x00024E40
		protected override TrieNode<TSequenceable, TSequence, TSubSequence, TValue> CreateRootNode(TSubSequence traversalKey, TSubSequence actualKey, TValue value)
		{
			TrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue> trieSplitNode = new TrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue>(null);
			trieSplitNode.AddEntry(traversalKey, value, actualKey);
			return trieSplitNode;
		}
	}
}
