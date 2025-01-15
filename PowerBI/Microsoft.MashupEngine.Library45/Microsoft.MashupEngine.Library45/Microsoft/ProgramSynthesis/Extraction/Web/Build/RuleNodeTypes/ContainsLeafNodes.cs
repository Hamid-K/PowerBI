using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200102C RID: 4140
	public struct ContainsLeafNodes : IProgramNodeBuilder, IEquatable<ContainsLeafNodes>
	{
		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06007A56 RID: 31318 RVA: 0x001A1B0A File Offset: 0x0019FD0A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A57 RID: 31319 RVA: 0x001A1B12 File Offset: 0x0019FD12
		private ContainsLeafNodes(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A58 RID: 31320 RVA: 0x001A1B1B File Offset: 0x0019FD1B
		public static ContainsLeafNodes CreateUnsafe(ProgramNode node)
		{
			return new ContainsLeafNodes(node);
		}

		// Token: 0x06007A59 RID: 31321 RVA: 0x001A1B24 File Offset: 0x0019FD24
		public static ContainsLeafNodes? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ContainsLeafNodes)
			{
				return null;
			}
			return new ContainsLeafNodes?(ContainsLeafNodes.CreateUnsafe(node));
		}

		// Token: 0x06007A5A RID: 31322 RVA: 0x001A1B59 File Offset: 0x0019FD59
		public ContainsLeafNodes(GrammarBuilders g, names value0, node value1)
		{
			this._node = g.Rule.ContainsLeafNodes.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007A5B RID: 31323 RVA: 0x001A1B7F File Offset: 0x0019FD7F
		public static implicit operator atomExpr(ContainsLeafNodes arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06007A5C RID: 31324 RVA: 0x001A1B8D File Offset: 0x0019FD8D
		public names names
		{
			get
			{
				return names.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x06007A5D RID: 31325 RVA: 0x001A1BA1 File Offset: 0x0019FDA1
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007A5E RID: 31326 RVA: 0x001A1BB5 File Offset: 0x0019FDB5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A5F RID: 31327 RVA: 0x001A1BC8 File Offset: 0x0019FDC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A60 RID: 31328 RVA: 0x001A1BF2 File Offset: 0x0019FDF2
		public bool Equals(ContainsLeafNodes other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003345 RID: 13125
		private ProgramNode _node;
	}
}
