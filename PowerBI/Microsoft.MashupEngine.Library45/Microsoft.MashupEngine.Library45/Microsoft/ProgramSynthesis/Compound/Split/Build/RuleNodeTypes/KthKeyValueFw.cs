using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000946 RID: 2374
	public struct KthKeyValueFw : IProgramNodeBuilder, IEquatable<KthKeyValueFw>
	{
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x000AD9A6 File Offset: 0x000ABBA6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000AD9AE File Offset: 0x000ABBAE
		private KthKeyValueFw(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000AD9B7 File Offset: 0x000ABBB7
		public static KthKeyValueFw CreateUnsafe(ProgramNode node)
		{
			return new KthKeyValueFw(node);
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000AD9C0 File Offset: 0x000ABBC0
		public static KthKeyValueFw? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthKeyValueFw)
			{
				return null;
			}
			return new KthKeyValueFw?(KthKeyValueFw.CreateUnsafe(node));
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000AD9F8 File Offset: 0x000ABBF8
		public KthKeyValueFw(GrammarBuilders g, key value0, fwPos value1, k value2, newLineSep value3, rowRecord value4)
		{
			this._node = g.Rule.KthKeyValueFw.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node, value4.Node });
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000ADA53 File Offset: 0x000ABC53
		public static implicit operator columnSelector(KthKeyValueFw arg)
		{
			return columnSelector.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x000ADA61 File Offset: 0x000ABC61
		public key key
		{
			get
			{
				return key.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06003749 RID: 14153 RVA: 0x000ADA75 File Offset: 0x000ABC75
		public fwPos fwPos
		{
			get
			{
				return fwPos.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x000ADA89 File Offset: 0x000ABC89
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x000ADA9D File Offset: 0x000ABC9D
		public newLineSep newLineSep
		{
			get
			{
				return newLineSep.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000ADAB1 File Offset: 0x000ABCB1
		public rowRecord rowRecord
		{
			get
			{
				return rowRecord.CreateUnsafe(this.Node.Children[4]);
			}
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000ADAC5 File Offset: 0x000ABCC5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000ADAD8 File Offset: 0x000ABCD8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x000ADB02 File Offset: 0x000ABD02
		public bool Equals(KthKeyValueFw other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A66 RID: 6758
		private ProgramNode _node;
	}
}
