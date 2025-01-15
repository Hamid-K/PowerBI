using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001008 RID: 4104
	public struct leafAtom_literalExpr : IProgramNodeBuilder, IEquatable<leafAtom_literalExpr>
	{
		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x060078DF RID: 30943 RVA: 0x0019F992 File Offset: 0x0019DB92
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060078E0 RID: 30944 RVA: 0x0019F99A File Offset: 0x0019DB9A
		private leafAtom_literalExpr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060078E1 RID: 30945 RVA: 0x0019F9A3 File Offset: 0x0019DBA3
		public static leafAtom_literalExpr CreateUnsafe(ProgramNode node)
		{
			return new leafAtom_literalExpr(node);
		}

		// Token: 0x060078E2 RID: 30946 RVA: 0x0019F9AC File Offset: 0x0019DBAC
		public static leafAtom_literalExpr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.leafAtom_literalExpr)
			{
				return null;
			}
			return new leafAtom_literalExpr?(leafAtom_literalExpr.CreateUnsafe(node));
		}

		// Token: 0x060078E3 RID: 30947 RVA: 0x0019F9E1 File Offset: 0x0019DBE1
		public leafAtom_literalExpr(GrammarBuilders g, literalExpr value0)
		{
			this._node = g.UnnamedConversion.leafAtom_literalExpr.BuildASTNode(value0.Node);
		}

		// Token: 0x060078E4 RID: 30948 RVA: 0x0019FA00 File Offset: 0x0019DC00
		public static implicit operator leafAtom(leafAtom_literalExpr arg)
		{
			return leafAtom.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x060078E5 RID: 30949 RVA: 0x0019FA0E File Offset: 0x0019DC0E
		public literalExpr literalExpr
		{
			get
			{
				return literalExpr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060078E6 RID: 30950 RVA: 0x0019FA22 File Offset: 0x0019DC22
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060078E7 RID: 30951 RVA: 0x0019FA38 File Offset: 0x0019DC38
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060078E8 RID: 30952 RVA: 0x0019FA62 File Offset: 0x0019DC62
		public bool Equals(leafAtom_literalExpr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003321 RID: 13089
		private ProgramNode _node;
	}
}
