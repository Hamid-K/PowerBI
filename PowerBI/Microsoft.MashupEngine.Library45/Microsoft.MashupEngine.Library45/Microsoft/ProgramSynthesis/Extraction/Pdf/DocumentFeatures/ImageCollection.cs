using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CCE RID: 3278
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImageCollection
	{
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x0600543F RID: 21567 RVA: 0x001095E0 File Offset: 0x001077E0
		public IReadOnlyList<Image> GlyphImages { get; }

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06005440 RID: 21568 RVA: 0x001095E8 File Offset: 0x001077E8
		public IReadOnlyList<Image> SeparatorImages { get; }

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06005441 RID: 21569 RVA: 0x001095F0 File Offset: 0x001077F0
		public IReadOnlyList<Image> OtherImages { get; }

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06005442 RID: 21570 RVA: 0x001095F8 File Offset: 0x001077F8
		public IEnumerable<Separator> Separators
		{
			get
			{
				return this.SeparatorImages.Select((Image image) => image.AsSeparator);
			}
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x00109624 File Offset: 0x00107824
		public IEnumerable<ImageGlyph> BuildGlyphs(PdfAnalyzerOptions options)
		{
			return this.GlyphImages.Select((Image image) => image.AsGlyph(options));
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x00109655 File Offset: 0x00107855
		private ImageCollection(IReadOnlyList<Image> glyphImages, IReadOnlyList<Image> separatorImages, IReadOnlyList<Image> otherImages)
		{
			this.GlyphImages = glyphImages;
			this.SeparatorImages = separatorImages;
			this.OtherImages = otherImages;
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x00109672 File Offset: 0x00107872
		private static int MinAreaCutoff([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds)
		{
			return (int)((double)pageBounds.Area() * 1E-05);
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x00109687 File Offset: 0x00107887
		internal static int MaxAreaCutoff([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds)
		{
			return (int)((double)pageBounds.Area() * 0.2);
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x0010969C File Offset: 0x0010789C
		public static ImageCollection Build([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, IReadOnlyList<IReadOnlyList<Glyph>> glyphs, IReadOnlyList<Image> images)
		{
			List<Image> list = new List<Image>();
			List<Image> list2 = new List<Image>();
			List<Image> list3 = new List<Image>();
			int num = ImageCollection.MinAreaCutoff(pageBounds);
			int num2 = ImageCollection.MaxAreaCutoff(pageBounds);
			QuadTree<Image, PixelUnit> quadTree = new QuadTree<Image, PixelUnit>(pageBounds);
			foreach (Image image5 in images.OrderBy((Image image) => image.RenderingOrder))
			{
				int num3 = image5.PixelBounds.Area();
				if (num3 <= num || num3 >= num2)
				{
					list3.Add(image5);
				}
				else
				{
					IEnumerable<Image> enumerable = quadTree.OverlappingElements(image5.PixelBounds.Extend(1)).ToList<Image>();
					Image image2 = image5;
					foreach (Image image3 in enumerable)
					{
						Optional<Bounds<PixelUnit>> optional = image2.PixelBounds.MaybeJoinExact(image3.PixelBounds);
						if (optional.HasValue)
						{
							quadTree.Remove(image3);
							image2 = new Image(optional.Value, Math.Max(image2.RenderingOrder, image3.RenderingOrder), null);
						}
					}
					if (image2.PixelBounds.Area() >= num2)
					{
						list3.Add(image2);
					}
					else
					{
						quadTree.Add(image2);
					}
				}
			}
			double num4;
			double num5;
			(from glyph in glyphs.SelectMany((IReadOnlyList<Glyph> g) => g)
				select glyph.ApparentPixelBoundsWithoutRotation.AspectRatio()).MaybeExtrema(null).OrElse(Record.Create<double, double>(1.0, 5.0)).Deconstruct(out num4, out num5);
			double num6 = num4;
			double num7 = num5;
			double num8 = Math.Max(1.0 / num6, num7) * 2.0;
			double num9 = 1.0 / num8;
			foreach (Image image4 in quadTree)
			{
				double num10 = image4.PixelBounds.AspectRatio();
				if (num10 < num9 || num10 > num8)
				{
					list.Add(image4);
				}
				else
				{
					list2.Add(image4);
				}
			}
			return new ImageCollection(list2, list, list3);
		}

		// Token: 0x04002608 RID: 9736
		private const double TinyImageCutoff = 1E-05;

		// Token: 0x04002609 RID: 9737
		private const double HugeImageCutoff = 0.2;
	}
}
