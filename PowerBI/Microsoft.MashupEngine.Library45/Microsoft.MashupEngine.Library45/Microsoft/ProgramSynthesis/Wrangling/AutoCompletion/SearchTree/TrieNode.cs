using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x0200026C RID: 620
	public abstract class TrieNode<TSequenceable, TSequence, TSubSequence, TValue> : IPrefixSearchTreeNode<TSequenceable, TSequence, TSubSequence, TrieNode<TSequenceable, TSequence, TSubSequence, TValue>, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>, TValue> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence>
	{
		// Token: 0x06000D55 RID: 3413 RVA: 0x00027067 File Offset: 0x00025267
		protected TrieNode(TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> edgeToParent)
		{
			this.EdgeToParent = edgeToParent;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00027076 File Offset: 0x00025276
		public TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> EdgeToParent { get; }

		// Token: 0x06000D57 RID: 3415
		public abstract TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> ChildFor(TSubSequence suffix);

		// Token: 0x06000D58 RID: 3416
		public abstract void AddEntry(TSubSequence suffix, TValue value, TSubSequence key);

		// Token: 0x06000D59 RID: 3417
		public abstract bool Lookup(TSubSequence key, out TValue result);

		// Token: 0x06000D5A RID: 3418
		public abstract TValue LookupOrCreate(TSubSequence key, TSubSequence suffix, Func<TValue> factory);

		// Token: 0x06000D5B RID: 3419
		public abstract IEnumerable<KeyValuePair<TSubSequence, TValue>> LookupAll(TSubSequence prefix);

		// Token: 0x06000D5C RID: 3420 RVA: 0x0002707E File Offset: 0x0002527E
		public TResult Accept<TResult>(IPrefixSearchTreeVisitor<TSequenceable, TSequence, TSubSequence, TValue, TrieNode<TSequenceable, TSequence, TSubSequence, TValue>, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>, TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000D5D RID: 3421
		public abstract IEnumerable<TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> GetEdges();

		// Token: 0x06000D5E RID: 3422
		public abstract IEnumerable<TrieNode<TSequenceable, TSequence, TSubSequence, TValue>> GetAllDescendants();

		// Token: 0x06000D5F RID: 3423
		public abstract IEnumerable<TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue>> GetAllLeaves();
	}
}
