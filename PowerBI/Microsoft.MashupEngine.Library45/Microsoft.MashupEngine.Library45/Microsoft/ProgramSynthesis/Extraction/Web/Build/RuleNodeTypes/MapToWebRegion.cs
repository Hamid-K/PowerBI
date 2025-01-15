using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200104B RID: 4171
	public struct MapToWebRegion : IProgramNodeBuilder, IEquatable<MapToWebRegion>
	{
		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06007BA6 RID: 31654 RVA: 0x001A3926 File Offset: 0x001A1B26
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x001A392E File Offset: 0x001A1B2E
		private MapToWebRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007BA8 RID: 31656 RVA: 0x001A3937 File Offset: 0x001A1B37
		public static MapToWebRegion CreateUnsafe(ProgramNode node)
		{
			return new MapToWebRegion(node);
		}

		// Token: 0x06007BA9 RID: 31657 RVA: 0x001A3940 File Offset: 0x001A1B40
		public static MapToWebRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MapToWebRegion)
			{
				return null;
			}
			return new MapToWebRegion?(MapToWebRegion.CreateUnsafe(node));
		}

		// Token: 0x06007BAA RID: 31658 RVA: 0x001A3975 File Offset: 0x001A1B75
		public MapToWebRegion(GrammarBuilders g, mapNodeInSequence value0, selection value1)
		{
			this._node = g.Rule.MapToWebRegion.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007BAB RID: 31659 RVA: 0x001A39A7 File Offset: 0x001A1BA7
		public static implicit operator subNodeSequence(MapToWebRegion arg)
		{
			return subNodeSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06007BAC RID: 31660 RVA: 0x001A39B5 File Offset: 0x001A1BB5
		public mapNodeInSequence mapNodeInSequence
		{
			get
			{
				return mapNodeInSequence.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06007BAD RID: 31661 RVA: 0x001A39D0 File Offset: 0x001A1BD0
		public selection selection
		{
			get
			{
				return selection.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007BAE RID: 31662 RVA: 0x001A39E4 File Offset: 0x001A1BE4
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007BAF RID: 31663 RVA: 0x001A39F8 File Offset: 0x001A1BF8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007BB0 RID: 31664 RVA: 0x001A3A22 File Offset: 0x001A1C22
		public bool Equals(MapToWebRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003364 RID: 13156
		private ProgramNode _node;
	}
}
