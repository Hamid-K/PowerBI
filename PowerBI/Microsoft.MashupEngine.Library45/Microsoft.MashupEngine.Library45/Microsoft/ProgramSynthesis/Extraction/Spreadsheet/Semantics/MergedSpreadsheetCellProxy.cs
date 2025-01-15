using System;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EBA RID: 3770
	public class MergedSpreadsheetCellProxy : IMergedSpreadsheetCellProxy, ISpreadsheetCell
	{
		// Token: 0x060066AF RID: 26287 RVA: 0x0014EFBB File Offset: 0x0014D1BB
		public MergedSpreadsheetCellProxy(ISpreadsheetCell originalCell, ISpreadsheetCell shadowedCell)
		{
			this.OriginalCell = originalCell;
			this.ShadowedCell = shadowedCell;
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x060066B0 RID: 26288 RVA: 0x0014EFD1 File Offset: 0x0014D1D1
		public ISpreadsheetCell OriginalCell { get; }

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x060066B1 RID: 26289 RVA: 0x0014EFD9 File Offset: 0x0014D1D9
		public ISpreadsheetCell ShadowedCell { get; }

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x060066B2 RID: 26290 RVA: 0x0014EFE1 File Offset: 0x0014D1E1
		public string AsString
		{
			get
			{
				return this.OriginalCell.AsString;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x060066B3 RID: 26291 RVA: 0x0014EFEE File Offset: 0x0014D1EE
		public bool IsError
		{
			get
			{
				return this.OriginalCell.IsError;
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x060066B4 RID: 26292 RVA: 0x0014EFFB File Offset: 0x0014D1FB
		public string Formula
		{
			get
			{
				return this.OriginalCell.Formula;
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x060066B5 RID: 26293 RVA: 0x0014F008 File Offset: 0x0014D208
		public string FormulaSharedId
		{
			get
			{
				return this.OriginalCell.FormulaSharedId;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x060066B6 RID: 26294 RVA: 0x0014F015 File Offset: 0x0014D215
		public Bounds<TableUnit> Span
		{
			get
			{
				return this.OriginalCell.Span;
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x060066B7 RID: 26295 RVA: 0x0014F022 File Offset: 0x0014D222
		public bool RowHidden
		{
			get
			{
				return this.ShadowedCell.RowHidden;
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x060066B8 RID: 26296 RVA: 0x0014F02F File Offset: 0x0014D22F
		public bool ColumnHidden
		{
			get
			{
				return this.ShadowedCell.ColumnHidden;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x060066B9 RID: 26297 RVA: 0x0014F03C File Offset: 0x0014D23C
		public ICellStyleInfo StyleInfo
		{
			get
			{
				return this.OriginalCell.StyleInfo;
			}
		}

		// Token: 0x060066BA RID: 26298 RVA: 0x0014F049 File Offset: 0x0014D249
		public override string ToString()
		{
			return "(merged)";
		}
	}
}
