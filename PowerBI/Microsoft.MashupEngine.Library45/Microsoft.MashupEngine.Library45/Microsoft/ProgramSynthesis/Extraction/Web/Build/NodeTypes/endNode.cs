using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001064 RID: 4196
	public struct endNode : IProgramNodeBuilder, IEquatable<endNode>
	{
		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06007CD9 RID: 31961 RVA: 0x001A59DE File Offset: 0x001A3BDE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007CDA RID: 31962 RVA: 0x001A59E6 File Offset: 0x001A3BE6
		private endNode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007CDB RID: 31963 RVA: 0x001A59EF File Offset: 0x001A3BEF
		public static endNode CreateUnsafe(ProgramNode node)
		{
			return new endNode(node);
		}

		// Token: 0x06007CDC RID: 31964 RVA: 0x001A59F8 File Offset: 0x001A3BF8
		public static endNode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.endNode)
			{
				return null;
			}
			return new endNode?(endNode.CreateUnsafe(node));
		}

		// Token: 0x06007CDD RID: 31965 RVA: 0x001A5A32 File Offset: 0x001A3C32
		public static endNode CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new endNode(new Hole(g.Symbol.endNode, holeId));
		}

		// Token: 0x06007CDE RID: 31966 RVA: 0x001A5A4A File Offset: 0x001A3C4A
		public KthNode Cast_KthNode()
		{
			return KthNode.CreateUnsafe(this.Node);
		}

		// Token: 0x06007CDF RID: 31967 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_KthNode(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007CE0 RID: 31968 RVA: 0x001A5A57 File Offset: 0x001A3C57
		public bool Is_KthNode(GrammarBuilders g, out KthNode value)
		{
			value = KthNode.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007CE1 RID: 31969 RVA: 0x001A5A6B File Offset: 0x001A3C6B
		public KthNode? As_KthNode(GrammarBuilders g)
		{
			return new KthNode?(KthNode.CreateUnsafe(this.Node));
		}

		// Token: 0x06007CE2 RID: 31970 RVA: 0x001A5A7D File Offset: 0x001A3C7D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007CE3 RID: 31971 RVA: 0x001A5A90 File Offset: 0x001A3C90
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007CE4 RID: 31972 RVA: 0x001A5ABA File Offset: 0x001A3CBA
		public bool Equals(endNode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400337D RID: 13181
		private ProgramNode _node;
	}
}
