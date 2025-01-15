using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200105E RID: 4190
	public struct subNode : IProgramNodeBuilder, IEquatable<subNode>
	{
		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06007C91 RID: 31889 RVA: 0x001A543E File Offset: 0x001A363E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C92 RID: 31890 RVA: 0x001A5446 File Offset: 0x001A3646
		private subNode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C93 RID: 31891 RVA: 0x001A544F File Offset: 0x001A364F
		public static subNode CreateUnsafe(ProgramNode node)
		{
			return new subNode(node);
		}

		// Token: 0x06007C94 RID: 31892 RVA: 0x001A5458 File Offset: 0x001A3658
		public static subNode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.subNode)
			{
				return null;
			}
			return new subNode?(subNode.CreateUnsafe(node));
		}

		// Token: 0x06007C95 RID: 31893 RVA: 0x001A5492 File Offset: 0x001A3692
		public static subNode CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new subNode(new Hole(g.Symbol.subNode, holeId));
		}

		// Token: 0x06007C96 RID: 31894 RVA: 0x001A54AA File Offset: 0x001A36AA
		public NodeToWebRegion Cast_NodeToWebRegion()
		{
			return NodeToWebRegion.CreateUnsafe(this.Node);
		}

		// Token: 0x06007C97 RID: 31895 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_NodeToWebRegion(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007C98 RID: 31896 RVA: 0x001A54B7 File Offset: 0x001A36B7
		public bool Is_NodeToWebRegion(GrammarBuilders g, out NodeToWebRegion value)
		{
			value = NodeToWebRegion.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007C99 RID: 31897 RVA: 0x001A54CB File Offset: 0x001A36CB
		public NodeToWebRegion? As_NodeToWebRegion(GrammarBuilders g)
		{
			return new NodeToWebRegion?(NodeToWebRegion.CreateUnsafe(this.Node));
		}

		// Token: 0x06007C9A RID: 31898 RVA: 0x001A54DD File Offset: 0x001A36DD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C9B RID: 31899 RVA: 0x001A54F0 File Offset: 0x001A36F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C9C RID: 31900 RVA: 0x001A551A File Offset: 0x001A371A
		public bool Equals(subNode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003377 RID: 13175
		private ProgramNode _node;
	}
}
