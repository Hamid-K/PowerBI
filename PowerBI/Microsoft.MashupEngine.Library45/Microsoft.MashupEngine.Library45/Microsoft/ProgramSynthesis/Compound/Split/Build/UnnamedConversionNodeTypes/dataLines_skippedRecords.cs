using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000938 RID: 2360
	public struct dataLines_skippedRecords : IProgramNodeBuilder, IEquatable<dataLines_skippedRecords>
	{
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x000ACB7E File Offset: 0x000AAD7E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000ACB86 File Offset: 0x000AAD86
		private dataLines_skippedRecords(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000ACB8F File Offset: 0x000AAD8F
		public static dataLines_skippedRecords CreateUnsafe(ProgramNode node)
		{
			return new dataLines_skippedRecords(node);
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000ACB98 File Offset: 0x000AAD98
		public static dataLines_skippedRecords? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.dataLines_skippedRecords)
			{
				return null;
			}
			return new dataLines_skippedRecords?(dataLines_skippedRecords.CreateUnsafe(node));
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000ACBCD File Offset: 0x000AADCD
		public dataLines_skippedRecords(GrammarBuilders g, skippedRecords value0)
		{
			this._node = g.UnnamedConversion.dataLines_skippedRecords.BuildASTNode(value0.Node);
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000ACBEC File Offset: 0x000AADEC
		public static implicit operator dataLines(dataLines_skippedRecords arg)
		{
			return dataLines.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060036AF RID: 13999 RVA: 0x000ACBFA File Offset: 0x000AADFA
		public skippedRecords skippedRecords
		{
			get
			{
				return skippedRecords.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000ACC0E File Offset: 0x000AAE0E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000ACC24 File Offset: 0x000AAE24
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000ACC4E File Offset: 0x000AAE4E
		public bool Equals(dataLines_skippedRecords other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A58 RID: 6744
		private ProgramNode _node;
	}
}
