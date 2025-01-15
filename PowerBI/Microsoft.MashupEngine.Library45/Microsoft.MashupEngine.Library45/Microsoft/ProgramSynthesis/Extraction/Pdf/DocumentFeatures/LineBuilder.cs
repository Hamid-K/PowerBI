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
	// Token: 0x02000CD8 RID: 3288
	[NullableContext(1)]
	[Nullable(0)]
	internal static class LineBuilder
	{
		// Token: 0x0600547D RID: 21629 RVA: 0x00109C70 File Offset: 0x00107E70
		public static IReadOnlyList<LineGroup> BuildLineGroups(FloatKeyReadOnlyDictionary<IReadOnlyList<IWord>> words)
		{
			IReadOnlyList<LineGroup> readOnlyList;
			using (Logger.Instance.InfoTiming("Recognize Lines", "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\LineBuilder.cs", 23))
			{
				if (words.IsEmpty<KeyValuePair<float, IReadOnlyList<IWord>>>())
				{
					Logger.Instance.Debug("No words for line recognition", null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\LineBuilder.cs", 25);
					return new LineGroup[0];
				}
				Func<float, IReadOnlyList<IWord>, IEnumerable<LineGroup>> func;
				if ((func = LineBuilder.<>O.<0>__BuildLineGroupsForSingleAngle) == null)
				{
					func = (LineBuilder.<>O.<0>__BuildLineGroupsForSingleAngle = new Func<float, IReadOnlyList<IWord>, IEnumerable<LineGroup>>(LineBuilder.BuildLineGroupsForSingleAngle));
				}
				readOnlyList = words.SelectMany2(func).ToList<LineGroup>();
			}
			Logger.Instance.Debug("Lines", readOnlyList, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\LineBuilder.cs", 32);
			return readOnlyList;
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x00109D1C File Offset: 0x00107F1C
		private static IEnumerable<LineGroup> BuildLineGroupsForSingleAngle(float angle, IReadOnlyList<IWord> words)
		{
			IEnumerable<Line> enumerable = LineBuilder.BuildLinesForSingleAngle(angle, words);
			List<Line> list = new List<Line>();
			Range<PixelUnit>? range = null;
			foreach (Line line2 in enumerable.OrderBy((Line line) => line.BasicVerticalBounds.Min))
			{
				int? num = ((range != null) ? new int?(range.GetValueOrDefault().Max) : null);
				int min = line2.BasicVerticalBounds.Min;
				if ((num.GetValueOrDefault() <= min) & (num != null))
				{
					yield return new LineGroup(list, angle, range.Value);
					list = new List<Line>();
					range = null;
				}
				list.Add(line2);
				range = new Range<PixelUnit>?((range != null) ? range.GetValueOrDefault().Join(line2.BasicVerticalBounds) : line2.BasicVerticalBounds);
				line2 = null;
			}
			IEnumerator<Line> enumerator = null;
			if (range != null)
			{
				yield return new LineGroup(list, angle, range.Value);
			}
			yield break;
			yield break;
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x00109D33 File Offset: 0x00107F33
		private static IEnumerable<Line> BuildLinesForSingleAngle(float angle, IReadOnlyList<IWord> words)
		{
			LinkedList<IWord> heightSortedWords = new LinkedList<IWord>(words.OrderByDescending((IWord word) => word.BasicVerticalBounds.Size()));
			for (LinkedListNode<IWord> largestNode = heightSortedWords.First; largestNode != null; largestNode = largestNode.Next)
			{
				IWord value = largestNode.Value;
				List<IWord> list = new List<IWord>();
				list.Add(value);
				Range<PixelUnit> range = value.BasicVerticalBounds;
				LinkedListNode<IWord> linkedListNode = largestNode.Next;
				while (linkedListNode != null)
				{
					IWord value2 = linkedListNode.Value;
					Range<PixelUnit> basicVerticalBounds = value2.BasicVerticalBounds;
					Optional<Range<PixelUnit>> optional = basicVerticalBounds.Intersect(value.BasicVerticalBounds);
					if (optional.HasValue && (double)optional.Value.Size() / (double)value.BasicVerticalBounds.Size() > 0.9)
					{
						list.Add(value2);
						range = range.Join(basicVerticalBounds);
						LinkedListNode<IWord> next = linkedListNode.Next;
						heightSortedWords.Remove(linkedListNode);
						linkedListNode = next;
					}
					else
					{
						linkedListNode = linkedListNode.Next;
					}
				}
				yield return new Line(list.OrderBy((IWord word) => word.ApparentPixelBoundsWithoutRotation.Left).ToList<IWord>(), angle, range);
			}
			yield break;
		}

		// Token: 0x04002625 RID: 9765
		private const double LineOverlap = 0.9;

		// Token: 0x02000CD9 RID: 3289
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002626 RID: 9766
			[Nullable(0)]
			public static Func<float, IReadOnlyList<IWord>, IEnumerable<LineGroup>> <0>__BuildLineGroupsForSingleAngle;
		}
	}
}
