using System;
using System.Collections.Generic;
using System.Drawing;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000008 RID: 8
	internal static class ColorHelper
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000022F8 File Offset: 0x000004F8
		internal static Color Lighten(Color c, double factor)
		{
			int num = (int)(factor * 255.0);
			return Color.FromArgb(Math.Min((int)c.R + num, 255), Math.Min((int)c.G + num, 255), Math.Min((int)c.B + num, 255));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002350 File Offset: 0x00000550
		internal static Color Darken(Color c, double factor)
		{
			int num = (int)(factor * 255.0);
			return Color.FromArgb(Math.Max((int)c.R - num, 0), Math.Max((int)c.G - num, 0), Math.Max((int)c.B - num, 0));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000239C File Offset: 0x0000059C
		internal static ColorRange CalculateFilledMapColors(string baseColorString)
		{
			double num = 0.6;
			int num2 = 50 / 2;
			Color color = ColorTranslator.FromHtml(baseColorString);
			double num3 = num / (double)num2;
			string text = ColorTranslator.ToHtml(ColorHelper.Lighten(color, num));
			double num4 = num - num3;
			string text2 = ColorTranslator.ToHtml(ColorHelper.Darken(color, num4));
			return new ColorRange(new List<string> { text, text2 }.ToReadOnlyCollection<string>());
		}
	}
}
