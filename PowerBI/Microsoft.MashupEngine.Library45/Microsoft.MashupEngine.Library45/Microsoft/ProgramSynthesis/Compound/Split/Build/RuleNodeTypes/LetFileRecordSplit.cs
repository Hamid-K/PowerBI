using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000959 RID: 2393
	public struct LetFileRecordSplit : IProgramNodeBuilder, IEquatable<LetFileRecordSplit>
	{
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000AED8A File Offset: 0x000ACF8A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000AED92 File Offset: 0x000ACF92
		private LetFileRecordSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000AED9B File Offset: 0x000ACF9B
		public static LetFileRecordSplit CreateUnsafe(ProgramNode node)
		{
			return new LetFileRecordSplit(node);
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000AEDA4 File Offset: 0x000ACFA4
		public static LetFileRecordSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetFileRecordSplit)
			{
				return null;
			}
			return new LetFileRecordSplit?(LetFileRecordSplit.CreateUnsafe(node));
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000AEDD9 File Offset: 0x000ACFD9
		public LetFileRecordSplit(GrammarBuilders g, splitFile value0, splitRecordsSelect value1)
		{
			this._node = new LetNode(g.Rule.LetFileRecordSplit, value0.Node, value1.Node);
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000AEDFF File Offset: 0x000ACFFF
		public static implicit operator topSplit(LetFileRecordSplit arg)
		{
			return topSplit.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600381E RID: 14366 RVA: 0x000AEE0D File Offset: 0x000AD00D
		public splitFile splitFile
		{
			get
			{
				return splitFile.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x000AEE21 File Offset: 0x000AD021
		public splitRecordsSelect splitRecordsSelect
		{
			get
			{
				return splitRecordsSelect.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000AEE35 File Offset: 0x000AD035
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000AEE48 File Offset: 0x000AD048
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000AEE72 File Offset: 0x000AD072
		public bool Equals(LetFileRecordSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A79 RID: 6777
		private ProgramNode _node;
	}
}
