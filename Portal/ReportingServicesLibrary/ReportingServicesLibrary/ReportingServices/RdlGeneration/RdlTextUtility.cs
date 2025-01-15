using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.RdlGeneration
{
	// Token: 0x02000382 RID: 898
	internal class RdlTextUtility
	{
		// Token: 0x06001DDC RID: 7644 RVA: 0x0007A2B4 File Offset: 0x000784B4
		public static string BuildWidthString(char c, int length)
		{
			int num = (int)((double)length * 1.02 + 2.0);
			return new string(c, num);
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x0007A2E0 File Offset: 0x000784E0
		public static SizeF CalculateStringSize(string str, Font font, int maxLines, float minWidth, Graphics g)
		{
			int num = 1;
			int length = str.Length;
			SizeF sizeF = g.MeasureString(str, font);
			SizeF sizeF2 = new SizeF(sizeF.Width, sizeF.Height);
			if (sizeF.Width <= minWidth)
			{
				return new SizeF(minWidth, sizeF.Height);
			}
			if (1 == maxLines)
			{
				return sizeF;
			}
			sizeF.Height = float.MaxValue;
			foreach (string text in RdlTextUtility.Tokenize(str))
			{
				float width = RdlTextUtility.CalculateStringSize(text, font, 1, 0f).Width;
				if (width > minWidth)
				{
					minWidth = width;
				}
			}
			float num2 = sizeF.Width;
			float num3 = minWidth;
			float num4 = sizeF.Width;
			int num5 = 0;
			using (StringFormat genericDefault = StringFormat.GenericDefault)
			{
				while (++num5 < 10)
				{
					sizeF = g.MeasureString(str, font, sizeF, genericDefault, out length, out num);
					if (num > maxLines || length < str.Length)
					{
						num3 = num4;
						num4 = (num4 + num2) / 2f;
					}
					else
					{
						if (num >= maxLines)
						{
							return sizeF;
						}
						if (num4 <= minWidth)
						{
							return sizeF;
						}
						num2 = num4;
						num4 = (num4 + num3) / 2f;
					}
					sizeF = new SizeF(num4, float.MaxValue);
				}
			}
			return sizeF2;
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0007A44C File Offset: 0x0007864C
		public static SizeF CalculateStringSize(string str, Font font, int maxLines, float minWidth)
		{
			SizeF sizeF;
			using (Bitmap bitmap = new Bitmap(1, 1))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					sizeF = RdlTextUtility.CalculateStringSize(str, font, maxLines, minWidth, graphics);
				}
			}
			return sizeF;
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0007A4A8 File Offset: 0x000786A8
		private static List<string> Tokenize(string str)
		{
			List<string> list = new List<string>();
			foreach (object obj in RdlTextUtility.__tokenizeRegex.Matches(str))
			{
				Match match = (Match)obj;
				list.Add(match.Value);
			}
			return list;
		}

		// Token: 0x04000C8F RID: 3215
		public const float SortButtonWidthInPixels = 19f;

		// Token: 0x04000C90 RID: 3216
		public const float ToggleButtonWidthInPixels = 11f;

		// Token: 0x04000C91 RID: 3217
		public const float ButtonPaddingInPixels = 1.1f;

		// Token: 0x04000C92 RID: 3218
		private static Regex __tokenizeRegex = new Regex("\\S+", RegexOptions.None);
	}
}
