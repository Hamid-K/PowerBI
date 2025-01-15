using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E61 RID: 3681
	public struct mProgram : IProgramNodeBuilder, IEquatable<mProgram>
	{
		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x060063A9 RID: 25513 RVA: 0x00143DE6 File Offset: 0x00141FE6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060063AA RID: 25514 RVA: 0x00143DEE File Offset: 0x00141FEE
		private mProgram(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060063AB RID: 25515 RVA: 0x00143DF7 File Offset: 0x00141FF7
		public static mProgram CreateUnsafe(ProgramNode node)
		{
			return new mProgram(node);
		}

		// Token: 0x060063AC RID: 25516 RVA: 0x00143E00 File Offset: 0x00142000
		public static mProgram? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.mProgram)
			{
				return null;
			}
			return new mProgram?(mProgram.CreateUnsafe(node));
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x00143E3A File Offset: 0x0014203A
		public static mProgram CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new mProgram(new Hole(g.Symbol.mProgram, holeId));
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x00143E52 File Offset: 0x00142052
		public bool Is_mProgram_mTable(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.mProgram_mTable;
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x00143E6C File Offset: 0x0014206C
		public bool Is_mProgram_mTable(GrammarBuilders g, out mProgram_mTable value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.mProgram_mTable)
			{
				value = mProgram_mTable.CreateUnsafe(this.Node);
				return true;
			}
			value = default(mProgram_mTable);
			return false;
		}

		// Token: 0x060063B0 RID: 25520 RVA: 0x00143EA4 File Offset: 0x001420A4
		public mProgram_mTable? As_mProgram_mTable(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.mProgram_mTable)
			{
				return null;
			}
			return new mProgram_mTable?(mProgram_mTable.CreateUnsafe(this.Node));
		}

		// Token: 0x060063B1 RID: 25521 RVA: 0x00143EE4 File Offset: 0x001420E4
		public mProgram_mTable Cast_mProgram_mTable(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.mProgram_mTable)
			{
				return mProgram_mTable.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_mProgram_mTable is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063B2 RID: 25522 RVA: 0x00143F39 File Offset: 0x00142139
		public bool Is_RemoveEmptyRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RemoveEmptyRows;
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x00143F53 File Offset: 0x00142153
		public bool Is_RemoveEmptyRows(GrammarBuilders g, out RemoveEmptyRows value)
		{
			if (this.Node.GrammarRule == g.Rule.RemoveEmptyRows)
			{
				value = RemoveEmptyRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RemoveEmptyRows);
			return false;
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x00143F88 File Offset: 0x00142188
		public RemoveEmptyRows? As_RemoveEmptyRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RemoveEmptyRows)
			{
				return null;
			}
			return new RemoveEmptyRows?(RemoveEmptyRows.CreateUnsafe(this.Node));
		}

		// Token: 0x060063B5 RID: 25525 RVA: 0x00143FC8 File Offset: 0x001421C8
		public RemoveEmptyRows Cast_RemoveEmptyRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RemoveEmptyRows)
			{
				return RemoveEmptyRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RemoveEmptyRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x0014401D File Offset: 0x0014221D
		public bool Is_RemoveEmptyColumns(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RemoveEmptyColumns;
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x00144037 File Offset: 0x00142237
		public bool Is_RemoveEmptyColumns(GrammarBuilders g, out RemoveEmptyColumns value)
		{
			if (this.Node.GrammarRule == g.Rule.RemoveEmptyColumns)
			{
				value = RemoveEmptyColumns.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RemoveEmptyColumns);
			return false;
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x0014406C File Offset: 0x0014226C
		public RemoveEmptyColumns? As_RemoveEmptyColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RemoveEmptyColumns)
			{
				return null;
			}
			return new RemoveEmptyColumns?(RemoveEmptyColumns.CreateUnsafe(this.Node));
		}

		// Token: 0x060063B9 RID: 25529 RVA: 0x001440AC File Offset: 0x001422AC
		public RemoveEmptyColumns Cast_RemoveEmptyColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RemoveEmptyColumns)
			{
				return RemoveEmptyColumns.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RemoveEmptyColumns is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063BA RID: 25530 RVA: 0x00144104 File Offset: 0x00142304
		public T Switch<T>(GrammarBuilders g, Func<mProgram_mTable, T> func0, Func<RemoveEmptyRows, T> func1, Func<RemoveEmptyColumns, T> func2)
		{
			mProgram_mTable mProgram_mTable;
			if (this.Is_mProgram_mTable(g, out mProgram_mTable))
			{
				return func0(mProgram_mTable);
			}
			RemoveEmptyRows removeEmptyRows;
			if (this.Is_RemoveEmptyRows(g, out removeEmptyRows))
			{
				return func1(removeEmptyRows);
			}
			RemoveEmptyColumns removeEmptyColumns;
			if (this.Is_RemoveEmptyColumns(g, out removeEmptyColumns))
			{
				return func2(removeEmptyColumns);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol mProgram");
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x00144170 File Offset: 0x00142370
		public void Switch(GrammarBuilders g, Action<mProgram_mTable> func0, Action<RemoveEmptyRows> func1, Action<RemoveEmptyColumns> func2)
		{
			mProgram_mTable mProgram_mTable;
			if (this.Is_mProgram_mTable(g, out mProgram_mTable))
			{
				func0(mProgram_mTable);
				return;
			}
			RemoveEmptyRows removeEmptyRows;
			if (this.Is_RemoveEmptyRows(g, out removeEmptyRows))
			{
				func1(removeEmptyRows);
				return;
			}
			RemoveEmptyColumns removeEmptyColumns;
			if (this.Is_RemoveEmptyColumns(g, out removeEmptyColumns))
			{
				func2(removeEmptyColumns);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol mProgram");
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x001441DB File Offset: 0x001423DB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x001441F0 File Offset: 0x001423F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x0014421A File Offset: 0x0014241A
		public bool Equals(mProgram other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C0B RID: 11275
		private ProgramNode _node;
	}
}
