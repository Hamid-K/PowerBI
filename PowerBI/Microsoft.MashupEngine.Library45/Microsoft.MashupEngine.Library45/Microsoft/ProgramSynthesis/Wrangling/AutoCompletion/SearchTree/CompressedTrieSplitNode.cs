using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x0200024F RID: 591
	public class CompressedTrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue> : TrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence>
	{
		// Token: 0x06000CA3 RID: 3235 RVA: 0x00025A97 File Offset: 0x00023C97
		public CompressedTrieSplitNode(TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> edgeToParent)
			: base(edgeToParent)
		{
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00025AA0 File Offset: 0x00023CA0
		public override void AddEntry(TSubSequence suffix, TValue value, TSubSequence key)
		{
			TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge = this.ChildFor(suffix);
			if (trieEdge != null && trieEdge == this.EpsilonEdge)
			{
				TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue> trieLeafNode = trieEdge.Child as TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue>;
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("The key {0} has already been mapped to a value: {1}.", new object[] { key, trieLeafNode.Value })));
			}
			if (trieEdge != null)
			{
				if (suffix.StartsWith(trieEdge.MatchedPrefix))
				{
					trieEdge.Child.AddEntry(suffix.AbsoluteSlice(suffix.Start + trieEdge.MatchedPrefix.Length, suffix.End), value, key);
					return;
				}
				uint num = suffix.FindFirstMismatchingIndex(trieEdge.MatchedPrefix);
				TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge2 = new TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>(this, suffix.AbsoluteSlice(suffix.Start, suffix.Start + num));
				CompressedTrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue> compressedTrieSplitNode = new CompressedTrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue>(trieEdge2);
				trieEdge2.Child = compressedTrieSplitNode;
				base.Children[trieEdge2.MatchedPrefix[0]] = trieEdge2;
				TSubSequence matchedPrefix = trieEdge.MatchedPrefix;
				TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge3 = new TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>(compressedTrieSplitNode, matchedPrefix.AbsoluteSlice(matchedPrefix.Start + num, matchedPrefix.End))
				{
					Child = trieEdge.Child
				};
				compressedTrieSplitNode.Children[trieEdge3.MatchedPrefix[0]] = trieEdge3;
				compressedTrieSplitNode.AddEntry(suffix.AbsoluteSlice(suffix.Start + num, suffix.End), value, key);
				return;
			}
			else
			{
				TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge4 = new TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>(this, suffix);
				if (suffix.Length > 0U)
				{
					base.Children[trieEdge4.MatchedPrefix[0]] = trieEdge4;
					CompressedTrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue> compressedTrieSplitNode2 = new CompressedTrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue>(trieEdge4);
					trieEdge4.Child = compressedTrieSplitNode2;
					compressedTrieSplitNode2.EpsilonEdge = new TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>(compressedTrieSplitNode2, suffix.AbsoluteSlice(suffix.End, suffix.End));
					compressedTrieSplitNode2.EpsilonEdge.Child = new TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue>(key, value, compressedTrieSplitNode2.EpsilonEdge);
					return;
				}
				this.EpsilonEdge = trieEdge4;
				this.EpsilonEdge.Child = new TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue>(key, value, this.EpsilonEdge);
				return;
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00025D04 File Offset: 0x00023F04
		public override bool Lookup(TSubSequence key, out TValue result)
		{
			result = default(TValue);
			TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge = this.ChildFor(key);
			return trieEdge != null && key.StartsWith(trieEdge.MatchedPrefix) && trieEdge.Child.Lookup(key.AbsoluteSlice(key.Start + trieEdge.MatchedPrefix.Length, key.End), out result);
		}
	}
}
