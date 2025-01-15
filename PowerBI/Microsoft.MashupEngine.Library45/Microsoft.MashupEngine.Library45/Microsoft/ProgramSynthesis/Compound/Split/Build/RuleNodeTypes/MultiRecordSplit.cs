using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200093F RID: 2367
	public struct MultiRecordSplit : IProgramNodeBuilder, IEquatable<MultiRecordSplit>
	{
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000AD202 File Offset: 0x000AB402
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000AD20A File Offset: 0x000AB40A
		private MultiRecordSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000AD213 File Offset: 0x000AB413
		public static MultiRecordSplit CreateUnsafe(ProgramNode node)
		{
			return new MultiRecordSplit(node);
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000AD21C File Offset: 0x000AB41C
		public static MultiRecordSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MultiRecordSplit)
			{
				return null;
			}
			return new MultiRecordSplit?(MultiRecordSplit.CreateUnsafe(node));
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000AD251 File Offset: 0x000AB451
		public MultiRecordSplit(GrammarBuilders g, multiRecordSplit value0)
		{
			this._node = g.Rule.MultiRecordSplit.BuildASTNode(value0.Node);
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000AD270 File Offset: 0x000AB470
		public static implicit operator splitRecords(MultiRecordSplit arg)
		{
			return splitRecords.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000AD27E File Offset: 0x000AB47E
		public multiRecordSplit multiRecordSplit
		{
			get
			{
				return multiRecordSplit.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000AD292 File Offset: 0x000AB492
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000AD2A8 File Offset: 0x000AB4A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x000AD2D2 File Offset: 0x000AB4D2
		public bool Equals(MultiRecordSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A5F RID: 6751
		private ProgramNode _node;
	}
}
