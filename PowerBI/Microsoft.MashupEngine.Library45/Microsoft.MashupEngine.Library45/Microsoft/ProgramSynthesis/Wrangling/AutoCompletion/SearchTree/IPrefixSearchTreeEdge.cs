using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x02000251 RID: 593
	public interface IPrefixSearchTreeEdge<TSequenceable, TSequence, out TSubSequence, out TNode, TEdge, TValue> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence> where TNode : class, IPrefixSearchTreeNode<TSequenceable, TSequence, TSubSequence, TNode, TEdge, TValue> where TEdge : IPrefixSearchTreeEdge<TSequenceable, TSequence, TSubSequence, TNode, TEdge, TValue>
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000CAC RID: 3244
		TSubSequence MatchedPrefix { get; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000CAD RID: 3245
		TNode Parent { get; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000CAE RID: 3246
		TNode Child { get; }
	}
}
