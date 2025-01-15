using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000960 RID: 2400
	public struct multiRecordSplit : IProgramNodeBuilder, IEquatable<multiRecordSplit>
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x000AF9EE File Offset: 0x000ADBEE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x000AF9F6 File Offset: 0x000ADBF6
		private multiRecordSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000AF9FF File Offset: 0x000ADBFF
		public static multiRecordSplit CreateUnsafe(ProgramNode node)
		{
			return new multiRecordSplit(node);
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000AFA08 File Offset: 0x000ADC08
		public static multiRecordSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.multiRecordSplit)
			{
				return null;
			}
			return new multiRecordSplit?(multiRecordSplit.CreateUnsafe(node));
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000AFA42 File Offset: 0x000ADC42
		public static multiRecordSplit CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new multiRecordSplit(new Hole(g.Symbol.multiRecordSplit, holeId));
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000AFA5A File Offset: 0x000ADC5A
		public LetMultiRecordSplit Cast_LetMultiRecordSplit()
		{
			return LetMultiRecordSplit.CreateUnsafe(this.Node);
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetMultiRecordSplit(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x000AFA67 File Offset: 0x000ADC67
		public bool Is_LetMultiRecordSplit(GrammarBuilders g, out LetMultiRecordSplit value)
		{
			value = LetMultiRecordSplit.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x000AFA7B File Offset: 0x000ADC7B
		public LetMultiRecordSplit? As_LetMultiRecordSplit(GrammarBuilders g)
		{
			return new LetMultiRecordSplit?(LetMultiRecordSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000AFA8D File Offset: 0x000ADC8D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000AFAA0 File Offset: 0x000ADCA0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000AFACA File Offset: 0x000ADCCA
		public bool Equals(multiRecordSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A80 RID: 6784
		private ProgramNode _node;
	}
}
