using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x02000252 RID: 594
	public interface IPrefixSearchTreeNode<TSequenceable, TSequence, TSubSequence, out TNode, TEdge, TValue> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence> where TNode : class, IPrefixSearchTreeNode<TSequenceable, TSequence, TSubSequence, TNode, TEdge, TValue> where TEdge : IPrefixSearchTreeEdge<TSequenceable, TSequence, TSubSequence, TNode, TEdge, TValue>
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000CAF RID: 3247
		TEdge EdgeToParent { get; }

		// Token: 0x06000CB0 RID: 3248
		TEdge ChildFor(TSubSequence prefix);

		// Token: 0x06000CB1 RID: 3249
		void AddEntry(TSubSequence suffix, TValue value, TSubSequence key);

		// Token: 0x06000CB2 RID: 3250
		bool Lookup(TSubSequence key, out TValue result);

		// Token: 0x06000CB3 RID: 3251
		TValue LookupOrCreate(TSubSequence key, TSubSequence suffix, Func<TValue> factory);

		// Token: 0x06000CB4 RID: 3252
		IEnumerable<KeyValuePair<TSubSequence, TValue>> LookupAll(TSubSequence prefix);

		// Token: 0x06000CB5 RID: 3253
		IEnumerable<TEdge> GetEdges();

		// Token: 0x06000CB6 RID: 3254
		TResult Accept<TResult>(IPrefixSearchTreeVisitor<TSequenceable, TSequence, TSubSequence, TValue, TNode, TEdge, TResult> visitor);
	}
}
