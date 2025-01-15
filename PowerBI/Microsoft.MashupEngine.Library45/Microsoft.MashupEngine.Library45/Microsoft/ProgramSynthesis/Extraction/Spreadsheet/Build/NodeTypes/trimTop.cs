using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E56 RID: 3670
	public struct trimTop : IProgramNodeBuilder, IEquatable<trimTop>
	{
		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x060062D7 RID: 25303 RVA: 0x00141A0E File Offset: 0x0013FC0E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060062D8 RID: 25304 RVA: 0x00141A16 File Offset: 0x0013FC16
		private trimTop(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060062D9 RID: 25305 RVA: 0x00141A1F File Offset: 0x0013FC1F
		public static trimTop CreateUnsafe(ProgramNode node)
		{
			return new trimTop(node);
		}

		// Token: 0x060062DA RID: 25306 RVA: 0x00141A28 File Offset: 0x0013FC28
		public static trimTop? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.trimTop)
			{
				return null;
			}
			return new trimTop?(trimTop.CreateUnsafe(node));
		}

		// Token: 0x060062DB RID: 25307 RVA: 0x00141A62 File Offset: 0x0013FC62
		public static trimTop CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new trimTop(new Hole(g.Symbol.trimTop, holeId));
		}

		// Token: 0x060062DC RID: 25308 RVA: 0x00141A7A File Offset: 0x0013FC7A
		public bool Is_trimTop_sheetSection(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.trimTop_sheetSection;
		}

		// Token: 0x060062DD RID: 25309 RVA: 0x00141A94 File Offset: 0x0013FC94
		public bool Is_trimTop_sheetSection(GrammarBuilders g, out trimTop_sheetSection value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.trimTop_sheetSection)
			{
				value = trimTop_sheetSection.CreateUnsafe(this.Node);
				return true;
			}
			value = default(trimTop_sheetSection);
			return false;
		}

		// Token: 0x060062DE RID: 25310 RVA: 0x00141ACC File Offset: 0x0013FCCC
		public trimTop_sheetSection? As_trimTop_sheetSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.trimTop_sheetSection)
			{
				return null;
			}
			return new trimTop_sheetSection?(trimTop_sheetSection.CreateUnsafe(this.Node));
		}

		// Token: 0x060062DF RID: 25311 RVA: 0x00141B0C File Offset: 0x0013FD0C
		public trimTop_sheetSection Cast_trimTop_sheetSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.trimTop_sheetSection)
			{
				return trimTop_sheetSection.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_trimTop_sheetSection is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062E0 RID: 25312 RVA: 0x00141B61 File Offset: 0x0013FD61
		public bool Is_FreezePaneTight(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FreezePaneTight;
		}

		// Token: 0x060062E1 RID: 25313 RVA: 0x00141B7B File Offset: 0x0013FD7B
		public bool Is_FreezePaneTight(GrammarBuilders g, out FreezePaneTight value)
		{
			if (this.Node.GrammarRule == g.Rule.FreezePaneTight)
			{
				value = FreezePaneTight.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FreezePaneTight);
			return false;
		}

		// Token: 0x060062E2 RID: 25314 RVA: 0x00141BB0 File Offset: 0x0013FDB0
		public FreezePaneTight? As_FreezePaneTight(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FreezePaneTight)
			{
				return null;
			}
			return new FreezePaneTight?(FreezePaneTight.CreateUnsafe(this.Node));
		}

		// Token: 0x060062E3 RID: 25315 RVA: 0x00141BF0 File Offset: 0x0013FDF0
		public FreezePaneTight Cast_FreezePaneTight(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FreezePaneTight)
			{
				return FreezePaneTight.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FreezePaneTight is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062E4 RID: 25316 RVA: 0x00141C45 File Offset: 0x0013FE45
		public bool Is_FreezePaneToBlanks(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FreezePaneToBlanks;
		}

		// Token: 0x060062E5 RID: 25317 RVA: 0x00141C5F File Offset: 0x0013FE5F
		public bool Is_FreezePaneToBlanks(GrammarBuilders g, out FreezePaneToBlanks value)
		{
			if (this.Node.GrammarRule == g.Rule.FreezePaneToBlanks)
			{
				value = FreezePaneToBlanks.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FreezePaneToBlanks);
			return false;
		}

		// Token: 0x060062E6 RID: 25318 RVA: 0x00141C94 File Offset: 0x0013FE94
		public FreezePaneToBlanks? As_FreezePaneToBlanks(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FreezePaneToBlanks)
			{
				return null;
			}
			return new FreezePaneToBlanks?(FreezePaneToBlanks.CreateUnsafe(this.Node));
		}

		// Token: 0x060062E7 RID: 25319 RVA: 0x00141CD4 File Offset: 0x0013FED4
		public FreezePaneToBlanks Cast_FreezePaneToBlanks(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FreezePaneToBlanks)
			{
				return FreezePaneToBlanks.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FreezePaneToBlanks is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062E8 RID: 25320 RVA: 0x00141D29 File Offset: 0x0013FF29
		public bool Is_FreezePaneToMultipleBlanks(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FreezePaneToMultipleBlanks;
		}

		// Token: 0x060062E9 RID: 25321 RVA: 0x00141D43 File Offset: 0x0013FF43
		public bool Is_FreezePaneToMultipleBlanks(GrammarBuilders g, out FreezePaneToMultipleBlanks value)
		{
			if (this.Node.GrammarRule == g.Rule.FreezePaneToMultipleBlanks)
			{
				value = FreezePaneToMultipleBlanks.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FreezePaneToMultipleBlanks);
			return false;
		}

		// Token: 0x060062EA RID: 25322 RVA: 0x00141D78 File Offset: 0x0013FF78
		public FreezePaneToMultipleBlanks? As_FreezePaneToMultipleBlanks(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FreezePaneToMultipleBlanks)
			{
				return null;
			}
			return new FreezePaneToMultipleBlanks?(FreezePaneToMultipleBlanks.CreateUnsafe(this.Node));
		}

		// Token: 0x060062EB RID: 25323 RVA: 0x00141DB8 File Offset: 0x0013FFB8
		public FreezePaneToMultipleBlanks Cast_FreezePaneToMultipleBlanks(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FreezePaneToMultipleBlanks)
			{
				return FreezePaneToMultipleBlanks.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FreezePaneToMultipleBlanks is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x00141E0D File Offset: 0x0014000D
		public bool Is_TrimTopMergedCellRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimTopMergedCellRows;
		}

		// Token: 0x060062ED RID: 25325 RVA: 0x00141E27 File Offset: 0x00140027
		public bool Is_TrimTopMergedCellRows(GrammarBuilders g, out TrimTopMergedCellRows value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimTopMergedCellRows)
			{
				value = TrimTopMergedCellRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimTopMergedCellRows);
			return false;
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x00141E5C File Offset: 0x0014005C
		public TrimTopMergedCellRows? As_TrimTopMergedCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimTopMergedCellRows)
			{
				return null;
			}
			return new TrimTopMergedCellRows?(TrimTopMergedCellRows.CreateUnsafe(this.Node));
		}

		// Token: 0x060062EF RID: 25327 RVA: 0x00141E9C File Offset: 0x0014009C
		public TrimTopMergedCellRows Cast_TrimTopMergedCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimTopMergedCellRows)
			{
				return TrimTopMergedCellRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimTopMergedCellRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062F0 RID: 25328 RVA: 0x00141EF1 File Offset: 0x001400F1
		public bool Is_TrimTopFullWidthMergedCellRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimTopFullWidthMergedCellRows;
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x00141F0B File Offset: 0x0014010B
		public bool Is_TrimTopFullWidthMergedCellRows(GrammarBuilders g, out TrimTopFullWidthMergedCellRows value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimTopFullWidthMergedCellRows)
			{
				value = TrimTopFullWidthMergedCellRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimTopFullWidthMergedCellRows);
			return false;
		}

		// Token: 0x060062F2 RID: 25330 RVA: 0x00141F40 File Offset: 0x00140140
		public TrimTopFullWidthMergedCellRows? As_TrimTopFullWidthMergedCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimTopFullWidthMergedCellRows)
			{
				return null;
			}
			return new TrimTopFullWidthMergedCellRows?(TrimTopFullWidthMergedCellRows.CreateUnsafe(this.Node));
		}

		// Token: 0x060062F3 RID: 25331 RVA: 0x00141F80 File Offset: 0x00140180
		public TrimTopFullWidthMergedCellRows Cast_TrimTopFullWidthMergedCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimTopFullWidthMergedCellRows)
			{
				return TrimTopFullWidthMergedCellRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimTopFullWidthMergedCellRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062F4 RID: 25332 RVA: 0x00141FD5 File Offset: 0x001401D5
		public bool Is_TrimTopSingleCellRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimTopSingleCellRows;
		}

		// Token: 0x060062F5 RID: 25333 RVA: 0x00141FEF File Offset: 0x001401EF
		public bool Is_TrimTopSingleCellRows(GrammarBuilders g, out TrimTopSingleCellRows value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimTopSingleCellRows)
			{
				value = TrimTopSingleCellRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimTopSingleCellRows);
			return false;
		}

		// Token: 0x060062F6 RID: 25334 RVA: 0x00142024 File Offset: 0x00140224
		public TrimTopSingleCellRows? As_TrimTopSingleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimTopSingleCellRows)
			{
				return null;
			}
			return new TrimTopSingleCellRows?(TrimTopSingleCellRows.CreateUnsafe(this.Node));
		}

		// Token: 0x060062F7 RID: 25335 RVA: 0x00142064 File Offset: 0x00140264
		public TrimTopSingleCellRows Cast_TrimTopSingleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimTopSingleCellRows)
			{
				return TrimTopSingleCellRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimTopSingleCellRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062F8 RID: 25336 RVA: 0x001420B9 File Offset: 0x001402B9
		public bool Is_TrimBelowTopBorder(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimBelowTopBorder;
		}

		// Token: 0x060062F9 RID: 25337 RVA: 0x001420D3 File Offset: 0x001402D3
		public bool Is_TrimBelowTopBorder(GrammarBuilders g, out TrimBelowTopBorder value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimBelowTopBorder)
			{
				value = TrimBelowTopBorder.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimBelowTopBorder);
			return false;
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x00142108 File Offset: 0x00140308
		public TrimBelowTopBorder? As_TrimBelowTopBorder(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimBelowTopBorder)
			{
				return null;
			}
			return new TrimBelowTopBorder?(TrimBelowTopBorder.CreateUnsafe(this.Node));
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x00142148 File Offset: 0x00140348
		public TrimBelowTopBorder Cast_TrimBelowTopBorder(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimBelowTopBorder)
			{
				return TrimBelowTopBorder.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimBelowTopBorder is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x0014219D File Offset: 0x0014039D
		public bool Is_TakeAfterEmptyRow(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TakeAfterEmptyRow;
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x001421B7 File Offset: 0x001403B7
		public bool Is_TakeAfterEmptyRow(GrammarBuilders g, out TakeAfterEmptyRow value)
		{
			if (this.Node.GrammarRule == g.Rule.TakeAfterEmptyRow)
			{
				value = TakeAfterEmptyRow.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TakeAfterEmptyRow);
			return false;
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x001421EC File Offset: 0x001403EC
		public TakeAfterEmptyRow? As_TakeAfterEmptyRow(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TakeAfterEmptyRow)
			{
				return null;
			}
			return new TakeAfterEmptyRow?(TakeAfterEmptyRow.CreateUnsafe(this.Node));
		}

		// Token: 0x060062FF RID: 25343 RVA: 0x0014222C File Offset: 0x0014042C
		public TakeAfterEmptyRow Cast_TakeAfterEmptyRow(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TakeAfterEmptyRow)
			{
				return TakeAfterEmptyRow.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TakeAfterEmptyRow is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x00142284 File Offset: 0x00140484
		public T Switch<T>(GrammarBuilders g, Func<trimTop_sheetSection, T> func0, Func<FreezePaneTight, T> func1, Func<FreezePaneToBlanks, T> func2, Func<FreezePaneToMultipleBlanks, T> func3, Func<TrimTopMergedCellRows, T> func4, Func<TrimTopFullWidthMergedCellRows, T> func5, Func<TrimTopSingleCellRows, T> func6, Func<TrimBelowTopBorder, T> func7, Func<TakeAfterEmptyRow, T> func8)
		{
			trimTop_sheetSection trimTop_sheetSection;
			if (this.Is_trimTop_sheetSection(g, out trimTop_sheetSection))
			{
				return func0(trimTop_sheetSection);
			}
			FreezePaneTight freezePaneTight;
			if (this.Is_FreezePaneTight(g, out freezePaneTight))
			{
				return func1(freezePaneTight);
			}
			FreezePaneToBlanks freezePaneToBlanks;
			if (this.Is_FreezePaneToBlanks(g, out freezePaneToBlanks))
			{
				return func2(freezePaneToBlanks);
			}
			FreezePaneToMultipleBlanks freezePaneToMultipleBlanks;
			if (this.Is_FreezePaneToMultipleBlanks(g, out freezePaneToMultipleBlanks))
			{
				return func3(freezePaneToMultipleBlanks);
			}
			TrimTopMergedCellRows trimTopMergedCellRows;
			if (this.Is_TrimTopMergedCellRows(g, out trimTopMergedCellRows))
			{
				return func4(trimTopMergedCellRows);
			}
			TrimTopFullWidthMergedCellRows trimTopFullWidthMergedCellRows;
			if (this.Is_TrimTopFullWidthMergedCellRows(g, out trimTopFullWidthMergedCellRows))
			{
				return func5(trimTopFullWidthMergedCellRows);
			}
			TrimTopSingleCellRows trimTopSingleCellRows;
			if (this.Is_TrimTopSingleCellRows(g, out trimTopSingleCellRows))
			{
				return func6(trimTopSingleCellRows);
			}
			TrimBelowTopBorder trimBelowTopBorder;
			if (this.Is_TrimBelowTopBorder(g, out trimBelowTopBorder))
			{
				return func7(trimBelowTopBorder);
			}
			TakeAfterEmptyRow takeAfterEmptyRow;
			if (this.Is_TakeAfterEmptyRow(g, out takeAfterEmptyRow))
			{
				return func8(takeAfterEmptyRow);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol trimTop");
		}

		// Token: 0x06006301 RID: 25345 RVA: 0x0014236C File Offset: 0x0014056C
		public void Switch(GrammarBuilders g, Action<trimTop_sheetSection> func0, Action<FreezePaneTight> func1, Action<FreezePaneToBlanks> func2, Action<FreezePaneToMultipleBlanks> func3, Action<TrimTopMergedCellRows> func4, Action<TrimTopFullWidthMergedCellRows> func5, Action<TrimTopSingleCellRows> func6, Action<TrimBelowTopBorder> func7, Action<TakeAfterEmptyRow> func8)
		{
			trimTop_sheetSection trimTop_sheetSection;
			if (this.Is_trimTop_sheetSection(g, out trimTop_sheetSection))
			{
				func0(trimTop_sheetSection);
				return;
			}
			FreezePaneTight freezePaneTight;
			if (this.Is_FreezePaneTight(g, out freezePaneTight))
			{
				func1(freezePaneTight);
				return;
			}
			FreezePaneToBlanks freezePaneToBlanks;
			if (this.Is_FreezePaneToBlanks(g, out freezePaneToBlanks))
			{
				func2(freezePaneToBlanks);
				return;
			}
			FreezePaneToMultipleBlanks freezePaneToMultipleBlanks;
			if (this.Is_FreezePaneToMultipleBlanks(g, out freezePaneToMultipleBlanks))
			{
				func3(freezePaneToMultipleBlanks);
				return;
			}
			TrimTopMergedCellRows trimTopMergedCellRows;
			if (this.Is_TrimTopMergedCellRows(g, out trimTopMergedCellRows))
			{
				func4(trimTopMergedCellRows);
				return;
			}
			TrimTopFullWidthMergedCellRows trimTopFullWidthMergedCellRows;
			if (this.Is_TrimTopFullWidthMergedCellRows(g, out trimTopFullWidthMergedCellRows))
			{
				func5(trimTopFullWidthMergedCellRows);
				return;
			}
			TrimTopSingleCellRows trimTopSingleCellRows;
			if (this.Is_TrimTopSingleCellRows(g, out trimTopSingleCellRows))
			{
				func6(trimTopSingleCellRows);
				return;
			}
			TrimBelowTopBorder trimBelowTopBorder;
			if (this.Is_TrimBelowTopBorder(g, out trimBelowTopBorder))
			{
				func7(trimBelowTopBorder);
				return;
			}
			TakeAfterEmptyRow takeAfterEmptyRow;
			if (this.Is_TakeAfterEmptyRow(g, out takeAfterEmptyRow))
			{
				func8(takeAfterEmptyRow);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol trimTop");
		}

		// Token: 0x06006302 RID: 25346 RVA: 0x00142454 File Offset: 0x00140654
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006303 RID: 25347 RVA: 0x00142468 File Offset: 0x00140668
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006304 RID: 25348 RVA: 0x00142492 File Offset: 0x00140692
		public bool Equals(trimTop other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C00 RID: 11264
		private ProgramNode _node;
	}
}
