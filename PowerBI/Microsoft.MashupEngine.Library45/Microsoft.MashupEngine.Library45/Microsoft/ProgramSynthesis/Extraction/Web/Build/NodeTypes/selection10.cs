using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001075 RID: 4213
	public struct selection10 : IProgramNodeBuilder, IEquatable<selection10>
	{
		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06007DE9 RID: 32233 RVA: 0x001A82DE File Offset: 0x001A64DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007DEA RID: 32234 RVA: 0x001A82E6 File Offset: 0x001A64E6
		private selection10(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007DEB RID: 32235 RVA: 0x001A82EF File Offset: 0x001A64EF
		public static selection10 CreateUnsafe(ProgramNode node)
		{
			return new selection10(node);
		}

		// Token: 0x06007DEC RID: 32236 RVA: 0x001A82F8 File Offset: 0x001A64F8
		public static selection10? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selection10)
			{
				return null;
			}
			return new selection10?(selection10.CreateUnsafe(node));
		}

		// Token: 0x06007DED RID: 32237 RVA: 0x001A8332 File Offset: 0x001A6532
		public static selection10 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selection10(new Hole(g.Symbol.selection10, holeId));
		}

		// Token: 0x06007DEE RID: 32238 RVA: 0x001A834A File Offset: 0x001A654A
		public selection10_allNodes Cast_selection10_allNodes()
		{
			return selection10_allNodes.CreateUnsafe(this.Node);
		}

		// Token: 0x06007DEF RID: 32239 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_selection10_allNodes(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007DF0 RID: 32240 RVA: 0x001A8357 File Offset: 0x001A6557
		public bool Is_selection10_allNodes(GrammarBuilders g, out selection10_allNodes value)
		{
			value = selection10_allNodes.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007DF1 RID: 32241 RVA: 0x001A836B File Offset: 0x001A656B
		public selection10_allNodes? As_selection10_allNodes(GrammarBuilders g)
		{
			return new selection10_allNodes?(selection10_allNodes.CreateUnsafe(this.Node));
		}

		// Token: 0x06007DF2 RID: 32242 RVA: 0x001A837D File Offset: 0x001A657D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007DF3 RID: 32243 RVA: 0x001A8390 File Offset: 0x001A6590
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007DF4 RID: 32244 RVA: 0x001A83BA File Offset: 0x001A65BA
		public bool Equals(selection10 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400338E RID: 13198
		private ProgramNode _node;
	}
}
