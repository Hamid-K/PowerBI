using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C26 RID: 7206
	public struct Transformation : IProgramNodeBuilder, IEquatable<Transformation>
	{
		// Token: 0x1700289F RID: 10399
		// (get) Token: 0x0600F2AE RID: 62126 RVA: 0x00341652 File Offset: 0x0033F852
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F2AF RID: 62127 RVA: 0x0034165A File Offset: 0x0033F85A
		private Transformation(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F2B0 RID: 62128 RVA: 0x00341663 File Offset: 0x0033F863
		public static Transformation CreateUnsafe(ProgramNode node)
		{
			return new Transformation(node);
		}

		// Token: 0x0600F2B1 RID: 62129 RVA: 0x0034166C File Offset: 0x0033F86C
		public static Transformation? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Transformation)
			{
				return null;
			}
			return new Transformation?(Transformation.CreateUnsafe(node));
		}

		// Token: 0x0600F2B2 RID: 62130 RVA: 0x003416A1 File Offset: 0x0033F8A1
		public Transformation(GrammarBuilders g, e value0)
		{
			this._node = g.Rule.Transformation.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F2B3 RID: 62131 RVA: 0x003416C0 File Offset: 0x0033F8C0
		public static implicit operator st(Transformation arg)
		{
			return st.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028A0 RID: 10400
		// (get) Token: 0x0600F2B4 RID: 62132 RVA: 0x003416CE File Offset: 0x0033F8CE
		public e e
		{
			get
			{
				return e.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F2B5 RID: 62133 RVA: 0x003416E2 File Offset: 0x0033F8E2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F2B6 RID: 62134 RVA: 0x003416F8 File Offset: 0x0033F8F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F2B7 RID: 62135 RVA: 0x00341722 File Offset: 0x0033F922
		public bool Equals(Transformation other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B15 RID: 23317
		private ProgramNode _node;
	}
}
