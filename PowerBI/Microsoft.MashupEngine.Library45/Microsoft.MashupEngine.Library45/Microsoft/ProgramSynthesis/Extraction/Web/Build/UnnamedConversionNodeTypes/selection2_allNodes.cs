using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001002 RID: 4098
	public struct selection2_allNodes : IProgramNodeBuilder, IEquatable<selection2_allNodes>
	{
		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x060078A3 RID: 30883 RVA: 0x0019F43A File Offset: 0x0019D63A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060078A4 RID: 30884 RVA: 0x0019F442 File Offset: 0x0019D642
		private selection2_allNodes(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060078A5 RID: 30885 RVA: 0x0019F44B File Offset: 0x0019D64B
		public static selection2_allNodes CreateUnsafe(ProgramNode node)
		{
			return new selection2_allNodes(node);
		}

		// Token: 0x060078A6 RID: 30886 RVA: 0x0019F454 File Offset: 0x0019D654
		public static selection2_allNodes? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.selection2_allNodes)
			{
				return null;
			}
			return new selection2_allNodes?(selection2_allNodes.CreateUnsafe(node));
		}

		// Token: 0x060078A7 RID: 30887 RVA: 0x0019F489 File Offset: 0x0019D689
		public selection2_allNodes(GrammarBuilders g, allNodes value0)
		{
			this._node = g.UnnamedConversion.selection2_allNodes.BuildASTNode(value0.Node);
		}

		// Token: 0x060078A8 RID: 30888 RVA: 0x0019F4A8 File Offset: 0x0019D6A8
		public static implicit operator selection2(selection2_allNodes arg)
		{
			return selection2.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x060078A9 RID: 30889 RVA: 0x0019F4B6 File Offset: 0x0019D6B6
		public allNodes allNodes
		{
			get
			{
				return allNodes.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060078AA RID: 30890 RVA: 0x0019F4CA File Offset: 0x0019D6CA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060078AB RID: 30891 RVA: 0x0019F4E0 File Offset: 0x0019D6E0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060078AC RID: 30892 RVA: 0x0019F50A File Offset: 0x0019D70A
		public bool Equals(selection2_allNodes other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400331B RID: 13083
		private ProgramNode _node;
	}
}
