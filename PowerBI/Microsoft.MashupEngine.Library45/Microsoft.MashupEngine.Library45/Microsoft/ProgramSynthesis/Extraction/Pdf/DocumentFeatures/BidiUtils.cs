using System;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000C66 RID: 3174
	public static class BidiUtils
	{
		// Token: 0x060051CA RID: 20938 RVA: 0x00101958 File Offset: 0x000FFB58
		public static TextDirection GetDefaultTextDirection(this BidiUnicodeCategory bidi)
		{
			switch (bidi)
			{
			case BidiUnicodeCategory.LeftToRight:
				return TextDirection.LeftToRight;
			case BidiUnicodeCategory.RightToLeft:
			case BidiUnicodeCategory.RightToLeftArabic:
				return TextDirection.RightToLeft;
			case BidiUnicodeCategory.EuropeanNumber:
			case BidiUnicodeCategory.EuropeanNumberSeparator:
			case BidiUnicodeCategory.EuropeanNumberTerminator:
			case BidiUnicodeCategory.ArabicNumber:
			case BidiUnicodeCategory.CommonNumberSeparator:
			case BidiUnicodeCategory.NonspacingMark:
			case BidiUnicodeCategory.BoundaryNeutral:
				return TextDirection.Weak;
			case BidiUnicodeCategory.ParagraphSeparator:
			case BidiUnicodeCategory.SegmentSeparator:
			case BidiUnicodeCategory.Whitespace:
			case BidiUnicodeCategory.OtherNeutral:
				return TextDirection.Neutral;
			}
			return TextDirection.Unknown;
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x001019B4 File Offset: 0x000FFBB4
		public static TextDirection GetNumbersAsRtlTextDirection(this BidiUnicodeCategory bidi)
		{
			if (!bidi.IsNumberCategory())
			{
				return bidi.GetDefaultTextDirection();
			}
			return TextDirection.RightToLeft;
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x001019C6 File Offset: 0x000FFBC6
		public static bool IsNumberCategory(this BidiUnicodeCategory bidi)
		{
			return bidi == BidiUnicodeCategory.EuropeanNumber || bidi == BidiUnicodeCategory.ArabicNumber;
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x001019D2 File Offset: 0x000FFBD2
		public static bool IsNumberRelatedCategory(this BidiUnicodeCategory bidi)
		{
			return bidi - BidiUnicodeCategory.EuropeanNumber <= 4;
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x001019DD File Offset: 0x000FFBDD
		public static bool IsStrong(this TextDirection textDirection)
		{
			return textDirection == TextDirection.LeftToRight || textDirection == TextDirection.RightToLeft;
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x001019E9 File Offset: 0x000FFBE9
		public static bool IsStrong(this BidiUnicodeCategory bidi)
		{
			return bidi.GetDefaultTextDirection().IsStrong();
		}
	}
}
