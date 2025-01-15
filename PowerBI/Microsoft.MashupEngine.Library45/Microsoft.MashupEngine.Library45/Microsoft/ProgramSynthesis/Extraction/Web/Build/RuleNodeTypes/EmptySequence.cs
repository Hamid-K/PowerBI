using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200100E RID: 4110
	public struct EmptySequence : IProgramNodeBuilder, IEquatable<EmptySequence>
	{
		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x0600791C RID: 31004 RVA: 0x0019FF02 File Offset: 0x0019E102
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600791D RID: 31005 RVA: 0x0019FF0A File Offset: 0x0019E10A
		private EmptySequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600791E RID: 31006 RVA: 0x0019FF13 File Offset: 0x0019E113
		public static EmptySequence CreateUnsafe(ProgramNode node)
		{
			return new EmptySequence(node);
		}

		// Token: 0x0600791F RID: 31007 RVA: 0x0019FF1C File Offset: 0x0019E11C
		public static EmptySequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.EmptySequence)
			{
				return null;
			}
			return new EmptySequence?(EmptySequence.CreateUnsafe(node));
		}

		// Token: 0x06007920 RID: 31008 RVA: 0x0019FF51 File Offset: 0x0019E151
		public EmptySequence(GrammarBuilders g)
		{
			this._node = g.Rule.EmptySequence.BuildASTNode(Array.Empty<ProgramNode>());
		}

		// Token: 0x06007921 RID: 31009 RVA: 0x0019FF6E File Offset: 0x0019E16E
		public static implicit operator resultSequence(EmptySequence arg)
		{
			return resultSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x06007922 RID: 31010 RVA: 0x0019FF7C File Offset: 0x0019E17C
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007923 RID: 31011 RVA: 0x0019FF90 File Offset: 0x0019E190
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007924 RID: 31012 RVA: 0x0019FFBA File Offset: 0x0019E1BA
		public bool Equals(EmptySequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003327 RID: 13095
		private ProgramNode _node;
	}
}
