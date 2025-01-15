using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D19 RID: 3353
	[NullableContext(1)]
	[Nullable(0)]
	internal class PdfTable<TCell> : IProsePdfTable<TCell>, IPdfTable, ITableBounded, IBounded<TableUnit> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x060055E9 RID: 21993 RVA: 0x0010F59C File Offset: 0x0010D79C
		public IProsePdfTable<TCell> CollapsedSection([Nullable(new byte[] { 0, 1 })] Bounds<TableUnit> tableBounds, SeparatorCollection separators, PdfAnalyzerOptions options, bool cleanTable = true, [Nullable(2)] AxisAligned<bool> combineAlongAxis = null)
		{
			List<PdfTable<TCell>.SplitInfo> list = null;
			RectangularArray<TCell> rectangularArray = this.Table.Section(tableBounds).Collapse(null);
			if (cleanTable)
			{
				rectangularArray = PdfTable<TCell>.CleanTableSection(rectangularArray, separators, options, combineAlongAxis, out list);
			}
			return new PdfTable<TCell>(rectangularArray, tableBounds, this.StartingPageIndex, this.EndingPageIndex, TableKind.Inferred, list);
		}

		// Token: 0x060055EA RID: 21994 RVA: 0x0010F5EA File Offset: 0x0010D7EA
		[return: Nullable(new byte[] { 0, 2 })]
		private static RectangularArray<TCell> CleanTableSection([Nullable(new byte[] { 0, 2 })] RectangularArray<TCell> table, SeparatorCollection separators, PdfAnalyzerOptions options, [Nullable(2)] AxisAligned<bool> combineAlongAxis, [Nullable(new byte[] { 1, 1, 0 })] out List<PdfTable<TCell>.SplitInfo> splitInfos)
		{
			if (combineAlongAxis == null || combineAlongAxis.Vertical)
			{
				table = PdfTable<TCell>.CombineCompatibleAlongAxis(table, Axis.Vertical, options);
			}
			if (combineAlongAxis == null || combineAlongAxis.Horizontal)
			{
				table = PdfTable<TCell>.CombineCompatibleAlongAxis(table, Axis.Horizontal, options);
			}
			return PdfTable<TCell>.FixOvermergedColumns(table, separators, options, out splitInfos);
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x0010F628 File Offset: 0x0010D828
		[return: Nullable(new byte[] { 0, 2 })]
		private static RectangularArray<TCell> CombineCompatibleAlongAxis([Nullable(new byte[] { 0, 2 })] RectangularArray<TCell> table, Axis axis, PdfAnalyzerOptions options)
		{
			PdfTable<TCell>.<>c__DisplayClass2_0 CS$<>8__locals1 = new PdfTable<TCell>.<>c__DisplayClass2_0();
			CS$<>8__locals1.axis = axis;
			CS$<>8__locals1.table = table;
			bool flag = options.Version >= PdfAnalyzerVersion.V1_2;
			CS$<>8__locals1.perpendicular = CS$<>8__locals1.axis.Perpendicular();
			List<List<int>> list = new List<List<int>>
			{
				new List<int> { 0 }
			};
			List<int> list2 = list[0];
			int num;
			BitArray bitArray;
			Range<PixelUnit> range;
			Justification? justification;
			CS$<>8__locals1.<CombineCompatibleAlongAxis>g__SummarizeSlice|0(CS$<>8__locals1.table.Slices(CS$<>8__locals1.axis).First<IEnumerable<TCell>>(), 0).Deconstruct(out num, out bitArray, out range, out justification);
			BitArray bitArray2 = bitArray;
			Range<PixelUnit> range2 = range;
			foreach (Record<int, BitArray, Range<PixelUnit>, Justification?> record in CS$<>8__locals1.table.Slices(CS$<>8__locals1.axis).Select(new Func<IEnumerable<TCell>, int, Record<int, BitArray, Range<PixelUnit>, Justification?>>(CS$<>8__locals1.<CombineCompatibleAlongAxis>g__SummarizeSlice|0)).Skip(1))
			{
				record.Deconstruct(out num, out bitArray, out range, out justification);
				int num2 = num;
				BitArray bitArray3 = bitArray;
				Range<PixelUnit> range3 = range;
				Justification? justification2 = justification;
				if (range2.Overlaps(range3))
				{
					goto IL_01AC;
				}
				justification = justification2;
				Justification justification3 = Justification.After;
				if (!((justification.GetValueOrDefault() == justification3) & (justification != null)))
				{
					justification = justification2;
					justification3 = Justification.Unknown;
					if (!((justification.GetValueOrDefault() == justification3) & (justification != null)))
					{
						goto IL_01C1;
					}
				}
				if (bitArray2.BitCount() < bitArray3.BitCount() && ((flag && bitArray3.Length <= 3) || bitArray3.BitCount() > 2))
				{
					if (bitArray2.Cast<bool>().ZipWith(bitArray3.Cast<bool>()).SkipWhile((Record<bool, bool> t) => !t.Item2)
						.All((Record<bool, bool> t) => !t.Item1))
					{
						goto IL_01AC;
					}
				}
				IL_01C1:
				range2 = range3;
				list2 = new List<int> { num2 };
				list.Add(list2);
				bitArray2 = bitArray3;
				continue;
				IL_01AC:
				if (!bitArray2.Clone().And(bitArray3).AllFalse())
				{
					goto IL_01C1;
				}
				range2 = range2.Join(range3);
				bitArray2.Or(bitArray3);
				list2.Add(num2);
			}
			RectangularArray<TCell> rectangularArray;
			if (CS$<>8__locals1.axis == Axis.Vertical)
			{
				rectangularArray = new RectangularArray<TCell>(list.Count, CS$<>8__locals1.table.Height);
				for (int i = 0; i < rectangularArray.Width; i++)
				{
					List<int> list3 = list[i];
					int y;
					for (y = 0; y < rectangularArray.Height; y = num + 1)
					{
						rectangularArray[i, y] = list3.Select((int col) => CS$<>8__locals1.table[col, y]).SingleOrDefault((TCell cell) => cell != null);
						num = y;
					}
				}
			}
			else
			{
				rectangularArray = new RectangularArray<TCell>(CS$<>8__locals1.table.Width, list.Count);
				for (int j = 0; j < rectangularArray.Height; j++)
				{
					List<int> list4 = list[j];
					int x;
					for (x = 0; x < rectangularArray.Width; x = num + 1)
					{
						rectangularArray[x, j] = list4.Select((int row) => CS$<>8__locals1.table[x, row]).SingleOrDefault((TCell cell) => cell != null);
						num = x;
					}
				}
			}
			return rectangularArray;
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x0010FA04 File Offset: 0x0010DC04
		[return: Nullable(new byte[] { 0, 2 })]
		private static RectangularArray<TCell> FixOvermergedColumns([Nullable(new byte[] { 0, 2 })] RectangularArray<TCell> table, SeparatorCollection separators, PdfAnalyzerOptions options, [Nullable(new byte[] { 1, 1, 0 })] out List<PdfTable<TCell>.SplitInfo> splitInfos)
		{
			PdfTable<TCell>.<>c__DisplayClass3_0 CS$<>8__locals1 = new PdfTable<TCell>.<>c__DisplayClass3_0();
			CS$<>8__locals1.table = table;
			CS$<>8__locals1.separators = separators;
			bool flag = options.Version >= PdfAnalyzerVersion.V1_1;
			splitInfos = new List<PdfTable<TCell>.SplitInfo>();
			bool flag2 = false;
			Range<PixelUnit> range = CS$<>8__locals1.<FixOvermergedColumns>g__ColumnHorizontal|0(0);
			int num = CS$<>8__locals1.<FixOvermergedColumns>g__ColumnCellCount|1(0);
			PdfTable<TCell>.<>c__DisplayClass3_1 CS$<>8__locals2 = new PdfTable<TCell>.<>c__DisplayClass3_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			CS$<>8__locals2.columnIndex = 1;
			while (CS$<>8__locals2.columnIndex < CS$<>8__locals2.CS$<>8__locals1.table.Width)
			{
				Range<PixelUnit> range2 = CS$<>8__locals2.CS$<>8__locals1.<FixOvermergedColumns>g__ColumnHorizontal|0(CS$<>8__locals2.columnIndex);
				int num2 = CS$<>8__locals2.CS$<>8__locals1.<FixOvermergedColumns>g__ColumnCellCount|1(CS$<>8__locals2.columnIndex);
				int columnIndex;
				if (range2.Overlaps(range))
				{
					foreach (Range<TableUnit> range3 in CS$<>8__locals2.<FixOvermergedColumns>g__BetweenBordersRowRanges|4())
					{
						if (range3.Size() > 1)
						{
							if (!CS$<>8__locals2.CS$<>8__locals1.table.Section(new Bounds<TableUnit>(new Range<TableUnit>(CS$<>8__locals2.columnIndex - 1, CS$<>8__locals2.columnIndex), range3)).ToEnumerableNonNull<TCell>().Any((TCell cell) => cell.BeforeAlignmentDotRow != null || cell.AfterAlignmentDotRow != null))
							{
								Optional<Range<PixelUnit>> optional = Range<PixelUnit>.MaybeIntersect(CS$<>8__locals2.CS$<>8__locals1.table.Column(CS$<>8__locals2.columnIndex - 1).ZipWith(CS$<>8__locals2.CS$<>8__locals1.table.Column(CS$<>8__locals2.columnIndex)).Skip(range3.Min)
									.Take(range3.Size())
									.Collect2<TCell, TCell>()
									.Select2((TCell a, TCell b) => Range<PixelUnit>.MaybeCreate(a.ApparentPixelBounds.Right, b.ApparentPixelBounds.Left))
									.SelectValues<Range<PixelUnit>>());
								if (optional.HasValue)
								{
									Range<PixelUnit> value = optional.Value;
									List<Record<int, int, PdfTable<TCell>.SplitInfo>> list = new List<Record<int, int, PdfTable<TCell>.SplitInfo>>();
									int num3 = 0;
									foreach (int num4 in range3.AsEnumerable)
									{
										TCell tcell = CS$<>8__locals2.CS$<>8__locals1.table[CS$<>8__locals2.columnIndex - 1, num4];
										if (tcell == null)
										{
											tcell = CS$<>8__locals2.CS$<>8__locals1.table[CS$<>8__locals2.columnIndex, num4];
										}
										else if (CS$<>8__locals2.CS$<>8__locals1.table[CS$<>8__locals2.columnIndex, num4] != null)
										{
											continue;
										}
										if (tcell != null && !tcell.IsBoundsFromBorders && tcell.ApparentPixelBounds.Horizontal.Contains(value))
										{
											if ((double)value.Size() <= 3.0 * tcell.Children.Max((IWord word) => word.AverageGlyphWidth()))
											{
												Optional<Record<TCell, TCell>> optional2 = tcell.SplitOnBoundary(Axis.Horizontal, value);
												if (!optional2.HasValue)
												{
													num3++;
												}
												else
												{
													TCell tcell2;
													TCell tcell3;
													optional2.Value.Deconstruct(out tcell2, out tcell3);
													TCell tcell4 = tcell2;
													TCell tcell5 = tcell3;
													if (tcell4 != null && tcell5 != null && (tcell4.Content.EndsWith("(") || tcell5.Content.StartsWith(")") || tcell5.Content.StartsWith(".") || (flag && (SpecialCharacters.EndsWithHyphen(tcell4.Content) || SpecialCharacters.StartsWithHyphen(tcell5.Content)))))
													{
														num3++;
													}
													else
													{
														list.Add(Record.Create<int, int, PdfTable<TCell>.SplitInfo>(CS$<>8__locals2.columnIndex, num4, new PdfTable<TCell>.SplitInfo(tcell, tcell4, tcell5, value)));
													}
												}
											}
										}
									}
									if (num3 == 0)
									{
										using (List<Record<int, int, PdfTable<TCell>.SplitInfo>>.Enumerator enumerator3 = list.GetEnumerator())
										{
											while (enumerator3.MoveNext())
											{
												Record<int, int, PdfTable<TCell>.SplitInfo> record = enumerator3.Current;
												int num5;
												PdfTable<TCell>.SplitInfo splitInfo;
												record.Deconstruct(out num5, out columnIndex, out splitInfo);
												int num6 = num5;
												int num7 = columnIndex;
												PdfTable<TCell>.SplitInfo splitInfo2 = splitInfo;
												CS$<>8__locals2.CS$<>8__locals1.table[num6 - 1, num7] = splitInfo2.Left;
												CS$<>8__locals2.CS$<>8__locals1.table[num6, num7] = splitInfo2.Right;
												splitInfos.Add(splitInfo2);
											}
											continue;
										}
									}
									if (num3 > 1 && num3 > 2 * list.Count && num3 * 4 > range3.Size())
									{
										flag2 = true;
										bool flag3 = num >= num2;
										foreach (int num8 in range3.AsEnumerable)
										{
											TCell tcell6 = CS$<>8__locals2.CS$<>8__locals1.table[CS$<>8__locals2.columnIndex - 1, num8];
											TCell tcell7 = CS$<>8__locals2.CS$<>8__locals1.table[CS$<>8__locals2.columnIndex, num8];
											if (tcell6 == null && flag3)
											{
												CS$<>8__locals2.CS$<>8__locals1.table[CS$<>8__locals2.columnIndex - 1, num8] = tcell7;
												PdfTable<TCell>.<>c__DisplayClass3_0 CS$<>8__locals3 = CS$<>8__locals2.CS$<>8__locals1;
												int columnIndex2 = CS$<>8__locals2.columnIndex;
												int num9 = num8;
												TCell tcell3 = default(TCell);
												CS$<>8__locals3.table[columnIndex2, num9] = tcell3;
											}
											else if (tcell7 == null && !flag3)
											{
												CS$<>8__locals2.CS$<>8__locals1.table[CS$<>8__locals2.columnIndex, num8] = tcell6;
												PdfTable<TCell>.<>c__DisplayClass3_0 CS$<>8__locals4 = CS$<>8__locals2.CS$<>8__locals1;
												int num10 = CS$<>8__locals2.columnIndex - 1;
												int num11 = num8;
												TCell tcell3 = default(TCell);
												CS$<>8__locals4.table[num10, num11] = tcell3;
											}
											else if (tcell6 != null && tcell7 != null)
											{
												TCell tcell8 = tcell6.CombineWithOverlappingCellInTable(tcell7);
												CS$<>8__locals2.CS$<>8__locals1.table[flag3 ? (CS$<>8__locals2.columnIndex - 1) : CS$<>8__locals2.columnIndex, num8] = tcell8;
												PdfTable<TCell>.<>c__DisplayClass3_0 CS$<>8__locals5 = CS$<>8__locals2.CS$<>8__locals1;
												int num12 = (flag3 ? CS$<>8__locals2.columnIndex : (CS$<>8__locals2.columnIndex - 1));
												int num13 = num8;
												TCell tcell3 = default(TCell);
												CS$<>8__locals5.table[num12, num13] = tcell3;
											}
										}
									}
								}
							}
						}
					}
				}
				range = range2;
				num = num2;
				columnIndex = CS$<>8__locals2.columnIndex;
				CS$<>8__locals2.columnIndex = columnIndex + 1;
			}
			if (!flag2)
			{
				return CS$<>8__locals1.table;
			}
			return CS$<>8__locals1.table.Collapse(null);
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x060055ED RID: 21997 RVA: 0x00110108 File Offset: 0x0010E308
		[Nullable(new byte[] { 0, 2 })]
		public RectangularArray<TCell> Table
		{
			[return: Nullable(new byte[] { 0, 2 })]
			get;
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x060055EE RID: 21998 RVA: 0x00110110 File Offset: 0x0010E310
		public int StartingPageIndex { get; }

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x060055EF RID: 21999 RVA: 0x00110118 File Offset: 0x0010E318
		public int EndingPageIndex { get; }

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x060055F0 RID: 22000 RVA: 0x00110120 File Offset: 0x0010E320
		public int Width { get; }

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x060055F1 RID: 22001 RVA: 0x00110128 File Offset: 0x0010E328
		public int Height { get; }

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x060055F2 RID: 22002 RVA: 0x00110130 File Offset: 0x0010E330
		public int? RecognizedHeaderRowCount
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x060055F3 RID: 22003 RVA: 0x00110146 File Offset: 0x0010E346
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<TableUnit> TableBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x060055F4 RID: 22004 RVA: 0x0011014E File Offset: 0x0010E34E
		[Nullable(new byte[] { 0, 1 })]
		Bounds<TableUnit> IBounded<TableUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.TableBounds;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x060055F5 RID: 22005 RVA: 0x00110156 File Offset: 0x0010E356
		// (set) Token: 0x060055F6 RID: 22006 RVA: 0x0011015E File Offset: 0x0010E35E
		[Nullable(2)]
		internal TableIdentity TableIdentity
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x060055F7 RID: 22007 RVA: 0x00110167 File Offset: 0x0010E367
		[Nullable(2)]
		TableIdentity IPdfTable.TableIdentity
		{
			[NullableContext(2)]
			get
			{
				return this.TableIdentity;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x060055F8 RID: 22008 RVA: 0x0011016F File Offset: 0x0010E36F
		[Nullable(2)]
		string IPdfTable.DisplayName
		{
			[NullableContext(2)]
			get
			{
				TableIdentity tableIdentity = this.TableIdentity;
				if (tableIdentity == null)
				{
					return null;
				}
				return tableIdentity.Identifier;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x060055F9 RID: 22009 RVA: 0x00110182 File Offset: 0x0010E382
		public TableKind Kind { get; }

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x060055FA RID: 22010 RVA: 0x0011018A File Offset: 0x0010E38A
		[Nullable(new byte[] { 2, 1, 0 })]
		internal IReadOnlyList<PdfTable<TCell>.SplitInfo> SplitInfos
		{
			[return: Nullable(new byte[] { 2, 1, 0 })]
			get;
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x00110194 File Offset: 0x0010E394
		private PdfTable([Nullable(new byte[] { 0, 2 })] RectangularArray<TCell> table, [Nullable(new byte[] { 0, 1 })] Bounds<TableUnit> tableBounds, int startingPageIndex, int endingPageIndex, TableKind kind, [Nullable(new byte[] { 2, 1, 0 })] IReadOnlyList<PdfTable<TCell>.SplitInfo> splitInfos = null)
		{
			this.Table = table;
			this.StartingPageIndex = startingPageIndex;
			this.EndingPageIndex = endingPageIndex;
			this.Width = table.GetLength(Axis.Horizontal);
			this.Height = table.GetLength(Axis.Vertical);
			this.TableBounds = tableBounds;
			this.Kind = kind;
			this.SplitInfos = splitInfos;
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x001101F0 File Offset: 0x0010E3F0
		[return: Nullable(new byte[] { 1, 2 })]
		public string[,] GetTextTable()
		{
			return this.Table.ToTextTable<TCell>();
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x00110200 File Offset: 0x0010E400
		public static IProsePdfTable<TCell> CreateFullPageTable([Nullable(new byte[] { 0, 2 })] RectangularArray<TCell> table, int pageIndex)
		{
			Bounds<TableUnit> bounds = new Bounds<TableUnit>(Vector<TableUnit>.Zero, table.Corner(Ordinal.BottomRight));
			return new PdfTable<TCell>(table, bounds, pageIndex, pageIndex, TableKind.Page, null);
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x0011022C File Offset: 0x0010E42C
		public IProsePdfTable<TCell> Section([Nullable(new byte[] { 0, 1 })] Bounds<TableUnit> tableBounds)
		{
			return new PdfTable<TCell>(this.Table.Section(tableBounds), tableBounds, this.StartingPageIndex, this.EndingPageIndex, TableKind.Inferred, null);
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x0011025C File Offset: 0x0010E45C
		[return: Nullable(new byte[] { 0, 0, 1 })]
		public Optional<Bounds<PixelUnit>> CalculateApparentPixelBounds()
		{
			return Bounds<PixelUnit>.MaybeJoin(from cell in this.Table.ToEnumerableNonNull<TCell>()
				select cell.ApparentPixelBounds);
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x00110294 File Offset: 0x0010E494
		[return: Nullable(new byte[] { 0, 0, 1 })]
		public Optional<Bounds<PixelUnit>> CalculateStablePixelBounds()
		{
			return Bounds<PixelUnit>.MaybeJoin(from cell in this.Table.ToEnumerableNonNull<TCell>()
				where !string.IsNullOrEmpty(cell.Content)
				select cell.StablePixelBounds);
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x001102FC File Offset: 0x0010E4FC
		[return: Nullable(new byte[] { 0, 1 })]
		public Optional<IApparentPixelBounded> CalculateApparentPixelBoundsWrapper()
		{
			Optional<Bounds<PixelUnit>> optional = this.CalculateApparentPixelBounds();
			if (!optional.HasValue)
			{
				return Optional<IApparentPixelBounded>.Nothing;
			}
			Optional<Bounds<PixelUnit>> optional2 = this.CalculateStablePixelBounds();
			Bounds<PixelUnit> value = optional.Value;
			return new ApparentPixelBoundsWrapper(optional2.Value, value).Some<IApparentPixelBounded>();
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x00110348 File Offset: 0x0010E548
		[return: Nullable(new byte[] { 0, 1 })]
		public Bounds<TableUnit>? FindTableBounds(IEnumerable<TCell> cells)
		{
			Dictionary<Direction, int> dictionary = new Dictionary<Direction, int>();
			HashSet<IWord> words = cells.SelectMany((TCell cell) => cell.Children).ConvertToHashSet<IWord>();
			if (words.Count == 0)
			{
				return null;
			}
			foreach (Direction direction in DirectionUtilities.Directions)
			{
				IEnumerable<int> enumerable = Enumerable.Range(0, this.Table.GetLength(direction.AlignedAxis()));
				if (direction.Derivative() == Derivative.Increasing)
				{
					enumerable = enumerable.Reverse<int>();
				}
				Axis perpendicularAxis = direction.AlignedAxis().Perpendicular();
				Optional<int> optional = enumerable.MaybeFirst((int i) => words.Overlaps(this.Table.Slice(perpendicularAxis, i).Collect<TCell>().SelectMany((TCell cell) => cell.Children)));
				if (!optional.HasValue)
				{
					throw new ArgumentException("Attempted to find table bounds of cells not in table.", "cells");
				}
				dictionary[direction] = optional.Value;
			}
			if (dictionary[Direction.Up] > dictionary[Direction.Down] || dictionary[Direction.Left] > dictionary[Direction.Right])
			{
				throw new ArgumentException("Sub-table boundaries are flipped. Something is wrong...");
			}
			return new Bounds<TableUnit>?(new Bounds<TableUnit>(dictionary));
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x001104B8 File Offset: 0x0010E6B8
		[return: Nullable(new byte[] { 0, 1 })]
		public Bounds<TableUnit>? FindTableBounds([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds)
		{
			Dictionary<Direction, int> dictionary = new Dictionary<Direction, int>();
			Func<IWord, bool> <>9__2;
			foreach (Direction direction in DirectionUtilities.Directions)
			{
				IEnumerable<int> enumerable = Enumerable.Range(0, this.Table.GetLength(direction.AlignedAxis()));
				if (direction.Derivative() == Derivative.Increasing)
				{
					enumerable = enumerable.Reverse<int>();
				}
				Axis perpendicularAxis = direction.AlignedAxis().Perpendicular();
				Optional<int> optional = enumerable.MaybeFirst(delegate(int i)
				{
					IEnumerable<IWord> enumerable2 = this.Table.Slice(perpendicularAxis, i).Collect<TCell>().SelectMany((TCell cell) => cell.Children);
					Func<IWord, bool> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (IWord word) => word.ApparentPixelBounds.Overlaps(bounds));
					}
					return enumerable2.Any(func);
				});
				if (!optional.HasValue)
				{
					Bounds<TableUnit>? bounds2 = null;
					return bounds2;
				}
				dictionary[direction] = optional.Value;
			}
			if (dictionary[Direction.Up] > dictionary[Direction.Down] || dictionary[Direction.Left] > dictionary[Direction.Right])
			{
				throw new ArgumentException("Sub-table boundaries are flipped. Something is wrong...");
			}
			return new Bounds<TableUnit>?(new Bounds<TableUnit>(dictionary));
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x001105E4 File Offset: 0x0010E7E4
		public override string ToString()
		{
			return this.Table.Select<string>(delegate([Nullable(2)] TCell cell)
			{
				TCell tcell = cell;
				if (tcell == null)
				{
					return null;
				}
				return tcell.Content;
			}).ToString();
		}

		// Token: 0x02000D1A RID: 3354
		[NullableContext(2)]
		[Nullable(0)]
		internal class SplitInfo
		{
			// Token: 0x17000FA1 RID: 4001
			// (get) Token: 0x06005605 RID: 22021 RVA: 0x0011062C File Offset: 0x0010E82C
			[Nullable(1)]
			public TCell Cell
			{
				[NullableContext(1)]
				get;
			}

			// Token: 0x17000FA2 RID: 4002
			// (get) Token: 0x06005606 RID: 22022 RVA: 0x00110634 File Offset: 0x0010E834
			public TCell Left { get; }

			// Token: 0x17000FA3 RID: 4003
			// (get) Token: 0x06005607 RID: 22023 RVA: 0x0011063C File Offset: 0x0010E83C
			public TCell Right { get; }

			// Token: 0x17000FA4 RID: 4004
			// (get) Token: 0x06005608 RID: 22024 RVA: 0x00110644 File Offset: 0x0010E844
			[Nullable(new byte[] { 0, 1 })]
			public Range<PixelUnit> Boundary
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x06005609 RID: 22025 RVA: 0x0011064C File Offset: 0x0010E84C
			public SplitInfo([Nullable(1)] TCell cell, TCell left, TCell right, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> boundary)
			{
				this.Cell = cell;
				this.Left = left;
				this.Right = right;
				this.Boundary = boundary;
			}

			// Token: 0x0600560A RID: 22026 RVA: 0x00110674 File Offset: 0x0010E874
			[NullableContext(1)]
			internal static QuadTree<TCell, PixelUnit> ApplySplits([Nullable(new byte[] { 1, 1, 0 })] IEnumerable<PdfTable<TCell>.SplitInfo> splits, QuadTree<TCell, PixelUnit> cells)
			{
				QuadTree<TCell, PixelUnit> quadTree = new QuadTree<TCell, PixelUnit>(cells.Bounds, cells);
				foreach (PdfTable<TCell>.SplitInfo splitInfo in splits)
				{
					if (quadTree.Contains(splitInfo.Cell))
					{
						quadTree.Remove(splitInfo.Cell);
						if (splitInfo.Left != null)
						{
							quadTree.Add(splitInfo.Left);
						}
						if (splitInfo.Right != null)
						{
							quadTree.Add(splitInfo.Right);
						}
					}
					else
					{
						foreach (TCell tcell in quadTree.OverlappingElements(splitInfo.Cell.ApparentPixelBounds).ToList<TCell>())
						{
							Optional<Record<TCell, TCell>> optional = tcell.SplitOnBoundary(Axis.Horizontal, splitInfo.Boundary);
							if (optional.HasValue)
							{
								TCell tcell2;
								TCell tcell3;
								optional.Value.Deconstruct(out tcell2, out tcell3);
								TCell tcell4 = tcell2;
								TCell tcell5 = tcell3;
								quadTree.Remove(tcell);
								if (tcell4 != null)
								{
									quadTree.Add(tcell4);
								}
								if (tcell5 != null)
								{
									quadTree.Add(tcell5);
								}
							}
						}
					}
				}
				return quadTree;
			}
		}
	}
}
