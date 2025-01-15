using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200095D RID: 2397
	public struct topSplit : IProgramNodeBuilder, IEquatable<topSplit>
	{
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x000AF17A File Offset: 0x000AD37A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000AF182 File Offset: 0x000AD382
		private topSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x000AF18B File Offset: 0x000AD38B
		public static topSplit CreateUnsafe(ProgramNode node)
		{
			return new topSplit(node);
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000AF194 File Offset: 0x000AD394
		public static topSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.topSplit)
			{
				return null;
			}
			return new topSplit?(topSplit.CreateUnsafe(node));
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x000AF1CE File Offset: 0x000AD3CE
		public static topSplit CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new topSplit(new Hole(g.Symbol.topSplit, holeId));
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x000AF1E6 File Offset: 0x000AD3E6
		public LetFileRecordSplit Cast_LetFileRecordSplit()
		{
			return LetFileRecordSplit.CreateUnsafe(this.Node);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetFileRecordSplit(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000AF1F3 File Offset: 0x000AD3F3
		public bool Is_LetFileRecordSplit(GrammarBuilders g, out LetFileRecordSplit value)
		{
			value = LetFileRecordSplit.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000AF207 File Offset: 0x000AD407
		public LetFileRecordSplit? As_LetFileRecordSplit(GrammarBuilders g)
		{
			return new LetFileRecordSplit?(LetFileRecordSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000AF219 File Offset: 0x000AD419
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000AF22C File Offset: 0x000AD42C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000AF256 File Offset: 0x000AD456
		public bool Equals(topSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A7D RID: 6781
		private ProgramNode _node;
	}
}
