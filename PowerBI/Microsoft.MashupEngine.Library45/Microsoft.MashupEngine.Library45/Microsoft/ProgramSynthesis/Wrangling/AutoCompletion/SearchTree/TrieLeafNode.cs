using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x02000267 RID: 615
	public class TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue> : TrieNode<TSequenceable, TSequence, TSubSequence, TValue> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence>
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x00026CA2 File Offset: 0x00024EA2
		public TrieLeafNode(TSubSequence key, TValue value, TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> edgeToParent)
			: base(edgeToParent)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00026CB9 File Offset: 0x00024EB9
		public TSubSequence Key { get; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00026CC1 File Offset: 0x00024EC1
		public TValue Value { get; }

		// Token: 0x06000D2D RID: 3373 RVA: 0x00002188 File Offset: 0x00000388
		public override TrieEdge<TSequenceable, TSequence, TSubSequence, TValue> ChildFor(TSubSequence suffix)
		{
			return null;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00026CC9 File Offset: 0x00024EC9
		public override void AddEntry(TSubSequence suffix, TValue value, TSubSequence key)
		{
			throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("{0}.{1} should never have been called.", new object[]
			{
				base.GetType(),
				"AddEntry"
			})));
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00026CF6 File Offset: 0x00024EF6
		public override bool Lookup(TSubSequence key, out TValue result)
		{
			if (key.Length == 0U)
			{
				result = this.Value;
				return true;
			}
			result = default(TValue);
			return false;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00026D1B File Offset: 0x00024F1B
		public override TValue LookupOrCreate(TSubSequence key, TSubSequence suffix, Func<TValue> factory)
		{
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("{0}.{1} should never have been called.", new object[]
			{
				base.GetType(),
				"LookupOrCreate"
			})));
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00026D48 File Offset: 0x00024F48
		public override IEnumerable<KeyValuePair<TSubSequence, TValue>> LookupAll(TSubSequence prefix)
		{
			if (prefix.Length == 0U)
			{
				yield return new KeyValuePair<TSubSequence, TValue>(this.Key, this.Value);
			}
			yield break;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00026D5F File Offset: 0x00024F5F
		public override IEnumerable<TrieNode<TSequenceable, TSequence, TSubSequence, TValue>> GetAllDescendants()
		{
			yield return this;
			yield break;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00026D6F File Offset: 0x00024F6F
		public override IEnumerable<TrieLeafNode<TSequenceable, TSequence, TSubSequence, TValue>> GetAllLeaves()
		{
			yield return this;
			yield break;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00026D7F File Offset: 0x00024F7F
		public override IEnumerable<TrieEdge<TSequenceable, TSequence, TSubSequence, TValue>> GetEdges()
		{
			yield break;
		}
	}
}
