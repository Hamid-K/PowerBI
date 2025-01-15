using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001006 RID: 4102
	public struct selection10_allNodes : IProgramNodeBuilder, IEquatable<selection10_allNodes>
	{
		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x060078CB RID: 30923 RVA: 0x0019F7CA File Offset: 0x0019D9CA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060078CC RID: 30924 RVA: 0x0019F7D2 File Offset: 0x0019D9D2
		private selection10_allNodes(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060078CD RID: 30925 RVA: 0x0019F7DB File Offset: 0x0019D9DB
		public static selection10_allNodes CreateUnsafe(ProgramNode node)
		{
			return new selection10_allNodes(node);
		}

		// Token: 0x060078CE RID: 30926 RVA: 0x0019F7E4 File Offset: 0x0019D9E4
		public static selection10_allNodes? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.selection10_allNodes)
			{
				return null;
			}
			return new selection10_allNodes?(selection10_allNodes.CreateUnsafe(node));
		}

		// Token: 0x060078CF RID: 30927 RVA: 0x0019F819 File Offset: 0x0019DA19
		public selection10_allNodes(GrammarBuilders g, allNodes value0)
		{
			this._node = g.UnnamedConversion.selection10_allNodes.BuildASTNode(value0.Node);
		}

		// Token: 0x060078D0 RID: 30928 RVA: 0x0019F838 File Offset: 0x0019DA38
		public static implicit operator selection10(selection10_allNodes arg)
		{
			return selection10.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x060078D1 RID: 30929 RVA: 0x0019F846 File Offset: 0x0019DA46
		public allNodes allNodes
		{
			get
			{
				return allNodes.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060078D2 RID: 30930 RVA: 0x0019F85A File Offset: 0x0019DA5A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060078D3 RID: 30931 RVA: 0x0019F870 File Offset: 0x0019DA70
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060078D4 RID: 30932 RVA: 0x0019F89A File Offset: 0x0019DA9A
		public bool Equals(selection10_allNodes other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400331F RID: 13087
		private ProgramNode _node;
	}
}
