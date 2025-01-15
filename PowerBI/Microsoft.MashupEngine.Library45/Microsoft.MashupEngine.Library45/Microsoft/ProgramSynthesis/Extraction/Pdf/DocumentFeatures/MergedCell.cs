using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CE7 RID: 3303
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class MergedCell<TCell> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x060054D3 RID: 21715 RVA: 0x0010A77F File Offset: 0x0010897F
		public IReadOnlyList<TCell> Cells { get; }

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x060054D4 RID: 21716 RVA: 0x0010A787 File Offset: 0x00108987
		protected PdfAnalyzerOptions Options { get; }

		// Token: 0x060054D5 RID: 21717 RVA: 0x0010A78F File Offset: 0x0010898F
		protected MergedCell(IReadOnlyList<TCell> cells, PdfAnalyzerOptions options)
		{
			this.Cells = cells;
			this.Options = options;
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x060054D6 RID: 21718 RVA: 0x0010A7A5 File Offset: 0x001089A5
		public virtual ICell AsICell
		{
			get
			{
				return CellBuilder.BuildCellFromUnsortedTextRunGroup(this.Cells.SelectMany((TCell cell) => cell.TextRuns));
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x060054D7 RID: 21719 RVA: 0x0010A7D6 File Offset: 0x001089D6
		internal virtual bool IsContentLikelyMultipleRows
		{
			get
			{
				return this.IsContentMultipleRowsConfidence == MergedCell<TCell>.CellIsMultipleRowsConfidence.LikelyMultipleRows;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x060054D8 RID: 21720 RVA: 0x0010A7E1 File Offset: 0x001089E1
		internal virtual bool IsContentPossiblyMultipleRows
		{
			get
			{
				return this.IsContentMultipleRowsConfidence >= MergedCell<TCell>.CellIsMultipleRowsConfidence.PossiblyMultipleRows;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x060054D9 RID: 21721 RVA: 0x0010A7F0 File Offset: 0x001089F0
		[Nullable(0)]
		internal virtual MergedCell<TCell>.CellIsMultipleRowsConfidence IsContentMultipleRowsConfidence
		{
			[NullableContext(0)]
			get
			{
				MergedCell<TCell>.<>c__DisplayClass15_0 CS$<>8__locals1 = new MergedCell<TCell>.<>c__DisplayClass15_0();
				if (this.Cells.Count <= 1)
				{
					return MergedCell<TCell>.CellIsMultipleRowsConfidence.DefinitelySingleRow;
				}
				if (this.Cells.Any((TCell cell) => SpecialCharacters.EndsWithHyphen(cell.Content)))
				{
					return MergedCell<TCell>.CellIsMultipleRowsConfidence.LikelySingleRow;
				}
				MergedCell<TCell>.<>c__DisplayClass15_0 CS$<>8__locals2 = CS$<>8__locals1;
				Func<string, bool> func;
				if (this.Options.Version < PdfAnalyzerVersion.V1_1)
				{
					func = delegate(string str)
					{
						if (str.Contains(','))
						{
							Func<char, bool> func4;
							if ((func4 = MergedCell<TCell>.<>O.<0>__IsLetter) == null)
							{
								func4 = (MergedCell<TCell>.<>O.<0>__IsLetter = new Func<char, bool>(char.IsLetter));
							}
							return str.Any(func4);
						}
						return false;
					};
				}
				else
				{
					func = delegate(string str)
					{
						if (str.Contains(", ") || str.EndsWith(","))
						{
							Func<char, bool> func5;
							if ((func5 = MergedCell<TCell>.<>O.<0>__IsLetter) == null)
							{
								func5 = (MergedCell<TCell>.<>O.<0>__IsLetter = new Func<char, bool>(char.IsLetter));
							}
							return str.Any(func5);
						}
						return false;
					};
				}
				CS$<>8__locals2.containsNonSeparatorComma = func;
				if (this.Cells.Any((TCell cell) => CS$<>8__locals1.containsNonSeparatorComma(cell.Content)))
				{
					return MergedCell<TCell>.CellIsMultipleRowsConfidence.LikelySingleRow;
				}
				MergedCell<TCell>.<>c__DisplayClass15_0 CS$<>8__locals3 = CS$<>8__locals1;
				Func<string, bool> func2;
				if (this.Options.Version < PdfAnalyzerVersion.V1_1)
				{
					func2 = (string str) => char.IsNumber(str, 0);
				}
				else
				{
					func2 = (string str) => char.IsNumber(str, 0) || (str.Length > 1 && char.IsNumber(str, 1) && SpecialCharacters.StartsWithCurrency(str));
				}
				CS$<>8__locals3.startsWithNumber = func2;
				MergedCell<TCell>.<>c__DisplayClass15_0 CS$<>8__locals4 = CS$<>8__locals1;
				Func<string, bool> func3;
				if (this.Options.Version < PdfAnalyzerVersion.V1_1)
				{
					func3 = (string str) => char.IsNumber(str, str.Length - 1);
				}
				else
				{
					func3 = (string str) => char.IsNumber(str, str.Length - 1) || (str.Length > 1 && char.IsNumber(str, str.Length - 2) && (str[str.Length - 1] == '%' || SpecialCharacters.EndsWithCurrency(str)));
				}
				CS$<>8__locals4.endsWithNumber = func3;
				if (!this.Cells.Any((TCell cell) => CS$<>8__locals1.startsWithNumber(cell.Content) && CS$<>8__locals1.endsWithNumber(cell.Content)))
				{
					return MergedCell<TCell>.CellIsMultipleRowsConfidence.LikelySingleRow;
				}
				if (!this.Cells.Any((TCell cell) => cell.Content.All((char ch) => !char.IsNumber(ch))))
				{
					return MergedCell<TCell>.CellIsMultipleRowsConfidence.LikelyMultipleRows;
				}
				return MergedCell<TCell>.CellIsMultipleRowsConfidence.PossiblyMultipleRows;
			}
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x0010A9A2 File Offset: 0x00108BA2
		public override string ToString()
		{
			return string.Join("\n", from cs in CellBuilder.SortCellGroup<TCell>(this.Cells)
				select string.Join(" ", cs.Select((TCell c) => c.Content)));
		}

		// Token: 0x02000CE8 RID: 3304
		[NullableContext(0)]
		internal enum CellIsMultipleRowsConfidence
		{
			// Token: 0x0400265B RID: 9819
			DefinitelySingleRow,
			// Token: 0x0400265C RID: 9820
			LikelySingleRow,
			// Token: 0x0400265D RID: 9821
			PossiblyMultipleRows,
			// Token: 0x0400265E RID: 9822
			LikelyMultipleRows
		}

		// Token: 0x02000CE9 RID: 3305
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400265F RID: 9823
			[Nullable(0)]
			public static Func<char, bool> <0>__IsLetter;
		}
	}
}
