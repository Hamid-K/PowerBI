using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D65 RID: 3429
	[NullableContext(1)]
	[Nullable(0)]
	internal class SeparatorGridBuilder
	{
		// Token: 0x06005797 RID: 22423 RVA: 0x001163AB File Offset: 0x001145AB
		public static QuadTree<SeparatorGrid, PixelUnit> Build(SeparatorCollection separatorCollection)
		{
			return new QuadTree<SeparatorGrid, PixelUnit>(separatorCollection.Groups.Bounds, SeparatorGridBuilder.BuildEnumerable(separatorCollection));
		}

		// Token: 0x06005798 RID: 22424 RVA: 0x001163C4 File Offset: 0x001145C4
		private static IEnumerable<SeparatorGrid> BuildEnumerable(SeparatorCollection separatorCollection)
		{
			QuadTree<ShadedBounds, PixelUnit> backgrounds = separatorCollection.Backgrounds;
			return separatorCollection.Groups.SelectValues((SeparatorGroup group) => SeparatorGridBuilder.MaybeAsSeparatorGrid(group, separatorCollection, backgrounds));
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x0011640C File Offset: 0x0011460C
		[return: Nullable(new byte[] { 0, 1 })]
		private static Optional<SeparatorGrid> MaybeAsSeparatorGrid(SeparatorGroup separatorGroup, SeparatorCollection separators, QuadTree<ShadedBounds, PixelUnit> backgrounds)
		{
			SeparatorGridBuilder.<>c__DisplayClass2_0 CS$<>8__locals1 = new SeparatorGridBuilder.<>c__DisplayClass2_0();
			CS$<>8__locals1.separators = separators;
			CS$<>8__locals1.separatorGroup = separatorGroup;
			CS$<>8__locals1.lineWidth = CS$<>8__locals1.separatorGroup.Separators.Max((Separator sep) => sep.LineWidth);
			CS$<>8__locals1.tolerance = Math.Max(5, CS$<>8__locals1.lineWidth);
			CS$<>8__locals1.contractCellsBy = CS$<>8__locals1.tolerance / 2;
			CS$<>8__locals1.visibleSeparators = new AxisAlignedList<Separator>((Axis axis) => CS$<>8__locals1.separatorGroup.Separators.Where((Separator sep) => sep.Line.Axis == axis).Where(new Func<Separator, bool>(base.<MaybeAsSeparatorGrid>g__IsVisible|1)));
			CS$<>8__locals1.borderPositions = new AxisAlignedList<Range<PixelUnit>>(delegate(Axis axis)
			{
				IEnumerable<Record<int, int>> enumerable = (from pos in CS$<>8__locals1.visibleSeparators[axis.Perpendicular()].Select((Separator sep) => sep.Line.Position).Concat(CS$<>8__locals1.visibleSeparators[axis].SelectMany((Separator sep) => new int[]
					{
						sep.Line.Range.Min,
						sep.Line.Range.Max
					})).AppendItem(CS$<>8__locals1.separatorGroup.PixelBounds[axis].Min)
						.AppendItem(CS$<>8__locals1.separatorGroup.PixelBounds[axis].Max)
						.Distinct<int>()
					orderby pos
					select pos).AdjacentClumps(CS$<>8__locals1.tolerance, null);
				Func<Record<int, int>, Range<PixelUnit>> func;
				if ((func = SeparatorGridBuilder.<>O.<0>__Create) == null)
				{
					func = (SeparatorGridBuilder.<>O.<0>__Create = new Func<Record<int, int>, Range<PixelUnit>>(Range<PixelUnit>.Create));
				}
				return enumerable.Select(func);
			});
			CS$<>8__locals1.borderPositions.Select((IReadOnlyList<Range<PixelUnit>> ranges) => ranges.Enumerate<Range<PixelUnit>>().SelectMany2((int tableIndex, Range<PixelUnit> range) => range.AsEnumerable.Select((int pos) => KVP.Create<int, int>(pos, tableIndex))).ToDictionary<int, int>());
			if (CS$<>8__locals1.borderPositions.Any((IReadOnlyList<Range<PixelUnit>> positions) => positions.Count <= 1))
			{
				return Optional<SeparatorGrid>.Nothing;
			}
			CS$<>8__locals1.cells = new RectangularArray<SeparatorGrid.SeparatorGridCell>(CS$<>8__locals1.borderPositions.Horizontal.Count - 1, CS$<>8__locals1.borderPositions.Vertical.Count - 1);
			CS$<>8__locals1.borders = new AxisAlignedList<MultiValueDictionary<int, Separator>>((Axis axis) => CS$<>8__locals1.borderPositions[axis].Select(delegate(Range<PixelUnit> range)
			{
				List<Separator> list = CS$<>8__locals1.visibleSeparators[axis.Perpendicular()].Where((Separator sep) => range.Contains(sep.Line.Position)).ToList<Separator>();
				MultiValueDictionary<int, Separator> multiValueDictionary = new MultiValueDictionary<int, Separator>();
				for (int m = 0; m < CS$<>8__locals1.borderPositions[axis.Perpendicular()].Count - 1; m++)
				{
					Range<PixelUnit> rangeAlongPos = new Range<PixelUnit>(CS$<>8__locals1.borderPositions[axis.Perpendicular()][m].Max + 1, CS$<>8__locals1.borderPositions[axis.Perpendicular()][m + 1].Min - 1);
					multiValueDictionary.AddRange(m, list.Where((Separator sep) => sep.Line.Range.Overlaps(rangeAlongPos)));
				}
				return multiValueDictionary;
			}));
			int num = 0;
			for (int i = 0; i < CS$<>8__locals1.cells.Width; i++)
			{
				for (int j = 0; j < CS$<>8__locals1.cells.Height; j++)
				{
					SeparatorGridBuilder.<>c__DisplayClass2_6 CS$<>8__locals2 = new SeparatorGridBuilder.<>c__DisplayClass2_6();
					CS$<>8__locals2.CS$<>8__locals2 = CS$<>8__locals1;
					if (CS$<>8__locals2.CS$<>8__locals2.cells[i, j] == null)
					{
						int num2 = CS$<>8__locals2.CS$<>8__locals2.cells.Width;
						int num3 = CS$<>8__locals2.CS$<>8__locals2.cells.Height;
						for (int k = i + 1; k <= num2; k++)
						{
							for (int l = j + 1; l <= num3; l++)
							{
								if (CS$<>8__locals2.CS$<>8__locals2.borders[Axis.Horizontal][k][l - 1].Any<Separator>())
								{
									num2 = k;
								}
								if (CS$<>8__locals2.CS$<>8__locals2.borders[Axis.Vertical][l][k - 1].Any<Separator>())
								{
									num3 = l;
								}
							}
						}
						SeparatorGridBuilder.<>c__DisplayClass2_6 CS$<>8__locals3 = CS$<>8__locals2;
						int num4 = i;
						int num5 = j;
						CS$<>8__locals3.cellBorderPositions = new Directed<int>(num4, num2, num5, num3);
						CS$<>8__locals2.cellTableBounds = new Bounds<TableUnit>(delegate(Direction dir)
						{
							if (dir.Derivative() != Derivative.Decreasing)
							{
								return CS$<>8__locals2.cellBorderPositions[dir] - 1;
							}
							return CS$<>8__locals2.cellBorderPositions[dir];
						});
						Bounds<PixelUnit> bounds = new Bounds<PixelUnit>((Direction dir) => CS$<>8__locals2.CS$<>8__locals2.borderPositions[dir.AlignedAxis()][((dir.Derivative() == Derivative.Increasing) ? 1 : 0) + CS$<>8__locals2.cellTableBounds[dir]][dir.Opposite().Derivative()]).Extend(-CS$<>8__locals2.CS$<>8__locals2.contractCellsBy);
						foreach (Direction direction in DirectionUtilities.Directions)
						{
							Axis axis = direction.AlignedAxis();
							int borderPosition = CS$<>8__locals2.cellBorderPositions[direction];
							if (!((direction.Derivative() == Derivative.Decreasing) ? (borderPosition == 0) : (borderPosition == CS$<>8__locals2.CS$<>8__locals2.cells.GetLength(axis))) && !new Ranges<PixelUnit>(CS$<>8__locals2.cellTableBounds[axis.Perpendicular()].AsEnumerable.SelectMany(delegate(int pos)
							{
								IEnumerable<Separator> enumerable2 = CS$<>8__locals2.CS$<>8__locals2.borders[axis][borderPosition][pos];
								Func<Separator, Range<PixelUnit>> func2;
								if ((func2 = CS$<>8__locals2.CS$<>8__locals2.<>9__21) == null)
								{
									func2 = (CS$<>8__locals2.CS$<>8__locals2.<>9__21 = (Separator border) => border.Line.Range.Expand(CS$<>8__locals2.CS$<>8__locals2.tolerance));
								}
								return enumerable2.Select(func2);
							})).Contains(bounds[axis.Perpendicular()]))
							{
								return Optional<SeparatorGrid>.Nothing;
							}
						}
						ShadedBounds shadedBounds = backgrounds.OverlappingElements(bounds).ArgMax((ShadedBounds b) => b.RenderingOrders.Max);
						Color color = ((shadedBounds != null) ? shadedBounds.ShadingColor : GraphicalPath.BackgroundColor);
						num++;
						Bounds<TableUnit> cellTableBounds = CS$<>8__locals2.cellTableBounds;
						SeparatorGrid.SeparatorGridCell separatorGridCell = new SeparatorGrid.SeparatorGridCell(new ShadedBounds(bounds, color, (shadedBounds != null) ? shadedBounds.RenderingOrders : Range<IndexUnit>.CreateAt(-1)), cellTableBounds);
						foreach (int num6 in separatorGridCell.Span.Horizontal.AsEnumerable)
						{
							foreach (int num7 in separatorGridCell.Span.Vertical.AsEnumerable)
							{
								if (CS$<>8__locals2.CS$<>8__locals2.cells[num6, num7] != null)
								{
									return Optional<SeparatorGrid>.Nothing;
								}
								CS$<>8__locals2.CS$<>8__locals2.cells[num6, num7] = separatorGridCell;
							}
						}
					}
				}
			}
			return (num > 1).Then(delegate
			{
				SeparatorGroup separatorGroup2 = CS$<>8__locals1.separatorGroup;
				RectangularArray<SeparatorGrid.SeparatorGridCell> cells = CS$<>8__locals1.cells;
				int lineWidth = CS$<>8__locals1.lineWidth;
				Func<Direction, bool> func3;
				if ((func3 = CS$<>8__locals1.<>9__22) == null)
				{
					func3 = (CS$<>8__locals1.<>9__22 = (Direction dir) => CS$<>8__locals1.borders[dir.AlignedAxis()][(dir.Derivative() == Derivative.Decreasing) ? 0 : CS$<>8__locals1.cells.GetLength(dir.AlignedAxis())].All((KeyValuePair<int, IReadOnlyCollection<Separator>> kv) => kv.Value.Any<Separator>()));
				}
				return new SeparatorGrid(separatorGroup2, cells, lineWidth, new Directed<bool>(func3));
			});
		}

		// Token: 0x02000D66 RID: 3430
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400285D RID: 10333
			[Nullable(0)]
			public static Func<Record<int, int>, Range<PixelUnit>> <0>__Create;
		}
	}
}
