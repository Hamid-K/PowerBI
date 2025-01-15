using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001057 RID: 4183
	public struct And : IProgramNodeBuilder, IEquatable<And>
	{
		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06007C2A RID: 31786 RVA: 0x001A45CE File Offset: 0x001A27CE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C2B RID: 31787 RVA: 0x001A45D6 File Offset: 0x001A27D6
		private And(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C2C RID: 31788 RVA: 0x001A45DF File Offset: 0x001A27DF
		public static And CreateUnsafe(ProgramNode node)
		{
			return new And(node);
		}

		// Token: 0x06007C2D RID: 31789 RVA: 0x001A45E8 File Offset: 0x001A27E8
		public static And? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.And)
			{
				return null;
			}
			return new And?(And.CreateUnsafe(node));
		}

		// Token: 0x06007C2E RID: 31790 RVA: 0x001A461D File Offset: 0x001A281D
		public And(GrammarBuilders g, fexpr value0, literalExpr value1)
		{
			this._node = g.Rule.And.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007C2F RID: 31791 RVA: 0x001A464F File Offset: 0x001A284F
		public static implicit operator fexpr(And arg)
		{
			return fexpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06007C30 RID: 31792 RVA: 0x001A465D File Offset: 0x001A285D
		public fexpr fexpr
		{
			get
			{
				return fexpr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06007C31 RID: 31793 RVA: 0x001A4671 File Offset: 0x001A2871
		public literalExpr literalExpr
		{
			get
			{
				return literalExpr.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007C32 RID: 31794 RVA: 0x001A4685 File Offset: 0x001A2885
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C33 RID: 31795 RVA: 0x001A4698 File Offset: 0x001A2898
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C34 RID: 31796 RVA: 0x001A46C2 File Offset: 0x001A28C2
		public bool Equals(And other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003370 RID: 13168
		private ProgramNode _node;
	}
}
