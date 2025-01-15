using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Learning
{
	// Token: 0x02000E76 RID: 3702
	internal class SpreadsheetPatterns
	{
		// Token: 0x06006539 RID: 25913 RVA: 0x00147618 File Offset: 0x00145818
		public SpreadsheetPatterns(ISpreadsheet spreadsheet)
		{
			this._spreadsheet = spreadsheet;
			this._fontNameLookup = SpreadsheetPatterns.BuildFontNameLookup(spreadsheet);
			this._cellSignatures = spreadsheet.Select((ISpreadsheetCell cell) => SpreadsheetPatterns.ComputeCellSignature(cell, this._fontNameLookup));
			this._singleSignatureByRow = (from row in this._cellSignatures.Rows()
				select (from sig in row.Skip(1)
					where sig != SpreadsheetPatterns.CellSignature.Blank
					select sig).Distinct<SpreadsheetPatterns.CellSignature>().MaybeOnly<SpreadsheetPatterns.CellSignature>().OrElseNull<SpreadsheetPatterns.CellSignature>()).ToList<SpreadsheetPatterns.CellSignature?>();
			SpreadsheetArea spreadsheetArea = Semantics.WholeSheet(this._spreadsheet);
			this._singleCellRows = spreadsheetArea.EnumerateRows(true, false, true, Derivative.Increasing).Select2((int _, IEnumerable<ISpreadsheetCell> row) => (from cell in row.MaybeOnly<ISpreadsheetCell>()
				select cell.Span.Left).OrElseNull<int>()).ToList<int?>();
			int num = Math.Min(64, spreadsheet.Size.X);
			ulong[] array = new ulong[spreadsheet.Size.Y];
			for (int i = 0; i < spreadsheet.Size.Y; i++)
			{
				ulong num2 = 0UL;
				for (int j = num - 1; j >= 0; j--)
				{
					ulong num3 = num2 << 1;
					ISpreadsheetCell spreadsheetCell = spreadsheet[j, i];
					num2 = num3 | (ulong)((!string.IsNullOrEmpty((spreadsheetCell != null) ? spreadsheetCell.AsString : null)) ? 1L : 0L);
				}
				array[i] = num2;
			}
			this._rowNonNullPatterns = array;
			IReadOnlyDictionary<BorderGroup, bool> borderGroupIsPartialTable = spreadsheet.Borders.BorderGroups.ToDictionary((BorderGroup g) => g, (BorderGroup g) => g.Span.Horizontal.AsEnumerable.Any((int x) => (from str in g.Span.Vertical.AsEnumerable.Select(delegate(int y)
				{
					ISpreadsheetCell spreadsheetCell2 = this._spreadsheet[x, y];
					if (spreadsheetCell2 == null)
					{
						return null;
					}
					return spreadsheetCell2.AsString;
				})
				where !string.IsNullOrWhiteSpace(str)
				select str).AllOrElseCompute(delegate(string str)
			{
				double num4;
				return double.TryParse(str, out num4);
			}, () => false)));
			this._borderGroupIsAbovePartialTable = spreadsheet.Borders.BorderGroups.ToDictionary((BorderGroup g) => g, delegate(BorderGroup g)
			{
				IGrouping<int, KeyValuePair<BorderGroup, bool>> grouping = (from t in borderGroupIsPartialTable
					group t by t.Key.Span.Top into @group
					where @group.Key > g.Span.Bottom
					select @group).ArgMin((IGrouping<int, KeyValuePair<BorderGroup, bool>> group) => group.Key);
				if (grouping == null)
				{
					return false;
				}
				return grouping.Where((KeyValuePair<BorderGroup, bool> t) => g.Span.Horizontal.Contains(t.Key.Span.Horizontal)).AnyOrElse((KeyValuePair<BorderGroup, bool> t) => t.Value && (from between in t.Key.Span.Vertical.BetweenExclusive(g.Span.Vertical)
					select between.AsEnumerable.All((int y) => this._rowNonNullPatterns[y] == 0UL)).OrElse(true), () => false);
			});
			IEnumerable<SpreadsheetArea> enumerable = Semantics.BorderedAreas(spreadsheetArea);
			Func<SpreadsheetArea, SpreadsheetArea> func;
			if ((func = SpreadsheetPatterns.<>O.<0>__Trim) == null)
			{
				func = (SpreadsheetPatterns.<>O.<0>__Trim = new Func<SpreadsheetArea, SpreadsheetArea>(Semantics.Trim));
			}
			this._trimmedBorderGroups = (from area in enumerable.Collect(func)
				select area.Span).ConvertToHashSet<Bounds<TableUnit>>();
			this._cellPatterns = SpreadsheetPatterns.ComputeDataPatterns(spreadsheet);
			this._isColumnNumeric = this._cellPatterns.Columns().Select(delegate(IEnumerable<SpreadsheetPatterns.DataPattern> col)
			{
				if (col.Windowed<SpreadsheetPatterns.DataPattern>().Any2((SpreadsheetPatterns.DataPattern above, SpreadsheetPatterns.DataPattern below) => above == SpreadsheetPatterns.DataPattern.Numeric && below == SpreadsheetPatterns.DataPattern.Other))
				{
					return false;
				}
				Dictionary<SpreadsheetPatterns.DataPattern, int> dictionary = (from p in col
					group p by p).ToDictionary((IGrouping<SpreadsheetPatterns.DataPattern, SpreadsheetPatterns.DataPattern> g) => g.Key, (IGrouping<SpreadsheetPatterns.DataPattern, SpreadsheetPatterns.DataPattern> g) => g.Count<SpreadsheetPatterns.DataPattern>());
				return dictionary.GetOrDefault(SpreadsheetPatterns.DataPattern.Numeric, 0) >= dictionary.GetOrDefault(SpreadsheetPatterns.DataPattern.Other, 0);
			}).ToArray<bool>();
		}

		// Token: 0x0600653A RID: 25914 RVA: 0x00147888 File Offset: 0x00145A88
		private static IReadOnlyDictionary<string, SpreadsheetPatterns.CellSignature> BuildFontNameLookup(ISpreadsheet spreadsheet)
		{
			return (from name in spreadsheet.PrunedCells(true, false, true).ToEnumerable().Collect(delegate(ISpreadsheetCell cell)
				{
					if (cell == null)
					{
						return null;
					}
					ICellStyleInfo styleInfo = cell.StyleInfo;
					if (styleInfo == null)
					{
						return null;
					}
					return styleInfo.FontName;
				})
				group name by name into g
				orderby g.Count<string>()
				select g.Key).SkipLast(1).ZipWith(SpreadsheetPatterns.FontNameCellSignatures).ToDictionary<string, SpreadsheetPatterns.CellSignature>();
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x0014794C File Offset: 0x00145B4C
		private static SpreadsheetPatterns.CellSignature ComputeCellSignature(ISpreadsheetCell cell, IReadOnlyDictionary<string, SpreadsheetPatterns.CellSignature> fontNameLookup)
		{
			if (cell == null || string.IsNullOrWhiteSpace(cell.AsString))
			{
				return SpreadsheetPatterns.CellSignature.Blank;
			}
			SpreadsheetPatterns.CellSignature cellSignature = SpreadsheetPatterns.CellSignature.None;
			if (cell.Formula != null)
			{
				cellSignature |= SpreadsheetPatterns.CellSignature.HasFormula;
			}
			ICellStyleInfo styleInfo = cell.StyleInfo;
			if (styleInfo != null && styleInfo.Bold)
			{
				cellSignature |= SpreadsheetPatterns.CellSignature.Bold;
			}
			ICellStyleInfo styleInfo2 = cell.StyleInfo;
			if (styleInfo2 != null && styleInfo2.Italic)
			{
				cellSignature |= SpreadsheetPatterns.CellSignature.Italic;
			}
			ICellStyleInfo styleInfo3 = cell.StyleInfo;
			bool flag;
			if (styleInfo3 == null)
			{
				flag = false;
			}
			else
			{
				int? num = styleInfo3.FontSize;
				int num2 = 10;
				flag = (num.GetValueOrDefault() < num2) & (num != null);
			}
			if (flag)
			{
				cellSignature |= SpreadsheetPatterns.CellSignature.Small;
			}
			ICellStyleInfo styleInfo4 = cell.StyleInfo;
			bool flag2;
			if (styleInfo4 == null)
			{
				flag2 = false;
			}
			else
			{
				int? num = styleInfo4.FontSize;
				int num2 = 10;
				flag2 = (num.GetValueOrDefault() > num2) & (num != null);
			}
			if (flag2)
			{
				cellSignature |= SpreadsheetPatterns.CellSignature.Large;
			}
			ICellStyleInfo styleInfo5 = cell.StyleInfo;
			bool flag3;
			if (styleInfo5 == null)
			{
				flag3 = false;
			}
			else
			{
				int? num = styleInfo5.FontSize;
				int num2 = 12;
				flag3 = (num.GetValueOrDefault() > num2) & (num != null);
			}
			if (flag3)
			{
				cellSignature |= SpreadsheetPatterns.CellSignature.Larger;
			}
			ICellStyleInfo styleInfo6 = cell.StyleInfo;
			bool flag4;
			if (styleInfo6 == null)
			{
				flag4 = false;
			}
			else
			{
				int? num = styleInfo6.FontSize;
				int num2 = 14;
				flag4 = (num.GetValueOrDefault() > num2) & (num != null);
			}
			if (flag4)
			{
				cellSignature |= SpreadsheetPatterns.CellSignature.VeryLarge;
			}
			ICellStyleInfo styleInfo7 = cell.StyleInfo;
			bool flag5;
			if (styleInfo7 == null)
			{
				flag5 = false;
			}
			else
			{
				int? num = styleInfo7.FontSize;
				int num2 = 16;
				flag5 = (num.GetValueOrDefault() > num2) & (num != null);
			}
			if (flag5)
			{
				cellSignature |= SpreadsheetPatterns.CellSignature.Huge;
			}
			ICellStyleInfo styleInfo8 = cell.StyleInfo;
			string text = ((styleInfo8 != null) ? styleInfo8.FontName : null);
			SpreadsheetPatterns.CellSignature cellSignature2;
			if (text != null && fontNameLookup.TryGetValue(text, out cellSignature2))
			{
				cellSignature |= cellSignature2;
			}
			return cellSignature;
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x00147ACC File Offset: 0x00145CCC
		private static RectangularArray<SpreadsheetPatterns.DataPattern> ComputeDataPatterns(ISpreadsheet spreadsheet)
		{
			RectangularArray<SpreadsheetPatterns.DataPattern> cellPatterns = spreadsheet.Select(new Func<ISpreadsheetCell, SpreadsheetPatterns.DataPattern>(SpreadsheetPatterns.<ComputeDataPatterns>g__ComputeCellDataPattern|15_0));
			cellPatterns.Span.Extend(Direction.Up, -1).AsEnumerable(Axis.Vertical, Derivative.Increasing).ForEach(delegate(Vector<TableUnit> pos)
			{
				if (cellPatterns[pos] == SpreadsheetPatterns.DataPattern.Year && cellPatterns[pos.X, pos.Y - 1] == SpreadsheetPatterns.DataPattern.Numeric)
				{
					cellPatterns[pos] = SpreadsheetPatterns.DataPattern.Numeric;
				}
			});
			return cellPatterns;
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x00147B30 File Offset: 0x00145D30
		private double HeaderScore(SpreadsheetArea area, IReadOnlyList<BorderGroup> borderGroupsInArea, out Optional<int> notHeaderRowIdxOpt)
		{
			SpreadsheetPatterns.<>c__DisplayClass17_0 CS$<>8__locals1 = new SpreadsheetPatterns.<>c__DisplayClass17_0();
			CS$<>8__locals1.area = area;
			CS$<>8__locals1.<>4__this = this;
			double num = 0.0;
			Border border2 = this._spreadsheet.Borders.Borders.Horizontal.Where2((int y, IReadOnlyList<Border> _) => y >= CS$<>8__locals1.area.Span.Top && y < CS$<>8__locals1.area.Span.Bottom).SelectMany((KeyValuePair<int, IReadOnlyList<Border>> kv) => kv.Value).FirstOrDefault((Border border) => border.Line.Range.Contains(CS$<>8__locals1.area.Span.Horizontal));
			int? num2 = ((border2 != null) ? new int?(border2.Line.Position) : null);
			int num3 = CS$<>8__locals1.area.Span.Top;
			if ((num2.GetValueOrDefault() == num3) & (num2 != null))
			{
				border2 = null;
			}
			notHeaderRowIdxOpt = default(Optional<int>);
			SpreadsheetPatterns.CellSignature? cellSignature = this._singleSignatureByRow[CS$<>8__locals1.area.Span.Top];
			if (cellSignature == null && CS$<>8__locals1.area.Span.Width() < CS$<>8__locals1.area.Spreadsheet.Size.X && CS$<>8__locals1.area.Span.Width() > 1)
			{
				cellSignature = (from sig in this._cellSignatures.Row(CS$<>8__locals1.area.Span.Top, CS$<>8__locals1.area.Span.Extend(Direction.Left, -1))
					where sig != SpreadsheetPatterns.CellSignature.Blank
					select sig).Distinct<SpreadsheetPatterns.CellSignature>().MaybeOnly<SpreadsheetPatterns.CellSignature>().OrElseNull<SpreadsheetPatterns.CellSignature>();
			}
			if (cellSignature != null)
			{
				CS$<>8__locals1.topSignature = cellSignature.GetValueOrDefault();
				if ((CS$<>8__locals1.topSignature & SpreadsheetPatterns.CellSignature.HasFormula) == SpreadsheetPatterns.CellSignature.None)
				{
					if (CS$<>8__locals1.topSignature != SpreadsheetPatterns.CellSignature.None && CS$<>8__locals1.area.Span.Top > 0)
					{
						SpreadsheetPatterns.CellSignature? cellSignature2 = this._singleSignatureByRow[CS$<>8__locals1.area.Span.Top - 1];
						SpreadsheetPatterns.CellSignature cellSignature3 = CS$<>8__locals1.topSignature;
						IReadOnlyList<Border> readOnlyList;
						if (((cellSignature2.GetValueOrDefault() == cellSignature3) & (cellSignature2 != null)) && (!this._spreadsheet.Borders.Borders.Horizontal.TryGetValue(CS$<>8__locals1.area.Span.Top, out readOnlyList) || !readOnlyList.Any((Border border) => border.Line.Range.Overlaps(CS$<>8__locals1.area.Span.Horizontal))) && CS$<>8__locals1.area.Span.Horizontal.AsEnumerable.Skip(1).Any((int idx) => CS$<>8__locals1.<>4__this._cellSignatures[idx, CS$<>8__locals1.area.Span.Top] != SpreadsheetPatterns.CellSignature.Blank && CS$<>8__locals1.<>4__this._cellSignatures[idx, CS$<>8__locals1.area.Span.Top - 1] != SpreadsheetPatterns.CellSignature.Blank))
						{
							num -= 7.0;
						}
					}
					if (CS$<>8__locals1.area.Span.Top > 0 && this._cellSignatures[CS$<>8__locals1.area.Span.Left, CS$<>8__locals1.area.Span.Top - 1] == SpreadsheetPatterns.CellSignature.Blank && CS$<>8__locals1.area.Span.Horizontal.AsEnumerable.Skip(1).Any((int x) => CS$<>8__locals1.<>4__this._cellSignatures[x, CS$<>8__locals1.area.Span.Top - 1] != SpreadsheetPatterns.CellSignature.Blank) && CS$<>8__locals1.area.Span.Horizontal.AsEnumerable.Skip(1).All((int x) => CS$<>8__locals1.<>4__this._cellSignatures[x, CS$<>8__locals1.area.Span.Top] == SpreadsheetPatterns.CellSignature.Blank || CS$<>8__locals1.<>4__this._cellSignatures[x, CS$<>8__locals1.area.Span.Top - 1] != SpreadsheetPatterns.CellSignature.Blank))
					{
						num -= 6.0;
					}
					Optional<ISpreadsheetCell> optional = CS$<>8__locals1.area.EnumerateRow(CS$<>8__locals1.area.Span.Top, true, false, true).MaybeOnly<ISpreadsheetCell>();
					if (optional.HasValue)
					{
						Optional<Record<int, IEnumerable<ISpreadsheetCell>>> optional2 = CS$<>8__locals1.area.EnumerateRows(true, false, true, Derivative.Increasing).MaybeFirst((Record<int, IEnumerable<ISpreadsheetCell>> row) => row.Item2.HasAtLeast(2));
						if (!optional2.HasValue)
						{
							num -= 101.0;
						}
						else if (optional.Value.Span.Width() <= 1 || optional.Value.Span.Left == CS$<>8__locals1.area.Span.Left)
						{
							IEnumerable<ISpreadsheetCell> enumerable;
							optional2.Value.Deconstruct(out num3, out enumerable);
							int rowIdx2 = num3;
							ISpreadsheetCell spreadsheetCell = CS$<>8__locals1.area.EnumerateCells(Axis.Horizontal, true, false, true, Derivative.Increasing, new int?(rowIdx2 - 1)).OnlyOrDefault<ISpreadsheetCell>();
							if (spreadsheetCell == null)
							{
								if (!CS$<>8__locals1.area.Span.Vertical.AsEnumerable.Any(delegate(int singleCellRowIdx)
								{
									if (singleCellRowIdx > rowIdx2)
									{
										int? num7 = CS$<>8__locals1.<>4__this._singleCellRows[singleCellRowIdx];
										if (num7 != null)
										{
											int valueOrDefault = num7.GetValueOrDefault();
											return CS$<>8__locals1.<>4__this._cellSignatures[valueOrDefault, singleCellRowIdx] == CS$<>8__locals1.topSignature;
										}
									}
									return false;
								}))
								{
									num -= 102.0;
								}
							}
							else
							{
								Vector<TableUnit> vector = spreadsheetCell.Span.Corner(Ordinal.BottomLeft);
								Vector<TableUnit> vector2 = vector + new Vector<TableUnit>(0, 1);
								if (!CS$<>8__locals1.area.Span.Contains(vector2))
								{
									num -= 104.0;
								}
								else
								{
									Bounds<TableUnit> span = new Bounds<TableUnit>(vector, vector2);
									if (!borderGroupsInArea.Any((BorderGroup group) => group.Span.Contains(span)))
									{
										SpreadsheetPatterns.CellSignature cellSignature4 = this._cellSignatures[vector2];
										if (cellSignature4 == SpreadsheetPatterns.CellSignature.Blank)
										{
											num -= 103.0;
										}
										else if (cellSignature4 != this._cellSignatures[vector])
										{
											num -= 50.0;
										}
									}
								}
							}
						}
					}
					notHeaderRowIdxOpt = CS$<>8__locals1.area.Span.Vertical.AsEnumerable.Skip(1).MaybeFirst(delegate(int rowIdx)
					{
						if (CS$<>8__locals1.<>4__this._rowNonNullPatterns[rowIdx] != 0UL)
						{
							if (CS$<>8__locals1.<>4__this._cellSignatures.Row(rowIdx, CS$<>8__locals1.area.Span).Any((SpreadsheetPatterns.CellSignature sig) => sig != SpreadsheetPatterns.CellSignature.Blank))
							{
								IEnumerable<SpreadsheetPatterns.CellSignature> enumerable3 = CS$<>8__locals1.<>4__this._cellSignatures.Row(rowIdx, CS$<>8__locals1.area.Span);
								Func<SpreadsheetPatterns.CellSignature, bool> func;
								if ((func = CS$<>8__locals1.<>9__26) == null)
								{
									func = (CS$<>8__locals1.<>9__26 = (SpreadsheetPatterns.CellSignature sig) => sig != CS$<>8__locals1.topSignature);
								}
								if (!enumerable3.All(func))
								{
									return CS$<>8__locals1.<>4__this._cellPatterns.Row(rowIdx, CS$<>8__locals1.area.Span).Any((SpreadsheetPatterns.DataPattern pattern) => (pattern & SpreadsheetPatterns.DataPattern.DefinitelyNumeric) > (SpreadsheetPatterns.DataPattern)0);
								}
								return true;
							}
						}
						return false;
					}).OrElseComputeOptional(delegate
					{
						IEnumerable<int> asEnumerable = CS$<>8__locals1.area.Span.Vertical.AsEnumerable;
						Func<int, bool> func2;
						if ((func2 = CS$<>8__locals1.<>9__28) == null)
						{
							func2 = (CS$<>8__locals1.<>9__28 = (int rowIdx) => CS$<>8__locals1.<>4__this._rowNonNullPatterns[rowIdx] != 0UL && CS$<>8__locals1.area.Span.Horizontal.AsEnumerable.All((int colIdx) => !CS$<>8__locals1.<>4__this._isColumnNumeric[colIdx] || (CS$<>8__locals1.<>4__this._cellPatterns[colIdx, rowIdx] & SpreadsheetPatterns.DataPattern.LikelyHeaderOfNumeric) > (SpreadsheetPatterns.DataPattern)0));
						}
						Optional<int> optional3 = asEnumerable.MaybeFirst(func2);
						Func<int, bool> func3;
						if ((func3 = CS$<>8__locals1.<>9__29) == null)
						{
							func3 = (CS$<>8__locals1.<>9__29 = (int notHeader) => notHeader > CS$<>8__locals1.area.Span.Top);
						}
						return optional3.Where(func3);
					});
					int topNonHeaderRowIdx = notHeaderRowIdxOpt.OrElse(CS$<>8__locals1.area.Span.Top);
					IEnumerable<IEnumerable<ISpreadsheetCell>> enumerable2 = CS$<>8__locals1.area.EnumerateRows(true, false, true, Derivative.Increasing).TakeWhile((Record<int, IEnumerable<ISpreadsheetCell>> r) => r.Item1 < topNonHeaderRowIdx).Select2((int idx, IEnumerable<ISpreadsheetCell> row) => row);
					if (notHeaderRowIdxOpt.HasValue)
					{
						if (!enumerable2.TakeWhile((IEnumerable<ISpreadsheetCell> row) => row.Where((ISpreadsheetCell cell) => cell.Span.Width() > 1).HasAtLeast(2)).Any<IEnumerable<ISpreadsheetCell>>())
						{
							if (enumerable2.TakeWhile(delegate(IEnumerable<ISpreadsheetCell> row)
							{
								Optional<ISpreadsheetCell> optional4 = row.MaybeOnly<ISpreadsheetCell>();
								Func<ISpreadsheetCell, bool> func4;
								if ((func4 = CS$<>8__locals1.<>9__32) == null)
								{
									func4 = (CS$<>8__locals1.<>9__32 = (ISpreadsheetCell only) => only.Span.Left == CS$<>8__locals1.area.Span.Left || (only.Span.Right != CS$<>8__locals1.area.Span.Right && CS$<>8__locals1.<>4__this._cellSignatures[only.Span.Left, only.Span.Top] != CS$<>8__locals1.<>4__this._cellSignatures[only.Span.Left, only.Span.Bottom + 1]));
								}
								return optional4.Select(func4).OrElse(false);
							}).Any<IEnumerable<ISpreadsheetCell>>())
							{
								notHeaderRowIdxOpt = default(Optional<int>);
								if (CS$<>8__locals1.topSignature != SpreadsheetPatterns.CellSignature.None)
								{
									num -= 21.0;
								}
							}
							else if (enumerable2.Max(delegate(IEnumerable<ISpreadsheetCell> r)
							{
								ISpreadsheetCell spreadsheetCell2 = r.LastOrDefault<ISpreadsheetCell>();
								if (spreadsheetCell2 == null)
								{
									return -1;
								}
								return spreadsheetCell2.Span.Right;
							}) < CS$<>8__locals1.area.Span.Right - 1)
							{
								num -= 3.0;
							}
							else if (enumerable2.Min(delegate(IEnumerable<ISpreadsheetCell> r)
							{
								ISpreadsheetCell spreadsheetCell3 = r.FirstOrDefault<ISpreadsheetCell>();
								if (spreadsheetCell3 == null)
								{
									return int.MaxValue;
								}
								return spreadsheetCell3.Span.Left;
							}) > CS$<>8__locals1.area.IncludedColumnsEnumerable.MaybeElementAt(3).OrElse(2147483647))
							{
								num -= 3.0;
							}
						}
					}
					if (notHeaderRowIdxOpt.HasValue)
					{
						IEnumerable<int> headerRowIndexes = Enumerable.Range(CS$<>8__locals1.area.Span.Top, notHeaderRowIdxOpt.Value - CS$<>8__locals1.area.Span.Top);
						if (enumerable2.Any(delegate(IEnumerable<ISpreadsheetCell> row)
						{
							Func<ISpreadsheetCell, SpreadsheetPatterns.DataPattern> func5;
							if ((func5 = CS$<>8__locals1.<>9__34) == null)
							{
								func5 = (CS$<>8__locals1.<>9__34 = (ISpreadsheetCell cell) => CS$<>8__locals1.<>4__this._cellPatterns[cell.Span.Corner(Ordinal.TopLeft)]);
							}
							return row.Select(func5).Any((SpreadsheetPatterns.DataPattern pattern) => (pattern & SpreadsheetPatterns.DataPattern.DefinitelyNumeric) > (SpreadsheetPatterns.DataPattern)0);
						}))
						{
							notHeaderRowIdxOpt = default(Optional<int>);
							if (CS$<>8__locals1.topSignature != SpreadsheetPatterns.CellSignature.None)
							{
								num -= 10.0;
							}
						}
						else if ((CS$<>8__locals1.topSignature & SpreadsheetPatterns.CellSignature.FontStyles) != SpreadsheetPatterns.CellSignature.None)
						{
							if (CS$<>8__locals1.area.Span.Horizontal.AsEnumerable.Where((int colIdx) => (from pattern in headerRowIndexes.Select((int rowIdx) => CS$<>8__locals1.<>4__this._cellPatterns[colIdx, rowIdx]).Distinct<SpreadsheetPatterns.DataPattern>()
								where pattern != SpreadsheetPatterns.DataPattern.Blank
								select pattern).OnlyOrDefault<SpreadsheetPatterns.DataPattern>() == SpreadsheetPatterns.DataPattern.Year).AllOrElseCompute(delegate(int colIdx)
							{
								if (CS$<>8__locals1.<>4__this._cellPatterns.Column(colIdx, CS$<>8__locals1.area.Span).All((SpreadsheetPatterns.DataPattern pattern) => pattern != SpreadsheetPatterns.DataPattern.Other))
								{
									IEnumerable<SpreadsheetPatterns.CellSignature> enumerable4 = CS$<>8__locals1.<>4__this._cellSignatures.Column(colIdx, CS$<>8__locals1.area.Span);
									Func<SpreadsheetPatterns.CellSignature, bool> func6;
									if ((func6 = CS$<>8__locals1.<>9__42) == null)
									{
										func6 = (CS$<>8__locals1.<>9__42 = (SpreadsheetPatterns.CellSignature sig) => sig == SpreadsheetPatterns.CellSignature.Blank || (sig & SpreadsheetPatterns.CellSignature.FontSizeAndStyles) == (CS$<>8__locals1.topSignature & SpreadsheetPatterns.CellSignature.FontSizeAndStyles));
									}
									return enumerable4.All(func6);
								}
								return false;
							}, () => false))
							{
								notHeaderRowIdxOpt = default(Optional<int>);
								if (CS$<>8__locals1.topSignature != SpreadsheetPatterns.CellSignature.None)
								{
									num -= 9.0;
								}
							}
						}
					}
				}
			}
			if (CS$<>8__locals1.area.Span.Right <= 64)
			{
				SpreadsheetPatterns.<>c__DisplayClass17_7 CS$<>8__locals6 = new SpreadsheetPatterns.<>c__DisplayClass17_7();
				CS$<>8__locals6.CS$<>8__locals6 = CS$<>8__locals1;
				CS$<>8__locals6.dataRowRange = CS$<>8__locals6.CS$<>8__locals6.area.Span.Vertical;
				if (notHeaderRowIdxOpt.HasValue)
				{
					CS$<>8__locals6.dataRowRange = new Range<TableUnit>(notHeaderRowIdxOpt.Value, CS$<>8__locals6.dataRowRange.Max);
				}
				CS$<>8__locals6.mostlyEmptyRowsSeen = 0;
				CS$<>8__locals6.sequentialEmptyRowsSeen = 0;
				CS$<>8__locals6.emptyRowsSeen = 0;
				foreach (int num4 in CS$<>8__locals6.dataRowRange.AsEnumerable)
				{
					if (this._rowNonNullPatterns[num4] == 0UL)
					{
						num3 = CS$<>8__locals6.mostlyEmptyRowsSeen;
						CS$<>8__locals6.mostlyEmptyRowsSeen = num3 + 1;
						num3 = CS$<>8__locals6.emptyRowsSeen;
						CS$<>8__locals6.emptyRowsSeen = num3 + 1;
					}
					else if ((this._rowNonNullPatterns[num4] & 18446744073709551614UL) == 0UL)
					{
						num3 = CS$<>8__locals6.mostlyEmptyRowsSeen;
						CS$<>8__locals6.mostlyEmptyRowsSeen = num3 + 1;
						CS$<>8__locals6.sequentialEmptyRowsSeen = Math.Max(CS$<>8__locals6.sequentialEmptyRowsSeen, CS$<>8__locals6.emptyRowsSeen);
						CS$<>8__locals6.emptyRowsSeen = 0;
					}
					else
					{
						if (CS$<>8__locals6.<HeaderScore>g__TooManyEmptyRows|43())
						{
							break;
						}
						CS$<>8__locals6.mostlyEmptyRowsSeen = 0;
						CS$<>8__locals6.emptyRowsSeen = 0;
						CS$<>8__locals6.sequentialEmptyRowsSeen = 0;
					}
				}
				if (CS$<>8__locals6.<HeaderScore>g__TooManyEmptyRows|43())
				{
					num -= 9.0;
				}
				CS$<>8__locals6.mask = ~((1UL << CS$<>8__locals6.CS$<>8__locals6.area.Span.Left + 3) - 1UL) & ((1UL << CS$<>8__locals6.CS$<>8__locals6.area.Span.Right + 1) - 1UL);
				int num5 = CS$<>8__locals6.CS$<>8__locals6.area.EnumerateRows(true, false, false, Derivative.Increasing).Count((Record<int, IEnumerable<ISpreadsheetCell>> r) => CS$<>8__locals6.dataRowRange.Contains(r.Item1) && r.Item2.Any<ISpreadsheetCell>());
				CS$<>8__locals6.majorityCount = num5 / 2 + 1;
				ulong num6 = CS$<>8__locals6.CS$<>8__locals6.area.Span.Horizontal.AsEnumerable.Select(delegate(int col)
				{
					if (!CS$<>8__locals6.CS$<>8__locals6.area.EnumerateCells(Axis.Vertical, true, false, false, Derivative.Increasing, new int?(col)).HasAtLeast(CS$<>8__locals6.majorityCount))
					{
						return 0UL;
					}
					return 1UL << col;
				}).Aggregate((ulong a, ulong b) => a | b) & CS$<>8__locals6.mask;
				if (border2 != null)
				{
					Range<TableUnit> range = new Range<TableUnit>(CS$<>8__locals6.CS$<>8__locals6.area.Span.Top, border2.Line.Position - 1);
					if (this._rowNonNullPatterns[range.Max] == 0UL)
					{
						range = range.Expand(-1, Derivative.Increasing);
					}
					if (range.Size() < 7)
					{
						if ((from y in range.AsEnumerable
							select CS$<>8__locals6.CS$<>8__locals6.<>4__this._rowNonNullPatterns[y] & CS$<>8__locals6.mask into pattern
							where pattern > 0UL
							select pattern).Windowed<ulong>().Any2((ulong a, ulong b) => (a & ~b) > 0UL))
						{
							num -= 11.0;
						}
						if ((num6 & ~((this._rowNonNullPatterns[range.Max] & CS$<>8__locals6.mask) != 0UL)) != 0UL)
						{
							num -= 13.0;
						}
					}
				}
			}
			if (notHeaderRowIdxOpt.HasValue)
			{
				num += 1.0;
			}
			else if (CS$<>8__locals1.area.Span.Top > 0)
			{
				if ((from pattern in this._cellPatterns.Row(CS$<>8__locals1.area.Span.Top, CS$<>8__locals1.area.Span)
					where pattern != SpreadsheetPatterns.DataPattern.Blank && pattern != SpreadsheetPatterns.DataPattern.Year && pattern != SpreadsheetPatterns.DataPattern.NonAlphanumeric && pattern != SpreadsheetPatterns.DataPattern.DateTime
					select pattern).SkipWhile((SpreadsheetPatterns.DataPattern pattern) => pattern == SpreadsheetPatterns.DataPattern.Other).AllOrElseCompute((SpreadsheetPatterns.DataPattern pattern) => pattern == SpreadsheetPatterns.DataPattern.Numeric, () => false) && CS$<>8__locals1.area.Span.Horizontal.AsEnumerable.Any((int x) => CS$<>8__locals1.<>4__this._cellPatterns[x, CS$<>8__locals1.area.Span.Top] == SpreadsheetPatterns.DataPattern.Numeric && (CS$<>8__locals1.<>4__this._cellPatterns[x, CS$<>8__locals1.area.Span.Top - 1] != SpreadsheetPatterns.DataPattern.Blank || (CS$<>8__locals1.area.Span.Top > 1 && CS$<>8__locals1.<>4__this._cellPatterns[x, CS$<>8__locals1.area.Span.Top - 2] != SpreadsheetPatterns.DataPattern.Blank))))
				{
					num -= 10.0;
				}
			}
			if (cellSignature != null)
			{
				SpreadsheetPatterns.CellSignature? cellSignature2 = cellSignature;
				SpreadsheetPatterns.CellSignature cellSignature3 = SpreadsheetPatterns.CellSignature.None;
				if (!((cellSignature2.GetValueOrDefault() == cellSignature3) & (cellSignature2 != null)))
				{
					return num;
				}
			}
			if ((!notHeaderRowIdxOpt.HasValue || notHeaderRowIdxOpt.Value >= CS$<>8__locals1.area.Span.Top + 2) && (from onlyCell in CS$<>8__locals1.area.EnumerateRow(CS$<>8__locals1.area.Span.Top, true, true, true).MaybeOnly<ISpreadsheetCell>()
				select onlyCell.Span.Left == CS$<>8__locals1.area.Span.Left).OrElse(false))
			{
				num -= 10.0;
			}
			return num;
		}

		// Token: 0x0600653E RID: 25918 RVA: 0x00148A50 File Offset: 0x00146C50
		private double HeaderInMiddleScore(SpreadsheetArea area, Optional<int> notHeaderRowIdxOpt)
		{
			SpreadsheetPatterns.<>c__DisplayClass18_0 CS$<>8__locals1 = new SpreadsheetPatterns.<>c__DisplayClass18_0();
			CS$<>8__locals1.area = area;
			CS$<>8__locals1.<>4__this = this;
			double num = 0.0;
			CS$<>8__locals1.notHeaderIdx = notHeaderRowIdxOpt.OrElse(CS$<>8__locals1.area.Span.Top + 1);
			CS$<>8__locals1.belowHeaderBounds = CS$<>8__locals1.area.Span.With(Direction.Up, notHeaderRowIdxOpt.OrElse(CS$<>8__locals1.area.Span.Top + 1));
			CS$<>8__locals1.headerRanges = (from range in CS$<>8__locals1.area.EnumerateRows(true, false, true, Derivative.Increasing).TakeWhile((Record<int, IEnumerable<ISpreadsheetCell>> r) => r.Item1 < CS$<>8__locals1.notHeaderIdx).SelectMany2((int idx, IEnumerable<ISpreadsheetCell> row) => row.Select((ISpreadsheetCell cell) => cell.Span.Horizontal))
				where range.Size() > 1
				select range).Distinct<Range<TableUnit>>().ToList<Range<TableUnit>>();
			if (CS$<>8__locals1.area.Spreadsheet.FreezePaneSize == null)
			{
				if ((from mergedSpan in CS$<>8__locals1.area.Spreadsheet.MergedCells
					where mergedSpan.Width() > 1 && CS$<>8__locals1.belowHeaderBounds.Contains(mergedSpan.Corner(Ordinal.TopLeft)) && !CS$<>8__locals1.headerRanges.Any((Range<TableUnit> headerRange) => headerRange.Contains(mergedSpan.Horizontal))
					group mergedSpan by mergedSpan.Top).Any(delegate(IGrouping<int, Bounds<TableUnit>> g)
				{
					if (!g.HasAtLeast(2))
					{
						Optional<Bounds<TableUnit>> optional = g.MaybeOnly<Bounds<TableUnit>>();
						Func<Bounds<TableUnit>, bool> func;
						if ((func = CS$<>8__locals1.<>9__14) == null)
						{
							func = (CS$<>8__locals1.<>9__14 = (Bounds<TableUnit> only) => only.Horizontal.Contains(CS$<>8__locals1.area.Span.Horizontal));
						}
						return optional.Select(func).OrElse(false);
					}
					return true;
				}))
				{
					num -= 79.0;
				}
			}
			if (!notHeaderRowIdxOpt.HasValue)
			{
				return num;
			}
			CS$<>8__locals1.bottomHeaderRowIdx = new Range<TableUnit>(CS$<>8__locals1.area.Span.Top, notHeaderRowIdxOpt.Value - 1).AsReverseEnumerable.First((int rowIdx) => CS$<>8__locals1.<>4__this._cellPatterns.Row(rowIdx, CS$<>8__locals1.area.Span).Any((SpreadsheetPatterns.DataPattern pattern) => pattern != SpreadsheetPatterns.DataPattern.Blank));
			SpreadsheetPatterns.DataPattern dataPattern = Enumerable.Range(CS$<>8__locals1.area.Span.Top, CS$<>8__locals1.bottomHeaderRowIdx - CS$<>8__locals1.area.Span.Top + 1).SelectMany((int headerRowIdx) => from p in CS$<>8__locals1.<>4__this._cellPatterns.Row(headerRowIdx, CS$<>8__locals1.area.Span).Skip(1)
				where p != SpreadsheetPatterns.DataPattern.Blank
				select p).Aggregate((SpreadsheetPatterns.DataPattern)0, (SpreadsheetPatterns.DataPattern a, SpreadsheetPatterns.DataPattern b) => a | b);
			if ((dataPattern & ~(SpreadsheetPatterns.DataPattern.Other | SpreadsheetPatterns.DataPattern.Year | SpreadsheetPatterns.DataPattern.DateTime)) != (SpreadsheetPatterns.DataPattern)0)
			{
				return num;
			}
			Dictionary<int, SpreadsheetPatterns.DataPattern> dictionary = CS$<>8__locals1.area.Span.Horizontal.AsEnumerable.ToDictionary((int i) => i, (int colIdx) => (from rowIdx in Enumerable.Range(CS$<>8__locals1.area.Span.Top, CS$<>8__locals1.bottomHeaderRowIdx - CS$<>8__locals1.area.Span.Top + 1)
				select CS$<>8__locals1.<>4__this._cellPatterns[colIdx, rowIdx] into pattern
				where pattern != SpreadsheetPatterns.DataPattern.Blank
				select pattern).Aggregate((SpreadsheetPatterns.DataPattern)0, (SpreadsheetPatterns.DataPattern a, SpreadsheetPatterns.DataPattern b) => a | b));
			SpreadsheetPatterns.CellSignature? cellSignature = CS$<>8__locals1.<HeaderInMiddleScore>g__RowSignature|11(CS$<>8__locals1.bottomHeaderRowIdx);
			if (cellSignature != null)
			{
				SpreadsheetPatterns.CellSignature valueOrDefault = cellSignature.GetValueOrDefault();
				SpreadsheetPatterns.CellSignature cellSignature2 = valueOrDefault & SpreadsheetPatterns.CellSignature.Fonts;
				foreach (int num2 in CS$<>8__locals1.area.Span.Horizontal.AsEnumerable)
				{
					if (this._isColumnNumeric[num2])
					{
						bool flag = false;
						for (int j = CS$<>8__locals1.bottomHeaderRowIdx + 2; j <= CS$<>8__locals1.area.Span.Bottom; j++)
						{
							SpreadsheetPatterns.DataPattern dataPattern2 = this._cellPatterns[num2, j];
							if (dataPattern2 == SpreadsheetPatterns.DataPattern.Numeric || dataPattern2 == SpreadsheetPatterns.DataPattern.Error)
							{
								flag = true;
							}
							else if (flag && this._cellPatterns[num2, j - 1] == SpreadsheetPatterns.DataPattern.Blank && (dataPattern2 & dataPattern) != (SpreadsheetPatterns.DataPattern)0)
							{
								SpreadsheetPatterns.CellSignature? cellSignature3 = CS$<>8__locals1.<HeaderInMiddleScore>g__RowSignature|11(j);
								cellSignature = cellSignature3;
								SpreadsheetPatterns.CellSignature cellSignature4 = valueOrDefault;
								if (!((cellSignature.GetValueOrDefault() == cellSignature4) & (cellSignature != null)))
								{
									if (cellSignature3 == null)
									{
										goto IL_03E7;
									}
									cellSignature = cellSignature3 & cellSignature2;
									cellSignature4 = SpreadsheetPatterns.CellSignature.None;
									if ((cellSignature.GetValueOrDefault() == cellSignature4) & (cellSignature != null))
									{
										goto IL_03E7;
									}
								}
								if ((dictionary[num2] & dataPattern2) != (SpreadsheetPatterns.DataPattern)0)
								{
									return num - 89.0;
								}
							}
							IL_03E7:;
						}
					}
				}
				return num;
			}
			return num;
		}

		// Token: 0x0600653F RID: 25919 RVA: 0x00148EA0 File Offset: 0x001470A0
		private double TitleInMiddleScore(SpreadsheetArea area, Optional<int> notHeaderRowIdxOpt)
		{
			double num = 0.0;
			int numHeaderRows = notHeaderRowIdxOpt.Select((int idx) => idx - area.Span.Top).OrElse(0);
			if (area.Span.Height() < numHeaderRows + 5)
			{
				return num;
			}
			SpreadsheetPatterns.CellSignature cellSignature;
			if (!notHeaderRowIdxOpt.HasValue)
			{
				cellSignature = SpreadsheetPatterns.CellSignature.None;
			}
			else
			{
				cellSignature = Enumerable.Range(area.Span.Top, numHeaderRows).SelectMany((int rowIdx) => this._cellSignatures.Row(rowIdx, area.Span).Skip(1)).Aggregate((SpreadsheetPatterns.CellSignature a, SpreadsheetPatterns.CellSignature b) => a | b);
			}
			SpreadsheetPatterns.CellSignature cellSignature2 = cellSignature;
			SpreadsheetPatterns.CellSignature cellSignature3 = ~(Enumerable.Range(notHeaderRowIdxOpt.OrElse(area.Span.Top), 5).SelectMany((int rowIdx) => this._cellSignatures.Row(rowIdx, area.Span).Skip((numHeaderRows == 0) ? 1 : 0)).Aggregate((SpreadsheetPatterns.CellSignature a, SpreadsheetPatterns.CellSignature b) => a | b) | cellSignature2 | SpreadsheetPatterns.CellSignature.HasFormula | SpreadsheetPatterns.CellSignature.Blank);
			foreach (int num2 in Enumerable.Range(0, area.Span.Left + 1))
			{
				int rowIdx2;
				int rowIdx;
				for (rowIdx = area.Span.Top + numHeaderRows + 5; rowIdx <= area.Span.Bottom; rowIdx = rowIdx2 + 1)
				{
					if (this._cellSignatures[num2, rowIdx - 1] == SpreadsheetPatterns.CellSignature.Blank && (this._cellSignatures[num2, rowIdx] & cellSignature3) != SpreadsheetPatterns.CellSignature.None && (this._cellPatterns.Row(rowIdx, area.Span).Skip(1).Contains(SpreadsheetPatterns.DataPattern.Other) || area.Span.Horizontal.AsEnumerable.Skip(1).Any((int colIdx) => this._isColumnNumeric[colIdx] && this._cellPatterns[colIdx, rowIdx] == SpreadsheetPatterns.DataPattern.Year)))
					{
						num -= 10.0;
					}
					rowIdx2 = rowIdx;
				}
			}
			return num;
		}

		// Token: 0x06006540 RID: 25920 RVA: 0x00149168 File Offset: 0x00147368
		private double BottomRowScore(SpreadsheetArea area, IReadOnlyList<BorderGroup> borderGroupsInArea)
		{
			double num = 0.0;
			Optional<ISpreadsheetCell> optional = area.EnumerateCells(Axis.Horizontal, true, false, false, Derivative.Increasing, new int?(area.Span.Bottom)).Select(delegate(ISpreadsheetCell cell)
			{
				IMergedSpreadsheetCellProxy mergedSpreadsheetCellProxy = cell as IMergedSpreadsheetCellProxy;
				return ((mergedSpreadsheetCellProxy != null) ? mergedSpreadsheetCellProxy.OriginalCell : null) ?? cell;
			}).Distinct<ISpreadsheetCell>()
				.MaybeOnly<ISpreadsheetCell>();
			if (optional.HasValue)
			{
				Bounds<TableUnit> bottomCellSpan = optional.Value.Span;
				if (!borderGroupsInArea.Any((BorderGroup group) => group.Span.Contains(bottomCellSpan) && group.Span.Top != bottomCellSpan.Top))
				{
					num -= 15.0;
				}
			}
			return num;
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x00149210 File Offset: 0x00147410
		private double FreezePaneScore(SpreadsheetArea area)
		{
			SpreadsheetPatterns.<>c__DisplayClass21_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.area = area;
			double num = 0.0;
			if (this._spreadsheet.FreezePaneSize != null)
			{
				if (this._spreadsheet.FreezePaneSize.X > 0 && CS$<>8__locals1.area.Span.Left >= this._spreadsheet.FreezePaneSize.X)
				{
					num -= 100.0;
				}
				if (this._spreadsheet.FreezePaneSize.Y > 0)
				{
					if (this._rowNonNullPatterns[this._spreadsheet.FreezePaneSize.Y - 1] == 0UL && (this._spreadsheet.FreezePaneSize.Y < 2 || this._rowNonNullPatterns[this._spreadsheet.FreezePaneSize.Y - 2] == 0UL))
					{
						return num;
					}
					if (CS$<>8__locals1.area.Span.Top >= this._spreadsheet.FreezePaneSize.Y)
					{
						num -= 100.0;
					}
					else if (this.<FreezePaneScore>g__HasNonEmptyAboveEmptyHeader|21_0(ref CS$<>8__locals1))
					{
						num -= 5.0;
					}
				}
			}
			return num;
		}

		// Token: 0x06006542 RID: 25922 RVA: 0x00149344 File Offset: 0x00147544
		private double MergedThroughEdgeScore(SpreadsheetArea area, out bool returnImmediately)
		{
			returnImmediately = false;
			foreach (Axis axis in AxisUtilities.Axes)
			{
				Direction edgeDirection = axis.Perpendicular().DecreasingDirection();
				int edge = area.Span[edgeDirection];
				if (area.EnumerateCells(axis, true, false, false, Derivative.Increasing, new int?(edge)).OfType<IMergedSpreadsheetCellProxy>().Any((IMergedSpreadsheetCellProxy cell) => cell.Span[edgeDirection] < edge))
				{
					returnImmediately = true;
					return -143.0;
				}
			}
			return 0.0;
		}

		// Token: 0x06006543 RID: 25923 RVA: 0x00149408 File Offset: 0x00147608
		private double BorderGroupsScore(SpreadsheetArea area, IReadOnlyList<BorderGroup> borderGroupsInArea, bool hasFullyMergedColumn, out bool returnImmediately)
		{
			if (borderGroupsInArea.Any((BorderGroup group) => group.Span.Bottom == area.Span.Bottom && this._borderGroupIsAbovePartialTable[group]))
			{
				returnImmediately = true;
				return -100.0;
			}
			if (borderGroupsInArea.Any((BorderGroup group) => area.Span.Equals(group.Span)) || this._trimmedBorderGroups.Contains(area.Span))
			{
				returnImmediately = true;
				double num;
				if (hasFullyMergedColumn && area.Span.Width() > 2)
				{
					if (area.EnumerateColumns(true, true, false, Derivative.Increasing).All((Record<int, IEnumerable<ISpreadsheetCell>> column) => column.Item2.Select(delegate(ISpreadsheetCell cell)
					{
						IMergedSpreadsheetCellProxy mergedSpreadsheetCellProxy = cell as IMergedSpreadsheetCellProxy;
						if (mergedSpreadsheetCellProxy == null)
						{
							return cell;
						}
						return mergedSpreadsheetCellProxy.OriginalCell;
					}).Distinct<ISpreadsheetCell>().HasAtLeast(2)))
					{
						num = (double)14;
						goto IL_00B9;
					}
				}
				num = (double)0;
				IL_00B9:
				return num;
			}
			Func<Vector<TableUnit>, bool> <>9__8;
			Func<AxisAlignedLine<TableUnit>, bool> <>9__5;
			if (borderGroupsInArea.Any(delegate(BorderGroup group)
			{
				if (!group.Span.Contains(area.Span))
				{
					return false;
				}
				ISpreadsheetCell spreadsheetCell = this._spreadsheet[group.Span.Left, group.Span.Top];
				Range<TableUnit> range = group.Span.Horizontal;
				if (range.Equals((spreadsheetCell != null) ? new Range<TableUnit>?(spreadsheetCell.Span.Horizontal) : null))
				{
					return false;
				}
				Directed<AxisAlignedLine<TableUnit>> edges = group.Span.Edges;
				Func<AxisAlignedLine<TableUnit>, bool> func;
				if ((func = <>9__5) == null)
				{
					func = (<>9__5 = delegate(AxisAlignedLine<TableUnit> edge)
					{
						IEnumerable<Vector<TableUnit>> asEnumerable = edge.AsEnumerable;
						Func<Vector<TableUnit>, bool> func2;
						if ((func2 = <>9__8) == null)
						{
							func2 = (<>9__8 = delegate(Vector<TableUnit> pos)
							{
								ISpreadsheetCell spreadsheetCell2 = this._spreadsheet[pos.X, pos.Y];
								return string.IsNullOrWhiteSpace((spreadsheetCell2 != null) ? spreadsheetCell2.AsString : null);
							});
						}
						return asEnumerable.All(func2);
					});
				}
				if (edges.Any(func))
				{
					return false;
				}
				if (string.IsNullOrWhiteSpace((spreadsheetCell != null) ? spreadsheetCell.AsString : null))
				{
					return true;
				}
				range = area.Span.Horizontal;
				if (!range.AsEnumerable.Skip(1).All(delegate(int x)
				{
					ISpreadsheetCell spreadsheetCell3 = this._spreadsheet[x, group.Span.Top];
					return string.IsNullOrWhiteSpace((spreadsheetCell3 != null) ? spreadsheetCell3.AsString : null);
				}))
				{
					range = area.Span.Horizontal;
					if (!range.AsEnumerable.Skip(1).All(delegate(int x)
					{
						ISpreadsheetCell spreadsheetCell4 = this._spreadsheet[x, group.Span.Bottom];
						return string.IsNullOrWhiteSpace((spreadsheetCell4 != null) ? spreadsheetCell4.AsString : null);
					}))
					{
						return true;
					}
				}
				return false;
			}))
			{
				returnImmediately = false;
				return -7.0;
			}
			returnImmediately = false;
			return 0.0;
		}

		// Token: 0x06006544 RID: 25924 RVA: 0x00149500 File Offset: 0x00147700
		private double BordersPastEdgeScore(SpreadsheetArea area)
		{
			Range<TableUnit> borderHorizontal = area.Span.Horizontal.Expand(1, Derivative.Increasing);
			IReadOnlyList<Border> readOnlyList = (from b in this._spreadsheet.Borders.Borders.Horizontal.Where2((int y, IReadOnlyList<Border> _) => area.Span.Top <= y && y <= area.Span.Bottom + 1).SelectMany2((int k, IReadOnlyList<Border> v) => v)
				where area.Span.Horizontal.Overlaps(b.Line.Range) && !borderHorizontal.Contains(b.Line.Range)
				select b).ToList<Border>();
			if (readOnlyList.Any<Border>())
			{
				if (Range<TableUnit>.Join(readOnlyList.Select((Border b) => b.Line.Range)).Expand(-1, Derivative.Increasing).Subtract(area.Span.Horizontal)
					.SelectMany((Range<TableUnit> r) => r.AsEnumerable)
					.Any((int x) => area.Span.Vertical.AsEnumerable.Any(delegate(int y)
					{
						ISpreadsheetCell spreadsheetCell = this._spreadsheet[x, y];
						return !string.IsNullOrEmpty((spreadsheetCell != null) ? spreadsheetCell.AsString : null) && !(this._spreadsheet[x, y] is IMergedSpreadsheetCellProxy);
					})))
				{
					return -9.0;
				}
			}
			return 0.0;
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x0014964C File Offset: 0x0014784C
		private double CellMergedInBothDirectionsScore(SpreadsheetArea area, Optional<int> notHeaderRowIdxOpt)
		{
			if (area.Spreadsheet.MergedCells.Any((Bounds<TableUnit> merged) => merged.Width() > 1 && (merged.Height() > 1 || (merged.Left == area.Span.Left && merged.Right > area.Span.Right)) && merged.Overlaps(area.Span) && notHeaderRowIdxOpt.Select((int notHeaderRowIdx) => merged.Top >= notHeaderRowIdx).OrElse(true) && (merged.Corner(Ordinal.TopLeft) != area.Span.Corner(Ordinal.TopLeft) || merged.Bottom >= area.Span.Bottom)))
			{
				return -108.0;
			}
			return 0.0;
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x001496A4 File Offset: 0x001478A4
		private double FooterScore(SpreadsheetArea area)
		{
			double num = 0.0;
			foreach (ISpreadsheetCell spreadsheetCell in area.EnumerateCells(Axis.Vertical, true, true, true, Derivative.Increasing, new int?(area.Span.Left)))
			{
				string asString = spreadsheetCell.AsString;
				if (asString != null && asString.Length > 0 && asString[0] == '*' && !area.EnumerateCells(Axis.Horizontal, true, true, true, Derivative.Increasing, new int?(spreadsheetCell.Span.Top)).HasAtLeast(2))
				{
					num -= 5.0;
				}
			}
			if (this._cellPatterns.Row(area.Span.Bottom, area.Span).Contains(SpreadsheetPatterns.DataPattern.Blank))
			{
				bool flag = this._cellPatterns.Row(area.Span.Bottom, area.Span).Skip(2).All((SpreadsheetPatterns.DataPattern pattern) => pattern == SpreadsheetPatterns.DataPattern.Blank);
				using (IEnumerator<ISpreadsheetCell> enumerator = area.EnumerateRow(area.Span.Bottom, true, false, true).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ISpreadsheetCell cell = enumerator.Current;
						if ((flag || cell.Span.Left > area.Span.Left + 1) && area.EnumerateColumn(cell.Span.Left, true, false, false).SkipLast(1).All((ISpreadsheetCell other) => other.Span.Horizontal != cell.Span.Horizontal))
						{
							num -= 20.0;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06006547 RID: 25927 RVA: 0x001498A4 File Offset: 0x00147AA4
		private double FormulaColumnScore(SpreadsheetArea area)
		{
			double num = 0.0;
			RectangularArray<ISpreadsheetCell> rectangularArray = area.Spreadsheet.PrunedCells(true, false, true);
			Func<ISpreadsheetCell, bool> <>9__1;
			foreach (Record<int, IEnumerable<ISpreadsheetCell>> record in area.EnumerateColumns(true, false, true, Derivative.Increasing))
			{
				int num2;
				IEnumerable<ISpreadsheetCell> enumerable;
				record.Deconstruct(out num2, out enumerable);
				int num3 = num2;
				IEnumerable<ISpreadsheetCell> enumerable2 = enumerable;
				ISpreadsheetCell spreadsheetCell = enumerable2.FirstOrDefault<ISpreadsheetCell>();
				if (spreadsheetCell != null)
				{
					if (spreadsheetCell.Formula != null)
					{
						IEnumerable<ISpreadsheetCell> enumerable3 = rectangularArray.Column(num3);
						Func<ISpreadsheetCell, bool> func;
						if ((func = <>9__1) == null)
						{
							func = (<>9__1 = (ISpreadsheetCell cell) => cell != null && cell.Span.Bottom < area.Span.Top);
						}
						ISpreadsheetCell spreadsheetCell2 = enumerable3.LastOrDefault(func);
						if (spreadsheetCell2 != null && spreadsheetCell2.Formula != null && SpreadsheetPatterns.<FormulaColumnScore>g__ColumnGenericFormula|28_0(spreadsheetCell2.Formula) == SpreadsheetPatterns.<FormulaColumnScore>g__ColumnGenericFormula|28_0(spreadsheetCell.Formula))
						{
							num -= 5.0;
						}
					}
					ISpreadsheetCell spreadsheetCell3 = enumerable2.Last<ISpreadsheetCell>();
					if (spreadsheetCell3.Formula != null)
					{
						ISpreadsheetCell spreadsheetCell4 = rectangularArray.Column(num3).Skip(area.Span.Bottom + 1).FirstOrDefault((ISpreadsheetCell cell) => cell != null);
						if (spreadsheetCell4 != null && spreadsheetCell4.Formula != null && SpreadsheetPatterns.<FormulaColumnScore>g__ColumnGenericFormula|28_0(spreadsheetCell4.Formula) == SpreadsheetPatterns.<FormulaColumnScore>g__ColumnGenericFormula|28_0(spreadsheetCell3.Formula))
						{
							num -= 5.0;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06006548 RID: 25928 RVA: 0x00149A5C File Offset: 0x00147C5C
		private double NumericColumnScore(SpreadsheetArea area)
		{
			double num = 0.0;
			foreach (Record<int, IEnumerable<ISpreadsheetCell>> record in area.EnumerateColumns(true, false, false, Derivative.Increasing))
			{
				int num2;
				IEnumerable<ISpreadsheetCell> enumerable;
				record.Deconstruct(out num2, out enumerable);
				int num3 = num2;
				IEnumerable<ISpreadsheetCell> enumerable2 = enumerable;
				if (this._isColumnNumeric[num3])
				{
					if (enumerable2.Any((ISpreadsheetCell cell) => cell.Span.Width() == 1))
					{
						if (this._cellPatterns.Column(num3, area.Span).All((SpreadsheetPatterns.DataPattern pattern) => (pattern & SpreadsheetPatterns.DataPattern.LikelyNumeric) == (SpreadsheetPatterns.DataPattern)0))
						{
							if (!area.EnumerateColumn(num3, false, false, false).Any((ISpreadsheetCell cell) => cell != null && cell.StyleInfo.Fill.IsActualFill))
							{
								num -= 5.0;
								continue;
							}
						}
						ISpreadsheetCell spreadsheetCell = enumerable2.FirstOrDefault<ISpreadsheetCell>();
						if (spreadsheetCell != null)
						{
							if (this._cellPatterns[num3, spreadsheetCell.Span.Top] == SpreadsheetPatterns.DataPattern.Numeric)
							{
								Optional<Record<int, SpreadsheetPatterns.DataPattern>> optional = this._cellPatterns.Column(num3).Enumerate<SpreadsheetPatterns.DataPattern>().Take(area.Span.Top)
									.MaybeLast((Record<int, SpreadsheetPatterns.DataPattern> r) => r.Item2 != SpreadsheetPatterns.DataPattern.Blank);
								if (optional.HasValue && optional.Value.Item2 == SpreadsheetPatterns.DataPattern.Numeric && this._cellSignatures[num3, optional.Value.Item1] == this._cellSignatures[num3, spreadsheetCell.Span.Top])
								{
									num -= 5.0;
								}
							}
							ISpreadsheetCell spreadsheetCell2 = enumerable2.Last<ISpreadsheetCell>();
							Optional<Record<int, SpreadsheetPatterns.DataPattern>> optional2 = this._cellPatterns.Column(num3).Enumerate<SpreadsheetPatterns.DataPattern>().Skip(area.Spreadsheet[num3, area.Span.Bottom].Span.Bottom + 1)
								.MaybeFirst((Record<int, SpreadsheetPatterns.DataPattern> r) => r.Item2 != SpreadsheetPatterns.DataPattern.Blank);
							if (optional2.HasValue && optional2.Value.Item2 == SpreadsheetPatterns.DataPattern.Numeric && this._cellSignatures[num3, optional2.Value.Item1] == this._cellSignatures[num3, spreadsheetCell2.Span.Top])
							{
								num -= 5.0;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06006549 RID: 25929 RVA: 0x00149D48 File Offset: 0x00147F48
		private double EmptyColumnsScore(SpreadsheetArea area)
		{
			if (!area.Span.Horizontal.AsEnumerable.Select((int colIdx) => this._cellPatterns.Column(colIdx, area.Span).All((SpreadsheetPatterns.DataPattern pattern) => pattern == SpreadsheetPatterns.DataPattern.Blank)).SplitRuns(null).Any((IGrouping<bool, bool> run) => run.Key && run.HasAtLeast(2)))
			{
				return 0.0;
			}
			if (area.Span.Left > 1)
			{
				return -5.0;
			}
			return -3.0;
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x00149DF4 File Offset: 0x00147FF4
		private double AnnotationsColumnScore(SpreadsheetArea area)
		{
			if (this._cellPatterns.Column(area.Span.Right, area.Span).All((SpreadsheetPatterns.DataPattern pattern) => pattern == SpreadsheetPatterns.DataPattern.Blank || pattern == SpreadsheetPatterns.DataPattern.NonAlphanumeric))
			{
				return -2.0;
			}
			return 0.0;
		}

		// Token: 0x0600654B RID: 25931 RVA: 0x00149E5C File Offset: 0x0014805C
		private double MergedFullHeightScore(SpreadsheetArea area)
		{
			if (area.EnumerateRow(area.Span.Top, true, false, true).Any((ISpreadsheetCell c) => c.Span.Bottom >= area.Span.Bottom))
			{
				return -10.0;
			}
			return 0.0;
		}

		// Token: 0x0600654C RID: 25932 RVA: 0x00149EC0 File Offset: 0x001480C0
		private double KeyValuePairScore(SpreadsheetArea area)
		{
			IList<ISpreadsheetCell> list;
			IList<ISpreadsheetCell> list2;
			area.EnumerateCells(Axis.Horizontal, true, true, true, Derivative.Increasing, null).PartitionByPredicate((ISpreadsheetCell cell) => cell.AsString.EndsWith(":", StringComparison.Ordinal), out list, out list2);
			if (list.Count > 3)
			{
				return -110.0;
			}
			return 0.0;
		}

		// Token: 0x0600654D RID: 25933 RVA: 0x00149F28 File Offset: 0x00148128
		internal double ScoreArea(SpreadsheetArea area, bool hasFullyMergedColumn)
		{
			double num = 0.0;
			if (area.Span.Height() > 2 && area.Span.Width() > 1 && area.IncludedColumnsEnumerable.HasAtLeast(2))
			{
				if (area.EnumerateRows(true, false, true, Derivative.Increasing).Where2((int rowIdx, IEnumerable<ISpreadsheetCell> row) => row.Any<ISpreadsheetCell>()).HasAtLeast(3))
				{
					num += this.FreezePaneScore(area);
					IReadOnlyList<BorderGroup> readOnlyList = area.Spreadsheet.Borders.BorderGroups.Where((BorderGroup group) => area.Span.Overlaps(group.Span)).ToList<BorderGroup>();
					bool flag;
					num += this.BorderGroupsScore(area, readOnlyList, hasFullyMergedColumn, out flag);
					Optional<int> optional;
					double num2 = this.HeaderScore(area, readOnlyList, out optional);
					num += this.CellMergedInBothDirectionsScore(area, optional);
					if (flag)
					{
						return num + Math.Max(0.0, num2);
					}
					num += num2;
					num += this.MergedThroughEdgeScore(area, out flag);
					if (flag)
					{
						return num;
					}
					num += this.MergedFullHeightScore(area);
					num += this.BordersPastEdgeScore(area);
					num += this.HeaderInMiddleScore(area, optional);
					num += this.TitleInMiddleScore(area, optional);
					num += this.BottomRowScore(area, readOnlyList);
					num += this.FooterScore(area);
					num += this.FormulaColumnScore(area);
					num += this.NumericColumnScore(area);
					num += this.EmptyColumnsScore(area);
					return num + this.AnnotationsColumnScore(area);
				}
			}
			return -200.0;
		}

		// Token: 0x0600654E RID: 25934 RVA: 0x0014A110 File Offset: 0x00148310
		private SpreadsheetPatterns.CellType ExtractNumberFormatCodes(string formatCode, string cellString)
		{
			double num;
			bool flag;
			if (!string.IsNullOrWhiteSpace(cellString) && double.TryParse(cellString, out num))
			{
				flag = (from ch in cellString.MaybeLastChar()
					select ch != '.').OrElse(true);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (formatCode == "General" || formatCode == null)
			{
				if (!flag2)
				{
					return SpreadsheetPatterns.CellType.General;
				}
				return SpreadsheetPatterns.CellType.Number;
			}
			else
			{
				HashSet<char> hashSet = new HashSet<char>();
				char c = '\0';
				for (int i = 0; i < formatCode.Length; i++)
				{
					if (c != '\0' && formatCode[i] == c)
					{
						c = '\0';
					}
					else if (c == '\0')
					{
						if (formatCode[i] == '[')
						{
							c = ']';
						}
						else if (formatCode[i] == '"')
						{
							c = '"';
						}
						else if (formatCode[i] == '\\')
						{
							i++;
						}
						else
						{
							hashSet.Add(char.ToLowerInvariant(formatCode[i]));
						}
					}
				}
				bool flag3 = hashSet.Contains('h') || hashSet.Contains('s');
				bool flag4 = hashSet.Contains('d') || hashSet.Contains('y') || (!flag3 && hashSet.Contains('m'));
				if (flag4 && flag3)
				{
					return SpreadsheetPatterns.CellType.DateTime;
				}
				if (flag4)
				{
					return SpreadsheetPatterns.CellType.Date;
				}
				if (flag3)
				{
					return SpreadsheetPatterns.CellType.Time;
				}
				if (!hashSet.Contains('#') && !hashSet.Contains('0'))
				{
					return SpreadsheetPatterns.CellType.General;
				}
				if (!string.IsNullOrWhiteSpace(cellString) && !flag2)
				{
					return SpreadsheetPatterns.CellType.General;
				}
				return SpreadsheetPatterns.CellType.Number;
			}
		}

		// Token: 0x0600654F RID: 25935 RVA: 0x0014A269 File Offset: 0x00148469
		private SpreadsheetPatterns.CellType ExtractNumberFormatCodes(ISpreadsheetCell cell)
		{
			string text;
			if (cell == null)
			{
				text = null;
			}
			else
			{
				ICellStyleInfo styleInfo = cell.StyleInfo;
				text = ((styleInfo != null) ? styleInfo.NumberFormat : null);
			}
			return this.ExtractNumberFormatCodes(text, (cell != null) ? cell.AsString : null);
		}

		// Token: 0x06006550 RID: 25936 RVA: 0x0014A298 File Offset: 0x00148498
		internal double ScoreTitleArea(SpreadsheetArea title, SpreadsheetArea table)
		{
			SpreadsheetPatterns.<>c__DisplayClass38_0 CS$<>8__locals1 = new SpreadsheetPatterns.<>c__DisplayClass38_0();
			CS$<>8__locals1.table = table;
			CS$<>8__locals1.title = title;
			CS$<>8__locals1.<>4__this = this;
			double num = 0.0;
			if (CS$<>8__locals1.title.Span.Vertical.Equals(CS$<>8__locals1.table.Span.Vertical))
			{
				return num - 900.0;
			}
			CS$<>8__locals1.trimmedStrings = (from str in CS$<>8__locals1.title.SectionAsStrings.ToEnumerable()
				where !string.IsNullOrWhiteSpace(str)
				select str.Trim()).ToList<string>();
			if (!CS$<>8__locals1.trimmedStrings.Any<string>())
			{
				return num - 1000.0;
			}
			CS$<>8__locals1.titleFont = (from cell in CS$<>8__locals1.title.Section.ToEnumerable()
				select cell.StyleInfo).Distinct(FontInfoEqualityComparer.Instance).OnlyOrDefault<ICellStyleInfo>();
			if (CS$<>8__locals1.titleFont == null)
			{
				return -100.0;
			}
			int num3;
			if (CS$<>8__locals1.titleFont.Underline)
			{
				bool flag;
				if (CS$<>8__locals1.titleFont.Color != null)
				{
					int? num2 = CS$<>8__locals1.titleFont.Color.Indexed;
					num3 = 12;
					if (!((num2.GetValueOrDefault() == num3) & (num2 != null)))
					{
						string rgb = CS$<>8__locals1.titleFont.Color.Rgb;
						flag = rgb != null && rgb.EndsWith("0000FF");
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					flag = false;
				}
				if (flag)
				{
					num -= 17.0;
				}
			}
			AxisAligned<string> alignment = CS$<>8__locals1.titleFont.Alignment;
			if (((alignment != null) ? alignment.Horizontal : null) == "right")
			{
				num -= 4.0;
			}
			Optional<int> optional = CS$<>8__locals1.table.Section.ToEnumerable().Collect(delegate(ISpreadsheetCell cell)
			{
				if (cell == null)
				{
					return null;
				}
				ICellStyleInfo styleInfo = cell.StyleInfo;
				if (styleInfo == null)
				{
					return null;
				}
				return styleInfo.FontSize;
			}).MaybeMin();
			if (optional.HasValue)
			{
				int value = optional.Value;
				int? num2 = CS$<>8__locals1.titleFont.FontSize;
				if ((value > num2.GetValueOrDefault()) & (num2 != null))
				{
					num -= 5.0;
				}
			}
			int num4 = CS$<>8__locals1.trimmedStrings.Sum((string str) => str.Length);
			if (num4 > 400)
			{
				num -= 10.0;
			}
			else if (num4 > 200)
			{
				num -= 5.0;
			}
			else if (num4 > 150)
			{
				num -= 1.0;
			}
			if (CS$<>8__locals1.title.SectionAsStrings.ToEnumerable().All(delegate(string str)
			{
				Func<char, bool> func;
				if ((func = SpreadsheetPatterns.<>O.<2>__IsLetter) == null)
				{
					func = (SpreadsheetPatterns.<>O.<2>__IsLetter = new Func<char, bool>(char.IsLetter));
				}
				return !str.Any(func);
			}))
			{
				if (!CS$<>8__locals1.title.SectionAsStrings.ToEnumerable().MaybeOnly<string>().Select(delegate(string str)
				{
					if (str.Length == 4)
					{
						Func<char, bool> func2;
						if ((func2 = SpreadsheetPatterns.<>O.<3>__IsDigit) == null)
						{
							func2 = (SpreadsheetPatterns.<>O.<3>__IsDigit = new Func<char, bool>(char.IsDigit));
						}
						return str.All(func2);
					}
					return false;
				})
					.OrElse(false))
				{
					num -= 100.0;
				}
			}
			if (CS$<>8__locals1.title.EnumerateCells(Axis.Horizontal, true, true, true, Derivative.Increasing, null).Select(new Func<ISpreadsheetCell, SpreadsheetPatterns.CellType>(this.ExtractNumberFormatCodes)).ConvertToHashSet<SpreadsheetPatterns.CellType>()
				.Any((SpreadsheetPatterns.CellType type) => type != SpreadsheetPatterns.CellType.General && type != SpreadsheetPatterns.CellType.Text && type != SpreadsheetPatterns.CellType.Date))
			{
				num -= 20.0;
			}
			num -= (double)(2 * (CS$<>8__locals1.title.Span.Width() - CS$<>8__locals1.title.IncludedColumnsEnumerable.Count<int>()));
			if (CS$<>8__locals1.table.Span.Overlaps(CS$<>8__locals1.title.Span) && !CS$<>8__locals1.table.Span.Contains(CS$<>8__locals1.title.Span))
			{
				num -= 10.0;
			}
			if (CS$<>8__locals1.title.Span.Top == 0 && CS$<>8__locals1.title.Span.Left == 0)
			{
				num -= 0.5;
			}
			IReadOnlyList<BorderGroup> readOnlyList = CS$<>8__locals1.table.Spreadsheet.Borders.BorderGroups.Where((BorderGroup group) => CS$<>8__locals1.table.Span.Overlaps(group.Span)).ToList<BorderGroup>();
			IReadOnlyList<BorderGroup> readOnlyList2 = CS$<>8__locals1.table.Spreadsheet.Borders.BorderGroups.Where((BorderGroup group) => CS$<>8__locals1.title.Span.Overlaps(group.Span)).ToList<BorderGroup>();
			if (!readOnlyList.Intersect(readOnlyList2).Any<BorderGroup>())
			{
				if (readOnlyList2.Any<BorderGroup>())
				{
					num -= 10.0;
				}
				else if (readOnlyList.Any<BorderGroup>())
				{
					num -= 1.0;
				}
			}
			Optional<int> optional2;
			this.HeaderScore(CS$<>8__locals1.table, readOnlyList, out optional2);
			CS$<>8__locals1.afterHeaderIdx = optional2.OrElse(CS$<>8__locals1.table.Span.Top + 1);
			bool flag2 = !CS$<>8__locals1.table.EnumerateCells(Axis.Horizontal, true, true, true, Derivative.Increasing, null).Any<ISpreadsheetCell>();
			bool flag3 = !CS$<>8__locals1.title.EnumerateCells(Axis.Horizontal, true, true, true, Derivative.Increasing, null).Any<ISpreadsheetCell>();
			if (flag3 != flag2)
			{
				num -= 100.0;
			}
			CS$<>8__locals1.prunedCells = CS$<>8__locals1.table.Spreadsheet.PrunedCells(true, !flag2, true);
			IReadOnlyList<int> readOnlyList3 = Enumerable.Range(CS$<>8__locals1.table.Span.Top, CS$<>8__locals1.afterHeaderIdx - CS$<>8__locals1.table.Span.Top).SelectMany(delegate(int headerRowIdx)
			{
				IReadOnlyList<string> headerStrings = base.<ScoreTitleArea>g__RowStrings|28(headerRowIdx).ToList<string>();
				if (!headerStrings.Collect<string>().Any<string>())
				{
					return Enumerable.Empty<int>();
				}
				return Enumerable.Range(0, CS$<>8__locals1.table.Spreadsheet.Size.Y).Where(delegate(int otherRowIdx)
				{
					if (!CS$<>8__locals1.table.Span.Vertical.Contains(otherRowIdx))
					{
						return !headerStrings.ZipWith(CS$<>8__locals1.<ScoreTitleArea>g__RowStrings|28(otherRowIdx)).Where2((string a, string b) => !object.Equals(a, b)).HasAtLeast(2);
					}
					return false;
				});
			}).ToList<int>();
			bool flag4 = CS$<>8__locals1.table.Span.Overlaps(CS$<>8__locals1.title.Span);
			if (!flag4)
			{
				Optional<Range<TableUnit>> optional3 = (from cell in CS$<>8__locals1.title.EnumerateCells(Axis.Horizontal, true, true, true, Derivative.Increasing, null)
					select cell.Span.Horizontal).Distinct<Range<TableUnit>>().MaybeOnly<Range<TableUnit>>();
				if (optional3.HasValue && optional3.Value.Size() > 1)
				{
					Range<TableUnit> value2 = optional3.Value;
					if (value2.Contains(CS$<>8__locals1.table.Span.Horizontal) && !readOnlyList3.Any<int>())
					{
						num += 2.0;
					}
					if (value2.Min < CS$<>8__locals1.table.Span.Left - 1)
					{
						num -= 4.0;
					}
					if (value2.Max > CS$<>8__locals1.table.Span.Right + 1)
					{
						num -= 4.0;
					}
				}
				double num5 = CS$<>8__locals1.table.Span.DistanceTo(CS$<>8__locals1.title.Span);
				num -= num5 * 0.1;
				if (num5 > 10.0)
				{
					num -= num5 * 0.5;
				}
				Optional<Range<TableUnit>> optional4 = CS$<>8__locals1.table.Span.Vertical.BetweenExclusive(CS$<>8__locals1.title.Span.Vertical);
				int num6 = 0;
				if (optional4.HasValue)
				{
					Bounds<TableUnit> between = new Bounds<TableUnit>(CS$<>8__locals1.table.Span.Horizontal, optional4.Value);
					num6 = between.Vertical.AsEnumerable.Count((int i) => CS$<>8__locals1.prunedCells.Row(i, between).Any((ISpreadsheetCell c) => ((c != null) ? c.AsString : null) != null));
					num -= 0.5 * (double)num6;
				}
				if (CS$<>8__locals1.title.Span.Right < CS$<>8__locals1.table.Span.Right && !CS$<>8__locals1.table.Span.Vertical.Contains(CS$<>8__locals1.title.Span.Vertical))
				{
					ISpreadsheet spreadsheet = CS$<>8__locals1.table.Spreadsheet;
					Range<TableUnit> vertical = CS$<>8__locals1.title.Span.Vertical;
					SpreadsheetArea spreadsheetArea = spreadsheet.Trim(new Bounds<TableUnit>(CS$<>8__locals1.table.Span.Horizontal.Subtract(CS$<>8__locals1.title.Span.Horizontal).Last<Range<TableUnit>>(), vertical), !flag2, true, true);
					if (spreadsheetArea != null)
					{
						if (spreadsheetArea.EnumerateCells(Axis.Horizontal, true, !flag3, true, Derivative.Increasing, null).Any((ISpreadsheetCell cell) => cell.Formula != null || CS$<>8__locals1.<>4__this.ExtractNumberFormatCodes(cell) > SpreadsheetPatterns.CellType.General))
						{
							num -= 5.0;
						}
						if (spreadsheetArea.EnumerateCells(Axis.Horizontal, true, !flag3, true, Derivative.Increasing, null).Where(delegate(ISpreadsheetCell cell)
						{
							if (cell.Span.Right == CS$<>8__locals1.table.Span.Right)
							{
								AxisAligned<string> alignment2 = cell.StyleInfo.Alignment;
								return !(((alignment2 != null) ? alignment2.Horizontal : null) == "right");
							}
							return true;
						}).AllOrElseCompute((ISpreadsheetCell cell) => FontInfoEqualityComparer.Instance.Equals(CS$<>8__locals1.titleFont, cell.StyleInfo), () => false))
						{
							num -= 4.0;
						}
					}
				}
				if (num6 > 0 && CS$<>8__locals1.title.Span.Left == 0 && CS$<>8__locals1.title.Span.Top == 0 && CS$<>8__locals1.table.Span.Top > CS$<>8__locals1.title.Span.Bottom + 1 && CS$<>8__locals1.table.Span.Bottom < CS$<>8__locals1.table.Spreadsheet.Size.Y - 5)
				{
					if ((from row in CS$<>8__locals1.prunedCells.Section(CS$<>8__locals1.table.Span.With(Axis.Vertical, new Range<TableUnit>(CS$<>8__locals1.table.Span.Bottom + 1, CS$<>8__locals1.table.Spreadsheet.Size.Y - 1))).Rows()
						where row.Collect<ISpreadsheetCell>().HasAtLeast(3)
						select row).HasAtLeast(3))
					{
						num -= 2.0;
					}
				}
				if (CS$<>8__locals1.title.Span.Right <= CS$<>8__locals1.table.Span.Left)
				{
					if (CS$<>8__locals1.table.Span.Horizontal.Distance(CS$<>8__locals1.title.Span.Horizontal) > 2)
					{
						num -= 10.0;
					}
					else if (CS$<>8__locals1.table.Span.Vertical.Contains(CS$<>8__locals1.title.Span.Vertical) && CS$<>8__locals1.title.Span.Bottom <= CS$<>8__locals1.afterHeaderIdx)
					{
						num += 2.0;
					}
				}
			}
			else
			{
				if (CS$<>8__locals1.title.Span.Top == CS$<>8__locals1.afterHeaderIdx)
				{
					SpreadsheetArea spreadsheetArea2 = CS$<>8__locals1.table.TrimToSpan(CS$<>8__locals1.table.Span.With(Axis.Vertical, CS$<>8__locals1.title.Span.Vertical));
					bool? flag5;
					if (spreadsheetArea2 == null)
					{
						flag5 = null;
					}
					else
					{
						SpreadsheetArea spreadsheetArea3 = spreadsheetArea2.Trim(!flag2, false, false);
						flag5 = ((spreadsheetArea3 != null) ? new bool?(spreadsheetArea3.Equals(CS$<>8__locals1.title)) : null);
					}
					bool? flag6 = flag5;
					if (flag6.GetValueOrDefault())
					{
						if (CS$<>8__locals1.table.EnumerateRows(true, !flag2, true, Derivative.Increasing).SkipWhile((Record<int, IEnumerable<ISpreadsheetCell>> row) => row.Item1 <= CS$<>8__locals1.title.Span.Bottom).Any(delegate(Record<int, IEnumerable<ISpreadsheetCell>> row)
						{
							FontInfoEqualityComparer instance = FontInfoEqualityComparer.Instance;
							ICellStyleInfo titleFont = CS$<>8__locals1.titleFont;
							ISpreadsheetCell spreadsheetCell = row.Item2.OnlyOrDefault<ISpreadsheetCell>();
							return instance.Equals(titleFont, (spreadsheetCell != null) ? spreadsheetCell.StyleInfo : null);
						}))
						{
							num -= 5.0;
							goto IL_0E82;
						}
						num += 2.0;
						goto IL_0E82;
					}
				}
				if (CS$<>8__locals1.title.Span.Top >= CS$<>8__locals1.afterHeaderIdx)
				{
					double num7 = (double)(CS$<>8__locals1.title.Span.Top - CS$<>8__locals1.afterHeaderIdx);
					if (num7 > 0.0 || CS$<>8__locals1.title.Span.Bottom != CS$<>8__locals1.table.Span.Bottom || CS$<>8__locals1.title.Span.Height() != 1)
					{
						num -= num7 + 5.0;
					}
				}
				else
				{
					if (CS$<>8__locals1.title.Span.Left > CS$<>8__locals1.table.Span.Left)
					{
						num -= 20.0;
					}
					else
					{
						if (CS$<>8__locals1.title.Span.Bottom != CS$<>8__locals1.afterHeaderIdx)
						{
							if (CS$<>8__locals1.title.Span.Bottom + 1 >= CS$<>8__locals1.afterHeaderIdx)
							{
								goto IL_0E17;
							}
							SpreadsheetArea spreadsheetArea4 = CS$<>8__locals1.table.TrimToSpan(CS$<>8__locals1.table.Span.With(Axis.Vertical, new Range<TableUnit>(CS$<>8__locals1.title.Span.Bottom + 1, CS$<>8__locals1.afterHeaderIdx - 1)));
							if (!(((spreadsheetArea4 != null) ? spreadsheetArea4.Trim(!flag2, false, true) : null) == null))
							{
								goto IL_0E17;
							}
						}
						num += 5.0;
					}
					IL_0E17:
					if (CS$<>8__locals1.table.TrimToSpan(CS$<>8__locals1.table.Span.With(Axis.Horizontal, CS$<>8__locals1.title.Span.Horizontal)).EnumerateCells(Axis.Horizontal, true, !flag2, true, Derivative.Increasing, null).Any((ISpreadsheetCell cell) => CS$<>8__locals1.<>4__this.ExtractNumberFormatCodes(cell) > SpreadsheetPatterns.CellType.General))
					{
						num -= 6.0;
					}
				}
			}
			IL_0E82:
			if (CS$<>8__locals1.title.EnumerateCells(Axis.Horizontal, true, true, true, Derivative.Increasing, null).Any((ISpreadsheetCell cell) => cell.Formula != null))
			{
				num -= 50.0;
			}
			if (CS$<>8__locals1.trimmedStrings.All(new Func<string, bool>(SpreadsheetPatterns.<ScoreTitleArea>g__InBrackets|38_15)))
			{
				num -= 5.0;
			}
			else if (CS$<>8__locals1.trimmedStrings.SkipLast(1).Any(new Func<string, bool>(SpreadsheetPatterns.<ScoreTitleArea>g__InBrackets|38_15)))
			{
				num -= 3.0;
			}
			else if (SpreadsheetPatterns.<ScoreTitleArea>g__InBrackets|38_15(CS$<>8__locals1.trimmedStrings.Last<string>()) && CS$<>8__locals1.trimmedStrings.Last<string>()[0] != '(')
			{
				num -= 3.0;
			}
			if (CS$<>8__locals1.trimmedStrings[0][0] == '*')
			{
				num -= 3.0;
			}
			if (CS$<>8__locals1.trimmedStrings.Last<string>().EndsWith("."))
			{
				num -= 3.0;
			}
			if (CS$<>8__locals1.trimmedStrings.Any((string str) => str.StartsWith("____")))
			{
				num -= 20.0;
			}
			else if (CS$<>8__locals1.title.Span.Right < CS$<>8__locals1.title.Spreadsheet.Size.X - 1 && CS$<>8__locals1.title.Span.Vertical.AsEnumerable.Any(delegate(int y)
			{
				ISpreadsheetCell spreadsheetCell2 = CS$<>8__locals1.title.Spreadsheet[CS$<>8__locals1.title.Span.Right + 1, y];
				bool? flag7;
				if (spreadsheetCell2 == null)
				{
					flag7 = null;
				}
				else
				{
					string asString = spreadsheetCell2.AsString;
					flag7 = ((asString != null) ? new bool?(asString.Trim().StartsWith("____")) : null);
				}
				bool? flag8 = flag7;
				return flag8.GetValueOrDefault();
			}))
			{
				num -= 10.0;
			}
			if (CS$<>8__locals1.trimmedStrings.Count == 1 && (from str in CS$<>8__locals1.table.SectionAsStrings.ToEnumerable()
				where str.Trim() == CS$<>8__locals1.trimmedStrings[0]
				select str).HasAtLeast(flag4 ? 2 : 1))
			{
				num -= 10.0;
			}
			IEnumerable<ISpreadsheetCell> enumerable;
			CS$<>8__locals1.title.EnumerateRows(true, true, false, Derivative.Increasing).First<Record<int, IEnumerable<ISpreadsheetCell>>>().Deconstruct(out num3, out enumerable);
			CS$<>8__locals1.topRowIdx = num3;
			IEnumerable<ISpreadsheetCell> enumerable2 = enumerable;
			if (CS$<>8__locals1.topRowIdx > 0 && enumerable2.Any((ISpreadsheetCell cell) => cell.Span.Horizontal.AsEnumerable.Any(delegate(int x)
			{
				ISpreadsheetCell spreadsheetCell3 = CS$<>8__locals1.title.Spreadsheet[x, CS$<>8__locals1.topRowIdx - 1];
				return spreadsheetCell3 != null && !string.IsNullOrWhiteSpace(spreadsheetCell3.AsString) && !SpreadsheetPatterns.<ScoreTitleArea>g__InBrackets|38_15(spreadsheetCell3.AsString) && spreadsheetCell3.Span.Horizontal.Size() >= cell.Span.Horizontal.Size() && CS$<>8__locals1.table.Span.Horizontal.Expand(2).Contains(spreadsheetCell3.Span.Horizontal) && FontInfoEqualityComparer.Instance.Equals(spreadsheetCell3.StyleInfo, cell.StyleInfo) && CS$<>8__locals1.title.Spreadsheet.Borders.ForCell(new Vector<TableUnit>(x, CS$<>8__locals1.topRowIdx)).Up == null;
			})))
			{
				num -= 10.0;
			}
			CS$<>8__locals1.title.EnumerateRows(true, true, false, Derivative.Decreasing).First<Record<int, IEnumerable<ISpreadsheetCell>>>().Deconstruct(out num3, out enumerable);
			CS$<>8__locals1.bottomRowIdx = num3;
			IEnumerable<ISpreadsheetCell> enumerable3 = enumerable;
			if (CS$<>8__locals1.bottomRowIdx + 1 < CS$<>8__locals1.title.Spreadsheet.Size.Y && enumerable3.Any((ISpreadsheetCell cell) => cell.Span.Horizontal.AsEnumerable.Any(delegate(int x)
			{
				ISpreadsheetCell spreadsheetCell4 = CS$<>8__locals1.title.Spreadsheet[x, CS$<>8__locals1.bottomRowIdx + 1];
				return spreadsheetCell4 != null && !string.IsNullOrWhiteSpace(spreadsheetCell4.AsString) && !SpreadsheetPatterns.<ScoreTitleArea>g__InBrackets|38_15(spreadsheetCell4.AsString) && spreadsheetCell4.Span.Horizontal.Size() >= cell.Span.Horizontal.Size() && CS$<>8__locals1.table.Span.Horizontal.Expand(2).Contains(spreadsheetCell4.Span.Horizontal) && FontInfoEqualityComparer.Instance.Equals(spreadsheetCell4.StyleInfo, cell.StyleInfo) && CS$<>8__locals1.title.Spreadsheet.Borders.ForCell(new Vector<TableUnit>(x, CS$<>8__locals1.bottomRowIdx)).Down == null;
			})))
			{
				num -= 10.0;
			}
			if (!CS$<>8__locals1.titleFont.Bold)
			{
				if (CS$<>8__locals1.table.Section.ToEnumerable().Any(delegate(ISpreadsheetCell cell)
				{
					ICellStyleInfo styleInfo2 = cell.StyleInfo;
					return styleInfo2 != null && styleInfo2.Bold && !string.IsNullOrWhiteSpace(cell.AsString);
				}))
				{
					num -= 2.0;
				}
			}
			if (CS$<>8__locals1.titleFont.Underline)
			{
				if (!CS$<>8__locals1.table.Section.ToEnumerable().Any(delegate(ISpreadsheetCell cell)
				{
					ICellStyleInfo styleInfo3 = cell.StyleInfo;
					return styleInfo3 != null && styleInfo3.Underline && !string.IsNullOrWhiteSpace(cell.AsString);
				}))
				{
					num += 1.5;
				}
			}
			if (CS$<>8__locals1.titleFont.Italic)
			{
				num -= 2.0;
			}
			if (CS$<>8__locals1.table.Span.Overlaps(CS$<>8__locals1.title.Span) && (CS$<>8__locals1.title.Span.Left != CS$<>8__locals1.table.Span.Left || CS$<>8__locals1.title.Span.Top != CS$<>8__locals1.table.Span.Top))
			{
				if ((from cell in CS$<>8__locals1.table.EnumerateRows(true, true, true, Derivative.Increasing).TakeWhile((Record<int, IEnumerable<ISpreadsheetCell>> r) => r.Item1 < CS$<>8__locals1.afterHeaderIdx).SelectMany((Record<int, IEnumerable<ISpreadsheetCell>> r) => r.Item2)
					where !CS$<>8__locals1.title.Span.Overlaps(cell.Span)
					select cell).Collect((ISpreadsheetCell cell) => cell.StyleInfo).Any(delegate(ICellStyleInfo style)
				{
					if (!FontInfoEqualityComparer.Instance.Equals(style, CS$<>8__locals1.titleFont))
					{
						int? num8 = style.FontSize;
						int? num9 = CS$<>8__locals1.titleFont.FontSize;
						if (!((num8.GetValueOrDefault() > num9.GetValueOrDefault()) & ((num8 != null) & (num9 != null))))
						{
							num9 = style.FontSize;
							num8 = CS$<>8__locals1.titleFont.FontSize;
							return ((num9.GetValueOrDefault() == num8.GetValueOrDefault()) & (num9 != null == (num8 != null))) && style.Bold && !CS$<>8__locals1.titleFont.Bold;
						}
					}
					return true;
				}))
				{
					num -= 50.0;
				}
			}
			return num;
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x0014B5C0 File Offset: 0x001497C0
		internal static SpreadsheetPatterns.Score_OutputFeatures Score_Output(LearningInfo learningInfo)
		{
			IReadOnlyList<SpreadsheetArea> readOnlyList = learningInfo.GetOutputs(InputKind.All).OfType<SpreadsheetArea>().ToList<SpreadsheetArea>();
			bool hasFullyMergedColumn = readOnlyList.All((SpreadsheetArea area) => area.EnumerateColumns(false, false, false, Derivative.Increasing).Any2(delegate(int idx, IEnumerable<ISpreadsheetCell> column)
			{
				int? num = null;
				foreach (ISpreadsheetCell spreadsheetCell in column)
				{
					int num2 = spreadsheetCell.Span.Width();
					if (num2 == 1)
					{
						return false;
					}
					if (num != null)
					{
						int valueOrDefault = num.GetValueOrDefault();
						if (valueOrDefault != num2)
						{
							return false;
						}
					}
					else
					{
						num = new int?(num2);
					}
				}
				return num != null;
			}));
			SpreadsheetPatterns.Score_OutputFeatures score_OutputFeatures = default(SpreadsheetPatterns.Score_OutputFeatures);
			score_OutputFeatures.Width = readOnlyList.MaybeAverage((SpreadsheetArea area) => (double)area.Span.Width()).OrElseDefault<double>();
			score_OutputFeatures.Height = readOnlyList.MaybeAverage((SpreadsheetArea area) => (double)area.Span.Height()).OrElseDefault<double>();
			score_OutputFeatures.HasFullyMergedColumn = hasFullyMergedColumn;
			score_OutputFeatures.IsAllMergedCells = readOnlyList.All((SpreadsheetArea area) => area.EnumerateCells(Axis.Horizontal, true, false, true, Derivative.Increasing, null).All((ISpreadsheetCell cell) => cell.Span.Width() > 1));
			score_OutputFeatures.IsAllSingleCellRows = readOnlyList.All((SpreadsheetArea area) => area.EnumerateRows(true, false, true, Derivative.Increasing).All2((int idx, IEnumerable<ISpreadsheetCell> row) => !row.HasAtLeast(2)));
			score_OutputFeatures.IsOnlyFrozenPanes = readOnlyList.All(delegate(SpreadsheetArea area)
			{
				Vector<TableUnit> freezePaneSize = area.Spreadsheet.FreezePaneSize;
				return !(freezePaneSize == null) && (freezePaneSize.X > area.Span.Right || freezePaneSize.Y > area.Span.Bottom);
			});
			score_OutputFeatures.LeftColumnFinalCharIsColon = (from cellText in readOnlyList.SelectMany((SpreadsheetArea area) => area.EnumerateCells(Axis.Vertical, true, true, true, Derivative.Increasing, new int?(area.Span.Left))).Collect(delegate(ISpreadsheetCell cell)
				{
					string asString = cell.AsString;
					if (asString == null)
					{
						return null;
					}
					return asString.Trim();
				})
				where !string.IsNullOrEmpty(cellText)
				select cellText[cellText.Length - 1]).Distinct<char>().OnlyOrDefault<char>() == ':';
			score_OutputFeatures.PatternScore = readOnlyList.MaybeAverage(delegate(SpreadsheetArea area)
			{
				AbstractSpreadsheet abstractSpreadsheet = area.Spreadsheet as AbstractSpreadsheet;
				return (((abstractSpreadsheet != null) ? abstractSpreadsheet.Patterns : null) ?? new SpreadsheetPatterns(area.Spreadsheet)).ScoreArea(area, hasFullyMergedColumn);
			}).OrElseDefault<double>();
			return score_OutputFeatures;
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x0014B7D0 File Offset: 0x001499D0
		public static double Score_StartTitle(LearningInfo learningInfo, ProgramNode node)
		{
			IReadOnlyList<SpreadsheetArea> readOnlyList = learningInfo.GetOutputs(InputKind.All).OfType<SpreadsheetArea>().ToList<SpreadsheetArea>();
			ProgramNode outputNode = null;
			ProgramSetRewriter.Rewrite(node, new Hole(node.Grammar.Symbol("output"), null), delegate(ProgramNode matchedOutputNode, IReadOnlyDictionary<Hole, ProgramNode> _)
			{
				outputNode = matchedOutputNode;
				return matchedOutputNode;
			});
			IEnumerable<SpreadsheetArea> enumerable;
			if (!(outputNode == null))
			{
				enumerable = learningInfo.AllInputs.Select(new Func<State, object>(outputNode.Invoke)).Cast<SpreadsheetArea>();
			}
			else
			{
				enumerable = readOnlyList.Select((SpreadsheetArea _) => null);
			}
			IEnumerable<SpreadsheetArea> enumerable2 = enumerable;
			return readOnlyList.ZipWith(enumerable2).MaybeAverage(delegate(Record<SpreadsheetArea, SpreadsheetArea> t)
			{
				AbstractSpreadsheet abstractSpreadsheet = t.Item1.Spreadsheet as AbstractSpreadsheet;
				return (((abstractSpreadsheet != null) ? abstractSpreadsheet.Patterns : null) ?? new SpreadsheetPatterns(t.Item1.Spreadsheet)).ScoreTitleArea(t.Item1, t.Item2);
			}).OrElseDefault<double>();
		}

		// Token: 0x06006558 RID: 25944 RVA: 0x0014B990 File Offset: 0x00149B90
		[CompilerGenerated]
		internal static SpreadsheetPatterns.DataPattern <ComputeDataPatterns>g__ComputeCellDataPattern|15_0(ISpreadsheetCell cell)
		{
			string asString = cell.AsString;
			string text = ((asString != null) ? asString.Trim() : null);
			if (string.IsNullOrWhiteSpace(text))
			{
				return SpreadsheetPatterns.DataPattern.Blank;
			}
			double num;
			if (double.TryParse(text, out num))
			{
				if (text.Length == 4 && num > 1500.0 && num < 2100.0)
				{
					return SpreadsheetPatterns.DataPattern.Year;
				}
				return SpreadsheetPatterns.DataPattern.Numeric;
			}
			else
			{
				DateTime dateTime;
				if (DateTime.TryParse(text, out dateTime))
				{
					return SpreadsheetPatterns.DataPattern.DateTime;
				}
				if (text == "ERROR")
				{
					return SpreadsheetPatterns.DataPattern.Error;
				}
				IEnumerable<char> enumerable = text;
				Func<char, bool> func;
				if ((func = SpreadsheetPatterns.<>O.<1>__IsLetterOrDigit) == null)
				{
					func = (SpreadsheetPatterns.<>O.<1>__IsLetterOrDigit = new Func<char, bool>(char.IsLetterOrDigit));
				}
				if (!enumerable.Any(func))
				{
					return SpreadsheetPatterns.DataPattern.NonAlphanumeric;
				}
				return SpreadsheetPatterns.DataPattern.Other;
			}
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x0014BA34 File Offset: 0x00149C34
		[CompilerGenerated]
		private bool <FreezePaneScore>g__HasNonEmptyAboveEmptyHeader|21_0(ref SpreadsheetPatterns.<>c__DisplayClass21_0 A_1)
		{
			for (int i = this._spreadsheet.FreezePaneSize.Y - 1; i >= A_1.area.Span.Top; i--)
			{
				foreach (int num in A_1.area.Span.Horizontal.AsEnumerable)
				{
					ISpreadsheetCell spreadsheetCell = this._spreadsheet[num, i];
					if (((spreadsheetCell != null) ? spreadsheetCell.AsString : null) != null)
					{
						ISpreadsheetCell spreadsheetCell2 = this._spreadsheet[num, i + 1];
						if (((spreadsheetCell2 != null) ? spreadsheetCell2.AsString : null) == null)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x0014BB08 File Offset: 0x00149D08
		[CompilerGenerated]
		internal static string <FormulaColumnScore>g__ColumnGenericFormula|28_0(string formula)
		{
			return SpreadsheetPatterns.DigitRegex.Replace(formula, string.Empty);
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x0014BB1C File Offset: 0x00149D1C
		[CompilerGenerated]
		internal static bool <ScoreTitleArea>g__InBrackets|38_15(string str)
		{
			char c = str[0];
			char c2 = str[str.Length - 1];
			return (c == '(' && c2 == ')') || (c == '[' && c2 == ']');
		}

		// Token: 0x04002C1F RID: 11295
		private readonly ISpreadsheet _spreadsheet;

		// Token: 0x04002C20 RID: 11296
		private readonly IReadOnlyDictionary<string, SpreadsheetPatterns.CellSignature> _fontNameLookup;

		// Token: 0x04002C21 RID: 11297
		private readonly RectangularArray<SpreadsheetPatterns.CellSignature> _cellSignatures;

		// Token: 0x04002C22 RID: 11298
		private readonly RectangularArray<SpreadsheetPatterns.DataPattern> _cellPatterns;

		// Token: 0x04002C23 RID: 11299
		private readonly IReadOnlyList<bool> _isColumnNumeric;

		// Token: 0x04002C24 RID: 11300
		private readonly IReadOnlyList<SpreadsheetPatterns.CellSignature?> _singleSignatureByRow;

		// Token: 0x04002C25 RID: 11301
		private readonly IReadOnlyList<int?> _singleCellRows;

		// Token: 0x04002C26 RID: 11302
		private readonly IReadOnlyList<ulong> _rowNonNullPatterns;

		// Token: 0x04002C27 RID: 11303
		private readonly IReadOnlyDictionary<BorderGroup, bool> _borderGroupIsAbovePartialTable;

		// Token: 0x04002C28 RID: 11304
		private readonly HashSet<Bounds<TableUnit>> _trimmedBorderGroups;

		// Token: 0x04002C29 RID: 11305
		private static readonly SpreadsheetPatterns.CellSignature[] FontNameCellSignatures = (from sig in EnumUtils.GetValues<SpreadsheetPatterns.CellSignature>()
			where SpreadsheetPatterns.CellSignature.Fonts.HasFlag(sig) && sig != SpreadsheetPatterns.CellSignature.Fonts
			orderby sig
			select sig).ToArray<SpreadsheetPatterns.CellSignature>();

		// Token: 0x04002C2A RID: 11306
		private static readonly Regex DigitRegex = new Regex("[0-9]", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x02000E77 RID: 3703
		[Flags]
		private enum CellSignature
		{
			// Token: 0x04002C2C RID: 11308
			None = 0,
			// Token: 0x04002C2D RID: 11309
			Blank = 1,
			// Token: 0x04002C2E RID: 11310
			HasFormula = 2,
			// Token: 0x04002C2F RID: 11311
			Bold = 4,
			// Token: 0x04002C30 RID: 11312
			Italic = 8,
			// Token: 0x04002C31 RID: 11313
			Small = 16,
			// Token: 0x04002C32 RID: 11314
			Large = 32,
			// Token: 0x04002C33 RID: 11315
			Larger = 64,
			// Token: 0x04002C34 RID: 11316
			VeryLarge = 128,
			// Token: 0x04002C35 RID: 11317
			Huge = 256,
			// Token: 0x04002C36 RID: 11318
			Font1 = 512,
			// Token: 0x04002C37 RID: 11319
			Font2 = 1024,
			// Token: 0x04002C38 RID: 11320
			Font3 = 2048,
			// Token: 0x04002C39 RID: 11321
			Fonts = 3584,
			// Token: 0x04002C3A RID: 11322
			FontStyles = 3596,
			// Token: 0x04002C3B RID: 11323
			FontSizeAndStyles = 4092
		}

		// Token: 0x02000E78 RID: 3704
		[Flags]
		private enum DataPattern
		{
			// Token: 0x04002C3D RID: 11325
			Blank = 2,
			// Token: 0x04002C3E RID: 11326
			Error = 4,
			// Token: 0x04002C3F RID: 11327
			Other = 8,
			// Token: 0x04002C40 RID: 11328
			Numeric = 16,
			// Token: 0x04002C41 RID: 11329
			Year = 32,
			// Token: 0x04002C42 RID: 11330
			NonAlphanumeric = 64,
			// Token: 0x04002C43 RID: 11331
			DateTime = 128,
			// Token: 0x04002C44 RID: 11332
			DefinitelyNumeric = 20,
			// Token: 0x04002C45 RID: 11333
			LikelyNumeric = 84,
			// Token: 0x04002C46 RID: 11334
			MaybeNumeric = 116,
			// Token: 0x04002C47 RID: 11335
			LikelyHeaderOfNumeric = 136,
			// Token: 0x04002C48 RID: 11336
			MaybeHeader = 168
		}

		// Token: 0x02000E79 RID: 3705
		private enum CellType
		{
			// Token: 0x04002C4A RID: 11338
			General,
			// Token: 0x04002C4B RID: 11339
			Boolean,
			// Token: 0x04002C4C RID: 11340
			Number,
			// Token: 0x04002C4D RID: 11341
			Date,
			// Token: 0x04002C4E RID: 11342
			Time,
			// Token: 0x04002C4F RID: 11343
			DateTime,
			// Token: 0x04002C50 RID: 11344
			Text,
			// Token: 0x04002C51 RID: 11345
			Error
		}

		// Token: 0x02000E7A RID: 3706
		internal struct Score_OutputFeatures
		{
			// Token: 0x17001214 RID: 4628
			// (get) Token: 0x0600655C RID: 25948 RVA: 0x0014BB58 File Offset: 0x00149D58
			// (set) Token: 0x0600655D RID: 25949 RVA: 0x0014BB60 File Offset: 0x00149D60
			public double Width { readonly get; set; }

			// Token: 0x17001215 RID: 4629
			// (get) Token: 0x0600655E RID: 25950 RVA: 0x0014BB69 File Offset: 0x00149D69
			// (set) Token: 0x0600655F RID: 25951 RVA: 0x0014BB71 File Offset: 0x00149D71
			public double Height { readonly get; set; }

			// Token: 0x17001216 RID: 4630
			// (get) Token: 0x06006560 RID: 25952 RVA: 0x0014BB7A File Offset: 0x00149D7A
			// (set) Token: 0x06006561 RID: 25953 RVA: 0x0014BB82 File Offset: 0x00149D82
			public bool HasFullyMergedColumn { readonly get; set; }

			// Token: 0x17001217 RID: 4631
			// (get) Token: 0x06006562 RID: 25954 RVA: 0x0014BB8B File Offset: 0x00149D8B
			// (set) Token: 0x06006563 RID: 25955 RVA: 0x0014BB93 File Offset: 0x00149D93
			public bool IsAllMergedCells { readonly get; set; }

			// Token: 0x17001218 RID: 4632
			// (get) Token: 0x06006564 RID: 25956 RVA: 0x0014BB9C File Offset: 0x00149D9C
			// (set) Token: 0x06006565 RID: 25957 RVA: 0x0014BBA4 File Offset: 0x00149DA4
			public bool IsAllSingleCellRows { readonly get; set; }

			// Token: 0x17001219 RID: 4633
			// (get) Token: 0x06006566 RID: 25958 RVA: 0x0014BBAD File Offset: 0x00149DAD
			// (set) Token: 0x06006567 RID: 25959 RVA: 0x0014BBB5 File Offset: 0x00149DB5
			public bool IsOnlyFrozenPanes { readonly get; set; }

			// Token: 0x1700121A RID: 4634
			// (get) Token: 0x06006568 RID: 25960 RVA: 0x0014BBBE File Offset: 0x00149DBE
			// (set) Token: 0x06006569 RID: 25961 RVA: 0x0014BBC6 File Offset: 0x00149DC6
			public bool LeftColumnFinalCharIsColon { readonly get; set; }

			// Token: 0x1700121B RID: 4635
			// (get) Token: 0x0600656A RID: 25962 RVA: 0x0014BBCF File Offset: 0x00149DCF
			// (set) Token: 0x0600656B RID: 25963 RVA: 0x0014BBD7 File Offset: 0x00149DD7
			public double PatternScore { readonly get; set; }
		}

		// Token: 0x02000E7B RID: 3707
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002C5A RID: 11354
			[Nullable(new byte[] { 0, 0, 2 })]
			public static Func<SpreadsheetArea, SpreadsheetArea> <0>__Trim;

			// Token: 0x04002C5B RID: 11355
			public static Func<char, bool> <1>__IsLetterOrDigit;

			// Token: 0x04002C5C RID: 11356
			public static Func<char, bool> <2>__IsLetter;

			// Token: 0x04002C5D RID: 11357
			public static Func<char, bool> <3>__IsDigit;
		}
	}
}
