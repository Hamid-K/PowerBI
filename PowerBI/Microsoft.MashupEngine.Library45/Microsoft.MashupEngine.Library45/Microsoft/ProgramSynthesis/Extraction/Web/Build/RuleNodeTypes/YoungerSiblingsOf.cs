using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001016 RID: 4118
	public struct YoungerSiblingsOf : IProgramNodeBuilder, IEquatable<YoungerSiblingsOf>
	{
		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x0600796F RID: 31087 RVA: 0x001A066A File Offset: 0x0019E86A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007970 RID: 31088 RVA: 0x001A0672 File Offset: 0x0019E872
		private YoungerSiblingsOf(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007971 RID: 31089 RVA: 0x001A067B File Offset: 0x0019E87B
		public static YoungerSiblingsOf CreateUnsafe(ProgramNode node)
		{
			return new YoungerSiblingsOf(node);
		}

		// Token: 0x06007972 RID: 31090 RVA: 0x001A0684 File Offset: 0x0019E884
		public static YoungerSiblingsOf? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.YoungerSiblingsOf)
			{
				return null;
			}
			return new YoungerSiblingsOf?(YoungerSiblingsOf.CreateUnsafe(node));
		}

		// Token: 0x06007973 RID: 31091 RVA: 0x001A06B9 File Offset: 0x0019E8B9
		public YoungerSiblingsOf(GrammarBuilders g, regionStart value0)
		{
			this._node = g.Rule.YoungerSiblingsOf.BuildASTNode(value0.Node);
		}

		// Token: 0x06007974 RID: 31092 RVA: 0x001A06D8 File Offset: 0x0019E8D8
		public static implicit operator regionStartSiblings(YoungerSiblingsOf arg)
		{
			return regionStartSiblings.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06007975 RID: 31093 RVA: 0x001A06E6 File Offset: 0x0019E8E6
		public regionStart regionStart
		{
			get
			{
				return regionStart.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007976 RID: 31094 RVA: 0x001A06FA File Offset: 0x0019E8FA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007977 RID: 31095 RVA: 0x001A0710 File Offset: 0x0019E910
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007978 RID: 31096 RVA: 0x001A073A File Offset: 0x0019E93A
		public bool Equals(YoungerSiblingsOf other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400332F RID: 13103
		private ProgramNode _node;
	}
}
