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
	// Token: 0x02000D7C RID: 3452
	[NullableContext(1)]
	[Nullable(0)]
	internal static class TextRunBuilder
	{
		// Token: 0x06005812 RID: 22546 RVA: 0x001179A0 File Offset: 0x00115BA0
		public static QuadTree<ITextRun, PixelUnit> Build([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, AlignmentDotCollection alignmentDotCollection, IReadOnlyList<PartialTextRun> partialTextRuns)
		{
			QuadTree<ITextRun, PixelUnit> quadTree = new QuadTree<ITextRun, PixelUnit>(pageBounds);
			if (partialTextRuns.IsEmpty<PartialTextRun>())
			{
				Logger.Instance.Debug("No partial text runs for textRun recognition.", null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\TextRunBuilder.cs", 16);
				return quadTree;
			}
			IStopwatchWrapper stopwatchWrapper = Logger.Instance.InfoTiming("Recognize TextRuns", "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\TextRunBuilder.cs", 20);
			int num = ImageCollection.MaxAreaCutoff(pageBounds);
			foreach (ITextRun textRun in partialTextRuns.Select((PartialTextRun partialTextRun) => partialTextRun.AsTextRun).Collect<ITextRun>())
			{
				TextRun textRun2 = (TextRun)textRun;
				if (textRun2.IsRotated && !textRun2.IsRotatedByRightAngle && textRun2.ApparentPixelBounds.Area() > num)
				{
					using (IEnumerator<IWord> enumerator2 = textRun2.Children.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							IWord word = enumerator2.Current;
							word.IsBackground = true;
						}
						continue;
					}
				}
				quadTree.Add(textRun2);
			}
			TextRunBuilder.ForceAlignmentDotsToSingleColumn(quadTree, alignmentDotCollection);
			stopwatchWrapper.Stop();
			Logger.Instance.Debug("TextRuns", quadTree.ToList<ITextRun>(), "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\TextRunBuilder.cs", 41);
			return quadTree;
		}

		// Token: 0x06005813 RID: 22547 RVA: 0x00117AF0 File Offset: 0x00115CF0
		private static void ForceAlignmentDotsToSingleColumn(QuadTree<ITextRun, PixelUnit> textRuns, AlignmentDotCollection.AlignmentDotColumn dotColumn)
		{
			TextRunBuilder.<>c__DisplayClass1_0 CS$<>8__locals1 = new TextRunBuilder.<>c__DisplayClass1_0();
			CS$<>8__locals1.dotColumn = dotColumn;
			CS$<>8__locals1.textRuns = textRuns;
			CS$<>8__locals1.textRunsInColumn = CS$<>8__locals1.textRuns.Where((ITextRun tr) => CS$<>8__locals1.dotColumn.DotRows.Contains(tr.AfterAlignmentDotRow)).ConvertToHashSet<ITextRun>();
			if (!CS$<>8__locals1.textRunsInColumn.Any<ITextRun>())
			{
				return;
			}
			Bounds<PixelUnit> bounds = Bounds<PixelUnit>.Join(CS$<>8__locals1.textRunsInColumn.Select((ITextRun tr) => tr.ApparentPixelBounds).PrependItem(CS$<>8__locals1.dotColumn.ApparentPixelBounds));
			bool flag;
			do
			{
				flag = false;
				foreach (ITextRun textRun in CS$<>8__locals1.textRuns.OverlappingElements(bounds).Except(CS$<>8__locals1.textRunsInColumn).ToList<ITextRun>())
				{
					TextRunBuilder.<>c__DisplayClass1_1 CS$<>8__locals2 = new TextRunBuilder.<>c__DisplayClass1_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					if (CS$<>8__locals2.CS$<>8__locals1.textRuns.Contains(textRun))
					{
						CS$<>8__locals2.newTextRun = textRun;
						if (!bounds.Contains(CS$<>8__locals2.newTextRun.ApparentPixelBounds))
						{
							bounds = bounds.Join(CS$<>8__locals2.newTextRun.ApparentPixelBounds);
							flag = true;
						}
						QuadTree<ITextRun, PixelUnit> textRuns2 = CS$<>8__locals2.CS$<>8__locals1.textRuns;
						Range<PixelUnit> vertical = CS$<>8__locals2.newTextRun.ApparentPixelBounds.Vertical;
						List<ITextRun> list = (from nearbyTextRun in textRuns2.OverlappingElements(new Bounds<PixelUnit>(bounds.Horizontal, vertical))
							where (double)nearbyTextRun.ScriptsInclusiveBounds.Vertical.Intersect(CS$<>8__locals2.newTextRun.ScriptsInclusiveBounds.Vertical).Value.Size() >= (double)Math.Max(nearbyTextRun.ScriptsInclusiveBounds.Height(), CS$<>8__locals2.newTextRun.ScriptsInclusiveBounds.Height()) * 0.2
							select nearbyTextRun).ToList<ITextRun>();
						ITextRun textRun2 = list.Where((ITextRun tr) => tr.ApparentPixelBounds.Left < CS$<>8__locals2.newTextRun.ApparentPixelBounds.Left).ArgMin((ITextRun tr) => tr.ApparentPixelBounds.Horizontal.Distance(CS$<>8__locals2.newTextRun.ApparentPixelBounds.Horizontal));
						ITextRun textRun3 = list.Where((ITextRun tr) => tr.ApparentPixelBounds.Right > CS$<>8__locals2.newTextRun.ApparentPixelBounds.Right).ArgMin((ITextRun tr) => tr.ApparentPixelBounds.Horizontal.Distance(CS$<>8__locals2.newTextRun.ApparentPixelBounds.Horizontal));
						if (!(false | CS$<>8__locals2.<ForceAlignmentDotsToSingleColumn>g__TryMergeTextRun|7(textRun2, true) | CS$<>8__locals2.<ForceAlignmentDotsToSingleColumn>g__TryMergeTextRun|7(textRun3, false)))
						{
							CS$<>8__locals2.CS$<>8__locals1.textRunsInColumn.Add(CS$<>8__locals2.newTextRun);
						}
					}
				}
			}
			while (flag);
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x00117D18 File Offset: 0x00115F18
		private static void ForceAlignmentDotsToSingleColumn(QuadTree<ITextRun, PixelUnit> textRuns, AlignmentDotCollection alignmentDotCollection)
		{
			foreach (AlignmentDotCollection.AlignmentDotColumn alignmentDotColumn in alignmentDotCollection.DotColumns)
			{
				TextRunBuilder.ForceAlignmentDotsToSingleColumn(textRuns, alignmentDotColumn);
			}
		}
	}
}
