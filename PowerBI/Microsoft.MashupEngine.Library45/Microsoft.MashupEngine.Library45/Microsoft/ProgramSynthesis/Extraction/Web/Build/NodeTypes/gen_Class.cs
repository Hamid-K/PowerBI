using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001087 RID: 4231
	public struct gen_Class : IProgramNodeBuilder, IEquatable<gen_Class>
	{
		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06007F43 RID: 32579 RVA: 0x001ABC6A File Offset: 0x001A9E6A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007F44 RID: 32580 RVA: 0x001ABC72 File Offset: 0x001A9E72
		private gen_Class(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007F45 RID: 32581 RVA: 0x001ABC7B File Offset: 0x001A9E7B
		public static gen_Class CreateUnsafe(ProgramNode node)
		{
			return new gen_Class(node);
		}

		// Token: 0x06007F46 RID: 32582 RVA: 0x001ABC84 File Offset: 0x001A9E84
		public static gen_Class? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.gen_Class)
			{
				return null;
			}
			return new gen_Class?(gen_Class.CreateUnsafe(node));
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x001ABCBE File Offset: 0x001A9EBE
		public static gen_Class CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new gen_Class(new Hole(g.Symbol.gen_Class, holeId));
		}

		// Token: 0x06007F48 RID: 32584 RVA: 0x001ABCD6 File Offset: 0x001A9ED6
		public GEN_ClassFilter Cast_GEN_ClassFilter()
		{
			return GEN_ClassFilter.CreateUnsafe(this.Node);
		}

		// Token: 0x06007F49 RID: 32585 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_GEN_ClassFilter(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x001ABCE3 File Offset: 0x001A9EE3
		public bool Is_GEN_ClassFilter(GrammarBuilders g, out GEN_ClassFilter value)
		{
			value = GEN_ClassFilter.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x001ABCF7 File Offset: 0x001A9EF7
		public GEN_ClassFilter? As_GEN_ClassFilter(GrammarBuilders g)
		{
			return new GEN_ClassFilter?(GEN_ClassFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x001ABD09 File Offset: 0x001A9F09
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007F4D RID: 32589 RVA: 0x001ABD1C File Offset: 0x001A9F1C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007F4E RID: 32590 RVA: 0x001ABD46 File Offset: 0x001A9F46
		public bool Equals(gen_Class other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033A0 RID: 13216
		private ProgramNode _node;
	}
}
