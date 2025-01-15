using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001085 RID: 4229
	public struct gen_NthChild : IProgramNodeBuilder, IEquatable<gen_NthChild>
	{
		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06007F2B RID: 32555 RVA: 0x001ABA8A File Offset: 0x001A9C8A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007F2C RID: 32556 RVA: 0x001ABA92 File Offset: 0x001A9C92
		private gen_NthChild(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007F2D RID: 32557 RVA: 0x001ABA9B File Offset: 0x001A9C9B
		public static gen_NthChild CreateUnsafe(ProgramNode node)
		{
			return new gen_NthChild(node);
		}

		// Token: 0x06007F2E RID: 32558 RVA: 0x001ABAA4 File Offset: 0x001A9CA4
		public static gen_NthChild? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.gen_NthChild)
			{
				return null;
			}
			return new gen_NthChild?(gen_NthChild.CreateUnsafe(node));
		}

		// Token: 0x06007F2F RID: 32559 RVA: 0x001ABADE File Offset: 0x001A9CDE
		public static gen_NthChild CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new gen_NthChild(new Hole(g.Symbol.gen_NthChild, holeId));
		}

		// Token: 0x06007F30 RID: 32560 RVA: 0x001ABAF6 File Offset: 0x001A9CF6
		public GEN_NthChildFilter Cast_GEN_NthChildFilter()
		{
			return GEN_NthChildFilter.CreateUnsafe(this.Node);
		}

		// Token: 0x06007F31 RID: 32561 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_GEN_NthChildFilter(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007F32 RID: 32562 RVA: 0x001ABB03 File Offset: 0x001A9D03
		public bool Is_GEN_NthChildFilter(GrammarBuilders g, out GEN_NthChildFilter value)
		{
			value = GEN_NthChildFilter.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007F33 RID: 32563 RVA: 0x001ABB17 File Offset: 0x001A9D17
		public GEN_NthChildFilter? As_GEN_NthChildFilter(GrammarBuilders g)
		{
			return new GEN_NthChildFilter?(GEN_NthChildFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F34 RID: 32564 RVA: 0x001ABB29 File Offset: 0x001A9D29
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007F35 RID: 32565 RVA: 0x001ABB3C File Offset: 0x001A9D3C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007F36 RID: 32566 RVA: 0x001ABB66 File Offset: 0x001A9D66
		public bool Equals(gen_NthChild other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400339E RID: 13214
		private ProgramNode _node;
	}
}
