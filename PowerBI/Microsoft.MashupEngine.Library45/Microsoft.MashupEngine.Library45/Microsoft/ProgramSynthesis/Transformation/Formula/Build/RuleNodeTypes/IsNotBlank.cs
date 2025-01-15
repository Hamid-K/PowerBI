using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200155C RID: 5468
	public struct IsNotBlank : IProgramNodeBuilder, IEquatable<IsNotBlank>
	{
		// Token: 0x17001F0D RID: 7949
		// (get) Token: 0x0600B29E RID: 45726 RVA: 0x002722FA File Offset: 0x002704FA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B29F RID: 45727 RVA: 0x00272302 File Offset: 0x00270502
		private IsNotBlank(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B2A0 RID: 45728 RVA: 0x0027230B File Offset: 0x0027050B
		public static IsNotBlank CreateUnsafe(ProgramNode node)
		{
			return new IsNotBlank(node);
		}

		// Token: 0x0600B2A1 RID: 45729 RVA: 0x00272314 File Offset: 0x00270514
		public static IsNotBlank? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsNotBlank)
			{
				return null;
			}
			return new IsNotBlank?(IsNotBlank.CreateUnsafe(node));
		}

		// Token: 0x0600B2A2 RID: 45730 RVA: 0x00272349 File Offset: 0x00270549
		public IsNotBlank(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.IsNotBlank.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B2A3 RID: 45731 RVA: 0x0027236F File Offset: 0x0027056F
		public static implicit operator condition(IsNotBlank arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F0E RID: 7950
		// (get) Token: 0x0600B2A4 RID: 45732 RVA: 0x0027237D File Offset: 0x0027057D
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F0F RID: 7951
		// (get) Token: 0x0600B2A5 RID: 45733 RVA: 0x00272391 File Offset: 0x00270591
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B2A6 RID: 45734 RVA: 0x002723A5 File Offset: 0x002705A5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B2A7 RID: 45735 RVA: 0x002723B8 File Offset: 0x002705B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B2A8 RID: 45736 RVA: 0x002723E2 File Offset: 0x002705E2
		public bool Equals(IsNotBlank other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400460A RID: 17930
		private ProgramNode _node;
	}
}
