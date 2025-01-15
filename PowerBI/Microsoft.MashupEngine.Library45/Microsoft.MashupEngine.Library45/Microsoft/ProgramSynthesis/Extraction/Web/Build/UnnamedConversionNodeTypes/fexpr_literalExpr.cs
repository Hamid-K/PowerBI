using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200100A RID: 4106
	public struct fexpr_literalExpr : IProgramNodeBuilder, IEquatable<fexpr_literalExpr>
	{
		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x060078F3 RID: 30963 RVA: 0x0019FB5A File Offset: 0x0019DD5A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060078F4 RID: 30964 RVA: 0x0019FB62 File Offset: 0x0019DD62
		private fexpr_literalExpr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060078F5 RID: 30965 RVA: 0x0019FB6B File Offset: 0x0019DD6B
		public static fexpr_literalExpr CreateUnsafe(ProgramNode node)
		{
			return new fexpr_literalExpr(node);
		}

		// Token: 0x060078F6 RID: 30966 RVA: 0x0019FB74 File Offset: 0x0019DD74
		public static fexpr_literalExpr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.fexpr_literalExpr)
			{
				return null;
			}
			return new fexpr_literalExpr?(fexpr_literalExpr.CreateUnsafe(node));
		}

		// Token: 0x060078F7 RID: 30967 RVA: 0x0019FBA9 File Offset: 0x0019DDA9
		public fexpr_literalExpr(GrammarBuilders g, literalExpr value0)
		{
			this._node = g.UnnamedConversion.fexpr_literalExpr.BuildASTNode(value0.Node);
		}

		// Token: 0x060078F8 RID: 30968 RVA: 0x0019FBC8 File Offset: 0x0019DDC8
		public static implicit operator fexpr(fexpr_literalExpr arg)
		{
			return fexpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x060078F9 RID: 30969 RVA: 0x0019FBD6 File Offset: 0x0019DDD6
		public literalExpr literalExpr
		{
			get
			{
				return literalExpr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060078FA RID: 30970 RVA: 0x0019FBEA File Offset: 0x0019DDEA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060078FB RID: 30971 RVA: 0x0019FC00 File Offset: 0x0019DE00
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060078FC RID: 30972 RVA: 0x0019FC2A File Offset: 0x0019DE2A
		public bool Equals(fexpr_literalExpr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003323 RID: 13091
		private ProgramNode _node;
	}
}
