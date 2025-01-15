using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x0200024C RID: 588
	public abstract class CompressedTrie<TSequenceable, TSequence, TSubSequence, TValue> : Trie<TSequenceable, TSequence, TSubSequence, TValue> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence>
	{
		// Token: 0x06000C94 RID: 3220 RVA: 0x00025873 File Offset: 0x00023A73
		protected override TrieNode<TSequenceable, TSequence, TSubSequence, TValue> CreateRootNode(TSubSequence traversalKey, TSubSequence actualKey, TValue value)
		{
			CompressedTrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue> compressedTrieSplitNode = new CompressedTrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue>(null);
			compressedTrieSplitNode.AddEntry(traversalKey, value, actualKey);
			return compressedTrieSplitNode;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00025884 File Offset: 0x00023A84
		public IEnumerable<IEnumerable<TSubSequence>> GetEdgeLabels()
		{
			if (base.Root == null)
			{
				yield return Enumerable.Empty<TSubSequence>();
				yield break;
			}
			Queue<TrieNode<TSequenceable, TSequence, TSubSequence, TValue>> queue = new Queue<TrieNode<TSequenceable, TSequence, TSubSequence, TValue>>();
			queue.Enqueue(base.Root);
			while (!queue.IsEmpty<TrieNode<TSequenceable, TSequence, TSubSequence, TValue>>())
			{
				TrieNode<TSequenceable, TSequence, TSubSequence, TValue> trieNode = queue.Dequeue();
				List<TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> edges = (from s in trieNode.GetEdges()
					orderby s.MatchedPrefix.Value
					select s).ToList<TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>>();
				yield return edges.Select((TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> e) => e.MatchedPrefix);
				foreach (TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge in edges)
				{
					queue.Enqueue(trieEdge.Child);
				}
				edges = null;
			}
			yield break;
		}
	}
}
