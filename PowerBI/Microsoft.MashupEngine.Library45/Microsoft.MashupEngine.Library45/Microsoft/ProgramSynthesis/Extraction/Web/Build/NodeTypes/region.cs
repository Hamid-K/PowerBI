using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001061 RID: 4193
	public struct region : IProgramNodeBuilder, IEquatable<region>
	{
		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06007CB5 RID: 31925 RVA: 0x001A570E File Offset: 0x001A390E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007CB6 RID: 31926 RVA: 0x001A5716 File Offset: 0x001A3916
		private region(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007CB7 RID: 31927 RVA: 0x001A571F File Offset: 0x001A391F
		public static region CreateUnsafe(ProgramNode node)
		{
			return new region(node);
		}

		// Token: 0x06007CB8 RID: 31928 RVA: 0x001A5728 File Offset: 0x001A3928
		public static region? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.region)
			{
				return null;
			}
			return new region?(region.CreateUnsafe(node));
		}

		// Token: 0x06007CB9 RID: 31929 RVA: 0x001A5762 File Offset: 0x001A3962
		public static region CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new region(new Hole(g.Symbol.region, holeId));
		}

		// Token: 0x06007CBA RID: 31930 RVA: 0x001A577A File Offset: 0x001A397A
		public LetRegion Cast_LetRegion()
		{
			return LetRegion.CreateUnsafe(this.Node);
		}

		// Token: 0x06007CBB RID: 31931 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetRegion(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007CBC RID: 31932 RVA: 0x001A5787 File Offset: 0x001A3987
		public bool Is_LetRegion(GrammarBuilders g, out LetRegion value)
		{
			value = LetRegion.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007CBD RID: 31933 RVA: 0x001A579B File Offset: 0x001A399B
		public LetRegion? As_LetRegion(GrammarBuilders g)
		{
			return new LetRegion?(LetRegion.CreateUnsafe(this.Node));
		}

		// Token: 0x06007CBE RID: 31934 RVA: 0x001A57AD File Offset: 0x001A39AD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007CBF RID: 31935 RVA: 0x001A57C0 File Offset: 0x001A39C0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007CC0 RID: 31936 RVA: 0x001A57EA File Offset: 0x001A39EA
		public bool Equals(region other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400337A RID: 13178
		private ProgramNode _node;
	}
}
