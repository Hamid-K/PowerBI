using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200157F RID: 5503
	public struct SliceBetween : IProgramNodeBuilder, IEquatable<SliceBetween>
	{
		// Token: 0x17001F73 RID: 8051
		// (get) Token: 0x0600B41C RID: 46108 RVA: 0x00274562 File Offset: 0x00272762
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B41D RID: 46109 RVA: 0x0027456A File Offset: 0x0027276A
		private SliceBetween(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B41E RID: 46110 RVA: 0x00274573 File Offset: 0x00272773
		public static SliceBetween CreateUnsafe(ProgramNode node)
		{
			return new SliceBetween(node);
		}

		// Token: 0x0600B41F RID: 46111 RVA: 0x0027457C File Offset: 0x0027277C
		public static SliceBetween? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SliceBetween)
			{
				return null;
			}
			return new SliceBetween?(SliceBetween.CreateUnsafe(node));
		}

		// Token: 0x0600B420 RID: 46112 RVA: 0x002745B1 File Offset: 0x002727B1
		public SliceBetween(GrammarBuilders g, x value0, sliceBetweenStartText value1, sliceBetweenEndText value2)
		{
			this._node = g.Rule.SliceBetween.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B421 RID: 46113 RVA: 0x002745DE File Offset: 0x002727DE
		public static implicit operator substring(SliceBetween arg)
		{
			return substring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x0600B422 RID: 46114 RVA: 0x002745EC File Offset: 0x002727EC
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F75 RID: 8053
		// (get) Token: 0x0600B423 RID: 46115 RVA: 0x00274600 File Offset: 0x00272800
		public sliceBetweenStartText sliceBetweenStartText
		{
			get
			{
				return sliceBetweenStartText.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F76 RID: 8054
		// (get) Token: 0x0600B424 RID: 46116 RVA: 0x00274614 File Offset: 0x00272814
		public sliceBetweenEndText sliceBetweenEndText
		{
			get
			{
				return sliceBetweenEndText.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B425 RID: 46117 RVA: 0x00274628 File Offset: 0x00272828
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B426 RID: 46118 RVA: 0x0027463C File Offset: 0x0027283C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B427 RID: 46119 RVA: 0x00274666 File Offset: 0x00272866
		public bool Equals(SliceBetween other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400462D RID: 17965
		private ProgramNode _node;
	}
}
