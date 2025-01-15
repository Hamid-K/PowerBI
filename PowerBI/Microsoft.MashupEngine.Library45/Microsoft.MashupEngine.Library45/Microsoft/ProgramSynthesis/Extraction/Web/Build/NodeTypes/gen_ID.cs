using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001088 RID: 4232
	public struct gen_ID : IProgramNodeBuilder, IEquatable<gen_ID>
	{
		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06007F4F RID: 32591 RVA: 0x001ABD5A File Offset: 0x001A9F5A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007F50 RID: 32592 RVA: 0x001ABD62 File Offset: 0x001A9F62
		private gen_ID(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007F51 RID: 32593 RVA: 0x001ABD6B File Offset: 0x001A9F6B
		public static gen_ID CreateUnsafe(ProgramNode node)
		{
			return new gen_ID(node);
		}

		// Token: 0x06007F52 RID: 32594 RVA: 0x001ABD74 File Offset: 0x001A9F74
		public static gen_ID? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.gen_ID)
			{
				return null;
			}
			return new gen_ID?(gen_ID.CreateUnsafe(node));
		}

		// Token: 0x06007F53 RID: 32595 RVA: 0x001ABDAE File Offset: 0x001A9FAE
		public static gen_ID CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new gen_ID(new Hole(g.Symbol.gen_ID, holeId));
		}

		// Token: 0x06007F54 RID: 32596 RVA: 0x001ABDC6 File Offset: 0x001A9FC6
		public GEN_IDFilter Cast_GEN_IDFilter()
		{
			return GEN_IDFilter.CreateUnsafe(this.Node);
		}

		// Token: 0x06007F55 RID: 32597 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_GEN_IDFilter(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007F56 RID: 32598 RVA: 0x001ABDD3 File Offset: 0x001A9FD3
		public bool Is_GEN_IDFilter(GrammarBuilders g, out GEN_IDFilter value)
		{
			value = GEN_IDFilter.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007F57 RID: 32599 RVA: 0x001ABDE7 File Offset: 0x001A9FE7
		public GEN_IDFilter? As_GEN_IDFilter(GrammarBuilders g)
		{
			return new GEN_IDFilter?(GEN_IDFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F58 RID: 32600 RVA: 0x001ABDF9 File Offset: 0x001A9FF9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007F59 RID: 32601 RVA: 0x001ABE0C File Offset: 0x001AA00C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007F5A RID: 32602 RVA: 0x001ABE36 File Offset: 0x001AA036
		public bool Equals(gen_ID other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033A1 RID: 13217
		private ProgramNode _node;
	}
}
