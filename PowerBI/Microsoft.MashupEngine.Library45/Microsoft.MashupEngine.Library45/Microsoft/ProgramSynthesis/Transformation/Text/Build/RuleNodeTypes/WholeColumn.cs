using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C29 RID: 7209
	public struct WholeColumn : IProgramNodeBuilder, IEquatable<WholeColumn>
	{
		// Token: 0x170028A5 RID: 10405
		// (get) Token: 0x0600F2CC RID: 62156 RVA: 0x003418FE File Offset: 0x0033FAFE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F2CD RID: 62157 RVA: 0x00341906 File Offset: 0x0033FB06
		private WholeColumn(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F2CE RID: 62158 RVA: 0x0034190F File Offset: 0x0033FB0F
		public static WholeColumn CreateUnsafe(ProgramNode node)
		{
			return new WholeColumn(node);
		}

		// Token: 0x0600F2CF RID: 62159 RVA: 0x00341918 File Offset: 0x0033FB18
		public static WholeColumn? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.WholeColumn)
			{
				return null;
			}
			return new WholeColumn?(WholeColumn.CreateUnsafe(node));
		}

		// Token: 0x0600F2D0 RID: 62160 RVA: 0x0034194D File Offset: 0x0033FB4D
		public WholeColumn(GrammarBuilders g, x value0)
		{
			this._node = g.Rule.WholeColumn.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F2D1 RID: 62161 RVA: 0x0034196C File Offset: 0x0033FB6C
		public static implicit operator SS(WholeColumn arg)
		{
			return SS.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028A6 RID: 10406
		// (get) Token: 0x0600F2D2 RID: 62162 RVA: 0x0034197A File Offset: 0x0033FB7A
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F2D3 RID: 62163 RVA: 0x0034198E File Offset: 0x0033FB8E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F2D4 RID: 62164 RVA: 0x003419A4 File Offset: 0x0033FBA4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F2D5 RID: 62165 RVA: 0x003419CE File Offset: 0x0033FBCE
		public bool Equals(WholeColumn other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B18 RID: 23320
		private ProgramNode _node;
	}
}
