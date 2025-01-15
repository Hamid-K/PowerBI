using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E57 RID: 3671
	public struct sheetSection : IProgramNodeBuilder, IEquatable<sheetSection>
	{
		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06006305 RID: 25349 RVA: 0x001424A6 File Offset: 0x001406A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006306 RID: 25350 RVA: 0x001424AE File Offset: 0x001406AE
		private sheetSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x001424B7 File Offset: 0x001406B7
		public static sheetSection CreateUnsafe(ProgramNode node)
		{
			return new sheetSection(node);
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x001424C0 File Offset: 0x001406C0
		public static sheetSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sheetSection)
			{
				return null;
			}
			return new sheetSection?(sheetSection.CreateUnsafe(node));
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x001424FA File Offset: 0x001406FA
		public static sheetSection CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sheetSection(new Hole(g.Symbol.sheetSection, holeId));
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x00142512 File Offset: 0x00140712
		public bool Is_sheetSection_horizontalSheetSection(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.sheetSection_horizontalSheetSection;
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x0014252C File Offset: 0x0014072C
		public bool Is_sheetSection_horizontalSheetSection(GrammarBuilders g, out sheetSection_horizontalSheetSection value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.sheetSection_horizontalSheetSection)
			{
				value = sheetSection_horizontalSheetSection.CreateUnsafe(this.Node);
				return true;
			}
			value = default(sheetSection_horizontalSheetSection);
			return false;
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x00142564 File Offset: 0x00140764
		public sheetSection_horizontalSheetSection? As_sheetSection_horizontalSheetSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.sheetSection_horizontalSheetSection)
			{
				return null;
			}
			return new sheetSection_horizontalSheetSection?(sheetSection_horizontalSheetSection.CreateUnsafe(this.Node));
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x001425A4 File Offset: 0x001407A4
		public sheetSection_horizontalSheetSection Cast_sheetSection_horizontalSheetSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.sheetSection_horizontalSheetSection)
			{
				return sheetSection_horizontalSheetSection.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_sheetSection_horizontalSheetSection is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600630E RID: 25358 RVA: 0x001425F9 File Offset: 0x001407F9
		public bool Is_TakeUntilEmptyColumn(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TakeUntilEmptyColumn;
		}

		// Token: 0x0600630F RID: 25359 RVA: 0x00142613 File Offset: 0x00140813
		public bool Is_TakeUntilEmptyColumn(GrammarBuilders g, out TakeUntilEmptyColumn value)
		{
			if (this.Node.GrammarRule == g.Rule.TakeUntilEmptyColumn)
			{
				value = TakeUntilEmptyColumn.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TakeUntilEmptyColumn);
			return false;
		}

		// Token: 0x06006310 RID: 25360 RVA: 0x00142648 File Offset: 0x00140848
		public TakeUntilEmptyColumn? As_TakeUntilEmptyColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TakeUntilEmptyColumn)
			{
				return null;
			}
			return new TakeUntilEmptyColumn?(TakeUntilEmptyColumn.CreateUnsafe(this.Node));
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x00142688 File Offset: 0x00140888
		public TakeUntilEmptyColumn Cast_TakeUntilEmptyColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TakeUntilEmptyColumn)
			{
				return TakeUntilEmptyColumn.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TakeUntilEmptyColumn is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006312 RID: 25362 RVA: 0x001426DD File Offset: 0x001408DD
		public bool Is_TrimRightSingleCellColumns(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimRightSingleCellColumns;
		}

		// Token: 0x06006313 RID: 25363 RVA: 0x001426F7 File Offset: 0x001408F7
		public bool Is_TrimRightSingleCellColumns(GrammarBuilders g, out TrimRightSingleCellColumns value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimRightSingleCellColumns)
			{
				value = TrimRightSingleCellColumns.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimRightSingleCellColumns);
			return false;
		}

		// Token: 0x06006314 RID: 25364 RVA: 0x0014272C File Offset: 0x0014092C
		public TrimRightSingleCellColumns? As_TrimRightSingleCellColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimRightSingleCellColumns)
			{
				return null;
			}
			return new TrimRightSingleCellColumns?(TrimRightSingleCellColumns.CreateUnsafe(this.Node));
		}

		// Token: 0x06006315 RID: 25365 RVA: 0x0014276C File Offset: 0x0014096C
		public TrimRightSingleCellColumns Cast_TrimRightSingleCellColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimRightSingleCellColumns)
			{
				return TrimRightSingleCellColumns.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimRightSingleCellColumns is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006316 RID: 25366 RVA: 0x001427C4 File Offset: 0x001409C4
		public T Switch<T>(GrammarBuilders g, Func<sheetSection_horizontalSheetSection, T> func0, Func<TakeUntilEmptyColumn, T> func1, Func<TrimRightSingleCellColumns, T> func2)
		{
			sheetSection_horizontalSheetSection sheetSection_horizontalSheetSection;
			if (this.Is_sheetSection_horizontalSheetSection(g, out sheetSection_horizontalSheetSection))
			{
				return func0(sheetSection_horizontalSheetSection);
			}
			TakeUntilEmptyColumn takeUntilEmptyColumn;
			if (this.Is_TakeUntilEmptyColumn(g, out takeUntilEmptyColumn))
			{
				return func1(takeUntilEmptyColumn);
			}
			TrimRightSingleCellColumns trimRightSingleCellColumns;
			if (this.Is_TrimRightSingleCellColumns(g, out trimRightSingleCellColumns))
			{
				return func2(trimRightSingleCellColumns);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol sheetSection");
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x00142830 File Offset: 0x00140A30
		public void Switch(GrammarBuilders g, Action<sheetSection_horizontalSheetSection> func0, Action<TakeUntilEmptyColumn> func1, Action<TrimRightSingleCellColumns> func2)
		{
			sheetSection_horizontalSheetSection sheetSection_horizontalSheetSection;
			if (this.Is_sheetSection_horizontalSheetSection(g, out sheetSection_horizontalSheetSection))
			{
				func0(sheetSection_horizontalSheetSection);
				return;
			}
			TakeUntilEmptyColumn takeUntilEmptyColumn;
			if (this.Is_TakeUntilEmptyColumn(g, out takeUntilEmptyColumn))
			{
				func1(takeUntilEmptyColumn);
				return;
			}
			TrimRightSingleCellColumns trimRightSingleCellColumns;
			if (this.Is_TrimRightSingleCellColumns(g, out trimRightSingleCellColumns))
			{
				func2(trimRightSingleCellColumns);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol sheetSection");
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x0014289B File Offset: 0x00140A9B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x001428B0 File Offset: 0x00140AB0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x001428DA File Offset: 0x00140ADA
		public bool Equals(sheetSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C01 RID: 11265
		private ProgramNode _node;
	}
}
