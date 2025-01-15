using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200106E RID: 4206
	public struct filterSelection3 : IProgramNodeBuilder, IEquatable<filterSelection3>
	{
		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06007D7D RID: 32125 RVA: 0x001A731E File Offset: 0x001A551E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007D7E RID: 32126 RVA: 0x001A7326 File Offset: 0x001A5526
		private filterSelection3(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007D7F RID: 32127 RVA: 0x001A732F File Offset: 0x001A552F
		public static filterSelection3 CreateUnsafe(ProgramNode node)
		{
			return new filterSelection3(node);
		}

		// Token: 0x06007D80 RID: 32128 RVA: 0x001A7338 File Offset: 0x001A5538
		public static filterSelection3? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.filterSelection3)
			{
				return null;
			}
			return new filterSelection3?(filterSelection3.CreateUnsafe(node));
		}

		// Token: 0x06007D81 RID: 32129 RVA: 0x001A7372 File Offset: 0x001A5572
		public static filterSelection3 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new filterSelection3(new Hole(g.Symbol.filterSelection3, holeId));
		}

		// Token: 0x06007D82 RID: 32130 RVA: 0x001A738A File Offset: 0x001A558A
		public LeafFilter3 Cast_LeafFilter3()
		{
			return LeafFilter3.CreateUnsafe(this.Node);
		}

		// Token: 0x06007D83 RID: 32131 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LeafFilter3(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007D84 RID: 32132 RVA: 0x001A7397 File Offset: 0x001A5597
		public bool Is_LeafFilter3(GrammarBuilders g, out LeafFilter3 value)
		{
			value = LeafFilter3.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007D85 RID: 32133 RVA: 0x001A73AB File Offset: 0x001A55AB
		public LeafFilter3? As_LeafFilter3(GrammarBuilders g)
		{
			return new LeafFilter3?(LeafFilter3.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D86 RID: 32134 RVA: 0x001A73BD File Offset: 0x001A55BD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007D87 RID: 32135 RVA: 0x001A73D0 File Offset: 0x001A55D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007D88 RID: 32136 RVA: 0x001A73FA File Offset: 0x001A55FA
		public bool Equals(filterSelection3 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003387 RID: 13191
		private ProgramNode _node;
	}
}
