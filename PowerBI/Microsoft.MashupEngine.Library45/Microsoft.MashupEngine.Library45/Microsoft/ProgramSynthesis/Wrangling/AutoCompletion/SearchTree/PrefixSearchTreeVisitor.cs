using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x02000257 RID: 599
	public class PrefixSearchTreeVisitor<TSequenceable, TSequence, TSubSequence, TValue, TNode, TEdge, TResult> : IPrefixSearchTreeVisitor<TSequenceable, TSequence, TSubSequence, TValue, TNode, TEdge, TResult> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence> where TNode : class, IPrefixSearchTreeNode<TSequenceable, TSequence, TSubSequence, TNode, TEdge, TValue> where TEdge : IPrefixSearchTreeEdge<TSequenceable, TSequence, TSubSequence, TNode, TEdge, TValue>
	{
		// Token: 0x06000CDB RID: 3291 RVA: 0x00025F6C File Offset: 0x0002416C
		public virtual TResult Visit(TNode node)
		{
			foreach (TEdge tedge in node.GetEdges())
			{
				tedge.Child.Accept<TResult>(this);
			}
			return default(TResult);
		}
	}
}
