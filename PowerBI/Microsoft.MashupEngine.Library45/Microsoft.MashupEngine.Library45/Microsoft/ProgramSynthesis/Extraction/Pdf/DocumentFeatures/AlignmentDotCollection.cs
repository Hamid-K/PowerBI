using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000C58 RID: 3160
	[NullableContext(1)]
	[Nullable(0)]
	public class AlignmentDotCollection
	{
		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06005181 RID: 20865 RVA: 0x0010048B File Offset: 0x000FE68B
		public QuadTree<AlignmentDotCollection.AlignmentDotColumn, PixelUnit> DotColumns { get; }

		// Token: 0x06005182 RID: 20866 RVA: 0x00100493 File Offset: 0x000FE693
		private AlignmentDotCollection(QuadTree<AlignmentDotCollection.AlignmentDotColumn, PixelUnit> dotColumns)
		{
			this.DotColumns = dotColumns;
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x001004A4 File Offset: 0x000FE6A4
		internal static AlignmentDotCollection Build([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, IReadOnlyList<IReadOnlyList<Glyph>> glyphs, PageStatistics pageStatistics, SeparatorCollection separators)
		{
			AlignmentDotCollection.<>c__DisplayClass6_0 CS$<>8__locals1 = new AlignmentDotCollection.<>c__DisplayClass6_0();
			CS$<>8__locals1.separators = separators;
			IReadOnlyList<IGrouping<int, Glyph>> readOnlyList = (from g in glyphs.SelectMany((IReadOnlyList<Glyph> g) => g)
				group g by g.ApparentPixelBounds.Top into g
				orderby g.Key
				select g).ToList<IGrouping<int, Glyph>>();
			HashSet<Glyph> hashSet = (from g in glyphs.SelectMany((IReadOnlyList<Glyph> g) => g)
				where !g.IsRotated && g.Text.Trim() == "."
				select g).ConvertToHashSet<Glyph>();
			HashSet<AlignmentDotCollection.AlignmentDotColumn> hashSet2 = new HashSet<AlignmentDotCollection.AlignmentDotColumn>();
			List<Record<IReadOnlyList<Glyph>, IReadOnlyList<Glyph>>> list = new List<Record<IReadOnlyList<Glyph>, IReadOnlyList<Glyph>>>();
			foreach (IEnumerable<Glyph> enumerable in (from d in hashSet
				group d by d.ApparentPixelBounds.Height()).ToList<IGrouping<int, Glyph>>())
			{
				List<Glyph> list2 = new List<Glyph>();
				int num = 0;
				int? num2 = null;
				using (IEnumerator<Glyph> enumerator2 = enumerable.OrderBy((Glyph d) => d.ApparentPixelBounds.Bottom).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						AlignmentDotCollection.<>c__DisplayClass6_1 CS$<>8__locals2 = new AlignmentDotCollection.<>c__DisplayClass6_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.dot = enumerator2.Current;
						if (hashSet.Contains(CS$<>8__locals2.dot))
						{
							double estimatedSpaceWidth = pageStatistics.GetEstimatedSpaceWidth(CS$<>8__locals2.dot);
							double num3 = estimatedSpaceWidth * 2.0;
							int? num4 = num2;
							int i = CS$<>8__locals2.dot.ApparentPixelBounds.Top;
							if (!((num4.GetValueOrDefault() == i) & (num4 != null)))
							{
								num2 = new int?(CS$<>8__locals2.dot.ApparentPixelBounds.Top);
								list2.RemoveAll((Glyph g) => g.ApparentPixelBounds.Bottom < CS$<>8__locals2.dot.ApparentPixelBounds.Top);
								int count = list2.Count;
								while (num < readOnlyList.Count && readOnlyList[num].Key <= CS$<>8__locals2.dot.ApparentPixelBounds.Bottom)
								{
									List<Glyph> list3 = list2;
									IEnumerable<Glyph> enumerable2 = readOnlyList[num];
									Func<Glyph, bool> func;
									if ((func = CS$<>8__locals2.<>9__15) == null)
									{
										func = (CS$<>8__locals2.<>9__15 = (Glyph g) => g.ApparentPixelBounds.Bottom >= CS$<>8__locals2.dot.ApparentPixelBounds.Top && !string.IsNullOrWhiteSpace(g.Text));
									}
									list3.AddRange(enumerable2.Where(func));
									num++;
								}
								if (list2.Count > count)
								{
									list2.Sort((Glyph a, Glyph b) => a.ApparentPixelBounds.Left - b.ApparentPixelBounds.Left);
								}
							}
							List<Glyph> list4 = new List<Glyph> { CS$<>8__locals2.dot };
							List<Glyph> list5 = new List<Glyph>();
							int? num5 = null;
							int num6 = int.MaxValue;
							int num7 = list2.IndexOf(CS$<>8__locals2.dot);
							IEnumerable<Glyph> enumerable3 = list2.Take(num7).Reverse<Glyph>();
							IEnumerable<Glyph> enumerable4 = list2.Skip(num7 + 1);
							IEnumerable<Glyph>[] array = new IEnumerable<Glyph>[] { enumerable3, enumerable4 };
							for (i = 0; i < array.Length; i++)
							{
								foreach (Record<Glyph, Glyph> record in array[i].PrependItem(CS$<>8__locals2.dot).Windowed<Glyph>())
								{
									Glyph glyph;
									Glyph glyph2;
									record.Deconstruct(out glyph, out glyph2);
									Glyph glyph3 = glyph;
									Glyph glyph4 = glyph2;
									int num8 = glyph3.ApparentPixelBounds.Horizontal.Distance(glyph4.ApparentPixelBounds.Horizontal);
									if (hashSet.Contains(glyph4) && glyph4.Font != null && object.Equals(glyph4.Font, CS$<>8__locals2.dot.Font) && (double)num8 <= num3 && (num5 == null || MathUtils.WithinTolerance(num5.Value, num8, 2)))
									{
										if (!(from overlap in glyph3.ApparentPixelBounds.Horizontal.Intersect(glyph4.ApparentPixelBounds.Horizontal)
											select overlap.Size() > 2).OrElse(false) && MathUtils.WithinTolerance(glyph3.ApparentPixelBounds.Top, glyph4.ApparentPixelBounds.Top, 2) && MathUtils.WithinTolerance(glyph3.ApparentPixelBounds.Bottom, glyph4.ApparentPixelBounds.Bottom, 2))
										{
											if (num5 == null)
											{
												num5 = new int?(num8);
											}
											list4.Add(glyph4);
											continue;
										}
									}
									num6 = Math.Min(num6, num8);
									list5.Add(glyph4);
									break;
								}
							}
							hashSet.ExceptWith(list4);
							if ((double)num6 <= estimatedSpaceWidth * 2.0)
							{
								list4.Sort((Glyph a, Glyph b) => a.ApparentPixelBounds.Left - b.ApparentPixelBounds.Left);
								if (list4.Count <= 4)
								{
									list.Add(new Record<IReadOnlyList<Glyph>, IReadOnlyList<Glyph>>(list4, list5));
								}
								else
								{
									AlignmentDotCollection.AlignmentDotRow newRow2 = AlignmentDotCollection.AlignmentDotRow.Create(list4);
									if (newRow2 != null && !hashSet2.Any((AlignmentDotCollection.AlignmentDotColumn col) => col.TryAdd(newRow2, CS$<>8__locals2.CS$<>8__locals1.separators)))
									{
										hashSet2.Add(new AlignmentDotCollection.AlignmentDotColumn(newRow2));
									}
								}
							}
						}
					}
				}
			}
			CS$<>8__locals1.averageDotsPosition = (from r in hashSet2.SelectMany((AlignmentDotCollection.AlignmentDotColumn col) => col.DotRows)
				group r by r.Font).ToDictionary((IGrouping<FontCharacteristics, AlignmentDotCollection.AlignmentDotRow> g) => g.Key, delegate(IGrouping<FontCharacteristics, AlignmentDotCollection.AlignmentDotRow> g)
			{
				double averageDistance = g.SelectMany((AlignmentDotCollection.AlignmentDotRow dotsRow) => dotsRow.DotGlyphs.Windowed<Glyph>()).Average((Record<Glyph, Glyph> pair) => pair.Item2.ApparentPixelBounds.Left - pair.Item1.ApparentPixelBounds.Left);
				IReadOnlyList<int> readOnlyList8 = g.Select((AlignmentDotCollection.AlignmentDotRow dotsRow) => (int)Math.Round((double)dotsRow.DotGlyphs[0].ApparentPixelBounds.Left % averageDistance)).Distinct<int>().ToList<int>();
				return Record.Create<double, IReadOnlyList<int>>(averageDistance, readOnlyList8);
			});
			foreach (Record<IReadOnlyList<Glyph>, IReadOnlyList<Glyph>> record2 in list)
			{
				IReadOnlyList<Glyph> readOnlyList2;
				IReadOnlyList<Glyph> readOnlyList3;
				record2.Deconstruct(out readOnlyList2, out readOnlyList3);
				IReadOnlyList<Glyph> readOnlyList4 = readOnlyList2;
				IReadOnlyList<Glyph> readOnlyList5 = readOnlyList3;
				if (readOnlyList4.Count >= 1)
				{
					FontCharacteristics font2 = readOnlyList4[0].Font;
					Record<double, IReadOnlyList<int>> record3;
					if (font2 != null && CS$<>8__locals1.averageDotsPosition.TryGetValue(font2, out record3))
					{
						double num9;
						IReadOnlyList<int> readOnlyList6;
						record3.Deconstruct(out num9, out readOnlyList6);
						double expectedDistance = num9;
						IReadOnlyList<int> readOnlyList7 = readOnlyList6;
						List<Glyph> maybeRow = new List<Glyph>(readOnlyList4);
						while (maybeRow.Count > 1)
						{
							if (MathUtils.WithinTolerance(expectedDistance, maybeRow.Windowed<Glyph>().Average((Record<Glyph, Glyph> pair) => pair.Item2.ApparentPixelBounds.Left - pair.Item1.ApparentPixelBounds.Left), 2.0))
							{
								break;
							}
							maybeRow.RemoveAt(0);
						}
						if (maybeRow.Count != 1 || !readOnlyList5.Any<Glyph>() || (double)readOnlyList5.Min((Glyph nearbyGlyph) => nearbyGlyph.ApparentPixelBounds.Horizontal.Distance(maybeRow[0].ApparentPixelBounds.Horizontal)) > expectedDistance / 2.0)
						{
							double offset = (double)maybeRow[0].ApparentPixelBounds.Left % expectedDistance;
							AlignmentDotCollection.AlignmentDotRow newRow;
							if (!readOnlyList7.Any((int expectedOffset) => MathUtils.WithinTolerance((double)expectedOffset, offset, 2.0, expectedDistance)))
							{
								if (maybeRow.Count > 1)
								{
									newRow = AlignmentDotCollection.AlignmentDotRow.Create(maybeRow.Skip(1).ToList<Glyph>());
								}
								else
								{
									newRow = null;
								}
							}
							else
							{
								newRow = AlignmentDotCollection.AlignmentDotRow.Create(maybeRow);
							}
							if (newRow != null && !hashSet2.Any((AlignmentDotCollection.AlignmentDotColumn col) => col.TryAdd(newRow, CS$<>8__locals1.separators)))
							{
								newRow.MarkAsNotAlignmentDots();
							}
						}
					}
				}
			}
			bool flag;
			do
			{
				flag = false;
				foreach (Record<AlignmentDotCollection.AlignmentDotColumn, AlignmentDotCollection.AlignmentDotColumn> record4 in hashSet2.UnorderedPairs(false).ToList<Record<AlignmentDotCollection.AlignmentDotColumn, AlignmentDotCollection.AlignmentDotColumn>>())
				{
					AlignmentDotCollection.AlignmentDotColumn alignmentDotColumn;
					AlignmentDotCollection.AlignmentDotColumn alignmentDotColumn2;
					record4.Deconstruct(out alignmentDotColumn, out alignmentDotColumn2);
					AlignmentDotCollection.AlignmentDotColumn alignmentDotColumn3 = alignmentDotColumn;
					AlignmentDotCollection.AlignmentDotColumn alignmentDotColumn4 = alignmentDotColumn2;
					if (hashSet2.Contains(alignmentDotColumn3) && hashSet2.Contains(alignmentDotColumn4) && alignmentDotColumn3.TryJoin(alignmentDotColumn4, CS$<>8__locals1.separators))
					{
						hashSet2.Remove(alignmentDotColumn4);
						flag = true;
					}
				}
			}
			while (flag);
			foreach (AlignmentDotCollection.AlignmentDotColumn alignmentDotColumn5 in hashSet2)
			{
				Func<FontCharacteristics, double> func2;
				if ((func2 = CS$<>8__locals1.<>9__25) == null)
				{
					func2 = (CS$<>8__locals1.<>9__25 = (FontCharacteristics font) => CS$<>8__locals1.averageDotsPosition[font].Item1);
				}
				alignmentDotColumn5.CleanupNearbyAlignmentRows(func2);
			}
			IEnumerable<AlignmentDotCollection.AlignmentDotColumn> enumerable5 = hashSet2.Where((AlignmentDotCollection.AlignmentDotColumn col) => col.RowCount == 1).ToList<AlignmentDotCollection.AlignmentDotColumn>();
			Dictionary<AlignmentDotCollection.AlignmentDotColumn, AlignmentDotCollection.AlignmentDotColumn> dictionary = new Dictionary<AlignmentDotCollection.AlignmentDotColumn, AlignmentDotCollection.AlignmentDotColumn>();
			using (IEnumerator<AlignmentDotCollection.AlignmentDotColumn> enumerator7 = enumerable5.GetEnumerator())
			{
				while (enumerator7.MoveNext())
				{
					AlignmentDotCollection.AlignmentDotColumn singleRowColumn = enumerator7.Current;
					int num10;
					AlignmentDotCollection.AlignmentDotColumn alignmentDotColumn6 = hashSet2.Where((AlignmentDotCollection.AlignmentDotColumn col) => col != singleRowColumn && col.ApparentPixelBounds.Overlaps(singleRowColumn.ApparentPixelBounds, Axis.Horizontal)).ArgMin((AlignmentDotCollection.AlignmentDotColumn col) => col.ApparentPixelBounds.Vertical.Distance(singleRowColumn.ApparentPixelBounds.Vertical), out num10);
					AlignmentDotCollection.AlignmentDotColumn alignmentDotColumn7 = dictionary.MaybeGet(singleRowColumn).OrElse(singleRowColumn);
					int num11 = 4 * singleRowColumn.ApparentPixelBounds.Height();
					if (alignmentDotColumn6 != null && num10 < num11 && alignmentDotColumn7.TryJoin(alignmentDotColumn6, null))
					{
						hashSet2.Remove(alignmentDotColumn6);
						dictionary[alignmentDotColumn6] = alignmentDotColumn7;
					}
				}
			}
			return new AlignmentDotCollection(new QuadTree<AlignmentDotCollection.AlignmentDotColumn, PixelUnit>(pageBounds, hashSet2));
		}

		// Token: 0x04002422 RID: 9250
		private const string AlignmentDotString = ".";

		// Token: 0x04002423 RID: 9251
		internal const double AlignmentDotSpaceWidthMultiplier = 2.0;

		// Token: 0x02000C59 RID: 3161
		[Nullable(0)]
		public class AlignmentDotRow : IApparentPixelBounded, IPixelBounded, IBounded<PixelUnit>
		{
			// Token: 0x17000E98 RID: 3736
			// (get) Token: 0x06005184 RID: 20868 RVA: 0x00100FDC File Offset: 0x000FF1DC
			public IReadOnlyList<Glyph> DotGlyphs { get; }

			// Token: 0x17000E99 RID: 3737
			// (get) Token: 0x06005185 RID: 20869 RVA: 0x00100FE4 File Offset: 0x000FF1E4
			public FontCharacteristics Font { get; }

			// Token: 0x17000E9A RID: 3738
			// (get) Token: 0x06005186 RID: 20870 RVA: 0x00100FEC File Offset: 0x000FF1EC
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> StablePixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return Bounds<PixelUnit>.Join(this.DotGlyphs.Select((Glyph g) => g.StablePixelBounds));
				}
			}

			// Token: 0x17000E9B RID: 3739
			// (get) Token: 0x06005187 RID: 20871 RVA: 0x0010101D File Offset: 0x000FF21D
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> ApparentPixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x17000E9C RID: 3740
			// (get) Token: 0x06005188 RID: 20872 RVA: 0x00101025 File Offset: 0x000FF225
			[Nullable(new byte[] { 0, 1 })]
			Bounds<PixelUnit> IPixelBounded.PixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.ApparentPixelBounds;
				}
			}

			// Token: 0x17000E9D RID: 3741
			// (get) Token: 0x06005189 RID: 20873 RVA: 0x00101025 File Offset: 0x000FF225
			[Nullable(new byte[] { 0, 1 })]
			Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.ApparentPixelBounds;
				}
			}

			// Token: 0x0600518A RID: 20874 RVA: 0x00101030 File Offset: 0x000FF230
			private AlignmentDotRow(IReadOnlyList<Glyph> dotGlyphs, FontCharacteristics font)
			{
				this.DotGlyphs = dotGlyphs;
				this.Font = font;
				this.ApparentPixelBounds = dotGlyphs[0].ApparentPixelBounds.With(Direction.Right, dotGlyphs.Last<Glyph>().ApparentPixelBounds.Right);
				foreach (Glyph glyph in dotGlyphs)
				{
					glyph.AlignmentDotRow = this;
				}
			}

			// Token: 0x0600518B RID: 20875 RVA: 0x001010B8 File Offset: 0x000FF2B8
			internal AlignmentDotCollection.AlignmentDotRow Join(AlignmentDotCollection.AlignmentDotRow other)
			{
				if (other.ApparentPixelBounds.Right < this.ApparentPixelBounds.Right)
				{
					return other.Join(this);
				}
				return new AlignmentDotCollection.AlignmentDotRow(this.DotGlyphs.Concat(other.DotGlyphs).ToList<Glyph>(), this.Font);
			}

			// Token: 0x0600518C RID: 20876 RVA: 0x0010110C File Offset: 0x000FF30C
			[return: Nullable(2)]
			public static AlignmentDotCollection.AlignmentDotRow Create(IReadOnlyList<Glyph> dotGlyphs)
			{
				FontCharacteristics font = dotGlyphs[0].Font;
				if (font == null)
				{
					return null;
				}
				return new AlignmentDotCollection.AlignmentDotRow(dotGlyphs, font);
			}

			// Token: 0x0600518D RID: 20877 RVA: 0x00101134 File Offset: 0x000FF334
			internal void MarkAsNotAlignmentDots()
			{
				foreach (Glyph glyph in this.DotGlyphs)
				{
					glyph.AlignmentDotRow = null;
				}
			}
		}

		// Token: 0x02000C5B RID: 3163
		[Nullable(0)]
		public class AlignmentDotColumn : IApparentPixelBounded, IPixelBounded, IBounded<PixelUnit>
		{
			// Token: 0x17000E9E RID: 3742
			// (get) Token: 0x06005191 RID: 20881 RVA: 0x00101194 File Offset: 0x000FF394
			public IEnumerable<AlignmentDotCollection.AlignmentDotRow> DotRows
			{
				get
				{
					return this._dotRows;
				}
			}

			// Token: 0x17000E9F RID: 3743
			// (get) Token: 0x06005192 RID: 20882 RVA: 0x0010119C File Offset: 0x000FF39C
			public int RowCount
			{
				get
				{
					return this._dotRows.Count;
				}
			}

			// Token: 0x17000EA0 RID: 3744
			// (get) Token: 0x06005193 RID: 20883 RVA: 0x001011A9 File Offset: 0x000FF3A9
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> StablePixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return Bounds<PixelUnit>.Join(this.DotRows.Select((AlignmentDotCollection.AlignmentDotRow g) => g.StablePixelBounds));
				}
			}

			// Token: 0x17000EA1 RID: 3745
			// (get) Token: 0x06005194 RID: 20884 RVA: 0x001011DA File Offset: 0x000FF3DA
			// (set) Token: 0x06005195 RID: 20885 RVA: 0x001011E2 File Offset: 0x000FF3E2
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> ApparentPixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
				[param: Nullable(new byte[] { 0, 1 })]
				private set;
			}

			// Token: 0x17000EA2 RID: 3746
			// (get) Token: 0x06005196 RID: 20886 RVA: 0x001011EB File Offset: 0x000FF3EB
			[Nullable(new byte[] { 0, 1 })]
			Bounds<PixelUnit> IPixelBounded.PixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.ApparentPixelBounds;
				}
			}

			// Token: 0x17000EA3 RID: 3747
			// (get) Token: 0x06005197 RID: 20887 RVA: 0x001011EB File Offset: 0x000FF3EB
			[Nullable(new byte[] { 0, 1 })]
			Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.ApparentPixelBounds;
				}
			}

			// Token: 0x06005198 RID: 20888 RVA: 0x001011F4 File Offset: 0x000FF3F4
			private AlignmentDotColumn(IEnumerable<AlignmentDotCollection.AlignmentDotRow> dotRows)
			{
				this._dotRows = dotRows.OrderBy((AlignmentDotCollection.AlignmentDotRow row) => row.ApparentPixelBounds.Bottom).ConvertToHashSet<AlignmentDotCollection.AlignmentDotRow>();
				this.ApparentPixelBounds = Bounds<PixelUnit>.Join(this.DotRows.Select((AlignmentDotCollection.AlignmentDotRow r) => r.ApparentPixelBounds));
			}

			// Token: 0x06005199 RID: 20889 RVA: 0x0010126C File Offset: 0x000FF46C
			public AlignmentDotColumn(AlignmentDotCollection.AlignmentDotRow dotRow)
			{
				this._dotRows = new HashSet<AlignmentDotCollection.AlignmentDotRow> { dotRow };
				this.ApparentPixelBounds = dotRow.ApparentPixelBounds;
			}

			// Token: 0x0600519A RID: 20890 RVA: 0x00101294 File Offset: 0x000FF494
			public bool TryAdd(AlignmentDotCollection.AlignmentDotRow newRow, SeparatorCollection separators)
			{
				if (this.ApparentPixelBounds.Contains(newRow.ApparentPixelBounds))
				{
					this._dotRows.Add(newRow);
					return true;
				}
				if (!newRow.ApparentPixelBounds.Horizontal.Overlaps(this.ApparentPixelBounds.Horizontal))
				{
					return false;
				}
				if (separators.AnySeparates(Axis.Horizontal, this, newRow))
				{
					return false;
				}
				this._dotRows.Add(newRow);
				this.ApparentPixelBounds = this.ApparentPixelBounds.Join(newRow.ApparentPixelBounds);
				return true;
			}

			// Token: 0x0600519B RID: 20891 RVA: 0x00101324 File Offset: 0x000FF524
			public bool TryJoin(AlignmentDotCollection.AlignmentDotColumn other, [Nullable(2)] SeparatorCollection separators)
			{
				if (!other.ApparentPixelBounds.Horizontal.Overlaps(this.ApparentPixelBounds.Horizontal))
				{
					return false;
				}
				if (separators != null && separators.AnySeparates(Axis.Horizontal, this, other))
				{
					return false;
				}
				this._dotRows.AddRange(other.DotRows);
				this.ApparentPixelBounds = this.ApparentPixelBounds.Join(other.ApparentPixelBounds);
				return true;
			}

			// Token: 0x0600519C RID: 20892 RVA: 0x00101398 File Offset: 0x000FF598
			public void CleanupNearbyAlignmentRows(Func<FontCharacteristics, double> averageDistanceFunc)
			{
				foreach (Record<AlignmentDotCollection.AlignmentDotRow, AlignmentDotCollection.AlignmentDotRow> record in (from dotRow in this._dotRows
					group dotRow by Record.Create<FontCharacteristics, Range<PixelUnit>>(dotRow.Font, dotRow.ApparentPixelBounds.Vertical) into g
					where g.Count<AlignmentDotCollection.AlignmentDotRow>() > 1
					select g).SelectMany((IGrouping<Record<FontCharacteristics, Range<PixelUnit>>, AlignmentDotCollection.AlignmentDotRow> g) => g.OrderBy((AlignmentDotCollection.AlignmentDotRow row) => row.ApparentPixelBounds.Left).Windowed<AlignmentDotCollection.AlignmentDotRow>()).ToList<Record<AlignmentDotCollection.AlignmentDotRow, AlignmentDotCollection.AlignmentDotRow>>())
				{
					AlignmentDotCollection.AlignmentDotRow alignmentDotRow;
					AlignmentDotCollection.AlignmentDotRow alignmentDotRow2;
					record.Deconstruct(out alignmentDotRow, out alignmentDotRow2);
					AlignmentDotCollection.AlignmentDotRow alignmentDotRow3 = alignmentDotRow;
					AlignmentDotCollection.AlignmentDotRow alignmentDotRow4 = alignmentDotRow2;
					double num = averageDistanceFunc(alignmentDotRow3.Font);
					if ((double)alignmentDotRow3.ApparentPixelBounds.Horizontal.Distance(alignmentDotRow4.ApparentPixelBounds.Horizontal) < num * 2.0)
					{
						this._dotRows.Remove(alignmentDotRow3);
						this._dotRows.Remove(alignmentDotRow4);
						this._dotRows.Add(alignmentDotRow3.Join(alignmentDotRow4));
					}
				}
			}

			// Token: 0x0400242A RID: 9258
			private readonly HashSet<AlignmentDotCollection.AlignmentDotRow> _dotRows;
		}
	}
}
