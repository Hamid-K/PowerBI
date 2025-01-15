using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x0200026D RID: 621
	public class TrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue> : TrieNode<TSequenceable, TSequence, TSubSequence, TValue> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence>
	{
		// Token: 0x06000D60 RID: 3424 RVA: 0x00027087 File Offset: 0x00025287
		public TrieSplitNode(TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> edgeToParent)
			: base(edgeToParent)
		{
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00027090 File Offset: 0x00025290
		protected Dictionary<TSequenceable, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> Children
		{
			get
			{
				Dictionary<TSequenceable, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> dictionary;
				if ((dictionary = this._children) == null)
				{
					dictionary = (this._children = new Dictionary<TSequenceable, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>>());
				}
				return dictionary;
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000270BC File Offset: 0x000252BC
		public override TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> ChildFor(TSubSequence suffix)
		{
			if (suffix.Length == 0U)
			{
				return this.EpsilonEdge;
			}
			if (this._children == null)
			{
				return null;
			}
			TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge;
			if (!this.Children.TryGetValue(suffix[0], out trieEdge))
			{
				return null;
			}
			return trieEdge;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00027108 File Offset: 0x00025308
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
				trieEdge.Child.AddEntry(suffix.AbsoluteSlice(suffix.Start + trieEdge.MatchedPrefix.Length, suffix.End), value, key);
				return;
			}
			if (suffix.Length == 0U)
			{
				TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge2 = new TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>(this, suffix);
				this.EpsilonEdge = trieEdge2;
				TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue> trieLeafNode2 = new TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue>(key, value, trieEdge2);
				trieEdge2.Child = trieLeafNode2;
				return;
			}
			TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge3 = new TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>(this, suffix.AbsoluteSlice(suffix.Start, suffix.Start + 1U));
			this.Children[suffix[0]] = trieEdge3;
			TrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue> trieSplitNode = new TrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue>(trieEdge3);
			trieEdge3.Child = trieSplitNode;
			trieSplitNode.AddEntry(suffix.AbsoluteSlice(suffix.Start + trieEdge3.MatchedPrefix.Length, suffix.End), value, key);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002726C File Offset: 0x0002546C
		public override bool Lookup(TSubSequence key, out TValue result)
		{
			result = default(TValue);
			TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge = this.ChildFor(key);
			return trieEdge != null && trieEdge.Child.Lookup(key.AbsoluteSlice(key.Start + trieEdge.MatchedPrefix.Length, key.End), out result);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000272CC File Offset: 0x000254CC
		protected TValue AddHere(TSubSequence suffix, TSubSequence key, Func<TValue> factory)
		{
			TValue tvalue = factory();
			this.AddEntry(suffix, tvalue, key);
			return tvalue;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x000272EC File Offset: 0x000254EC
		public override TValue LookupOrCreate(TSubSequence key, TSubSequence suffix, Func<TValue> factory)
		{
			TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge = this.ChildFor(suffix);
			if (trieEdge == null || (trieEdge != this.EpsilonEdge && suffix.Length == 0U) || !suffix.StartsWith(trieEdge.MatchedPrefix))
			{
				return this.AddHere(suffix, key, factory);
			}
			if (trieEdge == this.EpsilonEdge)
			{
				TValue tvalue;
				trieEdge.Child.Lookup(suffix, out tvalue);
				return tvalue;
			}
			TSubSequence tsubSequence = suffix.AbsoluteSlice(suffix.Start + trieEdge.MatchedPrefix.Length, suffix.End);
			return trieEdge.Child.LookupOrCreate(key, tsubSequence, factory);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00027394 File Offset: 0x00025594
		public override IEnumerable<KeyValuePair<TSubSequence, TValue>> LookupAll(TSubSequence prefix)
		{
			if (prefix.Length == 0U)
			{
				return from leaf in this.GetAllLeaves()
					select new KeyValuePair<TSubSequence, TValue>(leaf.Key, leaf.Value);
			}
			TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> trieEdge = this.ChildFor(prefix);
			if (trieEdge == null)
			{
				return Enumerable.Empty<KeyValuePair<TSubSequence, TValue>>();
			}
			if (prefix.Length >= trieEdge.MatchedPrefix.Length)
			{
				if (prefix.StartsWith(trieEdge.MatchedPrefix))
				{
					return trieEdge.Child.LookupAll(prefix.AbsoluteSlice(prefix.Start + trieEdge.MatchedPrefix.Length, prefix.End));
				}
				return Enumerable.Empty<KeyValuePair<TSubSequence, TValue>>();
			}
			else
			{
				if (trieEdge.MatchedPrefix.StartsWith(prefix))
				{
					return from leaf in trieEdge.Child.GetAllLeaves()
						select new KeyValuePair<TSubSequence, TValue>(leaf.Key, leaf.Value);
				}
				return Enumerable.Empty<KeyValuePair<TSubSequence, TValue>>();
			}
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000274A8 File Offset: 0x000256A8
		public override IEnumerable<TrieNode<TSequenceable, TSequence, TSubSequence, TValue>> GetAllDescendants()
		{
			IEnumerable<TrieNode<TSequenceable, TSequence, TSubSequence, TValue>> enumerable = Seq.Of<TrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue>>(new TrieSplitNode<TSequenceable, TSequence, TSubSequence, TValue>[] { this });
			Dictionary<TSequenceable, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> children = this._children;
			IEnumerable<TrieNode<TSequenceable, TSequence, TSubSequence, TValue>> enumerable2;
			if (children == null)
			{
				enumerable2 = null;
			}
			else
			{
				enumerable2 = children.SelectMany((KeyValuePair<TSequenceable, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> kvp) => kvp.Value.Child.GetAllDescendants());
			}
			return enumerable.Concat(enumerable2 ?? Enumerable.Empty<TrieNode<TSequenceable, TSequence, TSubSequence, TValue>>());
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00027508 File Offset: 0x00025708
		public override IEnumerable<TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue>> GetAllLeaves()
		{
			Dictionary<TSequenceable, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> children = this._children;
			IEnumerable<TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue>> enumerable;
			if (children == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = children.SelectMany((KeyValuePair<TSequenceable, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> kvp) => kvp.Value.Child.GetAllLeaves());
			}
			IEnumerable<TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue>> enumerable2 = enumerable ?? Enumerable.Empty<TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue>>();
			TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> epsilonEdge = this.EpsilonEdge;
			return enumerable2.Concat(((epsilonEdge != null) ? epsilonEdge.Child.GetAllLeaves() : null) ?? Enumerable.Empty<TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue>>());
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00027578 File Offset: 0x00025778
		public override IEnumerable<TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> GetEdges()
		{
			return ((this.EpsilonEdge == null) ? Enumerable.Empty<TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>>() : Seq.Of<TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>>(new TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>[] { this.EpsilonEdge })).Concat(this.Children.Select((KeyValuePair<TSequenceable, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> kvp) => kvp.Value));
		}

		// Token: 0x04000680 RID: 1664
		private volatile Dictionary<TSequenceable, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> _children;

		// Token: 0x04000681 RID: 1665
		protected TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> EpsilonEdge;
	}
}
