using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000087 RID: 135
	internal class FontSize
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000C699 File Offset: 0x0000A899
		internal FontSize()
			: this(FontSize.DefaultFontSize, FontSizeUnit.Pixels)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C6A7 File Offset: 0x0000A8A7
		internal FontSize(float fontSizeValue, FontSizeUnit fontSizeUnit = FontSizeUnit.Pixels)
		{
			this._value = ((fontSizeUnit == FontSizeUnit.Pixels) ? fontSizeValue : (fontSizeValue * FontSize.DotsPerInch / FontSize.PointsPerInch));
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0:F2}{1}", this._value, FontSize.PixelString);
		}

		// Token: 0x040001A7 RID: 423
		private static readonly float DotsPerInch = 96f;

		// Token: 0x040001A8 RID: 424
		private static readonly float DefaultFontSize = 12f;

		// Token: 0x040001A9 RID: 425
		private static readonly string PixelString = "px";

		// Token: 0x040001AA RID: 426
		private static readonly float PointsPerInch = 72f;

		// Token: 0x040001AB RID: 427
		private float _value;
	}
}
