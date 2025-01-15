using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001009 RID: 4105
	public struct literalExpr_atomExpr : IProgramNodeBuilder, IEquatable<literalExpr_atomExpr>
	{
		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x060078E9 RID: 30953 RVA: 0x0019FA76 File Offset: 0x0019DC76
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060078EA RID: 30954 RVA: 0x0019FA7E File Offset: 0x0019DC7E
		private literalExpr_atomExpr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060078EB RID: 30955 RVA: 0x0019FA87 File Offset: 0x0019DC87
		public static literalExpr_atomExpr CreateUnsafe(ProgramNode node)
		{
			return new literalExpr_atomExpr(node);
		}

		// Token: 0x060078EC RID: 30956 RVA: 0x0019FA90 File Offset: 0x0019DC90
		public static literalExpr_atomExpr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.literalExpr_atomExpr)
			{
				return null;
			}
			return new literalExpr_atomExpr?(literalExpr_atomExpr.CreateUnsafe(node));
		}

		// Token: 0x060078ED RID: 30957 RVA: 0x0019FAC5 File Offset: 0x0019DCC5
		public literalExpr_atomExpr(GrammarBuilders g, atomExpr value0)
		{
			this._node = g.UnnamedConversion.literalExpr_atomExpr.BuildASTNode(value0.Node);
		}

		// Token: 0x060078EE RID: 30958 RVA: 0x0019FAE4 File Offset: 0x0019DCE4
		public static implicit operator literalExpr(literalExpr_atomExpr arg)
		{
			return literalExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x060078EF RID: 30959 RVA: 0x0019FAF2 File Offset: 0x0019DCF2
		public atomExpr atomExpr
		{
			get
			{
				return atomExpr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060078F0 RID: 30960 RVA: 0x0019FB06 File Offset: 0x0019DD06
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060078F1 RID: 30961 RVA: 0x0019FB1C File Offset: 0x0019DD1C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060078F2 RID: 30962 RVA: 0x0019FB46 File Offset: 0x0019DD46
		public bool Equals(literalExpr_atomExpr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003322 RID: 13090
		private ProgramNode _node;
	}
}
