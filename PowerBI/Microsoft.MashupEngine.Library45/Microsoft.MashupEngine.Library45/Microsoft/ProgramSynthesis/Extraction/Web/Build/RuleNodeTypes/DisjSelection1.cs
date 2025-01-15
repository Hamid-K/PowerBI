using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001014 RID: 4116
	public struct DisjSelection1 : IProgramNodeBuilder, IEquatable<DisjSelection1>
	{
		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06007959 RID: 31065 RVA: 0x001A0472 File Offset: 0x0019E672
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600795A RID: 31066 RVA: 0x001A047A File Offset: 0x0019E67A
		private DisjSelection1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600795B RID: 31067 RVA: 0x001A0483 File Offset: 0x0019E683
		public static DisjSelection1 CreateUnsafe(ProgramNode node)
		{
			return new DisjSelection1(node);
		}

		// Token: 0x0600795C RID: 31068 RVA: 0x001A048C File Offset: 0x0019E68C
		public static DisjSelection1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DisjSelection1)
			{
				return null;
			}
			return new DisjSelection1?(DisjSelection1.CreateUnsafe(node));
		}

		// Token: 0x0600795D RID: 31069 RVA: 0x001A04C1 File Offset: 0x0019E6C1
		public DisjSelection1(GrammarBuilders g, selection value0, filterSelection value1)
		{
			this._node = g.Rule.DisjSelection1.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600795E RID: 31070 RVA: 0x001A04E7 File Offset: 0x0019E6E7
		public static implicit operator selection(DisjSelection1 arg)
		{
			return selection.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x0600795F RID: 31071 RVA: 0x001A04F5 File Offset: 0x0019E6F5
		public selection selection
		{
			get
			{
				return selection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06007960 RID: 31072 RVA: 0x001A0509 File Offset: 0x0019E709
		public filterSelection filterSelection
		{
			get
			{
				return filterSelection.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007961 RID: 31073 RVA: 0x001A051D File Offset: 0x0019E71D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007962 RID: 31074 RVA: 0x001A0530 File Offset: 0x0019E730
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007963 RID: 31075 RVA: 0x001A055A File Offset: 0x0019E75A
		public bool Equals(DisjSelection1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400332D RID: 13101
		private ProgramNode _node;
	}
}
