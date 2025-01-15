using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001079 RID: 4217
	public struct literalExpr : IProgramNodeBuilder, IEquatable<literalExpr>
	{
		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06007E55 RID: 32341 RVA: 0x001A97DA File Offset: 0x001A79DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007E56 RID: 32342 RVA: 0x001A97E2 File Offset: 0x001A79E2
		private literalExpr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007E57 RID: 32343 RVA: 0x001A97EB File Offset: 0x001A79EB
		public static literalExpr CreateUnsafe(ProgramNode node)
		{
			return new literalExpr(node);
		}

		// Token: 0x06007E58 RID: 32344 RVA: 0x001A97F4 File Offset: 0x001A79F4
		public static literalExpr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.literalExpr)
			{
				return null;
			}
			return new literalExpr?(literalExpr.CreateUnsafe(node));
		}

		// Token: 0x06007E59 RID: 32345 RVA: 0x001A982E File Offset: 0x001A7A2E
		public static literalExpr CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new literalExpr(new Hole(g.Symbol.literalExpr, holeId));
		}

		// Token: 0x06007E5A RID: 32346 RVA: 0x001A9846 File Offset: 0x001A7A46
		public literalExpr_atomExpr Cast_literalExpr_atomExpr()
		{
			return literalExpr_atomExpr.CreateUnsafe(this.Node);
		}

		// Token: 0x06007E5B RID: 32347 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_literalExpr_atomExpr(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007E5C RID: 32348 RVA: 0x001A9853 File Offset: 0x001A7A53
		public bool Is_literalExpr_atomExpr(GrammarBuilders g, out literalExpr_atomExpr value)
		{
			value = literalExpr_atomExpr.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007E5D RID: 32349 RVA: 0x001A9867 File Offset: 0x001A7A67
		public literalExpr_atomExpr? As_literalExpr_atomExpr(GrammarBuilders g)
		{
			return new literalExpr_atomExpr?(literalExpr_atomExpr.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E5E RID: 32350 RVA: 0x001A9879 File Offset: 0x001A7A79
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007E5F RID: 32351 RVA: 0x001A988C File Offset: 0x001A7A8C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007E60 RID: 32352 RVA: 0x001A98B6 File Offset: 0x001A7AB6
		public bool Equals(literalExpr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003392 RID: 13202
		private ProgramNode _node;
	}
}
