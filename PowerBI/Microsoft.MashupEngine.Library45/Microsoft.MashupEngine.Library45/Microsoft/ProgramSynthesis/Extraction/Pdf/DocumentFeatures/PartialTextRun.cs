using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D07 RID: 3335
	[NullableContext(1)]
	[Nullable(0)]
	public class PartialTextRun
	{
		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x0600556F RID: 21871 RVA: 0x0010C84B File Offset: 0x0010AA4B
		public IReadOnlyList<IWord> Words
		{
			get
			{
				return this._words;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06005570 RID: 21872 RVA: 0x0010C853 File Offset: 0x0010AA53
		// (set) Token: 0x06005571 RID: 21873 RVA: 0x0010C85B File Offset: 0x0010AA5B
		[Nullable(new byte[] { 0, 1 })]
		public Range<PixelUnit> BasicVerticalBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
			[param: Nullable(new byte[] { 0, 1 })]
			private set;
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06005572 RID: 21874 RVA: 0x0010C864 File Offset: 0x0010AA64
		// (set) Token: 0x06005573 RID: 21875 RVA: 0x0010C86C File Offset: 0x0010AA6C
		[Nullable(2)]
		public PartialTextRun NextPartialTextRun
		{
			[NullableContext(2)]
			get
			{
				return this._nextPartialTextRun;
			}
			[NullableContext(2)]
			set
			{
				if (this._nextPartialTextRun != null)
				{
					this._nextPartialTextRun._previousPartialTextRun = null;
				}
				if (value != null)
				{
					value._previousPartialTextRun = this;
					this.BasicVerticalBounds = this.BasicVerticalBounds.Join(value.BasicVerticalBounds);
				}
				else
				{
					this.BasicVerticalBounds = Range<PixelUnit>.Join(from w in this.Words
						where !w.IsWhitespace
						select w.BasicVerticalBounds);
				}
				this._nextPartialTextRun = value;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06005574 RID: 21876 RVA: 0x0010C913 File Offset: 0x0010AB13
		internal bool HasPreviousPartialTextRun
		{
			get
			{
				return this._previousPartialTextRun != null;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06005575 RID: 21877 RVA: 0x0010C91E File Offset: 0x0010AB1E
		private bool AreAnyPreviousSuperOrSubscript
		{
			get
			{
				if (!this._isSuperOrSubscript)
				{
					PartialTextRun previousPartialTextRun = this._previousPartialTextRun;
					return previousPartialTextRun != null && previousPartialTextRun.AreAnyPreviousSuperOrSubscript;
				}
				return true;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06005576 RID: 21878 RVA: 0x0010C93B File Offset: 0x0010AB3B
		private bool AreAnyNextSuperOrSubscript
		{
			get
			{
				if (!this._isSuperOrSubscript)
				{
					PartialTextRun nextPartialTextRun = this._nextPartialTextRun;
					return nextPartialTextRun != null && nextPartialTextRun.AreAnyNextSuperOrSubscript;
				}
				return true;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06005577 RID: 21879 RVA: 0x0010C958 File Offset: 0x0010AB58
		// (set) Token: 0x06005578 RID: 21880 RVA: 0x0010C96A File Offset: 0x0010AB6A
		public bool IsSuperOrSubscript
		{
			get
			{
				return this.AreAnyPreviousSuperOrSubscript || this.AreAnyNextSuperOrSubscript;
			}
			private set
			{
				this._isSuperOrSubscript = value;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06005579 RID: 21881 RVA: 0x0010C973 File Offset: 0x0010AB73
		internal Line Line { get; }

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x0600557A RID: 21882 RVA: 0x0010C97B File Offset: 0x0010AB7B
		// (set) Token: 0x0600557B RID: 21883 RVA: 0x0010C983 File Offset: 0x0010AB83
		public bool Ignore { get; set; }

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x0600557C RID: 21884 RVA: 0x0010C98C File Offset: 0x0010AB8C
		public IWord LastWord
		{
			get
			{
				return this._words.Last<IWord>();
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x0600557D RID: 21885 RVA: 0x0010C999 File Offset: 0x0010AB99
		public IWord LastNonWhitespaceWord
		{
			get
			{
				return this._words.Last((IWord word) => !word.IsWhitespace);
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x0600557E RID: 21886 RVA: 0x0010C9C8 File Offset: 0x0010ABC8
		public int Left
		{
			get
			{
				return this.Words[0].ApparentPixelBoundsWithoutRotation.Left;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x0600557F RID: 21887 RVA: 0x0010C9F0 File Offset: 0x0010ABF0
		public int Right
		{
			get
			{
				return this._words.MaybeLast((IWord word) => !word.IsWhitespace).OrElse(this.LastWord).ApparentPixelBoundsWithoutRotation.Right;
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06005580 RID: 21888 RVA: 0x0010CA40 File Offset: 0x0010AC40
		public int RightIncludingPostscripts
		{
			get
			{
				PartialTextRun nextPartialTextRun = this.NextPartialTextRun;
				if (nextPartialTextRun == null)
				{
					return (from script in this.GetScripts(new bool?(false), null)
						select script.RightIncludingPostscripts).AppendItem(this.Right).Max();
				}
				return nextPartialTextRun.RightIncludingPostscripts;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06005581 RID: 21889 RVA: 0x0010CAA8 File Offset: 0x0010ACA8
		public int LeftIncludingPrescripts
		{
			get
			{
				return (from script in this.GetScripts(new bool?(true), null)
					select script.LeftIncludingPrescripts).AppendItem(this.Left).Min();
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06005582 RID: 21890 RVA: 0x0010CB00 File Offset: 0x0010AD00
		public int BaseLine
		{
			get
			{
				return this.BasicVerticalBounds.Max;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06005583 RID: 21891 RVA: 0x0010CB1C File Offset: 0x0010AD1C
		public int Height
		{
			get
			{
				return this.BasicVerticalBounds.Size();
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06005584 RID: 21892 RVA: 0x0010CB37 File Offset: 0x0010AD37
		[Nullable(new byte[] { 0, 1 })]
		public Range<PixelUnit> BaseHorizontal
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return new Range<PixelUnit>(this.Left, this.Right);
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06005585 RID: 21893 RVA: 0x0010CB4A File Offset: 0x0010AD4A
		[Nullable(new byte[] { 0, 1 })]
		public Range<PixelUnit> FullHorizontal
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return new Range<PixelUnit>(this.LeftIncludingPrescripts, this.RightIncludingPostscripts);
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06005586 RID: 21894 RVA: 0x0010CB5D File Offset: 0x0010AD5D
		internal string BaseText
		{
			get
			{
				return string.Concat(this.Words.Select((IWord w) => ((w.HasSpaceBefore ?? true) ? " " : string.Empty) + w.Content)).Trim();
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06005587 RID: 21895 RVA: 0x0010CB93 File Offset: 0x0010AD93
		[Nullable(2)]
		public ITextRun AsTextRun
		{
			[NullableContext(2)]
			get
			{
				if (this.IsSuperOrSubscript || this.HasPreviousPartialTextRun || this.Ignore)
				{
					return null;
				}
				return this.AsTextRunInternal;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06005588 RID: 21896 RVA: 0x0010CBB8 File Offset: 0x0010ADB8
		[Nullable(2)]
		public ITextRun AsTextRunInternal
		{
			[NullableContext(2)]
			get
			{
				LogicalGlyphOrderingLine logicalGlyphOrderingLine;
				string text = this.GetText(out logicalGlyphOrderingLine, TextDirection.LeftToRight, true);
				IReadOnlyList<IWord> readOnlyList = logicalGlyphOrderingLine.AllWords.Where((IWord word) => !word.IsWhitespace).ToList<IWord>();
				if (!readOnlyList.Any<IWord>())
				{
					return null;
				}
				Bounds<PixelUnit> bounds = Bounds<PixelUnit>.Join(readOnlyList.Select((IWord word) => word.ApparentPixelBoundsWithoutRotation));
				Range<PixelUnit> basicVerticalBounds = this.BasicVerticalBounds;
				Bounds<PixelUnit> bounds2 = new Bounds<PixelUnit>(Range<PixelUnit>.Join(readOnlyList.Select((IWord word) => word.ApparentPixelBoundsWithoutRotation.Horizontal)), basicVerticalBounds);
				Glyph glyph = this.Words[0].Children[0];
				if (glyph.IsRotated && glyph.RotationAngle != null)
				{
					bounds2 = bounds2.Rotate(glyph.RotationAngle.Value, false);
					bounds = bounds.Rotate(glyph.RotationAngle.Value, false);
				}
				FontCharacteristics font = this.Words[0].Font;
				LogicalGlyphOrderingLine logicalGlyphOrderingLine2;
				string text2 = this.GetText(out logicalGlyphOrderingLine2, TextDirection.RightToLeft, true);
				Bounds<PixelUnit> bounds3 = bounds2;
				string text3 = text;
				LogicalGlyphOrderingLine logicalGlyphOrderingLine3 = logicalGlyphOrderingLine;
				FontCharacteristics fontCharacteristics = font;
				return new TextRun(bounds3, bounds, text3, logicalGlyphOrderingLine3, this, text2, logicalGlyphOrderingLine2, fontCharacteristics);
			}
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x0010CD14 File Offset: 0x0010AF14
		[return: Nullable(new byte[] { 0, 1 })]
		private Bounds<PixelUnit>? ScriptsInclusiveBounds(LogicalGlyphOrderingLine sortedLine)
		{
			IReadOnlyList<IWord> readOnlyList = sortedLine.AllWords.ToList<IWord>();
			if (readOnlyList.Count == 0)
			{
				return null;
			}
			Bounds<PixelUnit> bounds = Bounds<PixelUnit>.Join(readOnlyList.Select((IWord word) => word.ApparentPixelBoundsWithoutRotation));
			Range<PixelUnit> basicVerticalBounds = this.BasicVerticalBounds;
			Bounds<PixelUnit> bounds2 = new Bounds<PixelUnit>(Range<PixelUnit>.Join(readOnlyList.Select((IWord word) => word.ApparentPixelBoundsWithoutRotation.Horizontal)), basicVerticalBounds);
			Glyph glyph = this.Words[0].Children[0];
			if (glyph.IsRotated && glyph.RotationAngle != null)
			{
				bounds2 = bounds2.Rotate(glyph.RotationAngle.Value, false);
				bounds = bounds.Rotate(glyph.RotationAngle.Value, false);
			}
			return new Bounds<PixelUnit>?(bounds);
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x0010CE0C File Offset: 0x0010B00C
		private PartialTextRun Clone(bool detachPrevious = false, bool detachNext = false, bool excludePreScripts = false, bool excludePostScripts = false, [Nullable(new byte[] { 2, 1 })] IEnumerable<IWord> words = null)
		{
			PartialTextRun partialTextRun = new PartialTextRun(this._pageStatistics, this.Line, words ?? this.Words);
			if (!detachNext && this.NextPartialTextRun != null)
			{
				partialTextRun.NextPartialTextRun = this.NextPartialTextRun.Clone(true, false, false, false, null);
			}
			if (!detachPrevious && this._previousPartialTextRun != null)
			{
				this._previousPartialTextRun.Clone(false, true, false, false, null).NextPartialTextRun = this;
			}
			partialTextRun._isSuperOrSubscript = this._isSuperOrSubscript;
			PartialTextRun partialTextRun2 = partialTextRun;
			PartialTextRun[] scripts = this._scripts;
			partialTextRun2._scripts = ((scripts != null) ? scripts.Select(delegate(PartialTextRun script, int kind)
			{
				bool flag = (kind & 2) != 0;
				if (excludePostScripts && !flag)
				{
					return null;
				}
				if (excludePreScripts && flag)
				{
					return null;
				}
				if (script == null)
				{
					return null;
				}
				return script.Clone(false, false, false, false, null);
			}).ToArray<PartialTextRun>() : null);
			return partialTextRun;
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x0010CEC4 File Offset: 0x0010B0C4
		[return: Nullable(new byte[] { 0, 0, 2, 2 })]
		internal Optional<Record<PartialTextRun, PartialTextRun>> SplitOnBoundary(Axis axis, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> boundary)
		{
			if (this.Words.Any((IWord word) => word.IsRotated))
			{
				return Optional<Record<PartialTextRun, PartialTextRun>>.Nothing;
			}
			if (axis == Axis.Vertical)
			{
				if (boundary.IsBefore(this.BasicVerticalBounds, false))
				{
					return new Record<PartialTextRun, PartialTextRun>(null, this).Some<Record<PartialTextRun, PartialTextRun>>();
				}
				return new Record<PartialTextRun, PartialTextRun>(this, null).Some<Record<PartialTextRun, PartialTextRun>>();
			}
			else
			{
				if (boundary.IsBefore(this.FullHorizontal, false))
				{
					return new Record<PartialTextRun, PartialTextRun>(null, this).Some<Record<PartialTextRun, PartialTextRun>>();
				}
				if (boundary.IsAfter(this.FullHorizontal, false))
				{
					return new Record<PartialTextRun, PartialTextRun>(this, null).Some<Record<PartialTextRun, PartialTextRun>>();
				}
				IGrouping<int, Record<IWord, IWord>> grouping = (from pair in this.Words.Where((IWord word) => !word.IsWhitespace).Windowed<IWord>()
					group pair by pair.Item1.ApparentPixelBounds[axis].Distance(pair.Item2.ApparentPixelBounds[axis])).ArgMax((IGrouping<int, Record<IWord, IWord>> g) => g.Key);
				Func<Range<PixelUnit>, Optional<Range<PixelUnit>>> <>9__6;
				IReadOnlyList<Range<PixelUnit>> readOnlyList = ((grouping != null) ? grouping.Select2(delegate(IWord a, IWord b)
				{
					Optional<Range<PixelUnit>> optional2 = a.ApparentPixelBounds[axis].BetweenExclusive(b.ApparentPixelBounds[axis]);
					Func<Range<PixelUnit>, Optional<Range<PixelUnit>>> func;
					if ((func = <>9__6) == null)
					{
						func = (<>9__6 = (Range<PixelUnit> between) => between.Intersect(boundary));
					}
					return optional2.SelectMany(func);
				}).SelectValues<Range<PixelUnit>>().ToList<Range<PixelUnit>>() : null);
				if (readOnlyList != null && readOnlyList.Any<Range<PixelUnit>>())
				{
					boundary = readOnlyList.ArgMax((Range<PixelUnit> r) => r.Size());
				}
				List<IWord> list = new List<IWord>();
				List<IWord> list2 = new List<IWord>();
				foreach (IWord word6 in this.Words)
				{
					Optional<Record<IWord, IWord>> optional = word6.SplitOnBoundary(axis, boundary);
					if (!optional.HasValue)
					{
						return Optional<Record<PartialTextRun, PartialTextRun>>.Nothing;
					}
					IWord word2;
					IWord word3;
					optional.Value.Deconstruct(out word2, out word3);
					IWord word4 = word2;
					IWord word5 = word3;
					if (word4 != null)
					{
						list.Add(word4);
					}
					if (word5 != null)
					{
						list2.Add(word5);
					}
				}
				if (!list.Any<IWord>())
				{
					PartialTextRun previousPartialTextRun = this._previousPartialTextRun;
					return new Record<PartialTextRun, PartialTextRun>((previousPartialTextRun != null) ? previousPartialTextRun.Clone(false, true, false, false, null) : null, this.Clone(true, false, false, false, null)).Some<Record<PartialTextRun, PartialTextRun>>();
				}
				if (!list2.Any<IWord>())
				{
					PartialTextRun partialTextRun = this.Clone(false, true, false, false, null);
					PartialTextRun nextPartialTextRun = this.NextPartialTextRun;
					return new Record<PartialTextRun, PartialTextRun>(partialTextRun, (nextPartialTextRun != null) ? nextPartialTextRun.Clone(true, false, false, false, null) : null).Some<Record<PartialTextRun, PartialTextRun>>();
				}
				return new Record<PartialTextRun, PartialTextRun>(this.Clone(false, true, false, true, list), this.Clone(true, false, true, false, list2)).Some<Record<PartialTextRun, PartialTextRun>>();
			}
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x0010D17C File Offset: 0x0010B37C
		private PartialTextRun(PageStatistics pageStatistics, Line line, List<IWord> words)
		{
			this.Line = line;
			this._pageStatistics = pageStatistics;
			this._words = words;
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x0010D19C File Offset: 0x0010B39C
		private PartialTextRun(PageStatistics pageStatistics, Line line, IEnumerable<IWord> words)
			: this(pageStatistics, line, words.ToList<IWord>())
		{
			IEnumerable<IWord> enumerable;
			if (!this._words.All((IWord w) => w.IsWhitespace))
			{
				enumerable = this._words.Where((IWord w) => !w.IsWhitespace);
			}
			else
			{
				IEnumerable<IWord> words2 = this._words;
				enumerable = words2;
			}
			this.BasicVerticalBounds = Range<PixelUnit>.Join(enumerable.Select((IWord w) => w.BasicVerticalBounds));
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x0010D246 File Offset: 0x0010B446
		internal PartialTextRun(PageStatistics pageStatistics, Line line, IWord firstWord)
			: this(pageStatistics, line, new List<IWord> { firstWord })
		{
			this.BasicVerticalBounds = firstWord.BasicVerticalBounds;
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x0010D268 File Offset: 0x0010B468
		private bool Append(PartialTextRun newTextRun)
		{
			bool flag = newTextRun.Words.All((IWord word) => word.IsWhitespace);
			if (!newTextRun.ContainsPreScripts(null) && (flag || !this.ContainsPostScripts(null)))
			{
				if (!flag)
				{
					if (this._words.All((IWord word) => word.IsWhitespace))
					{
						this.BasicVerticalBounds = newTextRun.BasicVerticalBounds;
					}
					this.BasicVerticalBounds = this.BasicVerticalBounds.Join(newTextRun.BasicVerticalBounds);
				}
				if (!flag || (newTextRun.Right > this.Right && !this.ContainsPostScripts(null) && (this._words.Count > 1 || this._words[0].Content.Length > 1 || this._words[0].BeforeAlignmentDotRow == null)))
				{
					this._words.AddRange(newTextRun.Words);
				}
				foreach (bool flag2 in new bool[]
				{
					default(bool),
					true
				})
				{
					PartialTextRun script = newTextRun.GetScript(false, flag2);
					if (script != null)
					{
						this.SetScript(script, false, flag2);
					}
				}
				return true;
			}
			this.NextPartialTextRun = newTextRun;
			return false;
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x0010D3D4 File Offset: 0x0010B5D4
		internal bool TryAppendTextRun(PartialTextRun newTextRun, bool exactSameLine, PageStatistics pageStatistics, [Nullable(2)] out PartialTextRun splitOutEndOfPreviousTextRun, out IEnumerable<PartialTextRun> splitScripts, out double distanceBetweenTextRuns, out double glyphWidth, out bool appendedNewTextRun)
		{
			splitOutEndOfPreviousTextRun = null;
			splitScripts = Enumerable.Empty<PartialTextRun>();
			IWord word = newTextRun.Words.Single<IWord>();
			int height = this.Height;
			int height2 = newTextRun.Height;
			distanceBetweenTextRuns = (double)(newTextRun.LeftIncludingPrescripts - this.RightIncludingPostscripts) - 0.5;
			double num;
			double num2;
			pageStatistics.GetWordStatistics(this.LastNonWhitespaceWord, word, out glyphWidth, out num, out num2);
			bool flag = distanceBetweenTextRuns > num;
			bool flag2 = distanceBetweenTextRuns > num2;
			int num3 = Math.Max(height, height2) / 3;
			if (!exactSameLine && !MathUtils.WithinTolerance(this.BaseLine, newTextRun.BaseLine, 1) && (flag2 || height == height2 || !MathUtils.WithinTolerance(this.BasicVerticalBounds.Min, newTextRun.BasicVerticalBounds.Min, num3) || !MathUtils.WithinTolerance(this.BasicVerticalBounds.Max, newTextRun.BasicVerticalBounds.Max, num3)))
			{
				appendedNewTextRun = false;
				return false;
			}
			if (flag)
			{
				appendedNewTextRun = false;
				return true;
			}
			appendedNewTextRun = this.Append(newTextRun);
			return true;
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x0010D4DC File Offset: 0x0010B6DC
		public bool IsLinedUpWith(IWord newWord)
		{
			if (!(from intersection in this.BasicVerticalBounds.Intersect(newWord.BasicVerticalBounds)
				select intersection.Size() >= 3).OrElse(false))
			{
				return false;
			}
			IWord lastNonWhitespaceWord = this.LastNonWhitespaceWord;
			if (lastNonWhitespaceWord.Font != null && lastNonWhitespaceWord.BasicVerticalBounds != newWord.BasicVerticalBounds && lastNonWhitespaceWord.Font.Equals(newWord.Font))
			{
				return false;
			}
			Glyph glyph = lastNonWhitespaceWord.Children.Last<Glyph>();
			if (newWord.Children.First<Glyph>().ApparentPixelBoundsWithoutRotation.CenterAlongAxis(Axis.Horizontal) <= glyph.ApparentPixelBoundsWithoutRotation.CenterAlongAxis(Axis.Horizontal))
			{
				return false;
			}
			if (lastNonWhitespaceWord.ApparentPixelBoundsWithoutRotation.Horizontal.Distance(newWord.ApparentPixelBoundsWithoutRotation.Horizontal) > 0)
			{
				int height = this.Height;
				int num = newWord.BasicVerticalBounds.Size();
				if (height > num * 3)
				{
					return false;
				}
				if (num > height * 3)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x0010D5EE File Offset: 0x0010B7EE
		private PartialTextRun.ScriptIndex GetScriptIndex(bool pre, bool super)
		{
			return (pre ? PartialTextRun.ScriptIndex.Pre : PartialTextRun.ScriptIndex.Sub) | (super ? PartialTextRun.ScriptIndex.Super : PartialTextRun.ScriptIndex.Sub);
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x0010D600 File Offset: 0x0010B800
		public void SetScript(PartialTextRun script, bool pre, bool super)
		{
			this._scripts = this._scripts ?? new PartialTextRun[4];
			if (pre)
			{
				while (script._previousPartialTextRun != null)
				{
					script = script._previousPartialTextRun;
				}
			}
			this._scripts[(int)this.GetScriptIndex(pre, super)] = script;
			script.IsSuperOrSubscript = true;
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x0010D64F File Offset: 0x0010B84F
		[NullableContext(2)]
		public PartialTextRun GetScript(bool pre, bool super)
		{
			PartialTextRun[] scripts = this._scripts;
			if (scripts == null)
			{
				return null;
			}
			return scripts[(int)this.GetScriptIndex(pre, super)];
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x0010D668 File Offset: 0x0010B868
		public IEnumerable<PartialTextRun> GetScripts(bool? pre = null, bool? super = null)
		{
			if (this._scripts == null)
			{
				return Enumerable.Empty<PartialTextRun>();
			}
			if (pre == null && super == null)
			{
				return this._scripts.Collect<PartialTextRun>();
			}
			IEnumerable<bool> enumerable;
			if ((enumerable = ((pre != null) ? pre.GetValueOrDefault().Yield<bool>() : null)) == null)
			{
				(enumerable = new bool[2])[1] = 1;
			}
			return enumerable.SelectMany(delegate(bool preOption)
			{
				IEnumerable<bool> enumerable2;
				if ((enumerable2 = ((super != null) ? super.GetValueOrDefault().Yield<bool>() : null)) == null)
				{
					(enumerable2 = new bool[2])[1] = 1;
				}
				return enumerable2;
			}, (bool preOption, bool superOption) => this._scripts[(int)this.GetScriptIndex(preOption, superOption)]).Collect<PartialTextRun>();
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x0010D704 File Offset: 0x0010B904
		[return: Nullable(new byte[] { 1, 0, 1 })]
		private IEnumerable<Record<PartialTextRun.ScriptIndex, PartialTextRun>> GetIdentifiedScripts(bool? pre = null, bool? super = null)
		{
			if (this._scripts == null)
			{
				return Enumerable.Empty<Record<PartialTextRun.ScriptIndex, PartialTextRun>>();
			}
			IEnumerable<bool> enumerable;
			if ((enumerable = ((pre != null) ? pre.GetValueOrDefault().Yield<bool>() : null)) == null)
			{
				(enumerable = new bool[2])[1] = 1;
			}
			return from <>h__TransparentIdentifier2 in (from <>h__TransparentIdentifier0 in enumerable.SelectMany(delegate(bool preOption)
					{
						IEnumerable<bool> enumerable2;
						if ((enumerable2 = ((super != null) ? super.GetValueOrDefault().Yield<bool>() : null)) == null)
						{
							(enumerable2 = new bool[2])[1] = 1;
						}
						return enumerable2;
					}, (bool preOption, bool superOption) => new { preOption, superOption })
					select new
					{
						<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
						index = this.GetScriptIndex(<>h__TransparentIdentifier0.preOption, <>h__TransparentIdentifier0.superOption)
					}).Select(delegate(<>h__TransparentIdentifier1)
				{
					PartialTextRun[] scripts = this._scripts;
					return new
					{
						<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
						script = ((scripts != null) ? scripts[(int)<>h__TransparentIdentifier1.index] : null)
					};
				})
				where <>h__TransparentIdentifier2.script != null
				select Record.Create<PartialTextRun.ScriptIndex, PartialTextRun>(<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.index, <>h__TransparentIdentifier2.script);
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x0010D7F4 File Offset: 0x0010B9F4
		public bool ContainsScripts(bool? pre = null, bool? super = null)
		{
			if (this._scripts == null)
			{
				return false;
			}
			if (pre == null && super == null)
			{
				return true;
			}
			IEnumerable<bool> enumerable;
			if ((enumerable = ((pre != null) ? pre.GetValueOrDefault().Yield<bool>() : null)) == null)
			{
				(enumerable = new bool[2])[1] = 1;
			}
			return enumerable.SelectMany(delegate(bool preOption)
			{
				IEnumerable<bool> enumerable2;
				if ((enumerable2 = ((super != null) ? super.GetValueOrDefault().Yield<bool>() : null)) == null)
				{
					(enumerable2 = new bool[2])[1] = 1;
				}
				return enumerable2;
			}, delegate(bool preOption, bool superOption)
			{
				PartialTextRun[] scripts = this._scripts;
				if (scripts == null)
				{
					return null;
				}
				return scripts[(int)this.GetScriptIndex(preOption, superOption)];
			}).Any((PartialTextRun s) => s != null);
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x0010D89E File Offset: 0x0010BA9E
		public bool ContainsPreScripts(bool? super = null)
		{
			return this.ContainsScripts(new bool?(true), super);
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x0010D8AD File Offset: 0x0010BAAD
		public bool ContainsPostScripts(bool? super = null)
		{
			return this.ContainsScripts(new bool?(false), super);
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x0010D8BC File Offset: 0x0010BABC
		private IEnumerable<PartialTextRun> SplitScripts()
		{
			PartialTextRun[] scripts = this._scripts;
			IReadOnlyList<PartialTextRun> readOnlyList = ((scripts != null) ? scripts.Collect<PartialTextRun>().ToArray<PartialTextRun>() : null) ?? new PartialTextRun[0];
			this._scripts = null;
			foreach (PartialTextRun partialTextRun in readOnlyList)
			{
				partialTextRun.IsSuperOrSubscript = false;
			}
			return readOnlyList;
		}

		// Token: 0x0600559B RID: 21915 RVA: 0x0010D92C File Offset: 0x0010BB2C
		private static List<LogicalGlyphOrderingWord> ReorderWords(IReadOnlyList<LogicalGlyphOrderingWord> sortedWordsAndTextDirections, TextDirection textDirection)
		{
			List<LogicalGlyphOrderingWord> list = new List<LogicalGlyphOrderingWord>(sortedWordsAndTextDirections.Count);
			List<LogicalGlyphOrderingWord> list2 = new List<LogicalGlyphOrderingWord>(sortedWordsAndTextDirections.Count);
			List<LogicalGlyphOrderingWord> list3 = new List<LogicalGlyphOrderingWord>(sortedWordsAndTextDirections.Count);
			foreach (LogicalGlyphOrderingWord logicalGlyphOrderingWord in sortedWordsAndTextDirections)
			{
				if (logicalGlyphOrderingWord.TextDirection == TextDirection.RightToLeft)
				{
					if (textDirection == TextDirection.RightToLeft && list3.Count > 0)
					{
						list3.Reverse();
						list.AddRange(list3);
						list3.Clear();
					}
					if (logicalGlyphOrderingWord.Word.TextDirection == TextDirection.Weak && (logicalGlyphOrderingWord.IsEuropeanNumber || logicalGlyphOrderingWord.Word.Children[0].BidiCategory.IsNumberCategory()))
					{
						list2.Add(logicalGlyphOrderingWord);
					}
					else
					{
						List<LogicalGlyphOrderingWord> list4 = ((textDirection == TextDirection.RightToLeft) ? list : list3);
						if (list2.Count > 0)
						{
							list2.Reverse();
							list4.AddRange(list2);
							list2.Clear();
						}
						list4.Add(logicalGlyphOrderingWord);
					}
				}
				else if (textDirection == TextDirection.RightToLeft)
				{
					if (list2.Count > 0)
					{
						list2.Reverse();
						list.AddRange(list2);
						list2.Clear();
					}
					list3.Add(logicalGlyphOrderingWord);
				}
				else
				{
					if (list3.Count > 0)
					{
						list3.Reverse();
						list.AddRange(list3);
						list3.Clear();
					}
					if (list2.Count > 0)
					{
						list.AddRange(list2);
						list2.Clear();
					}
					list.Add(logicalGlyphOrderingWord);
				}
			}
			if (list3.Count > 0)
			{
				list3.Reverse();
				list.AddRange(list3);
			}
			if (list2.Count > 0)
			{
				if (textDirection == TextDirection.RightToLeft)
				{
					list2.Reverse();
				}
				list.AddRange(list2);
			}
			return list;
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x0010DADC File Offset: 0x0010BCDC
		private string GetText(out LogicalGlyphOrderingLine sortedWords, TextDirection textDirection, bool topLevel = false)
		{
			PartialTextRun.<>c__DisplayClass77_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.textDirection = textDirection;
			IEnumerable<IWord> enumerable = this.Words.OrderBy((IWord word) => word.ApparentPixelBoundsWithoutRotation.Left);
			IEnumerable<IWord> enumerable2;
			if (CS$<>8__locals1.textDirection != TextDirection.LeftToRight)
			{
				enumerable2 = enumerable.Reverse<IWord>();
			}
			else
			{
				IEnumerable<IWord> enumerable3 = enumerable;
				enumerable2 = enumerable3;
			}
			IEnumerable<IWord> enumerable4 = enumerable2;
			CS$<>8__locals1.sortedWordsAndTextDirections = (from word in enumerable4.SkipWhile((IWord word) => word.IsWhitespace)
				select new LogicalGlyphOrderingWord(word, word.TextDirection)).ToList<LogicalGlyphOrderingWord>();
			bool flag = false;
			int? num = null;
			TextDirection textDirection2 = ((CS$<>8__locals1.textDirection == TextDirection.LeftToRight) ? TextDirection.RightToLeft : TextDirection.LeftToRight);
			for (int i = 0; i < CS$<>8__locals1.sortedWordsAndTextDirections.Count; i++)
			{
				bool flag2 = CS$<>8__locals1.sortedWordsAndTextDirections[i].TextDirection == textDirection2;
				bool flag3 = flag;
				bool flag4;
				if (!flag2)
				{
					if (CS$<>8__locals1.textDirection == TextDirection.RightToLeft)
					{
						if (!CS$<>8__locals1.sortedWordsAndTextDirections[i].IsEuropeanNumber)
						{
							Glyph glyph = CS$<>8__locals1.sortedWordsAndTextDirections[i].Glyphs.FirstOrDefault<Glyph>();
							flag4 = glyph != null && glyph.BidiCategory.IsNumberCategory();
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag4 = false;
					}
				}
				else
				{
					flag4 = true;
				}
				flag = flag3 || flag4;
				if (num != null)
				{
					if (CS$<>8__locals1.sortedWordsAndTextDirections[i].TextDirection.IsStrong())
					{
						for (int j = num.Value + 1; j < i; j++)
						{
							CS$<>8__locals1.sortedWordsAndTextDirections[j].TextDirection = CS$<>8__locals1.sortedWordsAndTextDirections[i].TextDirection;
						}
						if (flag2)
						{
							num = new int?(i);
						}
						else
						{
							num = null;
						}
					}
				}
				else if (flag2)
				{
					num = new int?(i);
				}
				else
				{
					CS$<>8__locals1.sortedWordsAndTextDirections[i].TextDirection = CS$<>8__locals1.textDirection;
				}
			}
			if (num != null)
			{
				for (int k = num.Value + 1; k < CS$<>8__locals1.sortedWordsAndTextDirections.Count; k++)
				{
					CS$<>8__locals1.sortedWordsAndTextDirections[k].TextDirection = CS$<>8__locals1.textDirection;
				}
			}
			if (flag)
			{
				if (CS$<>8__locals1.sortedWordsAndTextDirections.Any((LogicalGlyphOrderingWord word) => word.Glyphs.Any((Glyph g) => g.BidiCategory == BidiUnicodeCategory.RightToLeftArabic)))
				{
					bool flag5 = false;
					foreach (LogicalGlyphOrderingWord logicalGlyphOrderingWord in PartialTextRun.ReorderWords(CS$<>8__locals1.sortedWordsAndTextDirections, CS$<>8__locals1.textDirection))
					{
						Glyph glyph2 = logicalGlyphOrderingWord.Glyphs.LastOrDefault<Glyph>();
						BidiUnicodeCategory? bidiUnicodeCategory = ((glyph2 != null) ? new BidiUnicodeCategory?(glyph2.BidiCategory) : null);
						if (bidiUnicodeCategory != null && bidiUnicodeCategory.GetValueOrDefault().IsStrong())
						{
							BidiUnicodeCategory? bidiUnicodeCategory2 = bidiUnicodeCategory;
							BidiUnicodeCategory bidiUnicodeCategory3 = BidiUnicodeCategory.RightToLeftArabic;
							flag5 = (bidiUnicodeCategory2.GetValueOrDefault() == bidiUnicodeCategory3) & (bidiUnicodeCategory2 != null);
						}
						else if (flag5 && logicalGlyphOrderingWord.IsEuropeanNumber)
						{
							logicalGlyphOrderingWord.IsEuropeanNumber = false;
						}
					}
				}
				foreach (Record<LogicalGlyphOrderingWord, LogicalGlyphOrderingWord, LogicalGlyphOrderingWord> record in CS$<>8__locals1.sortedWordsAndTextDirections.Windowed3<LogicalGlyphOrderingWord>())
				{
					LogicalGlyphOrderingWord logicalGlyphOrderingWord2;
					LogicalGlyphOrderingWord logicalGlyphOrderingWord3;
					LogicalGlyphOrderingWord logicalGlyphOrderingWord4;
					record.Deconstruct(out logicalGlyphOrderingWord2, out logicalGlyphOrderingWord3, out logicalGlyphOrderingWord4);
					LogicalGlyphOrderingWord logicalGlyphOrderingWord5 = logicalGlyphOrderingWord2;
					LogicalGlyphOrderingWord logicalGlyphOrderingWord6 = logicalGlyphOrderingWord3;
					LogicalGlyphOrderingWord logicalGlyphOrderingWord7 = logicalGlyphOrderingWord4;
					if (logicalGlyphOrderingWord5.IsEuropeanNumber && logicalGlyphOrderingWord7.IsEuropeanNumber)
					{
						Glyph glyph3 = logicalGlyphOrderingWord6.Glyphs.FirstOrDefault<Glyph>();
						BidiUnicodeCategory? bidiUnicodeCategory4 = ((glyph3 != null) ? new BidiUnicodeCategory?(glyph3.BidiCategory) : null);
						BidiUnicodeCategory? bidiUnicodeCategory2 = bidiUnicodeCategory4;
						BidiUnicodeCategory bidiUnicodeCategory3 = BidiUnicodeCategory.CommonNumberSeparator;
						if (!((bidiUnicodeCategory2.GetValueOrDefault() == bidiUnicodeCategory3) & (bidiUnicodeCategory2 != null)))
						{
							bidiUnicodeCategory2 = bidiUnicodeCategory4;
							bidiUnicodeCategory3 = BidiUnicodeCategory.EuropeanNumberSeparator;
							if (!((bidiUnicodeCategory2.GetValueOrDefault() == bidiUnicodeCategory3) & (bidiUnicodeCategory2 != null)))
							{
								continue;
							}
						}
						logicalGlyphOrderingWord6.IsEuropeanNumber = true;
					}
				}
				int num2 = -1;
				BidiUnicodeCategory bidiUnicodeCategory5 = BidiUnicodeCategory.Unknown;
				bool flag6 = false;
				for (int l = 0; l < CS$<>8__locals1.sortedWordsAndTextDirections.Count; l++)
				{
					Glyph glyph4 = CS$<>8__locals1.sortedWordsAndTextDirections[l].Glyphs.FirstOrDefault<Glyph>();
					BidiUnicodeCategory bidiUnicodeCategory6 = ((glyph4 != null) ? glyph4.BidiCategory : BidiUnicodeCategory.Unknown);
					if (bidiUnicodeCategory5 == bidiUnicodeCategory6)
					{
						if (flag6)
						{
							CS$<>8__locals1.sortedWordsAndTextDirections[l].IsEuropeanNumber = true;
						}
					}
					else
					{
						if (bidiUnicodeCategory5 == BidiUnicodeCategory.EuropeanNumberTerminator && CS$<>8__locals1.sortedWordsAndTextDirections[l].IsEuropeanNumber)
						{
							for (int m = num2; m < l; m++)
							{
								CS$<>8__locals1.sortedWordsAndTextDirections[m].IsEuropeanNumber = true;
							}
						}
						bidiUnicodeCategory5 = bidiUnicodeCategory6;
						num2 = l;
						flag6 = bidiUnicodeCategory6 == BidiUnicodeCategory.EuropeanNumberTerminator && l > 0 && CS$<>8__locals1.sortedWordsAndTextDirections[l - 1].IsEuropeanNumber;
						if (flag6)
						{
							CS$<>8__locals1.sortedWordsAndTextDirections[l].IsEuropeanNumber = true;
						}
					}
				}
				CS$<>8__locals1.sortedWordsAndTextDirections = PartialTextRun.ReorderWords(CS$<>8__locals1.sortedWordsAndTextDirections, CS$<>8__locals1.textDirection);
			}
			bool flag7 = CS$<>8__locals1.textDirection == TextDirection.RightToLeft;
			List<LogicalGlyphOrderingElement> list = new List<LogicalGlyphOrderingElement>();
			StringBuilder stringBuilder = new StringBuilder(CS$<>8__locals1.sortedWordsAndTextDirections.Sum((LogicalGlyphOrderingWord t) => t.Word.Content.Length + 1));
			bool flag8 = false;
			StringBuilder stringBuilder2 = new StringBuilder();
			LogicalGlyphOrderingLine logicalGlyphOrderingLine = null;
			LogicalGlyphOrderingLine logicalGlyphOrderingLine2 = null;
			Bounds<PixelUnit>? bounds = null;
			foreach (Record<PartialTextRun.ScriptIndex, PartialTextRun> record2 in this.GetIdentifiedScripts(new bool?(!flag7), null))
			{
				PartialTextRun.ScriptIndex scriptIndex;
				PartialTextRun partialTextRun;
				record2.Deconstruct(out scriptIndex, out partialTextRun);
				int num3 = (int)scriptIndex;
				PartialTextRun partialTextRun2 = partialTextRun;
				flag8 = true;
				bool flag9 = (num3 & 1) != 0;
				stringBuilder2.Append(flag9 ? '^' : '_');
				stringBuilder2.Append('{');
				LogicalGlyphOrderingLine logicalGlyphOrderingLine3;
				stringBuilder2.Append(partialTextRun2.GetText(out logicalGlyphOrderingLine3, CS$<>8__locals1.textDirection, false));
				stringBuilder2.Append('}');
				if (flag9)
				{
					logicalGlyphOrderingLine2 = logicalGlyphOrderingLine3;
				}
				else
				{
					logicalGlyphOrderingLine = logicalGlyphOrderingLine3;
				}
				Bounds<PixelUnit>? bounds2 = partialTextRun2.ScriptsInclusiveBounds(logicalGlyphOrderingLine3);
				Bounds<PixelUnit>? bounds4;
				if (bounds == null || bounds2 == null)
				{
					Bounds<PixelUnit>? bounds3 = bounds2;
					bounds4 = ((bounds3 != null) ? bounds3 : bounds);
				}
				else
				{
					bounds4 = new Bounds<PixelUnit>?(bounds.Value.Join(bounds2.Value));
				}
				bounds = bounds4;
				if (topLevel)
				{
					foreach (IWord word3 in logicalGlyphOrderingLine3.AllWords)
					{
						word3.IsSuperscriptOrSubscript = true;
					}
				}
			}
			if (bounds != null)
			{
				stringBuilder.Append(stringBuilder2.ToString());
				list.Add(new LogicalGlyphOrderingScript(CS$<>8__locals1.textDirection, logicalGlyphOrderingLine, logicalGlyphOrderingLine2, new Glyph(bounds.Value, stringBuilder2.ToString(), null, null, 0, null, BidiUnicodeCategory.OtherNeutral)));
			}
			if (flag8)
			{
				stringBuilder.Append(' ');
			}
			list.AddRange(CS$<>8__locals1.sortedWordsAndTextDirections);
			if (CS$<>8__locals1.sortedWordsAndTextDirections.Count > 0)
			{
				if (!CS$<>8__locals1.sortedWordsAndTextDirections[0].Word.IsWhitespace)
				{
					stringBuilder.Append(CS$<>8__locals1.sortedWordsAndTextDirections[0].Content);
				}
				TextDirection textDirection3 = CS$<>8__locals1.textDirection;
				BidiUnicodeCategory? bidiUnicodeCategory7 = null;
				PartialTextRun.<>c__DisplayClass77_1 CS$<>8__locals2;
				CS$<>8__locals2.i = 0;
				foreach (Record<LogicalGlyphOrderingWord, LogicalGlyphOrderingWord> record3 in CS$<>8__locals1.sortedWordsAndTextDirections.Windowed<LogicalGlyphOrderingWord>())
				{
					LogicalGlyphOrderingWord logicalGlyphOrderingWord3;
					LogicalGlyphOrderingWord logicalGlyphOrderingWord4;
					record3.Deconstruct(out logicalGlyphOrderingWord4, out logicalGlyphOrderingWord3);
					PartialTextRun.<>c__DisplayClass77_2 CS$<>8__locals3;
					CS$<>8__locals3.previousWordPair = logicalGlyphOrderingWord4;
					CS$<>8__locals3.wordPair = logicalGlyphOrderingWord3;
					int i2 = CS$<>8__locals2.i;
					CS$<>8__locals2.i = i2 + 1;
					PartialTextRun.<>c__DisplayClass77_3 CS$<>8__locals4;
					CS$<>8__locals4.word = CS$<>8__locals3.wordPair.Word;
					CS$<>8__locals4.previousWord = CS$<>8__locals3.previousWordPair.Word;
					if (!CS$<>8__locals4.word.IsWhitespace)
					{
						Glyph glyph5 = CS$<>8__locals3.previousWordPair.Glyphs.LastOrDefault((Glyph g) => g.BidiCategory.IsStrong());
						TextDirection? textDirection4 = ((glyph5 != null) ? new TextDirection?(glyph5.BidiCategory.GetDefaultTextDirection()) : null);
						textDirection3 = textDirection4 ?? textDirection3;
						if (textDirection4 != null)
						{
							bidiUnicodeCategory7 = null;
						}
						if (CS$<>8__locals4.previousWord.IsWhitespace || (CS$<>8__locals4.word.HasSpaceBefore ?? this.<GetText>g__WordGapIsSpace|77_7(ref CS$<>8__locals1, ref CS$<>8__locals3, ref CS$<>8__locals4)))
						{
							stringBuilder.Append(" ");
						}
						BidiUnicodeCategory bidiCategory = CS$<>8__locals3.wordPair.Word.Children[0].BidiCategory;
						BidiUnicodeCategory bidiCategory2 = CS$<>8__locals3.previousWordPair.Word.Children.Last<Glyph>().BidiCategory;
						if (bidiCategory2.IsNumberCategory())
						{
							bidiUnicodeCategory7 = new BidiUnicodeCategory?(bidiCategory2);
						}
						BidiUnicodeCategory? bidiUnicodeCategory2 = bidiUnicodeCategory7;
						BidiUnicodeCategory bidiUnicodeCategory3 = BidiUnicodeCategory.ArabicNumber;
						TextDirection textDirection5 = (((bidiUnicodeCategory2.GetValueOrDefault() == bidiUnicodeCategory3) & (bidiUnicodeCategory2 != null)) ? TextDirection.RightToLeft : textDirection3);
						if (CS$<>8__locals3.wordPair.TextDirection != CS$<>8__locals3.previousWordPair.TextDirection)
						{
							if (!bidiCategory.IsStrong())
							{
								if (bidiCategory.IsNumberCategory() || this.<GetText>g__nextStrongForN1|77_8(ref CS$<>8__locals1, ref CS$<>8__locals2) == CS$<>8__locals3.previousWordPair.TextDirection)
								{
									stringBuilder.Append((CS$<>8__locals3.wordPair.TextDirection == TextDirection.LeftToRight) ? '\u200e' : '\u200f');
									textDirection3 = CS$<>8__locals3.wordPair.TextDirection;
								}
							}
							else if (!CS$<>8__locals4.previousWord.TextDirection.IsStrong() && (textDirection5 != bidiCategory.GetDefaultTextDirection() || textDirection3 != bidiCategory.GetDefaultTextDirection()) && bidiUnicodeCategory7 != null && textDirection5 == TextDirection.RightToLeft)
							{
								stringBuilder.Append((textDirection3 == TextDirection.LeftToRight) ? '\u200e' : '\u200f');
							}
						}
						else if (CS$<>8__locals1.textDirection == TextDirection.RightToLeft && CS$<>8__locals3.previousWordPair.IsEuropeanNumber != CS$<>8__locals3.wordPair.IsEuropeanNumber && ((!CS$<>8__locals3.wordPair.IsEuropeanNumber && bidiCategory == BidiUnicodeCategory.EuropeanNumberTerminator) || (!CS$<>8__locals3.previousWordPair.IsEuropeanNumber && bidiCategory2 == BidiUnicodeCategory.EuropeanNumberTerminator)))
						{
							stringBuilder.Append('\u200f');
							textDirection3 = TextDirection.RightToLeft;
						}
						stringBuilder.Append(CS$<>8__locals3.wordPair.Content);
					}
				}
			}
			StringBuilder stringBuilder3 = new StringBuilder();
			LogicalGlyphOrderingLine logicalGlyphOrderingLine4 = null;
			LogicalGlyphOrderingLine logicalGlyphOrderingLine5 = null;
			Bounds<PixelUnit>? bounds5 = null;
			foreach (Record<PartialTextRun.ScriptIndex, PartialTextRun> record4 in this.GetIdentifiedScripts(new bool?(flag7), null))
			{
				PartialTextRun.ScriptIndex scriptIndex;
				PartialTextRun partialTextRun;
				record4.Deconstruct(out scriptIndex, out partialTextRun);
				int num4 = (int)scriptIndex;
				PartialTextRun partialTextRun3 = partialTextRun;
				bool flag10 = (num4 & 1) != 0;
				stringBuilder3.Append(flag10 ? '^' : '_');
				stringBuilder3.Append('{');
				LogicalGlyphOrderingLine logicalGlyphOrderingLine6;
				stringBuilder3.Append(partialTextRun3.GetText(out logicalGlyphOrderingLine6, CS$<>8__locals1.textDirection, false));
				stringBuilder3.Append('}');
				if (flag10)
				{
					logicalGlyphOrderingLine5 = logicalGlyphOrderingLine6;
				}
				else
				{
					logicalGlyphOrderingLine4 = logicalGlyphOrderingLine6;
				}
				Bounds<PixelUnit>? bounds6 = partialTextRun3.ScriptsInclusiveBounds(logicalGlyphOrderingLine6);
				Bounds<PixelUnit>? bounds7;
				if (bounds5 == null || bounds6 == null)
				{
					Bounds<PixelUnit>? bounds3 = bounds6;
					bounds7 = ((bounds3 != null) ? bounds3 : bounds5);
				}
				else
				{
					bounds7 = new Bounds<PixelUnit>?(bounds5.Value.Join(bounds6.Value));
				}
				bounds5 = bounds7;
				if (topLevel)
				{
					foreach (IWord word2 in logicalGlyphOrderingLine6.AllWords)
					{
						word2.IsSuperscriptOrSubscript = true;
					}
				}
			}
			if (bounds5 != null)
			{
				stringBuilder.Append(stringBuilder3.ToString());
				list.Add(new LogicalGlyphOrderingScript(CS$<>8__locals1.textDirection, logicalGlyphOrderingLine4, logicalGlyphOrderingLine5, new Glyph(bounds5.Value, stringBuilder3.ToString(), null, null, 0, null, BidiUnicodeCategory.OtherNeutral)));
			}
			if (this.NextPartialTextRun != null)
			{
				LogicalGlyphOrderingLine logicalGlyphOrderingLine7;
				string text = this.NextPartialTextRun.GetText(out logicalGlyphOrderingLine7, CS$<>8__locals1.textDirection, true);
				if (!string.IsNullOrWhiteSpace(text))
				{
					stringBuilder.Append(' ');
					stringBuilder.Append(text);
					list.AddRange(logicalGlyphOrderingLine7.Elements);
				}
			}
			sortedWords = new LogicalGlyphOrderingLine(CS$<>8__locals1.textDirection, list.ToList<LogicalGlyphOrderingElement>());
			return stringBuilder.ToString();
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x0010E7E4 File Offset: 0x0010C9E4
		public override string ToString()
		{
			string text = string.Join(", ", this.GetIdentifiedScripts(null, null).Select2((PartialTextRun.ScriptIndex idx, PartialTextRun script) => string.Format("{0}{1}={2}", ((idx & PartialTextRun.ScriptIndex.Pre) != PartialTextRun.ScriptIndex.Sub) ? "Pre" : "Post", ((idx & PartialTextRun.ScriptIndex.Super) != PartialTextRun.ScriptIndex.Sub) ? "Super" : "Sub", script)));
			string text2 = "PartialTextRun(BaseVertical={0}, BaseHorizontal={1}, FullHorizontal={2}, Words={{{3}}}, IsSuperOrSubscript={4}, HasPreviousPartialTextRun={5}, NextPartialTextRun={6}, Scripts={7}, Line={8})";
			object[] array = new object[9];
			array[0] = this.BasicVerticalBounds;
			array[1] = this.BaseHorizontal;
			array[2] = this.FullHorizontal;
			array[3] = string.Join(", ", this.Words.Select((IWord w) => w.MinimalToString()));
			array[4] = this.IsSuperOrSubscript;
			array[5] = this.HasPreviousPartialTextRun;
			array[6] = this.NextPartialTextRun;
			array[7] = text;
			array[8] = this.Line;
			return string.Format(text2, array);
		}

		// Token: 0x0600559E RID: 21918 RVA: 0x0010E8DC File Offset: 0x0010CADC
		[CompilerGenerated]
		private bool <GetText>g__WordGapIsSpace|77_7(ref PartialTextRun.<>c__DisplayClass77_0 A_1, ref PartialTextRun.<>c__DisplayClass77_2 A_2, ref PartialTextRun.<>c__DisplayClass77_3 A_3)
		{
			if (A_2.wordPair.TextDirection != A_2.previousWordPair.TextDirection)
			{
				return true;
			}
			int? num = ((A_2.wordPair.TextDirection == TextDirection.LeftToRight) ? (A_3.word.ApparentPixelBoundsWithoutRotation.Horizontal.IsAfter(A_3.previousWord.ApparentPixelBoundsWithoutRotation.Horizontal, true) ? new int?(A_3.word.ApparentPixelBoundsWithoutRotation.Left - A_3.previousWord.ApparentPixelBoundsWithoutRotation.Right) : null) : (A_3.previousWord.ApparentPixelBoundsWithoutRotation.Horizontal.IsAfter(A_3.word.ApparentPixelBoundsWithoutRotation.Horizontal, true) ? new int?(A_3.previousWord.ApparentPixelBoundsWithoutRotation.Left - A_3.word.ApparentPixelBoundsWithoutRotation.Right) : null));
			double num2 = this._pageStatistics.GetGapLimit(A_3.previousWord, A_3.word) ?? Math.Min(A_3.previousWord.AverageGlyphWidth(), A_3.word.AverageGlyphWidth());
			return num != null && (double)num.Value > num2 / 3.0;
		}

		// Token: 0x0600559F RID: 21919 RVA: 0x0010EA50 File Offset: 0x0010CC50
		[CompilerGenerated]
		private TextDirection <GetText>g__nextStrongForN1|77_8(ref PartialTextRun.<>c__DisplayClass77_0 A_1, ref PartialTextRun.<>c__DisplayClass77_1 A_2)
		{
			return (from w in A_1.sortedWordsAndTextDirections.Skip(A_2.i + 1)
				select w.Word.Children[0].BidiCategory).MaybeFirst((BidiUnicodeCategory bidi) => bidi.IsStrong() || bidi.IsNumberCategory()).Select(delegate(BidiUnicodeCategory bidi)
			{
				if (!bidi.IsNumberCategory())
				{
					return bidi.GetDefaultTextDirection();
				}
				return TextDirection.RightToLeft;
			}).OrElse(A_1.textDirection);
		}

		// Token: 0x040026C6 RID: 9926
		private const int MinVerticalOverlapSize = 3;

		// Token: 0x040026C7 RID: 9927
		private bool _isSuperOrSubscript;

		// Token: 0x040026C8 RID: 9928
		[Nullable(2)]
		private PartialTextRun _nextPartialTextRun;

		// Token: 0x040026C9 RID: 9929
		[Nullable(2)]
		private PartialTextRun _previousPartialTextRun;

		// Token: 0x040026CA RID: 9930
		[Nullable(2)]
		private PartialTextRun[] _scripts;

		// Token: 0x040026CB RID: 9931
		private readonly List<IWord> _words;

		// Token: 0x040026CF RID: 9935
		private readonly PageStatistics _pageStatistics;

		// Token: 0x02000D08 RID: 3336
		[NullableContext(0)]
		[Flags]
		private enum ScriptIndex
		{
			// Token: 0x040026D1 RID: 9937
			Sub = 0,
			// Token: 0x040026D2 RID: 9938
			Post = 0,
			// Token: 0x040026D3 RID: 9939
			Super = 1,
			// Token: 0x040026D4 RID: 9940
			Pre = 2
		}
	}
}
