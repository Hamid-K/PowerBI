using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D76 RID: 3446
	[NullableContext(1)]
	[Nullable(0)]
	internal static class TextDecorationKindUtil
	{
		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x060057D8 RID: 22488 RVA: 0x001171FE File Offset: 0x001153FE
		public static IReadOnlyList<TextDecorationKind> Kinds { get; } = new TextDecorationKind[]
		{
			TextDecorationKind.Underline,
			TextDecorationKind.Strikethrough,
			TextDecorationKind.Overline
		};

		// Token: 0x060057D9 RID: 22489 RVA: 0x00117208 File Offset: 0x00115408
		[return: Nullable(new byte[] { 0, 1 })]
		public static Range<PixelUnit> GetPositionRange(this TextDecorationKind kind, Glyph glyph, Direction baseLineEdge, int tolerance)
		{
			switch (kind)
			{
			case TextDecorationKind.Underline:
			{
				int num = glyph.ApparentPixelBounds[baseLineEdge];
				return Range<PixelUnit>.CreateUnordered(num, num + tolerance * baseLineEdge.Derivative().Value());
			}
			case TextDecorationKind.Strikethrough:
			{
				double num2 = glyph.ApparentPixelBounds[baseLineEdge.AlignedAxis()].Center();
				double num3 = (double)tolerance / 2.0;
				return new Range<PixelUnit>((int)Math.Round(num2 - num3), (int)Math.Round(num2 + num3));
			}
			case TextDecorationKind.Overline:
			{
				int num4 = glyph.ApparentPixelBounds[baseLineEdge.Opposite()];
				return Range<PixelUnit>.CreateUnordered(num4, num4 - tolerance * baseLineEdge.Derivative().Value());
			}
			default:
				throw new NotImplementedException("Unknown TextDecorationKind: " + kind.ToString());
			}
		}
	}
}
