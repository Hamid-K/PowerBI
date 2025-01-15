using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001081 RID: 4225
	public struct substring : IProgramNodeBuilder, IEquatable<substring>
	{
		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06007ECD RID: 32461 RVA: 0x001AA88A File Offset: 0x001A8A8A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007ECE RID: 32462 RVA: 0x001AA892 File Offset: 0x001A8A92
		private substring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007ECF RID: 32463 RVA: 0x001AA89B File Offset: 0x001A8A9B
		public static substring CreateUnsafe(ProgramNode node)
		{
			return new substring(node);
		}

		// Token: 0x06007ED0 RID: 32464 RVA: 0x001AA8A4 File Offset: 0x001A8AA4
		public static substring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.substring)
			{
				return null;
			}
			return new substring?(substring.CreateUnsafe(node));
		}

		// Token: 0x06007ED1 RID: 32465 RVA: 0x001AA8DE File Offset: 0x001A8ADE
		public static substring CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new substring(new Hole(g.Symbol.substring, holeId));
		}

		// Token: 0x06007ED2 RID: 32466 RVA: 0x001AA8F6 File Offset: 0x001A8AF6
		public Substring Cast_Substring()
		{
			return Substring.CreateUnsafe(this.Node);
		}

		// Token: 0x06007ED3 RID: 32467 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Substring(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007ED4 RID: 32468 RVA: 0x001AA903 File Offset: 0x001A8B03
		public bool Is_Substring(GrammarBuilders g, out Substring value)
		{
			value = Substring.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007ED5 RID: 32469 RVA: 0x001AA917 File Offset: 0x001A8B17
		public Substring? As_Substring(GrammarBuilders g)
		{
			return new Substring?(Substring.CreateUnsafe(this.Node));
		}

		// Token: 0x06007ED6 RID: 32470 RVA: 0x001AA929 File Offset: 0x001A8B29
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007ED7 RID: 32471 RVA: 0x001AA93C File Offset: 0x001A8B3C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007ED8 RID: 32472 RVA: 0x001AA966 File Offset: 0x001A8B66
		public bool Equals(substring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400339A RID: 13210
		private ProgramNode _node;
	}
}
