using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001089 RID: 4233
	public struct gen_NodeName : IProgramNodeBuilder, IEquatable<gen_NodeName>
	{
		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06007F5B RID: 32603 RVA: 0x001ABE4A File Offset: 0x001AA04A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007F5C RID: 32604 RVA: 0x001ABE52 File Offset: 0x001AA052
		private gen_NodeName(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007F5D RID: 32605 RVA: 0x001ABE5B File Offset: 0x001AA05B
		public static gen_NodeName CreateUnsafe(ProgramNode node)
		{
			return new gen_NodeName(node);
		}

		// Token: 0x06007F5E RID: 32606 RVA: 0x001ABE64 File Offset: 0x001AA064
		public static gen_NodeName? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.gen_NodeName)
			{
				return null;
			}
			return new gen_NodeName?(gen_NodeName.CreateUnsafe(node));
		}

		// Token: 0x06007F5F RID: 32607 RVA: 0x001ABE9E File Offset: 0x001AA09E
		public static gen_NodeName CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new gen_NodeName(new Hole(g.Symbol.gen_NodeName, holeId));
		}

		// Token: 0x06007F60 RID: 32608 RVA: 0x001ABEB6 File Offset: 0x001AA0B6
		public GEN_NodeNameFilter Cast_GEN_NodeNameFilter()
		{
			return GEN_NodeNameFilter.CreateUnsafe(this.Node);
		}

		// Token: 0x06007F61 RID: 32609 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_GEN_NodeNameFilter(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007F62 RID: 32610 RVA: 0x001ABEC3 File Offset: 0x001AA0C3
		public bool Is_GEN_NodeNameFilter(GrammarBuilders g, out GEN_NodeNameFilter value)
		{
			value = GEN_NodeNameFilter.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007F63 RID: 32611 RVA: 0x001ABED7 File Offset: 0x001AA0D7
		public GEN_NodeNameFilter? As_GEN_NodeNameFilter(GrammarBuilders g)
		{
			return new GEN_NodeNameFilter?(GEN_NodeNameFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F64 RID: 32612 RVA: 0x001ABEE9 File Offset: 0x001AA0E9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007F65 RID: 32613 RVA: 0x001ABEFC File Offset: 0x001AA0FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007F66 RID: 32614 RVA: 0x001ABF26 File Offset: 0x001AA126
		public bool Equals(gen_NodeName other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033A2 RID: 13218
		private ProgramNode _node;
	}
}
