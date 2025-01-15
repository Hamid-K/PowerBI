using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001564 RID: 5476
	public struct Or : IProgramNodeBuilder, IEquatable<Or>
	{
		// Token: 0x17001F2B RID: 7979
		// (get) Token: 0x0600B2FC RID: 45820 RVA: 0x00272BA2 File Offset: 0x00270DA2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B2FD RID: 45821 RVA: 0x00272BAA File Offset: 0x00270DAA
		private Or(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B2FE RID: 45822 RVA: 0x00272BB3 File Offset: 0x00270DB3
		public static Or CreateUnsafe(ProgramNode node)
		{
			return new Or(node);
		}

		// Token: 0x0600B2FF RID: 45823 RVA: 0x00272BBC File Offset: 0x00270DBC
		public static Or? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Or)
			{
				return null;
			}
			return new Or?(Or.CreateUnsafe(node));
		}

		// Token: 0x0600B300 RID: 45824 RVA: 0x00272BF1 File Offset: 0x00270DF1
		public Or(GrammarBuilders g, condition value0, condition value1)
		{
			this._node = g.Rule.Or.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B301 RID: 45825 RVA: 0x00272C17 File Offset: 0x00270E17
		public static implicit operator or(Or arg)
		{
			return or.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F2C RID: 7980
		// (get) Token: 0x0600B302 RID: 45826 RVA: 0x00272C25 File Offset: 0x00270E25
		public condition condition1
		{
			get
			{
				return condition.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F2D RID: 7981
		// (get) Token: 0x0600B303 RID: 45827 RVA: 0x00272C39 File Offset: 0x00270E39
		public condition condition2
		{
			get
			{
				return condition.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B304 RID: 45828 RVA: 0x00272C4D File Offset: 0x00270E4D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B305 RID: 45829 RVA: 0x00272C60 File Offset: 0x00270E60
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B306 RID: 45830 RVA: 0x00272C8A File Offset: 0x00270E8A
		public bool Equals(Or other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004612 RID: 17938
		private ProgramNode _node;
	}
}
