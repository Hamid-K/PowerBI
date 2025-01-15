using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001005 RID: 4101
	public struct selection8_allNodes : IProgramNodeBuilder, IEquatable<selection8_allNodes>
	{
		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x060078C1 RID: 30913 RVA: 0x0019F6E6 File Offset: 0x0019D8E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060078C2 RID: 30914 RVA: 0x0019F6EE File Offset: 0x0019D8EE
		private selection8_allNodes(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060078C3 RID: 30915 RVA: 0x0019F6F7 File Offset: 0x0019D8F7
		public static selection8_allNodes CreateUnsafe(ProgramNode node)
		{
			return new selection8_allNodes(node);
		}

		// Token: 0x060078C4 RID: 30916 RVA: 0x0019F700 File Offset: 0x0019D900
		public static selection8_allNodes? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.selection8_allNodes)
			{
				return null;
			}
			return new selection8_allNodes?(selection8_allNodes.CreateUnsafe(node));
		}

		// Token: 0x060078C5 RID: 30917 RVA: 0x0019F735 File Offset: 0x0019D935
		public selection8_allNodes(GrammarBuilders g, allNodes value0)
		{
			this._node = g.UnnamedConversion.selection8_allNodes.BuildASTNode(value0.Node);
		}

		// Token: 0x060078C6 RID: 30918 RVA: 0x0019F754 File Offset: 0x0019D954
		public static implicit operator selection8(selection8_allNodes arg)
		{
			return selection8.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x060078C7 RID: 30919 RVA: 0x0019F762 File Offset: 0x0019D962
		public allNodes allNodes
		{
			get
			{
				return allNodes.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060078C8 RID: 30920 RVA: 0x0019F776 File Offset: 0x0019D976
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060078C9 RID: 30921 RVA: 0x0019F78C File Offset: 0x0019D98C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060078CA RID: 30922 RVA: 0x0019F7B6 File Offset: 0x0019D9B6
		public bool Equals(selection8_allNodes other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400331E RID: 13086
		private ProgramNode _node;
	}
}
