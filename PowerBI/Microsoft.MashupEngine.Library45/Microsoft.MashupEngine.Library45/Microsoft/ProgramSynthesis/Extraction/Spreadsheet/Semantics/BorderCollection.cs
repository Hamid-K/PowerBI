using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EAA RID: 3754
	public class BorderCollection
	{
		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06006673 RID: 26227 RVA: 0x0014E51B File Offset: 0x0014C71B
		public ISpreadsheet Spreadsheet { get; }

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06006674 RID: 26228 RVA: 0x0014E523 File Offset: 0x0014C723
		public AxisAligned<SortedDictionary<int, IReadOnlyList<Border>>> Borders { get; }

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06006675 RID: 26229 RVA: 0x0014E52B File Offset: 0x0014C72B
		public IReadOnlyList<BorderGroup> BorderGroups { get; }

		// Token: 0x06006676 RID: 26230 RVA: 0x0014E533 File Offset: 0x0014C733
		public BorderCollection(ISpreadsheet spreadsheet)
		{
			this.Spreadsheet = spreadsheet;
			this.Borders = BorderCollection.IdentifyBorders(this.Spreadsheet);
			this.BorderGroups = BorderCollection.IdentifyBorderGroups(this.Borders);
		}

		// Token: 0x06006677 RID: 26231 RVA: 0x0014E564 File Offset: 0x0014C764
		public Directed<Border> ForCell(Vector<TableUnit> pos)
		{
			ISpreadsheetCell spreadsheetCell = this.Spreadsheet[pos.X, pos.Y];
			Bounds<TableUnit> span = ((spreadsheetCell != null) ? spreadsheetCell.Span : new Bounds<TableUnit>(pos, pos));
			return new Directed<Border>(delegate(Direction dir)
			{
				int num = span[dir] + ((dir.Derivative() == Derivative.Increasing) ? 1 : 0);
				IReadOnlyList<Border> readOnlyList;
				if (!this.Borders[dir.AlignedAxis().Perpendicular()].TryGetValue(num, out readOnlyList))
				{
					return null;
				}
				Range<TableUnit> range = span[dir.AlignedAxis().Perpendicular()].Expand(1, Derivative.Increasing);
				if (readOnlyList == null)
				{
					return null;
				}
				return readOnlyList.FirstOrDefault((Border border) => border.Line.Range.Contains(range));
			});
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x0014E5BC File Offset: 0x0014C7BC
		public bool IsCellFullyBordered(Vector<TableUnit> pos)
		{
			return this.ForCell(pos).All((Border b) => b != null);
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x0014E5E9 File Offset: 0x0014C7E9
		public bool IsCellFullyBordered(int x, int y)
		{
			return this.IsCellFullyBordered(new Vector<TableUnit>(x, y));
		}

		// Token: 0x0600667A RID: 26234 RVA: 0x0014E5F8 File Offset: 0x0014C7F8
		private static AxisAligned<SortedDictionary<int, IReadOnlyList<Border>>> IdentifyBorders(ISpreadsheet spreadsheet)
		{
			BorderCollection.<>c__DisplayClass13_0 CS$<>8__locals1 = new BorderCollection.<>c__DisplayClass13_0();
			CS$<>8__locals1.allBorderInfos = new AxisAligned<SortedDictionary<int, Dictionary<BorderInfo, Ranges<TableUnit>>>>((Axis axis) => new SortedDictionary<int, Dictionary<BorderInfo, Ranges<TableUnit>>>());
			foreach (ISpreadsheetCell spreadsheetCell in spreadsheet.EnumerateCells(Axis.Horizontal, false, false, false))
			{
				IMergedSpreadsheetCellProxy mergedSpreadsheetCellProxy = spreadsheetCell as IMergedSpreadsheetCellProxy;
				ICellStyleInfo cellStyleInfo = ((mergedSpreadsheetCellProxy != null) ? mergedSpreadsheetCellProxy.ShadowedCell.StyleInfo : spreadsheetCell.StyleInfo);
				if (cellStyleInfo != null && cellStyleInfo.Borders != null)
				{
					BorderCollection.<>c__DisplayClass13_1 CS$<>8__locals2;
					CS$<>8__locals2.borderSpan = new Bounds<TableUnit>(spreadsheetCell.Span.Corner(Ordinal.TopLeft), spreadsheetCell.Span.Corner(Ordinal.BottomRight) + new Vector<TableUnit>(1, 1));
					foreach (Record<Direction, BorderInfo> record in cellStyleInfo.Borders.Enumerate<BorderInfo>())
					{
						Direction direction;
						BorderInfo borderInfo;
						record.Deconstruct(out direction, out borderInfo);
						Direction direction2 = direction;
						BorderInfo borderInfo2 = borderInfo;
						if (borderInfo2.BorderStyle != null && (mergedSpreadsheetCellProxy == null || (direction2.Derivative() != Derivative.Decreasing && mergedSpreadsheetCellProxy.ShadowedCell.Span[direction2] == mergedSpreadsheetCellProxy.OriginalCell.Span[direction2] && mergedSpreadsheetCellProxy.ShadowedCell.Span[direction2.Opposite()] != mergedSpreadsheetCellProxy.OriginalCell.Span[direction2.Opposite()])))
						{
							CS$<>8__locals1.<IdentifyBorders>g__AddBorder|2(direction2, borderInfo2, ref CS$<>8__locals2);
						}
					}
				}
			}
			return new AxisAligned<SortedDictionary<int, IReadOnlyList<Border>>>((Axis axis) => CS$<>8__locals1.allBorderInfos[axis].Select2((int pos, Dictionary<BorderInfo, Ranges<TableUnit>> bordersAtPos) => new KeyValuePair<int, IReadOnlyList<Border>>(pos, bordersAtPos.SelectMany((KeyValuePair<BorderInfo, Ranges<TableUnit>> borders) => borders.Value.Select((Range<TableUnit> range) => new Border(new AxisAlignedLine<TableUnit>(axis, range.Expand(1, Derivative.Increasing), pos), borders.Key))).ToList<Border>())).ToSortedDictionary<int, IReadOnlyList<Border>>());
		}

		// Token: 0x0600667B RID: 26235 RVA: 0x0014E7F8 File Offset: 0x0014C9F8
		private static IReadOnlyList<BorderGroup> IdentifyBorderGroups(IReadOnlyList<Border> borders)
		{
			List<List<Border>> groups = borders.Select((Border border) => new List<Border> { border }).ToList<List<Border>>();
			bool flag;
			do
			{
				flag = false;
				int num = 0;
				while (!flag && num < groups.Count)
				{
					int j = num + 1;
					while (!flag && j < groups.Count)
					{
						if (groups[num].Any((Border borderI) => groups[j].Any((Border borderJ) => borderI.Intersects(borderJ))))
						{
							groups[num].AddRange(groups[j]);
							groups.RemoveAt(j);
							flag = true;
						}
						int i = j;
						j = i + 1;
					}
					num++;
				}
			}
			while (flag);
			return (from @group in groups
				select new BorderGroup(@group) into @group
				orderby @group.Span.Top, @group.Span.Left
				select @group).ToList<BorderGroup>();
		}

		// Token: 0x0600667C RID: 26236 RVA: 0x0014E98C File Offset: 0x0014CB8C
		private static IReadOnlyList<BorderGroup> IdentifyBorderGroups(AxisAligned<SortedDictionary<int, IReadOnlyList<Border>>> borders)
		{
			List<HashSet<Border>> list = new List<HashSet<Border>>();
			Dictionary<Border, HashSet<Border>> dictionary = new Dictionary<Border, HashSet<Border>>();
			Border border;
			foreach (KeyValuePair<int, IReadOnlyList<Border>> keyValuePair in borders.Horizontal)
			{
				int num;
				IReadOnlyList<Border> readOnlyList;
				keyValuePair.Deconstruct(out num, out readOnlyList);
				IReadOnlyList<Border> readOnlyList2 = readOnlyList;
				using (IEnumerator<Border> enumerator2 = readOnlyList2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						border = enumerator2.Current;
						HashSet<Border> hashSet = dictionary.GetOrCreateValue(border);
						if (hashSet.Count == 0)
						{
							hashSet.Add(border);
							list.Add(hashSet);
						}
						foreach (Border border2 in readOnlyList2)
						{
							if (border2.Intersects(border) && !hashSet.Contains(border2))
							{
								hashSet.Add(border2);
							}
						}
						foreach (KeyValuePair<int, IReadOnlyList<Border>> keyValuePair2 in borders.Vertical)
						{
							keyValuePair2.Deconstruct(out num, out readOnlyList);
							int num2 = num;
							IReadOnlyList<Border> readOnlyList3 = readOnlyList;
							if (border.Line.Range.Contains(num2) && num2 > 0)
							{
								foreach (Border border3 in readOnlyList3)
								{
									if (border3.Intersects(border) && !hashSet.Contains(border3))
									{
										HashSet<Border> hashSet2;
										if (dictionary.TryGetValue(border3, out hashSet2))
										{
											hashSet2.AddRange(hashSet);
											foreach (Border border4 in hashSet)
											{
												dictionary[border4] = hashSet2;
											}
											list.Remove(hashSet);
											hashSet = hashSet2;
										}
										else
										{
											hashSet.Add(border3);
											dictionary[border3] = hashSet;
										}
									}
								}
							}
						}
					}
				}
			}
			return (from @group in list
				where @group.Any((Border border) => border.Line.Axis == Axis.Vertical)
				select new BorderGroup(@group.ToList<Border>())).ToList<BorderGroup>();
		}
	}
}
