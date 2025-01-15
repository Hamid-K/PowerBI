using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200093A RID: 2362
	public struct skippedFooter_allRecords : IProgramNodeBuilder, IEquatable<skippedFooter_allRecords>
	{
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060036BD RID: 14013 RVA: 0x000ACD46 File Offset: 0x000AAF46
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000ACD4E File Offset: 0x000AAF4E
		private skippedFooter_allRecords(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x000ACD57 File Offset: 0x000AAF57
		public static skippedFooter_allRecords CreateUnsafe(ProgramNode node)
		{
			return new skippedFooter_allRecords(node);
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000ACD60 File Offset: 0x000AAF60
		public static skippedFooter_allRecords? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.skippedFooter_allRecords)
			{
				return null;
			}
			return new skippedFooter_allRecords?(skippedFooter_allRecords.CreateUnsafe(node));
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000ACD95 File Offset: 0x000AAF95
		public skippedFooter_allRecords(GrammarBuilders g, allRecords value0)
		{
			this._node = g.UnnamedConversion.skippedFooter_allRecords.BuildASTNode(value0.Node);
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000ACDB4 File Offset: 0x000AAFB4
		public static implicit operator skippedFooter(skippedFooter_allRecords arg)
		{
			return skippedFooter.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000ACDC2 File Offset: 0x000AAFC2
		public allRecords allRecords
		{
			get
			{
				return allRecords.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000ACDD6 File Offset: 0x000AAFD6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000ACDEC File Offset: 0x000AAFEC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000ACE16 File Offset: 0x000AB016
		public bool Equals(skippedFooter_allRecords other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A5A RID: 6746
		private ProgramNode _node;
	}
}
