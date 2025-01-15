using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000942 RID: 2370
	public struct KthLine : IProgramNodeBuilder, IEquatable<KthLine>
	{
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06003710 RID: 14096 RVA: 0x000AD4AE File Offset: 0x000AB6AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000AD4B6 File Offset: 0x000AB6B6
		private KthLine(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000AD4BF File Offset: 0x000AB6BF
		public static KthLine CreateUnsafe(ProgramNode node)
		{
			return new KthLine(node);
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000AD4C8 File Offset: 0x000AB6C8
		public static KthLine? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthLine)
			{
				return null;
			}
			return new KthLine?(KthLine.CreateUnsafe(node));
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000AD4FD File Offset: 0x000AB6FD
		public KthLine(GrammarBuilders g, k value0, rowRecord value1)
		{
			this._node = g.Rule.KthLine.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000AD523 File Offset: 0x000AB723
		public static implicit operator columnSelector(KthLine arg)
		{
			return columnSelector.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003716 RID: 14102 RVA: 0x000AD531 File Offset: 0x000AB731
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06003717 RID: 14103 RVA: 0x000AD545 File Offset: 0x000AB745
		public rowRecord rowRecord
		{
			get
			{
				return rowRecord.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000AD559 File Offset: 0x000AB759
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000AD56C File Offset: 0x000AB76C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000AD596 File Offset: 0x000AB796
		public bool Equals(KthLine other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A62 RID: 6754
		private ProgramNode _node;
	}
}
