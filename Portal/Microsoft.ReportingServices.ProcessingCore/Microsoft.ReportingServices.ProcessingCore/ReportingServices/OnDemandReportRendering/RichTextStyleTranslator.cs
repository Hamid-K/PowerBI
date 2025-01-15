using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000209 RID: 521
	internal sealed class RichTextStyleTranslator
	{
		// Token: 0x060013A8 RID: 5032 RVA: 0x00050B79 File Offset: 0x0004ED79
		internal static bool CompareWithInvariantCulture(string str1, string str2)
		{
			return string.Compare(str1, str2, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00050B88 File Offset: 0x0004ED88
		internal static bool TranslateHtmlFontSize(string value, out string translatedSize)
		{
			int num;
			if (int.TryParse(value, out num))
			{
				if (num <= 0)
				{
					translatedSize = "7.5pt";
				}
				else
				{
					switch (num)
					{
					case 1:
						translatedSize = "7.5pt";
						return true;
					case 2:
						translatedSize = "10pt";
						return true;
					case 3:
						translatedSize = "11pt";
						return true;
					case 4:
						translatedSize = "13.5pt";
						return true;
					case 5:
						translatedSize = "18pt";
						return true;
					case 6:
						translatedSize = "24pt";
						return true;
					}
					translatedSize = "36pt";
				}
				return true;
			}
			translatedSize = null;
			return false;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00050C15 File Offset: 0x0004EE15
		internal static string TranslateHtmlColor(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (value[0] == '#')
				{
					return value;
				}
				if (char.IsDigit(value[0]))
				{
					return "#" + value;
				}
			}
			return value;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00050C48 File Offset: 0x0004EE48
		internal static bool TranslateFontWeight(string styleString, out FontWeights fontWieght)
		{
			fontWieght = FontWeights.Normal;
			if (!string.IsNullOrEmpty(styleString))
			{
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Normal", styleString))
				{
					fontWieght = FontWeights.Normal;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("Bold", styleString))
				{
					fontWieght = FontWeights.Bold;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("Bolder", styleString))
				{
					fontWieght = FontWeights.Bold;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("100", styleString))
				{
					fontWieght = FontWeights.Thin;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("200", styleString))
				{
					fontWieght = FontWeights.ExtraLight;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("300", styleString))
				{
					fontWieght = FontWeights.Light;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("400", styleString))
				{
					fontWieght = FontWeights.Normal;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("500", styleString))
				{
					fontWieght = FontWeights.Medium;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("600", styleString))
				{
					fontWieght = FontWeights.SemiBold;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("700", styleString))
				{
					fontWieght = FontWeights.Bold;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("800", styleString))
				{
					fontWieght = FontWeights.ExtraBold;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("900", styleString))
				{
					fontWieght = FontWeights.Heavy;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("Thin", styleString))
				{
					fontWieght = FontWeights.Thin;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("ExtraLight", styleString))
				{
					fontWieght = FontWeights.ExtraLight;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("Light", styleString))
				{
					fontWieght = FontWeights.Light;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("Lighter", styleString))
				{
					fontWieght = FontWeights.Light;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("Medium", styleString))
				{
					fontWieght = FontWeights.Medium;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("SemiBold", styleString))
				{
					fontWieght = FontWeights.SemiBold;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("ExtraBold", styleString))
				{
					fontWieght = FontWeights.ExtraBold;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("Heavy", styleString))
				{
					fontWieght = FontWeights.Heavy;
				}
				else
				{
					if (!RichTextStyleTranslator.CompareWithInvariantCulture("Default", styleString))
					{
						return false;
					}
					fontWieght = FontWeights.Normal;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00050E10 File Offset: 0x0004F010
		internal static bool TranslateTextAlign(string styleString, out TextAlignments textAlignment)
		{
			textAlignment = TextAlignments.General;
			if (!string.IsNullOrEmpty(styleString))
			{
				if (RichTextStyleTranslator.CompareWithInvariantCulture("General", styleString))
				{
					textAlignment = TextAlignments.General;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("Left", styleString))
				{
					textAlignment = TextAlignments.Left;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("Center", styleString))
				{
					textAlignment = TextAlignments.Center;
				}
				else if (RichTextStyleTranslator.CompareWithInvariantCulture("Right", styleString))
				{
					textAlignment = TextAlignments.Right;
				}
				else
				{
					if (!RichTextStyleTranslator.CompareWithInvariantCulture("Default", styleString))
					{
						return false;
					}
					textAlignment = TextAlignments.General;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0200093C RID: 2364
		internal class StyleEnumConstants
		{
			// Token: 0x04003FFC RID: 16380
			internal const string Default = "Default";

			// Token: 0x04003FFD RID: 16381
			internal const string Normal = "Normal";

			// Token: 0x04003FFE RID: 16382
			internal const string General = "General";

			// Token: 0x04003FFF RID: 16383
			internal const string Center = "Center";

			// Token: 0x04004000 RID: 16384
			internal const string Left = "Left";

			// Token: 0x04004001 RID: 16385
			internal const string Right = "Right";

			// Token: 0x04004002 RID: 16386
			internal const string Thin = "Thin";

			// Token: 0x04004003 RID: 16387
			internal const string ExtraLight = "ExtraLight";

			// Token: 0x04004004 RID: 16388
			internal const string Light = "Light";

			// Token: 0x04004005 RID: 16389
			internal const string Lighter = "Lighter";

			// Token: 0x04004006 RID: 16390
			internal const string Medium = "Medium";

			// Token: 0x04004007 RID: 16391
			internal const string SemiBold = "SemiBold";

			// Token: 0x04004008 RID: 16392
			internal const string Bold = "Bold";

			// Token: 0x04004009 RID: 16393
			internal const string Bolder = "Bolder";

			// Token: 0x0400400A RID: 16394
			internal const string ExtraBold = "ExtraBold";

			// Token: 0x0400400B RID: 16395
			internal const string Heavy = "Heavy";

			// Token: 0x0400400C RID: 16396
			internal const string FontWeight100 = "100";

			// Token: 0x0400400D RID: 16397
			internal const string FontWeight200 = "200";

			// Token: 0x0400400E RID: 16398
			internal const string FontWeight300 = "300";

			// Token: 0x0400400F RID: 16399
			internal const string FontWeight400 = "400";

			// Token: 0x04004010 RID: 16400
			internal const string FontWeight500 = "500";

			// Token: 0x04004011 RID: 16401
			internal const string FontWeight600 = "600";

			// Token: 0x04004012 RID: 16402
			internal const string FontWeight700 = "700";

			// Token: 0x04004013 RID: 16403
			internal const string FontWeight800 = "800";

			// Token: 0x04004014 RID: 16404
			internal const string FontWeight900 = "900";
		}
	}
}
