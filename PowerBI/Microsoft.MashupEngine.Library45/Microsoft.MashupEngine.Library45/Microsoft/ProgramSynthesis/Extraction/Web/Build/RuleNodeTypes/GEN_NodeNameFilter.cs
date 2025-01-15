using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001049 RID: 4169
	public struct GEN_NodeNameFilter : IProgramNodeBuilder, IEquatable<GEN_NodeNameFilter>
	{
		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06007B90 RID: 31632 RVA: 0x001A372E File Offset: 0x001A192E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B91 RID: 31633 RVA: 0x001A3736 File Offset: 0x001A1936
		private GEN_NodeNameFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B92 RID: 31634 RVA: 0x001A373F File Offset: 0x001A193F
		public static GEN_NodeNameFilter CreateUnsafe(ProgramNode node)
		{
			return new GEN_NodeNameFilter(node);
		}

		// Token: 0x06007B93 RID: 31635 RVA: 0x001A3748 File Offset: 0x001A1948
		public static GEN_NodeNameFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.GEN_NodeNameFilter)
			{
				return null;
			}
			return new GEN_NodeNameFilter?(GEN_NodeNameFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B94 RID: 31636 RVA: 0x001A377D File Offset: 0x001A197D
		public GEN_NodeNameFilter(GrammarBuilders g, obj value0, obj value1)
		{
			this._node = g.Rule.GEN_NodeNameFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007B95 RID: 31637 RVA: 0x001A37A3 File Offset: 0x001A19A3
		public static implicit operator gen_NodeName(GEN_NodeNameFilter arg)
		{
			return gen_NodeName.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06007B96 RID: 31638 RVA: 0x001A37B1 File Offset: 0x001A19B1
		public obj obj1
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06007B97 RID: 31639 RVA: 0x001A37C5 File Offset: 0x001A19C5
		public obj obj2
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B98 RID: 31640 RVA: 0x001A37D9 File Offset: 0x001A19D9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B99 RID: 31641 RVA: 0x001A37EC File Offset: 0x001A19EC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B9A RID: 31642 RVA: 0x001A3816 File Offset: 0x001A1A16
		public bool Equals(GEN_NodeNameFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003362 RID: 13154
		private ProgramNode _node;
	}
}
