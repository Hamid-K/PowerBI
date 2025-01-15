using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001003 RID: 4099
	public struct selection4_allNodes : IProgramNodeBuilder, IEquatable<selection4_allNodes>
	{
		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x060078AD RID: 30893 RVA: 0x0019F51E File Offset: 0x0019D71E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060078AE RID: 30894 RVA: 0x0019F526 File Offset: 0x0019D726
		private selection4_allNodes(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060078AF RID: 30895 RVA: 0x0019F52F File Offset: 0x0019D72F
		public static selection4_allNodes CreateUnsafe(ProgramNode node)
		{
			return new selection4_allNodes(node);
		}

		// Token: 0x060078B0 RID: 30896 RVA: 0x0019F538 File Offset: 0x0019D738
		public static selection4_allNodes? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.selection4_allNodes)
			{
				return null;
			}
			return new selection4_allNodes?(selection4_allNodes.CreateUnsafe(node));
		}

		// Token: 0x060078B1 RID: 30897 RVA: 0x0019F56D File Offset: 0x0019D76D
		public selection4_allNodes(GrammarBuilders g, allNodes value0)
		{
			this._node = g.UnnamedConversion.selection4_allNodes.BuildASTNode(value0.Node);
		}

		// Token: 0x060078B2 RID: 30898 RVA: 0x0019F58C File Offset: 0x0019D78C
		public static implicit operator selection4(selection4_allNodes arg)
		{
			return selection4.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x060078B3 RID: 30899 RVA: 0x0019F59A File Offset: 0x0019D79A
		public allNodes allNodes
		{
			get
			{
				return allNodes.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060078B4 RID: 30900 RVA: 0x0019F5AE File Offset: 0x0019D7AE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060078B5 RID: 30901 RVA: 0x0019F5C4 File Offset: 0x0019D7C4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060078B6 RID: 30902 RVA: 0x0019F5EE File Offset: 0x0019D7EE
		public bool Equals(selection4_allNodes other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400331C RID: 13084
		private ProgramNode _node;
	}
}
