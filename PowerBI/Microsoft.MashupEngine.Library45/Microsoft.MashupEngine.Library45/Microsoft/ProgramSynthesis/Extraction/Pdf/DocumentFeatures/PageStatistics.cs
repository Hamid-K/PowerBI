using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D05 RID: 3333
	[NullableContext(1)]
	[Nullable(0)]
	public class PageStatistics
	{
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x0600554F RID: 21839 RVA: 0x0010C254 File Offset: 0x0010A454
		public IReadOnlyDictionary<float, double> GapLimits { get; }

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06005550 RID: 21840 RVA: 0x0010C25C File Offset: 0x0010A45C
		public IReadOnlyDictionary<float, double> AverageGlyphWidths { get; }

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06005551 RID: 21841 RVA: 0x0010C264 File Offset: 0x0010A464
		private HashSet<FontCharacteristics> FixedWidthFonts { get; }

		// Token: 0x06005552 RID: 21842 RVA: 0x0010C26C File Offset: 0x0010A46C
		public PageStatistics(IReadOnlyList<IReadOnlyList<Glyph>> glyphs)
		{
			FloatKeyReadOnlyDictionary<IReadOnlyList<Glyph>> floatKeyReadOnlyDictionary = PageStatistics.GlyphsByFontSize(glyphs.SelectMany((IReadOnlyList<Glyph> group) => group));
			this.GapLimits = PageStatistics.CalculateGapLimits(floatKeyReadOnlyDictionary);
			this.AverageGlyphWidths = PageStatistics.CalculateAverageGlyphWidths(floatKeyReadOnlyDictionary);
			this.FixedWidthFonts = (from g in glyphs.SelectMany((IReadOnlyList<Glyph> @group) => @group).GroupByNonNull((Glyph glyph) => glyph.Font)
				where g.Select((Glyph glyph) => glyph.ApparentPixelBoundsWithoutRotation.Width()).ExtremaWithin(2)
				select g.Key).ConvertToHashSet<FontCharacteristics>();
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x0010C35E File Offset: 0x0010A55E
		public PageStatistics(IReadOnlyDictionary<float, double> gapLimits, IReadOnlyDictionary<float, double> averageGlyphWidths, HashSet<FontCharacteristics> fixedWidthFonts)
		{
			this.GapLimits = gapLimits;
			this.AverageGlyphWidths = averageGlyphWidths;
			this.FixedWidthFonts = fixedWidthFonts;
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x0010C37B File Offset: 0x0010A57B
		[NullableContext(2)]
		public bool IsFixedWidth(FontCharacteristics font)
		{
			return font != null && this.FixedWidthFonts.Contains(font);
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x0010C390 File Offset: 0x0010A590
		private static FloatKeyReadOnlyDictionary<IReadOnlyList<Glyph>> GlyphsByFontSize(IEnumerable<Glyph> glyphs)
		{
			return FloatKeyReadOnlyDictionary<IReadOnlyList<Glyph>>.CreateDropNullKeys<Glyph>(glyphs.Where((Glyph glyph) => glyph.IsLetterOrNumber), delegate(Glyph glyph)
			{
				FontCharacteristics font = glyph.Font;
				if (font == null)
				{
					return null;
				}
				return new float?(font.FontSize);
			}, ([Nullable(new byte[] { 1, 0 })] IEnumerable<Glyph> gs) => gs.ToList<Glyph>());
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x0010C405 File Offset: 0x0010A605
		private static FloatKeyReadOnlyDictionary<double> CalculateAverageGlyphWidths(FloatKeyReadOnlyDictionary<IReadOnlyList<Glyph>> glyphsBySize)
		{
			return glyphsBySize.Select<double>((IReadOnlyList<Glyph> glyphs) => glyphs.Average((Glyph glyph) => glyph.ApparentPixelBoundsWithoutRotation.Width()));
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x0010C42C File Offset: 0x0010A62C
		private static FloatKeyReadOnlyDictionary<double> CalculateGapLimits(FloatKeyReadOnlyDictionary<IReadOnlyList<Glyph>> glyphsBySize)
		{
			return glyphsBySize.Select<double>((IReadOnlyList<Glyph> glyphs) => (double)(from glyph in glyphs
				select glyph.ApparentPixelBoundsWithoutRotation.Width() into width
				orderby width descending
				select width).ElementAt((int)Math.Floor((double)glyphs.Count * 0.2)) * 0.8);
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x0010C454 File Offset: 0x0010A654
		[NullableContext(2)]
		public double? GetGapLimit(FontCharacteristics font)
		{
			if (font != null)
			{
				return this.GapLimits.MaybeGet(font.FontSize).OrElseNull<double>();
			}
			return null;
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x0010C484 File Offset: 0x0010A684
		public double? GetGapLimit(Glyph glyph)
		{
			return this.GetGapLimit(glyph.Font);
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x0010C494 File Offset: 0x0010A694
		public double? GetGapLimit(IWord previousWord, IWord word)
		{
			double? gapLimit = this.GetGapLimit(previousWord.Font);
			double? gapLimit2 = this.GetGapLimit(word.Font);
			if (gapLimit == null)
			{
				return gapLimit2;
			}
			double valueOrDefault = gapLimit.GetValueOrDefault();
			if (gapLimit2 != null)
			{
				double valueOrDefault2 = gapLimit2.GetValueOrDefault();
				return new double?(Math.Min(valueOrDefault, valueOrDefault2));
			}
			return new double?(valueOrDefault);
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x0010C4F4 File Offset: 0x0010A6F4
		public double GetEstimatedSpaceWidth(Glyph glyph)
		{
			return (double)glyph.ApparentPixelBoundsWithoutRotation.Width() * (this.IsFixedWidth(glyph.Font) ? 1.1 : (glyph.IsLetterOrNumber ? 0.8 : 2.0));
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x0010C548 File Offset: 0x0010A748
		private void GetWordStatistics(IWord word, out double averageGlyphWidth, out double estimatedSpaceDistance, bool? isSymbol = null)
		{
			if (word.Font != null && this.AverageGlyphWidths.TryGetValue(word.Font.FontSize, out averageGlyphWidth))
			{
				estimatedSpaceDistance = (this.IsFixedWidth(word.Font) ? (word.AverageGlyphWidth() * 1.1) : (averageGlyphWidth * 0.8));
				return;
			}
			averageGlyphWidth = word.AverageGlyphWidth();
			estimatedSpaceDistance = averageGlyphWidth * ((isSymbol ?? word.IsSymbol) ? 2.0 : 0.8);
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x0010C5E4 File Offset: 0x0010A7E4
		public void GetWordStatistics(IWord previousWord, IWord word, out double averageGlyphWidth, out double estimatedSpaceDistance, out double smallerEstimatedSpaceDistance)
		{
			if (previousWord.IsImage && word.IsImage)
			{
				averageGlyphWidth = 0.0;
				estimatedSpaceDistance = (smallerEstimatedSpaceDistance = 0.0);
				return;
			}
			if (previousWord.IsImage || word.IsImage)
			{
				if (previousWord.IsImage)
				{
					this.GetWordStatistics(word, out averageGlyphWidth, out estimatedSpaceDistance, null);
				}
				else
				{
					this.GetWordStatistics(previousWord, out averageGlyphWidth, out estimatedSpaceDistance, null);
				}
				smallerEstimatedSpaceDistance = estimatedSpaceDistance;
				return;
			}
			bool flag = word.IsSymbol && previousWord.IsSymbol;
			double num;
			double num2;
			this.GetWordStatistics(word, out num, out num2, new bool?(flag));
			double num3;
			double num4;
			this.GetWordStatistics(previousWord, out num3, out num4, new bool?(flag));
			averageGlyphWidth = Math.Max(num, num3);
			estimatedSpaceDistance = Math.Max(num2, num4);
			smallerEstimatedSpaceDistance = Math.Min(num2, num4);
		}

		// Token: 0x040026AE RID: 9902
		private const double SpaceWidth = 0.8;

		// Token: 0x040026AF RID: 9903
		private const double FixedWidthSpaceWidth = 1.1;

		// Token: 0x040026B0 RID: 9904
		private const double SymbolSpaceWidth = 2.0;

		// Token: 0x040026B1 RID: 9905
		private const double GapLimitPercentile = 0.2;

		// Token: 0x040026B2 RID: 9906
		private const double GapLimitFraction = 0.8;

		// Token: 0x040026B3 RID: 9907
		public static double FontSizeEpsilon = FloatKeyReadOnlyDictionary<double>.Epsilon;
	}
}
