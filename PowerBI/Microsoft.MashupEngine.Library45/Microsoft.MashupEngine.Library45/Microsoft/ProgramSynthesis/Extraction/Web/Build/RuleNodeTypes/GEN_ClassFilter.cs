using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001047 RID: 4167
	public struct GEN_ClassFilter : IProgramNodeBuilder, IEquatable<GEN_ClassFilter>
	{
		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06007B7A RID: 31610 RVA: 0x001A3536 File Offset: 0x001A1736
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B7B RID: 31611 RVA: 0x001A353E File Offset: 0x001A173E
		private GEN_ClassFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B7C RID: 31612 RVA: 0x001A3547 File Offset: 0x001A1747
		public static GEN_ClassFilter CreateUnsafe(ProgramNode node)
		{
			return new GEN_ClassFilter(node);
		}

		// Token: 0x06007B7D RID: 31613 RVA: 0x001A3550 File Offset: 0x001A1750
		public static GEN_ClassFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.GEN_ClassFilter)
			{
				return null;
			}
			return new GEN_ClassFilter?(GEN_ClassFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B7E RID: 31614 RVA: 0x001A3585 File Offset: 0x001A1785
		public GEN_ClassFilter(GrammarBuilders g, obj value0, obj value1)
		{
			this._node = g.Rule.GEN_ClassFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007B7F RID: 31615 RVA: 0x001A35AB File Offset: 0x001A17AB
		public static implicit operator gen_Class(GEN_ClassFilter arg)
		{
			return gen_Class.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06007B80 RID: 31616 RVA: 0x001A35B9 File Offset: 0x001A17B9
		public obj obj1
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06007B81 RID: 31617 RVA: 0x001A35CD File Offset: 0x001A17CD
		public obj obj2
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B82 RID: 31618 RVA: 0x001A35E1 File Offset: 0x001A17E1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B83 RID: 31619 RVA: 0x001A35F4 File Offset: 0x001A17F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B84 RID: 31620 RVA: 0x001A361E File Offset: 0x001A181E
		public bool Equals(GEN_ClassFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003360 RID: 13152
		private ProgramNode _node;
	}
}
