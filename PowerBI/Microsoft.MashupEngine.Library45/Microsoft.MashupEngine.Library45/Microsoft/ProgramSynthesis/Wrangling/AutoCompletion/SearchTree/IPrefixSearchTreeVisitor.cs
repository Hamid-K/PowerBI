using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x02000253 RID: 595
	public interface IPrefixSearchTreeVisitor<TSequenceable, TSequence, TSubSequence, TValue, in TNode, TEdge, out TResult> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence> where TNode : class, IPrefixSearchTreeNode<TSequenceable, TSequence, TSubSequence, TNode, TEdge, TValue> where TEdge : IPrefixSearchTreeEdge<TSequenceable, TSequence, TSubSequence, TNode, TEdge, TValue>
	{
		// Token: 0x06000CB7 RID: 3255
		TResult Visit(TNode node);
	}
}
