using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Utils.Logging;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D13 RID: 3347
	[NullableContext(1)]
	[Nullable(0)]
	internal static class PartialTextRunBuilder
	{
		// Token: 0x060055D8 RID: 21976 RVA: 0x0010EED8 File Offset: 0x0010D0D8
		private static bool TryRecognizeSuperOrSubScript(PartialTextRun newTextRun, PartialTextRun previousTextRun, double glyphWidth, [Nullable(2)] out PartialTextRun removeFromFrontier)
		{
			Func<Range<PixelUnit>, bool> <>9__0;
			foreach (bool flag in new bool[]
			{
				default(bool),
				true
			})
			{
				PartialTextRun partialTextRun;
				PartialTextRun partialTextRun2;
				if (flag)
				{
					partialTextRun = newTextRun;
					partialTextRun2 = previousTextRun;
				}
				else
				{
					partialTextRun = previousTextRun;
					partialTextRun2 = newTextRun;
				}
				int height = partialTextRun.Height;
				int height2 = partialTextRun2.Height;
				int num = partialTextRun2.BasicVerticalBounds.Center().CompareTo(partialTextRun.BasicVerticalBounds.Center());
				if (num == 0)
				{
					break;
				}
				bool flag2 = num < 0;
				if ((flag2 || partialTextRun2.BaseLine >= partialTextRun.BaseLine) && !partialTextRun2.IsSuperOrSubscript && (double)height2 <= (double)height * 0.9 && (double)partialTextRun2.BasicVerticalBounds.Intersect(partialTextRun.BasicVerticalBounds).Value.Size() >= (double)height * 0.2)
				{
					Optional<Range<PixelUnit>> optional = partialTextRun.BaseHorizontal.Intersect(partialTextRun2.BaseHorizontal);
					Func<Range<PixelUnit>, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (Range<PixelUnit> overlap) => (double)overlap.Size() >= glyphWidth / 2.0);
					}
					if (!optional.Select(func).OrElse(false))
					{
						if (partialTextRun.GetScript(flag, flag2) != null)
						{
							break;
						}
						int num2 = (flag ? (newTextRun.LeftIncludingPrescripts - previousTextRun.Right) : (newTextRun.Left - previousTextRun.RightIncludingPostscripts));
						if ((flag ? glyphWidth : (glyphWidth / 3.0)) >= (double)num2 && !partialTextRun2.ContainsScripts(new bool?(!flag), null))
						{
							partialTextRun.SetScript(partialTextRun2, flag, flag2);
							removeFromFrontier = (flag ? partialTextRun2 : null);
							return true;
						}
					}
				}
			}
			removeFromFrontier = null;
			return false;
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x0010F0B8 File Offset: 0x0010D2B8
		private static IEnumerable<PartialTextRun> SplitLineGroupIntoTextRuns(SeparatorCollection separators, PageStatistics pageStatistics, LineGroup lineGroup)
		{
			Func<IRotatedPixelBounded, IRotatedPixelBounded, bool> func = separators.AnySeparatesFunc(Axis.Vertical, lineGroup.Lines[0].Words[0].Children[0].TransformationMatrix);
			List<PartialTextRun> list = new List<PartialTextRun>();
			HashSet<PartialTextRun> hashSet = new HashSet<PartialTextRun>(IdentityEquality.Comparer);
			foreach (Record<Line, IWord> record in from t in lineGroup.Lines.SelectMany((Line line) => line.Words.Select((IWord w) => Record.Create<Line, IWord>(line, w)))
				orderby t.Item2.ApparentPixelBoundsWithoutRotation.Left
				select t)
			{
				Line line3;
				IWord word3;
				record.Deconstruct(out line3, out word3);
				Line line2 = line3;
				IWord word2 = word3;
				int num = word2.BasicVerticalBounds.Size();
				PartialTextRun partialTextRun = new PartialTextRun(pageStatistics, line2, word2);
				foreach (PartialTextRun partialTextRun2 in hashSet.OrderByDescending((PartialTextRun c) => c.Right).ToList<PartialTextRun>())
				{
					IWord lastNonWhitespaceWord = partialTextRun2.LastNonWhitespaceWord;
					int height = partialTextRun2.Height;
					bool flag = partialTextRun2.Line == line2;
					if (flag || partialTextRun2.IsLinedUpWith(word2))
					{
						PartialTextRun partialTextRun3;
						IEnumerable<PartialTextRun> enumerable;
						double num2;
						double num3;
						bool flag2;
						if (func(lastNonWhitespaceWord, word2))
						{
							hashSet.Remove(partialTextRun2);
						}
						else if (partialTextRun2.TryAppendTextRun(partialTextRun, flag, pageStatistics, out partialTextRun3, out enumerable, out num2, out num3, out flag2))
						{
							if (flag2)
							{
								partialTextRun = null;
							}
							if (partialTextRun3 != null)
							{
								list.Add(partialTextRun3);
							}
							list.AddRange(enumerable);
							if (!flag2)
							{
								hashSet.Remove(partialTextRun2);
								break;
							}
							if (partialTextRun3 != null)
							{
								hashSet.Remove(partialTextRun2);
								hashSet.Add(partialTextRun3);
								break;
							}
							break;
						}
						else if (num2 > num3)
						{
							hashSet.Remove(partialTextRun2);
						}
						else if (!word2.IsWhitespace)
						{
							PartialTextRun partialTextRun4;
							if (PartialTextRunBuilder.TryRecognizeSuperOrSubScript(partialTextRun, partialTextRun2, num3, out partialTextRun4))
							{
								if (partialTextRun4 != null)
								{
									hashSet.Remove(partialTextRun4);
								}
								if (partialTextRun.IsSuperOrSubscript)
								{
									break;
								}
							}
							else if (partialTextRun2.IsSuperOrSubscript || height > num || (height == num && num2 > 0.0) || (double)partialTextRun2.RightIncludingPostscripts + partialTextRun2.LastNonWhitespaceWord.AverageGlyphWidth() < (double)partialTextRun.LeftIncludingPrescripts - word2.AverageGlyphWidth())
							{
								hashSet.Remove(partialTextRun2);
							}
							else
							{
								if (word2.MayBeOverlay && num2 < 0.0)
								{
									partialTextRun = null;
									break;
								}
								if (lastNonWhitespaceWord.MayBeOverlay && num2 < 0.0)
								{
									hashSet.Remove(partialTextRun2);
									partialTextRun2.Ignore = true;
								}
							}
						}
					}
				}
				if (partialTextRun != null)
				{
					if (partialTextRun.Words.Any((IWord word) => !word.IsWhitespace))
					{
						list.Add(partialTextRun);
						hashSet.Add(partialTextRun);
					}
					else if (partialTextRun.ContainsScripts(null, null))
					{
						list.Add(partialTextRun);
					}
				}
			}
			return list;
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x0010F434 File Offset: 0x0010D634
		public static IReadOnlyList<PartialTextRun> Build(PageStatistics pageStatistics, SeparatorCollection separators, IReadOnlyList<LineGroup> lineGroups)
		{
			IReadOnlyList<PartialTextRun> readOnlyList;
			using (Logger.Instance.InfoTiming("Recognize PartialTextRuns", "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\PartialTextRunBuilder.cs", 245))
			{
				if (lineGroups.IsEmpty<LineGroup>())
				{
					Logger.Instance.Debug("No lines for partial textRun recognition.", null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\PartialTextRunBuilder.cs", 247);
					readOnlyList = new PartialTextRun[0];
				}
				else
				{
					readOnlyList = lineGroups.SelectMany((LineGroup lineGroup) => PartialTextRunBuilder.SplitLineGroupIntoTextRuns(separators, pageStatistics, lineGroup)).ToList<PartialTextRun>();
				}
			}
			return readOnlyList;
		}

		// Token: 0x0400270F RID: 9999
		internal const double SuperOrSubscriptOverlapLimit = 0.2;

		// Token: 0x04002710 RID: 10000
		private const double SuperOrSubscriptSizeFraction = 0.9;
	}
}
