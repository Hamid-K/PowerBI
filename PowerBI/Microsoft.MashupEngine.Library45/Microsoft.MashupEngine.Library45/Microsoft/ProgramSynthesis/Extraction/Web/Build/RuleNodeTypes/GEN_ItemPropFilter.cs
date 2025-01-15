using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200104A RID: 4170
	public struct GEN_ItemPropFilter : IProgramNodeBuilder, IEquatable<GEN_ItemPropFilter>
	{
		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06007B9B RID: 31643 RVA: 0x001A382A File Offset: 0x001A1A2A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B9C RID: 31644 RVA: 0x001A3832 File Offset: 0x001A1A32
		private GEN_ItemPropFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B9D RID: 31645 RVA: 0x001A383B File Offset: 0x001A1A3B
		public static GEN_ItemPropFilter CreateUnsafe(ProgramNode node)
		{
			return new GEN_ItemPropFilter(node);
		}

		// Token: 0x06007B9E RID: 31646 RVA: 0x001A3844 File Offset: 0x001A1A44
		public static GEN_ItemPropFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.GEN_ItemPropFilter)
			{
				return null;
			}
			return new GEN_ItemPropFilter?(GEN_ItemPropFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B9F RID: 31647 RVA: 0x001A3879 File Offset: 0x001A1A79
		public GEN_ItemPropFilter(GrammarBuilders g, obj value0, obj value1)
		{
			this._node = g.Rule.GEN_ItemPropFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007BA0 RID: 31648 RVA: 0x001A389F File Offset: 0x001A1A9F
		public static implicit operator gen_ItemProp(GEN_ItemPropFilter arg)
		{
			return gen_ItemProp.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06007BA1 RID: 31649 RVA: 0x001A38AD File Offset: 0x001A1AAD
		public obj obj1
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06007BA2 RID: 31650 RVA: 0x001A38C1 File Offset: 0x001A1AC1
		public obj obj2
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x001A38D5 File Offset: 0x001A1AD5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007BA4 RID: 31652 RVA: 0x001A38E8 File Offset: 0x001A1AE8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007BA5 RID: 31653 RVA: 0x001A3912 File Offset: 0x001A1B12
		public bool Equals(GEN_ItemPropFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003363 RID: 13155
		private ProgramNode _node;
	}
}
