using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics
{
	// Token: 0x02000C13 RID: 3091
	[NullableContext(1)]
	[Nullable(0)]
	public static class Semantics
	{
		// Token: 0x06004FDE RID: 20446 RVA: 0x000FB668 File Offset: 0x000F9868
		public static BoundsOnPdfPage SnapToGlyphs(BoundsOnPdfPage bounds)
		{
			return Bounds<PixelUnit>.MaybeJoin(from b in bounds.PageData.GetGlyphs().SelectMany((IReadOnlyList<Glyph> g) => from glyph in g
					where !string.IsNullOrWhiteSpace(glyph.Text)
					select glyph.ApparentPixelBounds)
				where bounds.Bounds.Contains(b)
				select b).Select(new Func<Bounds<PixelUnit>, BoundsOnPdfPage>(bounds.GetBoundsOnSamePage)).OrElse(bounds);
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x000FB6ED File Offset: 0x000F98ED
		[return: Nullable(2)]
		public static BoundsOnPdfPage Between(Axis _, BoundsOnPdfPage before, BoundsOnPdfPage after)
		{
			if (!before.SamePageAs(after))
			{
				return null;
			}
			return Bounds<PixelUnit>.MaybeBetween(before.Bounds, after.Bounds).Select(new Func<Bounds<PixelUnit>, BoundsOnPdfPage>(before.GetBoundsOnSamePage)).OrElseDefault<BoundsOnPdfPage>();
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x000FB724 File Offset: 0x000F9924
		public static BoundsOnPdfPage NextSeparator(BoundsOnPdfPage baseBounds, Direction dir, int k)
		{
			return (from separator in baseBounds.PageData.BuildSeparatorCollection().Separators[dir.AlignedAxis().Perpendicular()].OverlappingElements(baseBounds.Bounds.With(dir, baseBounds.PageData.GetPageBounds()[dir])).OrderByClosest(dir)
				select baseBounds.GetBoundsOnSamePage(separator.PixelBounds)).ElementAtOrDefault(k);
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x000FB7B4 File Offset: 0x000F99B4
		public static BoundsOnPdfPage NextSameWidthSeparator(BoundsOnPdfPage baseBounds, Direction dir, int k, int tolerance)
		{
			SeparatorCollection separatorCollection = baseBounds.PageData.BuildSeparatorCollection();
			int baseBoundsSize = baseBounds.Bounds[dir.AlignedAxis().Perpendicular()].Size();
			return (from separator in (from separator in separatorCollection.Separators[dir.AlignedAxis().Perpendicular()].OverlappingElements(baseBounds.Bounds.With(dir, baseBounds.PageData.GetPageBounds()[dir]))
					where MathUtils.WithinTolerance(baseBoundsSize, separator.Line.Range.Size(), tolerance)
					select separator).OrderByClosest(dir)
				select baseBounds.GetBoundsOnSamePage(separator.PixelBounds)).ElementAtOrDefault(k);
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x000FB888 File Offset: 0x000F9A88
		[return: Nullable(2)]
		public static BoundsOnPdfPage NextFontSizeDecrease(BoundsOnPdfPage baseBounds, Direction dir)
		{
			float? num = null;
			foreach (ITextRun textRun in baseBounds.PageData.BuildTextRuns().ContainedElements(baseBounds.Bounds.With(dir, baseBounds.PageData.GetPageBounds()[dir])).OrderByClosest(dir))
			{
				if (textRun.Font != null)
				{
					if (num == null)
					{
						num = new float?(textRun.Font.FontSize);
					}
					else if (textRun.Font.FontSize < num.Value)
					{
						int num2 = textRun.ScriptsInclusiveBounds[dir.Opposite()];
						return baseBounds.GetBoundsOnSamePage(baseBounds.Bounds.With(dir.AlignedAxis(), new Range<PixelUnit>(num2, num2)));
					}
				}
			}
			return null;
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x000FB990 File Offset: 0x000F9B90
		public static BoundsOnPdfPage PageBounds(SinglePagePdfRegion pdfRegion)
		{
			return pdfRegion.GetBoundsOnSamePage(pdfRegion.PageData.GetPageBounds());
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x000FB9A4 File Offset: 0x000F9BA4
		[return: Nullable(2)]
		public static BoundsOnPdfPage CombineBounds(BoundsOnPdfPage horizontal, BoundsOnPdfPage vertical)
		{
			if (!horizontal.SamePageAs(vertical))
			{
				return null;
			}
			return horizontal.GetBoundsOnSamePage(new Bounds<PixelUnit>(horizontal.Bounds.Horizontal, vertical.Bounds.Vertical));
		}
	}
}
