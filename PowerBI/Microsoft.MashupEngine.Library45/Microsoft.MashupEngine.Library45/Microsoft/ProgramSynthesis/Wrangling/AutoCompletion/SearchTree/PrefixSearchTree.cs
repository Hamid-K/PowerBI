using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x02000255 RID: 597
	public abstract class PrefixSearchTree<TSequenceable, TSequence, TSubSequence, TValue, TNode, TEdge> : IPrefixSearchTree<TSequenceable, TSequence, TSubSequence, TValue> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence> where TNode : class, IPrefixSearchTreeNode<TSequenceable, TSequence, TSubSequence, TNode, TEdge, TValue> where TEdge : IPrefixSearchTreeEdge<TSequenceable, TSequence, TSubSequence, TNode, TEdge, TValue>
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00025D78 File Offset: 0x00023F78
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00025D80 File Offset: 0x00023F80
		protected TNode Root { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000CC8 RID: 3272
		protected abstract TSubSequence EmptySubSequence { get; }

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00025D89 File Offset: 0x00023F89
		public void Add(TSequence key, TValue value)
		{
			this.Add(this.TraversalSubSequenceForKey(key), this.SubSequenceForSequence(key), value);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00025DA0 File Offset: 0x00023FA0
		public bool TryGetValue(TSequence key, out TValue result)
		{
			return this.Lookup(this.TraversalSubSequenceForKey(key), out result);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00025DB0 File Offset: 0x00023FB0
		public TValue GetOrCreate(TSequence key, Func<TValue> factory)
		{
			TSubSequence tsubSequence = this.TraversalSubSequenceForKey(key);
			TSubSequence tsubSequence2 = this.SubSequenceForSequence(key);
			if (this.Root == null)
			{
				TValue tvalue = factory();
				this.Add(tsubSequence, tsubSequence2, tvalue);
				return tvalue;
			}
			return this.Root.LookupOrCreate(tsubSequence2, tsubSequence, factory);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00025DFF File Offset: 0x00023FFF
		public IEnumerable<KeyValuePair<TSequence, TValue>> PrefixLookup(TSequence prefix)
		{
			return from s in this.PrefixLookup(this.TraversalSubSequenceForKey(prefix))
				select new KeyValuePair<TSequence, TValue>(s.Key.Value, s.Value);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00025E34 File Offset: 0x00024034
		public void Clear()
		{
			this.Root = default(TNode);
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00025E50 File Offset: 0x00024050
		public IEnumerable<KeyValuePair<TSequence, TValue>> Items
		{
			get
			{
				return from r in this.PrefixLookup(this.EmptySubSequence)
					select new KeyValuePair<TSequence, TValue>(r.Key.Value, r.Value);
			}
		}

		// Token: 0x06000CCF RID: 3279
		protected abstract TNode CreateRootNode(TSubSequence traversalKey, TSubSequence key, TValue value);

		// Token: 0x06000CD0 RID: 3280
		protected abstract TSubSequence SubSequenceForSequence(TSequence sequence);

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00025E82 File Offset: 0x00024082
		protected virtual TSubSequence TraversalSubSequenceForKey(TSequence key)
		{
			return this.SubSequenceForSequence(key);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00025E8B File Offset: 0x0002408B
		private void Add(TSubSequence traversalKey, TSubSequence key, TValue value)
		{
			if (this.Root == null)
			{
				this.Root = this.CreateRootNode(traversalKey, key, value);
				return;
			}
			this.Root.AddEntry(traversalKey, value, key);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00025EBD File Offset: 0x000240BD
		private bool Lookup(TSubSequence key, out TValue result)
		{
			result = default(TValue);
			return this.Root != null && this.Root.Lookup(key, out result);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00025EE7 File Offset: 0x000240E7
		private IEnumerable<KeyValuePair<TSubSequence, TValue>> PrefixLookup(TSubSequence prefix)
		{
			if (this.Root == null)
			{
				return Enumerable.Empty<KeyValuePair<TSubSequence, TValue>>();
			}
			return this.Root.LookupAll(prefix);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00025F10 File Offset: 0x00024110
		public TResult Accept<TResult>(IPrefixSearchTreeVisitor<TSequenceable, TSequence, TSubSequence, TValue, TNode, TEdge, TResult> visitor)
		{
			if (this.Root == null)
			{
				return default(TResult);
			}
			return visitor.Visit(this.Root);
		}
	}
}
