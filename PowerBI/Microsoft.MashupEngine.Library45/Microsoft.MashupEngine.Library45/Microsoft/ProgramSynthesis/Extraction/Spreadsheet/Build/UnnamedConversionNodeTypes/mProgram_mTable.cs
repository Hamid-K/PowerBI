using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E11 RID: 3601
	public struct mProgram_mTable : IProgramNodeBuilder, IEquatable<mProgram_mTable>
	{
		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06005FEA RID: 24554 RVA: 0x0013CF16 File Offset: 0x0013B116
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x0013CF1E File Offset: 0x0013B11E
		private mProgram_mTable(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x0013CF27 File Offset: 0x0013B127
		public static mProgram_mTable CreateUnsafe(ProgramNode node)
		{
			return new mProgram_mTable(node);
		}

		// Token: 0x06005FED RID: 24557 RVA: 0x0013CF30 File Offset: 0x0013B130
		public static mProgram_mTable? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.mProgram_mTable)
			{
				return null;
			}
			return new mProgram_mTable?(mProgram_mTable.CreateUnsafe(node));
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x0013CF65 File Offset: 0x0013B165
		public mProgram_mTable(GrammarBuilders g, mTable value0)
		{
			this._node = g.UnnamedConversion.mProgram_mTable.BuildASTNode(value0.Node);
		}

		// Token: 0x06005FEF RID: 24559 RVA: 0x0013CF84 File Offset: 0x0013B184
		public static implicit operator mProgram(mProgram_mTable arg)
		{
			return mProgram.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06005FF0 RID: 24560 RVA: 0x0013CF92 File Offset: 0x0013B192
		public mTable mTable
		{
			get
			{
				return mTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x0013CFA6 File Offset: 0x0013B1A6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x0013CFBC File Offset: 0x0013B1BC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x0013CFE6 File Offset: 0x0013B1E6
		public bool Equals(mProgram_mTable other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BBB RID: 11195
		private ProgramNode _node;
	}
}
