using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000945 RID: 2373
	public struct KthKeyQuote : IProgramNodeBuilder, IEquatable<KthKeyQuote>
	{
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x000AD852 File Offset: 0x000ABA52
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000AD85A File Offset: 0x000ABA5A
		private KthKeyQuote(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000AD863 File Offset: 0x000ABA63
		public static KthKeyQuote CreateUnsafe(ProgramNode node)
		{
			return new KthKeyQuote(node);
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000AD86C File Offset: 0x000ABA6C
		public static KthKeyQuote? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthKeyQuote)
			{
				return null;
			}
			return new KthKeyQuote?(KthKeyQuote.CreateUnsafe(node));
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000AD8A4 File Offset: 0x000ABAA4
		public KthKeyQuote(GrammarBuilders g, key value0, k value1, newLineSep value2, rowRecord value3)
		{
			this._node = g.Rule.KthKeyQuote.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node });
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000AD8F5 File Offset: 0x000ABAF5
		public static implicit operator columnSelector(KthKeyQuote arg)
		{
			return columnSelector.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000AD903 File Offset: 0x000ABB03
		public key key
		{
			get
			{
				return key.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600373C RID: 14140 RVA: 0x000AD917 File Offset: 0x000ABB17
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600373D RID: 14141 RVA: 0x000AD92B File Offset: 0x000ABB2B
		public newLineSep newLineSep
		{
			get
			{
				return newLineSep.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600373E RID: 14142 RVA: 0x000AD93F File Offset: 0x000ABB3F
		public rowRecord rowRecord
		{
			get
			{
				return rowRecord.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000AD953 File Offset: 0x000ABB53
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000AD968 File Offset: 0x000ABB68
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000AD992 File Offset: 0x000ABB92
		public bool Equals(KthKeyQuote other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A65 RID: 6757
		private ProgramNode _node;
	}
}
