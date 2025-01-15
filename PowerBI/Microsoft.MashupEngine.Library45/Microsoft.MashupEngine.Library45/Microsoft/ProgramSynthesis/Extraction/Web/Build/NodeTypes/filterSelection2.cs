using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200106B RID: 4203
	public struct filterSelection2 : IProgramNodeBuilder, IEquatable<filterSelection2>
	{
		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06007D4D RID: 32077 RVA: 0x001A6BB6 File Offset: 0x001A4DB6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007D4E RID: 32078 RVA: 0x001A6BBE File Offset: 0x001A4DBE
		private filterSelection2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007D4F RID: 32079 RVA: 0x001A6BC7 File Offset: 0x001A4DC7
		public static filterSelection2 CreateUnsafe(ProgramNode node)
		{
			return new filterSelection2(node);
		}

		// Token: 0x06007D50 RID: 32080 RVA: 0x001A6BD0 File Offset: 0x001A4DD0
		public static filterSelection2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.filterSelection2)
			{
				return null;
			}
			return new filterSelection2?(filterSelection2.CreateUnsafe(node));
		}

		// Token: 0x06007D51 RID: 32081 RVA: 0x001A6C0A File Offset: 0x001A4E0A
		public static filterSelection2 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new filterSelection2(new Hole(g.Symbol.filterSelection2, holeId));
		}

		// Token: 0x06007D52 RID: 32082 RVA: 0x001A6C22 File Offset: 0x001A4E22
		public LeafFilter2 Cast_LeafFilter2()
		{
			return LeafFilter2.CreateUnsafe(this.Node);
		}

		// Token: 0x06007D53 RID: 32083 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LeafFilter2(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007D54 RID: 32084 RVA: 0x001A6C2F File Offset: 0x001A4E2F
		public bool Is_LeafFilter2(GrammarBuilders g, out LeafFilter2 value)
		{
			value = LeafFilter2.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007D55 RID: 32085 RVA: 0x001A6C43 File Offset: 0x001A4E43
		public LeafFilter2? As_LeafFilter2(GrammarBuilders g)
		{
			return new LeafFilter2?(LeafFilter2.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D56 RID: 32086 RVA: 0x001A6C55 File Offset: 0x001A4E55
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007D57 RID: 32087 RVA: 0x001A6C68 File Offset: 0x001A4E68
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007D58 RID: 32088 RVA: 0x001A6C92 File Offset: 0x001A4E92
		public bool Equals(filterSelection2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003384 RID: 13188
		private ProgramNode _node;
	}
}
