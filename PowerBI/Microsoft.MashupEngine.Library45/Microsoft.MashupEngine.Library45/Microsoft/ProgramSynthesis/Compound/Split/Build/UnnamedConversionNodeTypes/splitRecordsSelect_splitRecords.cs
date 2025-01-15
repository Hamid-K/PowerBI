using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000937 RID: 2359
	public struct splitRecordsSelect_splitRecords : IProgramNodeBuilder, IEquatable<splitRecordsSelect_splitRecords>
	{
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600369F RID: 13983 RVA: 0x000ACA9A File Offset: 0x000AAC9A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000ACAA2 File Offset: 0x000AACA2
		private splitRecordsSelect_splitRecords(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000ACAAB File Offset: 0x000AACAB
		public static splitRecordsSelect_splitRecords CreateUnsafe(ProgramNode node)
		{
			return new splitRecordsSelect_splitRecords(node);
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000ACAB4 File Offset: 0x000AACB4
		public static splitRecordsSelect_splitRecords? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.splitRecordsSelect_splitRecords)
			{
				return null;
			}
			return new splitRecordsSelect_splitRecords?(splitRecordsSelect_splitRecords.CreateUnsafe(node));
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000ACAE9 File Offset: 0x000AACE9
		public splitRecordsSelect_splitRecords(GrammarBuilders g, splitRecords value0)
		{
			this._node = g.UnnamedConversion.splitRecordsSelect_splitRecords.BuildASTNode(value0.Node);
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000ACB08 File Offset: 0x000AAD08
		public static implicit operator splitRecordsSelect(splitRecordsSelect_splitRecords arg)
		{
			return splitRecordsSelect.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060036A5 RID: 13989 RVA: 0x000ACB16 File Offset: 0x000AAD16
		public splitRecords splitRecords
		{
			get
			{
				return splitRecords.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000ACB2A File Offset: 0x000AAD2A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000ACB40 File Offset: 0x000AAD40
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000ACB6A File Offset: 0x000AAD6A
		public bool Equals(splitRecordsSelect_splitRecords other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A57 RID: 6743
		private ProgramNode _node;
	}
}
