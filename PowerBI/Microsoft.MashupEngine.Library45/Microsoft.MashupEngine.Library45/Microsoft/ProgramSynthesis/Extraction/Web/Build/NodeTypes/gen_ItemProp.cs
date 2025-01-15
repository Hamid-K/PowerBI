using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200108A RID: 4234
	public struct gen_ItemProp : IProgramNodeBuilder, IEquatable<gen_ItemProp>
	{
		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06007F67 RID: 32615 RVA: 0x001ABF3A File Offset: 0x001AA13A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007F68 RID: 32616 RVA: 0x001ABF42 File Offset: 0x001AA142
		private gen_ItemProp(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007F69 RID: 32617 RVA: 0x001ABF4B File Offset: 0x001AA14B
		public static gen_ItemProp CreateUnsafe(ProgramNode node)
		{
			return new gen_ItemProp(node);
		}

		// Token: 0x06007F6A RID: 32618 RVA: 0x001ABF54 File Offset: 0x001AA154
		public static gen_ItemProp? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.gen_ItemProp)
			{
				return null;
			}
			return new gen_ItemProp?(gen_ItemProp.CreateUnsafe(node));
		}

		// Token: 0x06007F6B RID: 32619 RVA: 0x001ABF8E File Offset: 0x001AA18E
		public static gen_ItemProp CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new gen_ItemProp(new Hole(g.Symbol.gen_ItemProp, holeId));
		}

		// Token: 0x06007F6C RID: 32620 RVA: 0x001ABFA6 File Offset: 0x001AA1A6
		public GEN_ItemPropFilter Cast_GEN_ItemPropFilter()
		{
			return GEN_ItemPropFilter.CreateUnsafe(this.Node);
		}

		// Token: 0x06007F6D RID: 32621 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_GEN_ItemPropFilter(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007F6E RID: 32622 RVA: 0x001ABFB3 File Offset: 0x001AA1B3
		public bool Is_GEN_ItemPropFilter(GrammarBuilders g, out GEN_ItemPropFilter value)
		{
			value = GEN_ItemPropFilter.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007F6F RID: 32623 RVA: 0x001ABFC7 File Offset: 0x001AA1C7
		public GEN_ItemPropFilter? As_GEN_ItemPropFilter(GrammarBuilders g)
		{
			return new GEN_ItemPropFilter?(GEN_ItemPropFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F70 RID: 32624 RVA: 0x001ABFD9 File Offset: 0x001AA1D9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007F71 RID: 32625 RVA: 0x001ABFEC File Offset: 0x001AA1EC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007F72 RID: 32626 RVA: 0x001AC016 File Offset: 0x001AA216
		public bool Equals(gen_ItemProp other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033A3 RID: 13219
		private ProgramNode _node;
	}
}
