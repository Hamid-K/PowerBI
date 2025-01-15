using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000944 RID: 2372
	public struct KthTwoLineKeyValue : IProgramNodeBuilder, IEquatable<KthTwoLineKeyValue>
	{
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x000AD6FE File Offset: 0x000AB8FE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000AD706 File Offset: 0x000AB906
		private KthTwoLineKeyValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000AD70F File Offset: 0x000AB90F
		public static KthTwoLineKeyValue CreateUnsafe(ProgramNode node)
		{
			return new KthTwoLineKeyValue(node);
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000AD718 File Offset: 0x000AB918
		public static KthTwoLineKeyValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthTwoLineKeyValue)
			{
				return null;
			}
			return new KthTwoLineKeyValue?(KthTwoLineKeyValue.CreateUnsafe(node));
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000AD750 File Offset: 0x000AB950
		public KthTwoLineKeyValue(GrammarBuilders g, key value0, sep value1, k value2, rowRecord value3)
		{
			this._node = g.Rule.KthTwoLineKeyValue.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node });
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000AD7A1 File Offset: 0x000AB9A1
		public static implicit operator columnSelector(KthTwoLineKeyValue arg)
		{
			return columnSelector.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x000AD7AF File Offset: 0x000AB9AF
		public key key
		{
			get
			{
				return key.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x000AD7C3 File Offset: 0x000AB9C3
		public sep sep
		{
			get
			{
				return sep.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x000AD7D7 File Offset: 0x000AB9D7
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x000AD7EB File Offset: 0x000AB9EB
		public rowRecord rowRecord
		{
			get
			{
				return rowRecord.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000AD7FF File Offset: 0x000AB9FF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000AD814 File Offset: 0x000ABA14
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000AD83E File Offset: 0x000ABA3E
		public bool Equals(KthTwoLineKeyValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A64 RID: 6756
		private ProgramNode _node;
	}
}
