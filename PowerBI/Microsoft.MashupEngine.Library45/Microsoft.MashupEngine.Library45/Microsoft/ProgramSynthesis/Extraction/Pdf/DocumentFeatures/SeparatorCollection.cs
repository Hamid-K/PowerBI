using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D4B RID: 3403
	[NullableContext(1)]
	[Nullable(0)]
	public class SeparatorCollection
	{
		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x060056F4 RID: 22260 RVA: 0x00113292 File Offset: 0x00111492
		public AxisAligned<QuadTree<Separator, PixelUnit>> Separators { get; }

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x060056F5 RID: 22261 RVA: 0x0011329A File Offset: 0x0011149A
		public QuadTree<ShadedBounds, PixelUnit> Backgrounds { get; }

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x060056F6 RID: 22262 RVA: 0x001132A2 File Offset: 0x001114A2
		public AxisAligned<IReadOnlyList<TextDecoration>> TextDecorations { get; }

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x060056F7 RID: 22263 RVA: 0x001132AA File Offset: 0x001114AA
		public QuadTree<SeparatorGroup, PixelUnit> Groups { get; }

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x060056F8 RID: 22264 RVA: 0x001132B4 File Offset: 0x001114B4
		internal static SeparatorCollection Empty
		{
			get
			{
				return new SeparatorCollection(new AxisAligned<QuadTree<Separator, PixelUnit>>(new QuadTree<Separator, PixelUnit>(Bounds<PixelUnit>.Zero)), new QuadTree<ShadedBounds, PixelUnit>(Bounds<PixelUnit>.Zero), new AxisAlignedList<TextDecoration>((Axis _) => new TextDecoration[0]), new QuadTree<SeparatorGroup, PixelUnit>(Bounds<PixelUnit>.Zero));
			}
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x0011330D File Offset: 0x0011150D
		internal SeparatorCollection(AxisAligned<QuadTree<Separator, PixelUnit>> separators, QuadTree<ShadedBounds, PixelUnit> backgrounds, AxisAligned<IReadOnlyList<TextDecoration>> textDecorations, QuadTree<SeparatorGroup, PixelUnit> separatorGroups)
		{
			this.Separators = separators;
			this.Backgrounds = backgrounds;
			this.TextDecorations = textDecorations;
			this.Groups = separatorGroups;
		}

		// Token: 0x060056FA RID: 22266 RVA: 0x00113332 File Offset: 0x00111532
		public bool AnySeparates(Axis axis, IApparentPixelBounded first, IApparentPixelBounded second)
		{
			return this.AnySeparatesBounds(axis, first.ApparentPixelBounds, second.ApparentPixelBounds);
		}

		// Token: 0x060056FB RID: 22267 RVA: 0x00113348 File Offset: 0x00111548
		public bool AnySeparatesBounds(Axis axis, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> first, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> second)
		{
			return (from between in Bounds<PixelUnit>.MaybeBetweenCenters(axis.Perpendicular(), first, second)
				select this.Separators[axis].OverlappingElements(between).Any<Separator>()).OrElse(false);
		}

		// Token: 0x060056FC RID: 22268 RVA: 0x00113394 File Offset: 0x00111594
		public Func<IRotatedPixelBounded, IRotatedPixelBounded, bool> AnySeparatesFunc(Axis axis, [Nullable(2)] TransformationMatrix matrix)
		{
			Axis realAxis;
			if (matrix == null || !matrix.IsRotated)
			{
				realAxis = axis;
			}
			else
			{
				if (!matrix.IsRotatedByRightAngle)
				{
					return (IRotatedPixelBounded _, IRotatedPixelBounded __) => false;
				}
				realAxis = (matrix.IsRotatedByEvenRightAngle ? axis : axis.Perpendicular());
			}
			return (IRotatedPixelBounded first, IRotatedPixelBounded second) => this.AnySeparates(realAxis, first, second);
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x00113410 File Offset: 0x00111610
		public Color GetBackgroundColor(IApparentPixelBounded el)
		{
			ShadedBounds shadedBounds = this.Backgrounds.OverlappingElements(el.ApparentPixelBounds).ArgMax((ShadedBounds b) => b.RenderingOrders.Max);
			if (shadedBounds == null)
			{
				return GraphicalPath.BackgroundColor;
			}
			return shadedBounds.ShadingColor;
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x00113464 File Offset: 0x00111664
		internal static SeparatorCollection Build(PdfAnalyzerOptions options, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, IReadOnlyList<IReadOnlyList<Glyph>> glyphs, PathCollection paths, ImageCollection images)
		{
			SeparatorCollection.<>c__DisplayClass21_0 CS$<>8__locals1 = new SeparatorCollection.<>c__DisplayClass21_0();
			CS$<>8__locals1.pageBounds = pageBounds;
			CS$<>8__locals1.paths = paths;
			CS$<>8__locals1.glyphs = glyphs;
			if (!CS$<>8__locals1.paths.Paths.Any<GraphicalPath>() && !images.SeparatorImages.Any<Image>())
			{
				return SeparatorCollection.Empty;
			}
			int? num = CS$<>8__locals1.glyphs.Select((IReadOnlyList<Glyph> g) => g[0]).MaybeMin((Glyph g) => g.ApparentPixelBoundsWithoutRotation.Height()).OrElseNull<int>();
			CS$<>8__locals1.boxAsLineWidthCutoff = Math.Min((num - 1) ?? int.MaxValue, 10);
			CS$<>8__locals1.separators = new AxisAligned<QuadTree<Separator, PixelUnit>>((Axis _) => new QuadTree<Separator, PixelUnit>(CS$<>8__locals1.pageBounds));
			CS$<>8__locals1.backgrounds = new QuadTree<ShadedBounds, PixelUnit>(CS$<>8__locals1.pageBounds);
			CS$<>8__locals1.enableThickPathAxisChanging = options.Version >= PdfAnalyzerVersion.V1_1;
			CS$<>8__locals1.mergeBoxesBehind = options.Version >= PdfAnalyzerVersion.V1_3;
			using (IEnumerator<GraphicalPath> enumerator = CS$<>8__locals1.paths.Paths.OrderBy((GraphicalPath p) => p.RenderingOrder).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SeparatorCollection.<>c__DisplayClass21_2 CS$<>8__locals2 = new SeparatorCollection.<>c__DisplayClass21_2();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals2.path = enumerator.Current;
					if (CS$<>8__locals2.path.IsStroked)
					{
						IEnumerable<Record<Vector<PixelUnit>, Vector<PixelUnit>>> enumerable = CS$<>8__locals2.path.EnumerateLines().OrElseDefault<IEnumerable<Record<Vector<PixelUnit>, Vector<PixelUnit>>>>();
						if (enumerable != null)
						{
							foreach (Record<Vector<PixelUnit>, Vector<PixelUnit>> record in enumerable)
							{
								Optional<AxisAlignedLine<PixelUnit>> optional = record.MaybeAsAxisAlignedLine<PixelUnit>();
								if (optional.HasValue)
								{
									AxisAlignedLine<PixelUnit> value = optional.Value;
									CS$<>8__locals2.CS$<>8__locals1.<Build>g__AddLineToSeparatorsForPath|4(value, CS$<>8__locals2.path);
								}
							}
						}
					}
					Color? fillColor = CS$<>8__locals2.path.FillColor;
					if (fillColor != null)
					{
						CS$<>8__locals2.pathFillColor = fillColor.GetValueOrDefault();
						Optional<Bounds<PixelUnit>> optional2 = CS$<>8__locals2.path.MaybeAsBox();
						if (optional2.HasValue)
						{
							CS$<>8__locals2.<Build>g__AddBox|8(optional2.Value, true);
						}
						else if (!CS$<>8__locals2.path.IsStroked)
						{
							Optional<List<AxisAlignedLine<PixelUnit>>> optional3 = from lines in CS$<>8__locals2.path.EnumerateAxisAlignedLines()
								select lines.ToList<AxisAlignedLine<PixelUnit>>();
							if (optional3.HasValue)
							{
								Optional<AxisAlignedLine<PixelUnit>> optional4 = optional3.SelectMany((List<AxisAlignedLine<PixelUnit>> lines) => lines.MaybeOnly<AxisAlignedLine<PixelUnit>>());
								if (optional4.HasValue)
								{
									AxisAlignedLine<PixelUnit> value2 = optional4.Value;
									CS$<>8__locals2.CS$<>8__locals1.<Build>g__AddLineToSeparators|3(value2, CS$<>8__locals2.path.FillColor, null, 1);
								}
								else
								{
									foreach (AxisAlignedLine<PixelUnit> axisAlignedLine in optional3.Value)
									{
										CS$<>8__locals2.CS$<>8__locals1.<Build>g__AddLineToSeparators|3(axisAlignedLine, null, CS$<>8__locals2.path.FillColor, 0);
									}
									foreach (Bounds<PixelUnit> bounds in optional3.Value.AsBoxes())
									{
										CS$<>8__locals2.<Build>g__AddBox|8(bounds, false);
									}
								}
							}
						}
					}
				}
			}
			foreach (Separator separator in images.Separators)
			{
				CS$<>8__locals1.separators[separator.Line.Axis].Add(separator);
			}
			AxisAlignedList<TextDecoration> axisAlignedList = new AxisAlignedList<TextDecoration>((Axis axis) => SeparatorCollection.RecognizeTextDecorations(axis, CS$<>8__locals1.separators, CS$<>8__locals1.glyphs));
			QuadTree<SeparatorGroup, PixelUnit> quadTree = SeparatorCollection.RecognizeSeparatorGroups(CS$<>8__locals1.separators, num);
			return new SeparatorCollection(CS$<>8__locals1.separators, CS$<>8__locals1.backgrounds, axisAlignedList, quadTree);
		}

		// Token: 0x060056FF RID: 22271 RVA: 0x00113928 File Offset: 0x00111B28
		private static QuadTree<SeparatorGroup, PixelUnit> RecognizeSeparatorGroups(AxisAligned<QuadTree<Separator, PixelUnit>> separators, int? minGlyphHeight)
		{
			SeparatorCollection.<>c__DisplayClass22_0 CS$<>8__locals1 = new SeparatorCollection.<>c__DisplayClass22_0();
			if (separators.All((QuadTree<Separator, PixelUnit> tree) => tree.IsEmpty()))
			{
				return new QuadTree<SeparatorGroup, PixelUnit>(separators.Horizontal.Bounds);
			}
			CS$<>8__locals1.minSeparatorLength = (2 * minGlyphHeight) ?? 50;
			CS$<>8__locals1.containingGroup = new Dictionary<Separator, AxisAlignedSet<Separator>>();
			Dictionary<Separator, Record<Direction, Separator>> dictionary = new Dictionary<Separator, Record<Direction, Separator>>();
			CS$<>8__locals1.groups = new HashSet<AxisAlignedSet<Separator>>();
			foreach (Separator separator in separators.Horizontal)
			{
				if (!CS$<>8__locals1.<RecognizeSeparatorGroups>g__IsSeparatorTooSmall|2(separator))
				{
					SeparatorCollection.<>c__DisplayClass22_2 CS$<>8__locals2;
					CS$<>8__locals2.newGroup = new AxisAlignedSet<Separator>();
					CS$<>8__locals2.newGroup.Horizontal.Add(separator);
					Func<Separator, bool> func = SeparatorCollection.<RecognizeSeparatorGroups>g__StyleMatchesFunc|22_3(separator);
					int num = Math.Max(2, separator.LineWidth) + 2;
					IEnumerable<Separator> enumerable = separators.Vertical.OverlappingElements(separator.PixelBounds.Extend(num));
					Directed<List<Separator>> directed = new Directed<List<Separator>>((Direction _) => new List<Separator>());
					foreach (Separator separator2 in enumerable)
					{
						if (!CS$<>8__locals1.<RecognizeSeparatorGroups>g__IsSeparatorTooSmall|2(separator2) && func(separator2))
						{
							CS$<>8__locals1.<RecognizeSeparatorGroups>g__AddVerticalSeparatorToGroup|11(separator2, ref CS$<>8__locals2);
						}
						else
						{
							if (MathUtils.WithinTolerance(separator.Line.Range.Min, separator2.Line.Position, num))
							{
								directed[Direction.Left].Add(separator2);
							}
							else if (MathUtils.WithinTolerance(separator.Line.Range.Max, separator2.Line.Position, num))
							{
								directed[Direction.Right].Add(separator2);
							}
							if (MathUtils.WithinTolerance(separator.Line.Position, separator2.Line.Range.Min, num))
							{
								directed[Direction.Down].Add(separator2);
							}
							else if (MathUtils.WithinTolerance(separator.Line.Position, separator2.Line.Range.Max, num))
							{
								directed[Direction.Up].Add(separator2);
							}
						}
					}
					if (directed[Direction.Left].Count > 0 && directed[Direction.Right].Count > 0)
					{
						foreach (Separator separator3 in directed[Direction.Left].Concat(directed[Direction.Right]))
						{
							CS$<>8__locals1.<RecognizeSeparatorGroups>g__AddVerticalSeparatorToGroup|11(separator3, ref CS$<>8__locals2);
						}
					}
					foreach (Direction direction in Axis.Vertical.AlignedDirections())
					{
						foreach (Separator separator4 in directed[direction])
						{
							Record<Direction, Separator> record;
							if (dictionary.TryGetValue(separator4, out record))
							{
								Direction direction2;
								Separator separator5;
								record.Deconstruct(out direction2, out separator5);
								Direction direction3 = direction2;
								Separator otherSeparator = separator5;
								if (direction3 != direction)
								{
									CS$<>8__locals1.<RecognizeSeparatorGroups>g__AddVerticalSeparatorToGroup|11(separator4, ref CS$<>8__locals2);
									dictionary.Remove(separator4);
									AxisAlignedSet<Separator> axisAlignedSet = CS$<>8__locals1.groups.OnlyOrDefault((AxisAlignedSet<Separator> g) => g.Horizontal.Contains(otherSeparator));
									if (axisAlignedSet != null)
									{
										CS$<>8__locals1.<RecognizeSeparatorGroups>g__MergeGroup|10(axisAlignedSet, ref CS$<>8__locals2);
									}
								}
							}
							else
							{
								dictionary[separator4] = Record.Create<Direction, Separator>(direction, separator);
							}
						}
					}
					CS$<>8__locals1.groups.Add(CS$<>8__locals2.newGroup);
				}
			}
			using (Dictionary<Separator, Record<Direction, Separator>>.Enumerator enumerator5 = dictionary.GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					SeparatorCollection.<>c__DisplayClass22_4 CS$<>8__locals4 = new SeparatorCollection.<>c__DisplayClass22_4();
					Separator separator5;
					Record<Direction, Separator> record2;
					enumerator5.Current.Deconstruct(out separator5, out record2);
					Direction direction2;
					Separator separator6;
					record2.Deconstruct(out direction2, out separator6);
					CS$<>8__locals4.unmatchedSeparator = separator5;
					CS$<>8__locals4.dir = direction2;
					CS$<>8__locals4.connectedSeparator = separator6;
					int expectedPos = CS$<>8__locals4.unmatchedSeparator.Line.Range[CS$<>8__locals4.dir.Derivative()];
					int tolerance2 = Math.Max(2, CS$<>8__locals4.connectedSeparator.LineWidth) + 2;
					Func<Separator, bool> <>9__16;
					AxisAlignedSet<Separator> axisAlignedSet2 = CS$<>8__locals1.groups.OnlyOrDefault(delegate(AxisAlignedSet<Separator> g)
					{
						if (g.Vertical.Any<Separator>() && !g.Vertical.Contains(CS$<>8__locals4.unmatchedSeparator))
						{
							IEnumerable<Separator> horizontal = g.Horizontal;
							Func<Separator, bool> func2;
							if ((func2 = CS$<>8__locals4.<>9__15) == null)
							{
								func2 = (CS$<>8__locals4.<>9__15 = (Separator sep) => sep.Line.Range.Contains(CS$<>8__locals4.unmatchedSeparator.Line.Position));
							}
							if (horizontal.Any(func2))
							{
								IEnumerable<Separator> horizontal2 = g.Horizontal;
								Func<Separator, bool> func3;
								if ((func3 = <>9__16) == null)
								{
									func3 = (<>9__16 = (Separator sep) => MathUtils.WithinTolerance(expectedPos, sep.Line.Position, tolerance2));
								}
								if (!horizontal2.Any(func3))
								{
									int expectedPos2 = expectedPos;
									int num2;
									if (CS$<>8__locals4.dir != Direction.Up)
									{
										num2 = g.Vertical.Max((Separator vertical) => vertical.Line.Range.Max);
									}
									else
									{
										num2 = g.Vertical.Min((Separator vertical) => vertical.Line.Range.Min);
									}
									return MathUtils.WithinTolerance(expectedPos2, num2, tolerance2);
								}
							}
						}
						return false;
					});
					if (axisAlignedSet2 != null)
					{
						AxisAlignedSet<Separator> axisAlignedSet3 = CS$<>8__locals1.groups.SingleOrDefault((AxisAlignedSet<Separator> g) => g.Vertical.Contains(CS$<>8__locals4.unmatchedSeparator));
						if (axisAlignedSet3 != null)
						{
							CS$<>8__locals1.<RecognizeSeparatorGroups>g__MergeGroupInto|4(axisAlignedSet3, axisAlignedSet2);
						}
						else
						{
							axisAlignedSet2.Vertical.Add(CS$<>8__locals4.unmatchedSeparator);
						}
						CS$<>8__locals1.<RecognizeSeparatorGroups>g__MergeGroupInto|4(CS$<>8__locals1.groups.Single((AxisAlignedSet<Separator> g) => g.Horizontal.Contains(CS$<>8__locals4.connectedSeparator)), axisAlignedSet2);
					}
				}
			}
			QuadTree<SeparatorGroup, PixelUnit> quadTree = new QuadTree<SeparatorGroup, PixelUnit>(separators.Horizontal.Bounds.Join(separators.Vertical.Bounds), CS$<>8__locals1.groups.Select((AxisAlignedSet<Separator> g) => new SeparatorGroup(g.AsEnumerable.SelectMany((HashSet<Separator> set) => set))).Concat(from sep in separators.Vertical.Except(CS$<>8__locals1.containingGroup.Keys)
				where !base.<RecognizeSeparatorGroups>g__IsSeparatorTooSmall|2(sep)
				select new SeparatorGroup(sep.Yield<Separator>())));
			bool flag;
			do
			{
				flag = false;
				using (IEnumerator<SeparatorGroup> enumerator6 = quadTree.OrderBy((SeparatorGroup g) => g, CompareByIBoundedSizeDescending<SeparatorGroup, PixelUnit>.Instance).GetEnumerator())
				{
					while (enumerator6.MoveNext())
					{
						SeparatorGroup separatorGroup = enumerator6.Current;
						int tolerance = 4;
						IReadOnlyList<SeparatorGroup> readOnlyList = (from other in quadTree.OverlappingElements(separatorGroup.PixelBounds)
							where other != separatorGroup && AxisUtilities.Axes.Any((Axis axis) => RangeUtils.WithinToleranceOnBothSides<PixelUnit>(separatorGroup.PixelBounds[axis], other.PixelBounds[axis], tolerance))
							select other).ToList<SeparatorGroup>();
						if (readOnlyList.Any<SeparatorGroup>())
						{
							quadTree.RemoveRange(readOnlyList.PrependItem(separatorGroup));
							quadTree.Add(new SeparatorGroup(readOnlyList.PrependItem(separatorGroup).SelectMany((SeparatorGroup g) => g.Separators)));
							flag = true;
							break;
						}
					}
				}
			}
			while (flag);
			return quadTree;
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x001140F8 File Offset: 0x001122F8
		private static IReadOnlyList<TextDecoration> RecognizeTextDecorations(Axis axis, AxisAligned<QuadTree<Separator, PixelUnit>> separators, IReadOnlyList<IReadOnlyList<Glyph>> glyphs)
		{
			Dictionary<Record<Separator, TextDecorationKind>, List<Glyph>> dictionary = new Dictionary<Record<Separator, TextDecorationKind>, List<Glyph>>();
			Axis perpendicular = axis.Perpendicular();
			IReadOnlyList<IGrouping<int, Separator>> readOnlyList = (from sep in separators[axis]
				where sep.StrokingColor != null
				group sep by sep.Line.Position into g
				orderby g.Key
				select g).ToList<IGrouping<int, Separator>>();
			IEnumerable<IGrouping<Range<PixelUnit>, Record<TextDecorationKind, IReadOnlyList<Glyph>>>> enumerable = from glyphGroup in glyphs
				let glyph = glyphGroup[0]
				where glyph.BaseLineEdge != null
				let baseLineEdge = glyph.BaseLineEdge.Value
				where baseLineEdge.AlignedAxis() == perpendicular
				let tolerance = 2 + glyph.ApparentPixelBounds[perpendicular].Size() / 5
				from kind in TextDecorationKindUtil.Kinds
				group Record.Create<TextDecorationKind, IReadOnlyList<Glyph>>(kind, glyphGroup) by kind.GetPositionRange(glyph, baseLineEdge, tolerance) into g
				orderby g.Key.Min
				select g;
			int num = 0;
			Glyph glyph;
			foreach (IGrouping<Range<PixelUnit>, Record<TextDecorationKind, IReadOnlyList<Glyph>>> grouping in enumerable)
			{
				Range<PixelUnit> range = grouping.Key;
				Optional<int> optional = from p in readOnlyList.Enumerate<IGrouping<int, Separator>>().Skip(num).MaybeFirst((Record<int, IGrouping<int, Separator>> p) => p.Item2.Key > range.Min)
					select p.Item1;
				if (!optional.HasValue)
				{
					break;
				}
				num = optional.Value;
				IReadOnlyList<Separator> readOnlyList2 = readOnlyList.Skip(num).TakeWhile((IGrouping<int, Separator> p) => range.Max > p.Key).SelectMany((IGrouping<int, Separator> seps) => seps)
					.ToList<Separator>();
				if (readOnlyList2.Any<Separator>())
				{
					foreach (Record<TextDecorationKind, IReadOnlyList<Glyph>> record in grouping)
					{
						TextDecorationKind textDecorationKind;
						IReadOnlyList<Glyph> readOnlyList3;
						record.Deconstruct(out textDecorationKind, out readOnlyList3);
						TextDecorationKind textDecorationKind2 = textDecorationKind;
						using (IEnumerator<Glyph> enumerator3 = readOnlyList3.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								glyph = enumerator3.Current;
								foreach (Separator separator in readOnlyList2)
								{
									if (separator.Line.Range.Expand(2).Contains(glyph.ApparentPixelBounds[axis]))
									{
										dictionary.GetOrCreateValue(Record.Create<Separator, TextDecorationKind>(separator, textDecorationKind2)).Add(glyph);
									}
								}
							}
						}
					}
				}
			}
			Comparison<Glyph> <>9__19;
			Func<Glyph, int> <>9__20;
			Func<Glyph, Range<PixelUnit>> <>9__21;
			HashSet<TextDecoration> hashSet = dictionary.Where2(delegate(Record<Separator, TextDecorationKind> decorationInfo, List<Glyph> decoratedGlyphs)
			{
				Separator separator2;
				TextDecorationKind textDecorationKind3;
				decorationInfo.Deconstruct(out separator2, out textDecorationKind3);
				Separator separator3 = separator2;
				Comparison<Glyph> comparison;
				if ((comparison = <>9__19) == null)
				{
					comparison = (<>9__19 = (Glyph a, Glyph b) => a.ApparentPixelBounds[axis].Min - b.ApparentPixelBounds[axis].Min);
				}
				decoratedGlyphs.Sort(comparison);
				Range<PixelUnit> range2 = new Range<PixelUnit>(decoratedGlyphs[0].ApparentPixelBounds[axis].Min, decoratedGlyphs.Last<Glyph>().ApparentPixelBounds[axis].Max);
				Func<Glyph, int> func;
				if ((func = <>9__20) == null)
				{
					func = (<>9__20 = (Glyph glyph) => glyph.ApparentPixelBounds[axis].Size());
				}
				double num2 = decoratedGlyphs.Average(func);
				int num3 = Math.Max(5, (int)Math.Round(num2 / 2.0));
				if (!MathUtils.WithinTolerance(range2.Min, separator3.Line.Range.Min, num3) || !MathUtils.WithinTolerance(range2.Max, separator3.Line.Range.Max, num3))
				{
					return false;
				}
				int num4 = separator3.Line.Range.Min;
				int num5 = (int)Math.Round(2.0 * num2);
				Func<Glyph, Range<PixelUnit>> func2;
				if ((func2 = <>9__21) == null)
				{
					func2 = (<>9__21 = (Glyph g) => g.ApparentPixelBounds[axis]);
				}
				foreach (Range<PixelUnit> range3 in decoratedGlyphs.Select(func2))
				{
					if (range3.Min - num4 > num5)
					{
						return false;
					}
					num4 = Math.Max(num4, range3.Max);
				}
				return true;
			}).Select2((Record<Separator, TextDecorationKind> decorationInfo, List<Glyph> decoratedGlyphs) => new TextDecoration(decorationInfo.Item2, decorationInfo.Item1, decoratedGlyphs)).ConvertToHashSet<TextDecoration>();
			foreach (IGrouping<Separator, TextDecoration> grouping2 in (from t in hashSet
				group t by t.Separator).ToList<IGrouping<Separator, TextDecoration>>())
			{
				if (separators[perpendicular].OverlappingElements(grouping2.Key.PixelBounds).Any<Separator>())
				{
					hashSet.ExceptWith(grouping2);
				}
				else if (grouping2.Any((TextDecoration t) => t.Kind == TextDecorationKind.Underline))
				{
					hashSet.ExceptWith(grouping2.Where((TextDecoration t) => t.Kind == TextDecorationKind.Overline));
				}
			}
			foreach (TextDecoration textDecoration in hashSet)
			{
				separators[axis].Remove(textDecoration.Separator);
			}
			return hashSet.ToList<TextDecoration>();
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x001146B8 File Offset: 0x001128B8
		[return: Nullable(new byte[] { 1, 0, 1, 1 })]
		internal IEnumerable<Record<Separator, Separator>> EnumerateSeparatorPairs(Axis axis)
		{
			IEnumerable<Record<Range<PixelUnit>, List<Separator>>> enumerable;
			IReadOnlyList<Record<Range<PixelUnit>, List<Separator>>> groupedAndMergedSeparators = this.Separators[axis].OrderBy((Separator sep) => sep.Line.Position).GroupBy((Separator sep) => sep.Line.Position, (Separator sep) => sep, (int pos, IEnumerable<Separator> seps) => seps.GroupBy((Separator sep) => Record.Create<int, Color?>(sep.LineWidth, sep.StrokingColor), (Separator sep) => sep, (Record<int, Color?> sepStyle, IEnumerable<Separator> enumerable) => SeparatorCollection.<EnumerateSeparatorPairs>g__MergeOverlappingSeparators|24_0(pos, sepStyle, enumerable)).GroupBy((Record<IEnumerable<Separator>, Range<PixelUnit>> r) => r.Item2, (Record<IEnumerable<Separator>, Range<PixelUnit>> r) => r.Item1, (Range<PixelUnit> verticalRange, IEnumerable<IEnumerable<Separator>> xs) => Record.Create<Range<PixelUnit>, List<Separator>>(verticalRange, (from sep in xs.SelectMany((IEnumerable<Separator> x) => x)
				orderby sep.Line.Range.Size() descending
				select sep).ToList<Separator>())).ToList<Record<Range<PixelUnit>, List<Separator>>>()).SelectMany((List<Record<Range<PixelUnit>, List<Separator>>> g) => g)
				.ToList<Record<Range<PixelUnit>, List<Separator>>>();
			int num;
			for (int i = 1; i < groupedAndMergedSeparators.Count; i = num + 1)
			{
				SeparatorCollection.<>c__DisplayClass24_2 CS$<>8__locals1 = new SeparatorCollection.<>c__DisplayClass24_2();
				CS$<>8__locals1.bottomPosMin = groupedAndMergedSeparators[i].Item1.Min;
				using (List<Separator>.Enumerator enumerator = groupedAndMergedSeparators[i].Item2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SeparatorCollection.<>c__DisplayClass24_3 CS$<>8__locals2 = new SeparatorCollection.<>c__DisplayClass24_3();
						CS$<>8__locals2.bottom = enumerator.Current;
						enumerable = groupedAndMergedSeparators.Take(i).Reverse<Record<Range<PixelUnit>, List<Separator>>>();
						Func<Record<Range<PixelUnit>, List<Separator>>, bool> func;
						if ((func = CS$<>8__locals1.<>9__19) == null)
						{
							func = (CS$<>8__locals1.<>9__19 = (Record<Range<PixelUnit>, List<Separator>> g) => g.Item1.Max < CS$<>8__locals1.bottomPosMin);
						}
						Optional<int> optional = from r in enumerable.Where(func).MaybeFirst(delegate(Record<Range<PixelUnit>, List<Separator>> g)
							{
								IEnumerable<Separator> item = g.Item2;
								Func<Separator, bool> func4;
								if ((func4 = CS$<>8__locals2.<>9__22) == null)
								{
									func4 = (CS$<>8__locals2.<>9__22 = (Separator possibleTop) => possibleTop.Line.Range.Overlaps(CS$<>8__locals2.bottom.Line.Range));
								}
								return item.Any(func4);
							})
							select r.Item1.Min;
						if (optional.HasValue)
						{
							int topPosMin = optional.Value;
							IEnumerable<Record<Range<PixelUnit>, List<Separator>>> enumerable2 = groupedAndMergedSeparators.Take(i).Reverse<Record<Range<PixelUnit>, List<Separator>>>();
							Func<Record<Range<PixelUnit>, List<Separator>>, bool> func2;
							Func<Record<Range<PixelUnit>, List<Separator>>, bool> <>9__23;
							if ((func2 = <>9__23) == null)
							{
								func2 = (<>9__23 = (Record<Range<PixelUnit>, List<Separator>> g) => g.Item1.Max >= topPosMin);
							}
							IEnumerable<Separator> enumerable3 = enumerable2.Where(func2).SelectMany2((Range<PixelUnit> _, List<Separator> g) => g);
							Func<Separator, bool> func3;
							if ((func3 = CS$<>8__locals2.<>9__25) == null)
							{
								func3 = (CS$<>8__locals2.<>9__25 = (Separator top) => top.Line.Range.Overlaps(CS$<>8__locals2.bottom.Line.Range));
							}
							foreach (Separator separator in enumerable3.Where(func3))
							{
								yield return Record.Create<Separator, Separator>(separator, CS$<>8__locals2.bottom);
							}
							IEnumerator<Separator> enumerator2 = null;
							CS$<>8__locals2 = null;
						}
					}
				}
				List<Separator>.Enumerator enumerator = default(List<Separator>.Enumerator);
				CS$<>8__locals1 = null;
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x06005702 RID: 22274 RVA: 0x001146D0 File Offset: 0x001128D0
		[CompilerGenerated]
		internal static bool <RecognizeSeparatorGroups>g__IsBackgroundColored|22_1(Separator separator)
		{
			return separator.StrokingColor != null && separator.StrokingColor.Value.ColorEquals(GraphicalPath.BackgroundColor);
		}

		// Token: 0x06005703 RID: 22275 RVA: 0x00114708 File Offset: 0x00112908
		[CompilerGenerated]
		internal static Func<Separator, bool> <RecognizeSeparatorGroups>g__StyleMatchesFunc|22_3(Separator separator)
		{
			bool separatorIsBackgroundColored = SeparatorCollection.<RecognizeSeparatorGroups>g__IsBackgroundColored|22_1(separator);
			Color? strokingColor = separator.StrokingColor;
			if (strokingColor != null)
			{
				Color separatorStrokingColor = strokingColor.GetValueOrDefault();
				return delegate(Separator other)
				{
					if (other.StrokingColor != null)
					{
						return separatorStrokingColor.ColorEquals(other.StrokingColor.Value, 2);
					}
					if (separatorIsBackgroundColored)
					{
						return true;
					}
					Color? fillColor = other.FillColor;
					if (fillColor != null)
					{
						Color valueOrDefault = fillColor.GetValueOrDefault();
						return separatorStrokingColor.ColorEquals(valueOrDefault, 2);
					}
					return false;
				};
			}
			return delegate(Separator other)
			{
				if ((other.StrokingColor != null || other.FillColor == null || separator.FillColor == null || !other.FillColor.Value.ColorEquals(separator.FillColor.Value, 2)) && !SeparatorCollection.<RecognizeSeparatorGroups>g__IsBackgroundColored|22_1(other))
				{
					Color? fillColor2 = separator.FillColor;
					if (fillColor2 != null)
					{
						Color valueOrDefault2 = fillColor2.GetValueOrDefault();
						if (other.StrokingColor != null)
						{
							return valueOrDefault2.ColorEquals(other.StrokingColor.Value, 2);
						}
					}
					return false;
				}
				return true;
			};
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x00114770 File Offset: 0x00112970
		[NullableContext(0)]
		[CompilerGenerated]
		[return: Nullable(new byte[] { 0, 1, 1, 0, 1 })]
		internal static Record<IEnumerable<Separator>, Range<PixelUnit>> <EnumerateSeparatorPairs>g__MergeOverlappingSeparators|24_0(int pos, Record<int, Color?> sepStyle, [Nullable(1)] IEnumerable<Separator> sameStyleSeps)
		{
			int num;
			Color? color;
			sepStyle.Deconstruct(out num, out color);
			int lineWidth = num;
			Color? strokingColor = color;
			int distanceLimit = Math.Max(1, (int)Math.Ceiling((double)lineWidth / 2.0));
			return Record.Create<IEnumerable<Separator>, Range<PixelUnit>>(from line in (from sep in sameStyleSeps
					select sep.Line into line
					orderby line.Range.Min
					select line).AggregateSeedFunc((AxisAlignedLine<PixelUnit> first) => ImmutableList<AxisAlignedLine<PixelUnit>>.Empty.Add(first), delegate(ImmutableList<AxisAlignedLine<PixelUnit>> lines, AxisAlignedLine<PixelUnit> line)
				{
					if (lines.Last<AxisAlignedLine<PixelUnit>>().Range.Distance(line.Range) <= distanceLimit)
					{
						return lines.SetItem(lines.Count - 1, new AxisAlignedLine<PixelUnit>(line.Axis, line.Range.Join(lines.Last<AxisAlignedLine<PixelUnit>>().Range), line.Position));
					}
					return lines.Add(line);
				})
				select new Separator(line, strokingColor, null, lineWidth), new Range<PixelUnit>(pos - distanceLimit, pos + distanceLimit));
		}

		// Token: 0x040027D2 RID: 10194
		private const int BoxAsLineWidthCutoff = 10;

		// Token: 0x040027D3 RID: 10195
		private const int TextDecorationToleranceFractionDenominator = 5;
	}
}
