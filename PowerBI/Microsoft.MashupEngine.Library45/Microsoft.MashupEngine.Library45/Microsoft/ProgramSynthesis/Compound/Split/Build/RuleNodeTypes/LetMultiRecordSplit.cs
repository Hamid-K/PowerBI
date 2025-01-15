using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200095A RID: 2394
	public struct LetMultiRecordSplit : IProgramNodeBuilder, IEquatable<LetMultiRecordSplit>
	{
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x000AEE86 File Offset: 0x000AD086
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000AEE8E File Offset: 0x000AD08E
		private LetMultiRecordSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000AEE97 File Offset: 0x000AD097
		public static LetMultiRecordSplit CreateUnsafe(ProgramNode node)
		{
			return new LetMultiRecordSplit(node);
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000AEEA0 File Offset: 0x000AD0A0
		public static LetMultiRecordSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetMultiRecordSplit)
			{
				return null;
			}
			return new LetMultiRecordSplit?(LetMultiRecordSplit.CreateUnsafe(node));
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000AEED5 File Offset: 0x000AD0D5
		public LetMultiRecordSplit(GrammarBuilders g, primarySelector value0, mapColumnSelectors value1)
		{
			this._node = new LetNode(g.Rule.LetMultiRecordSplit, value0.Node, value1.Node);
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000AEEFB File Offset: 0x000AD0FB
		public static implicit operator multiRecordSplit(LetMultiRecordSplit arg)
		{
			return multiRecordSplit.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06003829 RID: 14377 RVA: 0x000AEF09 File Offset: 0x000AD109
		public primarySelector primarySelector
		{
			get
			{
				return primarySelector.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600382A RID: 14378 RVA: 0x000AEF1D File Offset: 0x000AD11D
		public mapColumnSelectors mapColumnSelectors
		{
			get
			{
				return mapColumnSelectors.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000AEF31 File Offset: 0x000AD131
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000AEF44 File Offset: 0x000AD144
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000AEF6E File Offset: 0x000AD16E
		public bool Equals(LetMultiRecordSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A7A RID: 6778
		private ProgramNode _node;
	}
}
