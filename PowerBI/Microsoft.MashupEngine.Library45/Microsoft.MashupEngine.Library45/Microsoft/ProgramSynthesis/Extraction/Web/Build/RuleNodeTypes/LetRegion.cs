using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001059 RID: 4185
	public struct LetRegion : IProgramNodeBuilder, IEquatable<LetRegion>
	{
		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06007C3F RID: 31807 RVA: 0x001A47BA File Offset: 0x001A29BA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C40 RID: 31808 RVA: 0x001A47C2 File Offset: 0x001A29C2
		private LetRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C41 RID: 31809 RVA: 0x001A47CB File Offset: 0x001A29CB
		public static LetRegion CreateUnsafe(ProgramNode node)
		{
			return new LetRegion(node);
		}

		// Token: 0x06007C42 RID: 31810 RVA: 0x001A47D4 File Offset: 0x001A29D4
		public static LetRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetRegion)
			{
				return null;
			}
			return new LetRegion?(LetRegion.CreateUnsafe(node));
		}

		// Token: 0x06007C43 RID: 31811 RVA: 0x001A4809 File Offset: 0x001A2A09
		public LetRegion(GrammarBuilders g, beginNode value0, _LetB0 value1)
		{
			this._node = new LetNode(g.Rule.LetRegion, value0.Node, value1.Node);
		}

		// Token: 0x06007C44 RID: 31812 RVA: 0x001A482F File Offset: 0x001A2A2F
		public static implicit operator region(LetRegion arg)
		{
			return region.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06007C45 RID: 31813 RVA: 0x001A483D File Offset: 0x001A2A3D
		public beginNode beginNode
		{
			get
			{
				return beginNode.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06007C46 RID: 31814 RVA: 0x001A4851 File Offset: 0x001A2A51
		public _LetB0 _LetB0
		{
			get
			{
				return _LetB0.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007C47 RID: 31815 RVA: 0x001A4865 File Offset: 0x001A2A65
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C48 RID: 31816 RVA: 0x001A4878 File Offset: 0x001A2A78
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C49 RID: 31817 RVA: 0x001A48A2 File Offset: 0x001A2AA2
		public bool Equals(LetRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003372 RID: 13170
		private ProgramNode _node;
	}
}
