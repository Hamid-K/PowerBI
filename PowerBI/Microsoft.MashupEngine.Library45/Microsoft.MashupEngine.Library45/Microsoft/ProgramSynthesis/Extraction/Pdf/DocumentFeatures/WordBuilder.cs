using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Utils.Logging;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000DA4 RID: 3492
	internal static class WordBuilder
	{
		// Token: 0x0600590A RID: 22794 RVA: 0x0011AD20 File Offset: 0x00118F20
		[NullableContext(1)]
		public static FloatKeyReadOnlyDictionary<IReadOnlyList<IWord>> Build(PdfAnalyzerOptions options, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, IReadOnlyList<IReadOnlyList<Glyph>> glyphGroups, AlignmentDotCollection alignmentDotCollection, PageStatistics pageStatistics, ImageCollection images, SeparatorCollection separators)
		{
			WordBuilder.<>c__DisplayClass0_0 CS$<>8__locals1;
			CS$<>8__locals1.pageStatistics = pageStatistics;
			CS$<>8__locals1.separators = separators;
			CS$<>8__locals1.pageBounds = pageBounds;
			if (glyphGroups.IsEmpty<IReadOnlyList<Glyph>>())
			{
				Logger.Instance.Debug("No glyphs for word recognition.", null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\WordBuilder.cs", 25);
				return FloatKeyReadOnlyDictionary<IReadOnlyList<IWord>>.Empty;
			}
			IStopwatchWrapper stopwatchWrapper = Logger.Instance.InfoTiming("Recognize Words", "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\WordBuilder.cs", 29);
			bool flag = options.Version >= PdfAnalyzerVersion.V1_1;
			CS$<>8__locals1.splitOnTextDirection = options.Version >= PdfAnalyzerVersion.V1_2;
			bool flag2 = options.Version >= PdfAnalyzerVersion.V1_3;
			CS$<>8__locals1.wordList = new FloatKeyMultiValueDictionary<Word>();
			foreach (ImageGlyph imageGlyph in images.BuildGlyphs(options))
			{
				FloatKeyMultiValueDictionary<Word> wordList = CS$<>8__locals1.wordList;
				TransformationMatrix transformationMatrix = imageGlyph.TransformationMatrix;
				wordList.MaybeAdd((transformationMatrix != null) ? transformationMatrix.RotationAngle : 0f, Word.MaybeCreate(new ImageGlyph[] { imageGlyph }, CS$<>8__locals1.pageBounds, flag ? null : new bool?(true), TextDirection.Neutral, null, null, null));
			}
			CS$<>8__locals1.currentGlyphs = new List<Glyph>();
			CS$<>8__locals1.seenSpace = (flag ? null : new bool?(false));
			CS$<>8__locals1.previousGlyph = null;
			CS$<>8__locals1.textDirection = TextDirection.Neutral;
			CS$<>8__locals1.lastWord = null;
			CS$<>8__locals1.beforeDotsRow = null;
			foreach (IReadOnlyList<Glyph> readOnlyList in glyphGroups)
			{
				if (flag)
				{
					CS$<>8__locals1.currentGlyphs = new List<Glyph>();
					CS$<>8__locals1.seenSpace = null;
					CS$<>8__locals1.previousGlyph = null;
				}
				Func<IRotatedPixelBounded, IRotatedPixelBounded, bool> func = CS$<>8__locals1.separators.AnySeparatesFunc(Axis.Vertical, readOnlyList[0].TransformationMatrix);
				using (IEnumerator<Glyph> enumerator3 = readOnlyList.OrderBy((Glyph glyph) => glyph.ApparentPixelBoundsWithoutRotation.Left).GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						WordBuilder.<>c__DisplayClass0_1 CS$<>8__locals2 = new WordBuilder.<>c__DisplayClass0_1();
						CS$<>8__locals2.glyph = enumerator3.Current;
						if (!flag2 || !CS$<>8__locals1.separators.Backgrounds.OverlappingElements(CS$<>8__locals2.glyph.ApparentPixelBounds).Any(delegate(ShadedBounds el)
						{
							if (el.RenderingOrders.Min > CS$<>8__locals2.glyph.RenderingOrder && el.ShadingColor.A == 255)
							{
								return CS$<>8__locals2.glyph.ApparentPixelBounds.Subtract(el.PixelBounds).Sum((Bounds<PixelUnit> b) => b.Area()) < CS$<>8__locals2.glyph.ApparentPixelBounds.Area() / 4;
							}
							return false;
						}))
						{
							WordBuilder.<>c__DisplayClass0_2 CS$<>8__locals3;
							CS$<>8__locals3.glyphDirection = CS$<>8__locals2.glyph.BidiCategory.GetDefaultTextDirection();
							if (CS$<>8__locals1.splitOnTextDirection && CS$<>8__locals1.currentGlyphs.Count == 0)
							{
								CS$<>8__locals1.textDirection = CS$<>8__locals3.glyphDirection;
							}
							if (string.IsNullOrWhiteSpace(CS$<>8__locals2.glyph.Text))
							{
								WordBuilder.<Build>g__CompleteWord|0_2(null, ref CS$<>8__locals1);
								if (flag)
								{
									CS$<>8__locals2.<Build>g__AddGlyph|7(ref CS$<>8__locals1, ref CS$<>8__locals3);
									WordBuilder.<Build>g__CompleteWord|0_2(null, ref CS$<>8__locals1);
									CS$<>8__locals1.seenSpace = null;
								}
								else
								{
									CS$<>8__locals1.seenSpace = new bool?(true);
								}
							}
							else
							{
								if (CS$<>8__locals2.glyph.IsAlignmentDot)
								{
									Glyph previousGlyph = CS$<>8__locals1.previousGlyph;
									if (previousGlyph != null && previousGlyph.IsAlignmentDot)
									{
										CS$<>8__locals1.beforeDotsRow = CS$<>8__locals2.glyph.AlignmentDotRow;
										CS$<>8__locals1.previousGlyph = CS$<>8__locals2.glyph;
										continue;
									}
								}
								bool flag3 = CS$<>8__locals1.previousGlyph == null && CS$<>8__locals2.glyph.IsAlignmentDot && CS$<>8__locals1.lastWord != null && !CS$<>8__locals1.lastWord.IsRotated && !CS$<>8__locals2.glyph.IsRotated;
								if (flag3 && CS$<>8__locals1.lastWord != null)
								{
									CS$<>8__locals1.previousGlyph = CS$<>8__locals1.lastWord.Children.Last<Glyph>();
								}
								if (CS$<>8__locals1.previousGlyph != null)
								{
									int? num = (CS$<>8__locals2.glyph.ApparentPixelBoundsWithoutRotation.Horizontal.IsAfter(CS$<>8__locals1.previousGlyph.ApparentPixelBoundsWithoutRotation.Horizontal, true) ? new int?(CS$<>8__locals2.glyph.ApparentPixelBoundsWithoutRotation.Left - CS$<>8__locals1.previousGlyph.ApparentPixelBoundsWithoutRotation.Right) : null);
									if (CS$<>8__locals2.glyph.IsAlignmentDot || CS$<>8__locals1.previousGlyph.IsAlignmentDot)
									{
										if (num != null && (double)num.Value <= CS$<>8__locals1.pageStatistics.GetEstimatedSpaceWidth(CS$<>8__locals2.glyph.IsAlignmentDot ? CS$<>8__locals2.glyph : CS$<>8__locals1.previousGlyph) * 2.0 && !func(CS$<>8__locals1.previousGlyph, CS$<>8__locals2.glyph))
										{
											if (CS$<>8__locals2.glyph.IsAlignmentDot)
											{
												if (flag3 && CS$<>8__locals1.lastWord != null)
												{
													CS$<>8__locals1.lastWord.AfterAlignmentDotRow = CS$<>8__locals2.glyph.AlignmentDotRow;
												}
												else
												{
													WordBuilder.<Build>g__CompleteWord|0_2(CS$<>8__locals2.glyph.AlignmentDotRow, ref CS$<>8__locals1);
												}
												CS$<>8__locals1.beforeDotsRow = CS$<>8__locals2.glyph.AlignmentDotRow;
											}
											else if (CS$<>8__locals1.previousGlyph.IsAlignmentDot)
											{
												CS$<>8__locals1.beforeDotsRow = CS$<>8__locals1.previousGlyph.AlignmentDotRow;
											}
										}
										else if (CS$<>8__locals2.glyph.IsAlignmentDot)
										{
											CS$<>8__locals1.beforeDotsRow = CS$<>8__locals2.glyph.AlignmentDotRow;
											WordBuilder.<Build>g__CompleteWord|0_2(null, ref CS$<>8__locals1);
										}
									}
									else if (CS$<>8__locals2.glyph.IsLetterOrNumber && CS$<>8__locals1.previousGlyph.IsLetterOrNumber)
									{
										double? gapLimit = CS$<>8__locals1.pageStatistics.GetGapLimit(CS$<>8__locals2.glyph);
										if (gapLimit == null || (num != null && (double)num.Value > gapLimit.Value) || func(CS$<>8__locals1.previousGlyph, CS$<>8__locals2.glyph))
										{
											WordBuilder.<Build>g__CompleteWord|0_2(null, ref CS$<>8__locals1);
											CS$<>8__locals1.seenSpace = new bool?(true);
										}
									}
									else
									{
										WordBuilder.<Build>g__CompleteWord|0_2(null, ref CS$<>8__locals1);
									}
								}
								if (CS$<>8__locals1.splitOnTextDirection && ((CS$<>8__locals1.currentGlyphs.Count >= 1 && CS$<>8__locals1.currentGlyphs[0].BidiCategory != CS$<>8__locals2.glyph.BidiCategory && (CS$<>8__locals1.currentGlyphs[0].BidiCategory.IsNumberRelatedCategory() || CS$<>8__locals2.glyph.BidiCategory.IsNumberRelatedCategory())) || (CS$<>8__locals1.textDirection != CS$<>8__locals3.glyphDirection && (CS$<>8__locals1.textDirection == TextDirection.RightToLeft || CS$<>8__locals3.glyphDirection == TextDirection.RightToLeft))))
								{
									WordBuilder.<Build>g__CompleteWord|0_2(null, ref CS$<>8__locals1);
								}
								CS$<>8__locals2.<Build>g__AddGlyph|7(ref CS$<>8__locals1, ref CS$<>8__locals3);
								CS$<>8__locals1.previousGlyph = CS$<>8__locals2.glyph;
							}
						}
					}
				}
				WordBuilder.<Build>g__CompleteWord|0_2(null, ref CS$<>8__locals1);
			}
			FloatKeyReadOnlyDictionary<IReadOnlyList<IWord>> floatKeyReadOnlyDictionary = CS$<>8__locals1.wordList.Select<IReadOnlyList<IWord>>((List<Word> wordsByAngle) => wordsByAngle.ToList<Word>());
			stopwatchWrapper.Stop();
			if (Logger.Instance.ShouldDebug())
			{
				Logger.Instance.Debug("Words", floatKeyReadOnlyDictionary.Values.SelectMany((IReadOnlyList<IWord> w) => w).ToList<IWord>(), "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\WordBuilder.cs", 215);
			}
			return floatKeyReadOnlyDictionary;
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x0011B488 File Offset: 0x00119688
		[NullableContext(2)]
		[CompilerGenerated]
		internal static void <Build>g__CompleteWord|0_2(AlignmentDotCollection.AlignmentDotRow afterDotsRow = null, ref WordBuilder.<>c__DisplayClass0_0 A_1)
		{
			if (A_1.currentGlyphs.Any<Glyph>())
			{
				if (A_1.beforeDotsRow != null)
				{
					if ((from intersection in A_1.beforeDotsRow.ApparentPixelBounds.Vertical.Intersect(A_1.currentGlyphs.First<Glyph>().ApparentPixelBounds.Vertical)
						select intersection.Size() <= 2).OrElse(true) || (double)A_1.beforeDotsRow.ApparentPixelBounds.Horizontal.Distance(A_1.currentGlyphs.First<Glyph>().ApparentPixelBounds.Horizontal) > A_1.pageStatistics.GetEstimatedSpaceWidth(A_1.beforeDotsRow.DotGlyphs.Last<Glyph>()) * 2.0 || A_1.separators.AnySeparates(Axis.Vertical, A_1.beforeDotsRow, A_1.currentGlyphs[0]))
					{
						A_1.beforeDotsRow = null;
					}
				}
				IReadOnlyList<Glyph> currentGlyphs = A_1.currentGlyphs;
				Bounds<PixelUnit> pageBounds = A_1.pageBounds;
				FontCharacteristics font = A_1.currentGlyphs.First<Glyph>().Font;
				Optional<Word> optional = Word.MaybeCreate(currentGlyphs, pageBounds, A_1.seenSpace, A_1.textDirection, font, A_1.beforeDotsRow, afterDotsRow);
				FloatKeyMultiValueDictionary<Word> wordList = A_1.wordList;
				TransformationMatrix transformationMatrix = A_1.currentGlyphs[0].TransformationMatrix;
				wordList.MaybeAdd((transformationMatrix != null) ? transformationMatrix.RotationAngle : 0f, optional);
				A_1.seenSpace = new bool?(false);
				A_1.textDirection = TextDirection.Neutral;
				A_1.currentGlyphs = new List<Glyph>();
				if (optional.Select((Word w) => !w.IsWhitespace).OrElse(true))
				{
					A_1.lastWord = optional.OrElseDefault<Word>();
					A_1.beforeDotsRow = null;
				}
			}
			A_1.previousGlyph = null;
		}
	}
}
