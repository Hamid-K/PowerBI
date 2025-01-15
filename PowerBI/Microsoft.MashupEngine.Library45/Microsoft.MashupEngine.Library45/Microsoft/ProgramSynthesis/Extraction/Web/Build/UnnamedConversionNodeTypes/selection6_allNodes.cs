using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001004 RID: 4100
	public struct selection6_allNodes : IProgramNodeBuilder, IEquatable<selection6_allNodes>
	{
		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x060078B7 RID: 30903 RVA: 0x0019F602 File Offset: 0x0019D802
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060078B8 RID: 30904 RVA: 0x0019F60A File Offset: 0x0019D80A
		private selection6_allNodes(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060078B9 RID: 30905 RVA: 0x0019F613 File Offset: 0x0019D813
		public static selection6_allNodes CreateUnsafe(ProgramNode node)
		{
			return new selection6_allNodes(node);
		}

		// Token: 0x060078BA RID: 30906 RVA: 0x0019F61C File Offset: 0x0019D81C
		public static selection6_allNodes? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.selection6_allNodes)
			{
				return null;
			}
			return new selection6_allNodes?(selection6_allNodes.CreateUnsafe(node));
		}

		// Token: 0x060078BB RID: 30907 RVA: 0x0019F651 File Offset: 0x0019D851
		public selection6_allNodes(GrammarBuilders g, allNodes value0)
		{
			this._node = g.UnnamedConversion.selection6_allNodes.BuildASTNode(value0.Node);
		}

		// Token: 0x060078BC RID: 30908 RVA: 0x0019F670 File Offset: 0x0019D870
		public static implicit operator selection6(selection6_allNodes arg)
		{
			return selection6.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x060078BD RID: 30909 RVA: 0x0019F67E File Offset: 0x0019D87E
		public allNodes allNodes
		{
			get
			{
				return allNodes.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060078BE RID: 30910 RVA: 0x0019F692 File Offset: 0x0019D892
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060078BF RID: 30911 RVA: 0x0019F6A8 File Offset: 0x0019D8A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060078C0 RID: 30912 RVA: 0x0019F6D2 File Offset: 0x0019D8D2
		public bool Equals(selection6_allNodes other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400331D RID: 13085
		private ProgramNode _node;
	}
}
