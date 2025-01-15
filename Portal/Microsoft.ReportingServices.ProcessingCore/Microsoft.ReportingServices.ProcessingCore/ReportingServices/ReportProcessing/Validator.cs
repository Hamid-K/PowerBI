using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200077F RID: 1919
	internal sealed class Validator
	{
		// Token: 0x06006B01 RID: 27393 RVA: 0x001AFF19 File Offset: 0x001AE119
		private Validator()
		{
		}

		// Token: 0x06006B02 RID: 27394 RVA: 0x001AFF24 File Offset: 0x001AE124
		internal static bool ValidateColor(string color, out string newColor, bool allowTransparency)
		{
			if (color == null || (color.Length == 7 && color[0] == '#' && Validator.m_colorRegex.Match(color).Success) || (allowTransparency && color.Length == 9 && color[0] == '#' && Validator.m_colorRegexTransparency.Match(color).Success))
			{
				newColor = color;
				return true;
			}
			string text;
			Color color2;
			if (Validator.ValidateReportColor(color, out text, out color2, allowTransparency))
			{
				if (text == null)
				{
					newColor = color;
				}
				else
				{
					newColor = text;
				}
				return true;
			}
			newColor = null;
			return false;
		}

		// Token: 0x06006B03 RID: 27395 RVA: 0x001AFFA7 File Offset: 0x001AE1A7
		internal static bool ValidateColor(string color, out Color c)
		{
			return Validator.ValidateColor(color, out c, false);
		}

		// Token: 0x06006B04 RID: 27396 RVA: 0x001AFFB4 File Offset: 0x001AE1B4
		internal static bool ValidateColor(string color, out Color c, bool allowTransparency)
		{
			if (color == null)
			{
				c = Color.Empty;
				return true;
			}
			if ((color.Length == 7 && color[0] == '#' && Validator.m_colorRegex.Match(color).Success) || (allowTransparency && color.Length == 9 && color[0] == '#' && Validator.m_colorRegexTransparency.Match(color).Success))
			{
				Validator.ColorFromArgb(color, out c, allowTransparency);
				return true;
			}
			string text;
			if (Validator.ValidateReportColor(color, out text, out c, allowTransparency))
			{
				if (text != null)
				{
					Validator.ColorFromArgb(text, out c, allowTransparency);
				}
				return true;
			}
			c = Color.Empty;
			return false;
		}

		// Token: 0x06006B05 RID: 27397 RVA: 0x001B0050 File Offset: 0x001AE250
		internal static void ParseColor(string color, out Color c)
		{
			Validator.ParseColor(color, out c, false);
		}

		// Token: 0x06006B06 RID: 27398 RVA: 0x001B005C File Offset: 0x001AE25C
		internal static void ParseColor(string color, out Color c, bool allowTransparency)
		{
			if (color == null)
			{
				c = Color.Empty;
				return;
			}
			if ((color.Length == 7 && color[0] == '#' && Validator.m_colorRegex.Match(color).Success) || (allowTransparency && color.Length == 9 && color[0] == '#' && Validator.m_colorRegexTransparency.Match(color).Success))
			{
				Validator.ColorFromArgb(color, out c, allowTransparency);
				return;
			}
			c = Color.FromName(color);
		}

		// Token: 0x06006B07 RID: 27399 RVA: 0x001B00E0 File Offset: 0x001AE2E0
		private static void ColorFromArgb(string color, out Color c, bool allowTransparency)
		{
			try
			{
				if (!allowTransparency && color.Length != 7)
				{
					c = Color.FromArgb(0, 0, 0);
				}
				else
				{
					c = Color.FromArgb(Convert.ToInt32(color.Substring(1), 16));
					if (color.Length == 7)
					{
						c = Color.FromArgb(255, c);
					}
				}
			}
			catch
			{
				c = Color.FromArgb(0, 0, 0);
			}
		}

		// Token: 0x06006B08 RID: 27400 RVA: 0x001B0164 File Offset: 0x001AE364
		private static bool ValidateReportColor(string color, out string newColor, out Color c, bool allowTransparency)
		{
			c = Color.FromName(color);
			if (c.A == 0 && c.R == 0 && c.G == 0 && c.B == 0)
			{
				if (string.Compare("LightGrey", color, StringComparison.OrdinalIgnoreCase) == 0)
				{
					newColor = "#d3d3d3";
					return true;
				}
				newColor = null;
				return false;
			}
			else
			{
				KnownColor knownColor = c.ToKnownColor();
				if (knownColor - KnownColor.ActiveBorder <= 25)
				{
					newColor = null;
					return false;
				}
				if (knownColor != KnownColor.Transparent)
				{
					newColor = null;
					return true;
				}
				newColor = null;
				return allowTransparency;
			}
		}

		// Token: 0x06006B09 RID: 27401 RVA: 0x001B01DC File Offset: 0x001AE3DC
		internal static bool ValidateSizeString(string sizeString, out RVUnit sizeValue)
		{
			bool flag;
			try
			{
				sizeValue = RVUnit.Parse(sizeString, CultureInfo.InvariantCulture);
				if (sizeValue.Type == RVUnitType.Pixel)
				{
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			catch
			{
				sizeValue = RVUnit.Empty;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06006B0A RID: 27402 RVA: 0x001B022C File Offset: 0x001AE42C
		internal static bool ValidateSizeUnitType(RVUnit sizeValue)
		{
			switch (sizeValue.Type)
			{
			case RVUnitType.Cm:
			case RVUnitType.Inch:
			case RVUnitType.Mm:
			case RVUnitType.Pica:
			case RVUnitType.Point:
				return true;
			}
			return false;
		}

		// Token: 0x06006B0B RID: 27403 RVA: 0x001B0272 File Offset: 0x001AE472
		internal static bool ValidateSizeIsPositive(RVUnit sizeValue)
		{
			return sizeValue.Value >= 0.0;
		}

		// Token: 0x06006B0C RID: 27404 RVA: 0x001B0289 File Offset: 0x001AE489
		internal static bool ValidateSizeValue(double sizeInMM, double minValue, double maxValue)
		{
			return sizeInMM >= minValue && sizeInMM <= maxValue;
		}

		// Token: 0x06006B0D RID: 27405 RVA: 0x001B0298 File Offset: 0x001AE498
		internal static void ParseSize(string size, out double sizeInMM)
		{
			RVUnit rvunit = RVUnit.Parse(size, CultureInfo.InvariantCulture);
			sizeInMM = Converter.ConvertToMM(rvunit);
		}

		// Token: 0x06006B0E RID: 27406 RVA: 0x001B02B9 File Offset: 0x001AE4B9
		internal static bool ValidateEmbeddedImageName(string embeddedImageName, EmbeddedImageHashtable embeddedImages)
		{
			return embeddedImageName != null && embeddedImages != null && embeddedImages.ContainsKey(embeddedImageName);
		}

		// Token: 0x06006B0F RID: 27407 RVA: 0x001B02CC File Offset: 0x001AE4CC
		internal static bool ValidateSpecificLanguage(string language, out CultureInfo culture)
		{
			bool flag;
			try
			{
				culture = CultureInfo.CreateSpecificCulture(language);
				if (culture.IsNeutralCulture)
				{
					culture = null;
					flag = false;
				}
				else
				{
					culture = new CultureInfo(culture.Name, false);
					flag = true;
				}
			}
			catch (ArgumentException)
			{
				culture = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06006B10 RID: 27408 RVA: 0x001B0320 File Offset: 0x001AE520
		internal static bool ValidateLanguage(string language, out CultureInfo culture)
		{
			bool flag;
			try
			{
				culture = new CultureInfo(language, false);
				flag = true;
			}
			catch (ArgumentException)
			{
				culture = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06006B11 RID: 27409 RVA: 0x001B0354 File Offset: 0x001AE554
		internal static bool CreateCalendar(string calendarName, out Calendar calendar)
		{
			calendar = null;
			bool flag = false;
			if (Validator.CompareWithInvariantCulture(calendarName, "Gregorian Arabic"))
			{
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.Arabic);
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Gregorian Middle East French"))
			{
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.MiddleEastFrench);
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Gregorian Transliterated English"))
			{
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.TransliteratedEnglish);
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Gregorian Transliterated French"))
			{
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.TransliteratedFrench);
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Gregorian US English"))
			{
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Hebrew"))
			{
				calendar = new HebrewCalendar();
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Hijri"))
			{
				calendar = new HijriCalendar();
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Japanese"))
			{
				calendar = new JapaneseCalendar();
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Korea"))
			{
				calendar = new KoreanCalendar();
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Taiwan"))
			{
				calendar = new TaiwanCalendar();
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Thai Buddhist"))
			{
				calendar = new ThaiBuddhistCalendar();
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Gregorian"))
			{
				calendar = new GregorianCalendar();
			}
			return flag;
		}

		// Token: 0x06006B12 RID: 27410 RVA: 0x001B0494 File Offset: 0x001AE694
		internal static bool CreateCalendar(Calendars calendarType, out Calendar calendar)
		{
			calendar = null;
			bool flag = false;
			switch (calendarType)
			{
			case Calendars.Default:
			case Calendars.Gregorian:
				calendar = new GregorianCalendar();
				break;
			case Calendars.GregorianArabic:
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.Arabic);
				break;
			case Calendars.GregorianMiddleEastFrench:
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.MiddleEastFrench);
				break;
			case Calendars.GregorianTransliteratedEnglish:
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.TransliteratedEnglish);
				break;
			case Calendars.GregorianTransliteratedFrench:
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.TransliteratedFrench);
				break;
			case Calendars.GregorianUSEnglish:
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
				break;
			case Calendars.Hebrew:
				calendar = new HebrewCalendar();
				break;
			case Calendars.Hijri:
				calendar = new HijriCalendar();
				break;
			case Calendars.Japanese:
				calendar = new JapaneseCalendar();
				break;
			case Calendars.Korean:
				calendar = new KoreanCalendar();
				break;
			case Calendars.Julian:
				calendar = new JulianCalendar();
				break;
			case Calendars.Taiwan:
				calendar = new TaiwanCalendar();
				break;
			case Calendars.ThaiBuddhist:
				calendar = new ThaiBuddhistCalendar();
				break;
			}
			return flag;
		}

		// Token: 0x06006B13 RID: 27411 RVA: 0x001B0570 File Offset: 0x001AE770
		internal static bool ValidateCalendar(CultureInfo langauge, Calendars calendarType)
		{
			if (calendarType == Calendars.Gregorian)
			{
				return true;
			}
			Calendar calendar;
			bool flag = Validator.CreateCalendar(calendarType, out calendar);
			return Validator.ValidateCalendar(langauge, flag, calendar);
		}

		// Token: 0x06006B14 RID: 27412 RVA: 0x001B0594 File Offset: 0x001AE794
		internal static bool ValidateCalendar(CultureInfo langauge, string calendarName)
		{
			if (Validator.CompareWithInvariantCulture(calendarName, "Gregorian"))
			{
				return true;
			}
			Calendar calendar;
			bool flag = Validator.CreateCalendar(calendarName, out calendar);
			return Validator.ValidateCalendar(langauge, flag, calendar);
		}

		// Token: 0x06006B15 RID: 27413 RVA: 0x001B05C4 File Offset: 0x001AE7C4
		private static bool ValidateCalendar(CultureInfo langauge, bool isGregorianSubType, Calendar calendar)
		{
			if (calendar == null)
			{
				return false;
			}
			Calendar[] optionalCalendars = langauge.OptionalCalendars;
			if (optionalCalendars != null)
			{
				for (int i = 0; i < optionalCalendars.Length; i++)
				{
					if (optionalCalendars[i].GetType() == calendar.GetType())
					{
						if (!isGregorianSubType)
						{
							return true;
						}
						GregorianCalendarTypes calendarType = ((GregorianCalendar)calendar).CalendarType;
						GregorianCalendarTypes calendarType2 = ((GregorianCalendar)optionalCalendars[i]).CalendarType;
						if (calendarType == calendarType2)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06006B16 RID: 27414 RVA: 0x001B0628 File Offset: 0x001AE828
		internal static bool ValidateNumeralVariant(CultureInfo language, int numVariant)
		{
			if (numVariant < 1 || numVariant > 7)
			{
				return false;
			}
			if (numVariant < 3)
			{
				return true;
			}
			string text = language.TwoLetterISOLanguageName;
			if (text == null)
			{
				text = language.ThreeLetterISOLanguageName;
			}
			if (numVariant == 3)
			{
				if (Validator.CompareWithInvariantCulture(text, "ar") || Validator.CompareWithInvariantCulture(text, "ur") || Validator.CompareWithInvariantCulture(text, "fa") || Validator.CompareWithInvariantCulture(text, "hi") || Validator.CompareWithInvariantCulture(text, "kok") || Validator.CompareWithInvariantCulture(text, "mr") || Validator.CompareWithInvariantCulture(text, "sa") || Validator.CompareWithInvariantCulture(text, "bn") || Validator.CompareWithInvariantCulture(text, "pa") || Validator.CompareWithInvariantCulture(text, "gu") || Validator.CompareWithInvariantCulture(text, "or") || Validator.CompareWithInvariantCulture(text, "ta") || Validator.CompareWithInvariantCulture(text, "te") || Validator.CompareWithInvariantCulture(text, "kn") || Validator.CompareWithInvariantCulture(text, "ms") || Validator.CompareWithInvariantCulture(text, "th") || Validator.CompareWithInvariantCulture(text, "lo") || Validator.CompareWithInvariantCulture(text, "bo"))
				{
					return true;
				}
			}
			else if (numVariant == 7)
			{
				if (Validator.CompareWithInvariantCulture(text, "ko"))
				{
					return true;
				}
			}
			else
			{
				if (Validator.CompareWithInvariantCulture(text, "ko") || Validator.CompareWithInvariantCulture(text, "ja"))
				{
					return true;
				}
				text = language.Name;
				if (Validator.CompareWithInvariantCulture(text, "zh-CHT") || Validator.CompareWithInvariantCulture(text, "zh-CHS"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006B17 RID: 27415 RVA: 0x001B07B4 File Offset: 0x001AE9B4
		internal static bool ValidateColumns(int columns)
		{
			return columns >= 1 && columns <= 1000;
		}

		// Token: 0x06006B18 RID: 27416 RVA: 0x001B07C5 File Offset: 0x001AE9C5
		internal static bool ValidateNumeralVariant(int numeralVariant)
		{
			return numeralVariant >= 1 && numeralVariant <= 7;
		}

		// Token: 0x06006B19 RID: 27417 RVA: 0x001B07D4 File Offset: 0x001AE9D4
		internal static bool ValidateBorderStyle(string borderStyle, out string borderStyleForLine)
		{
			if (Validator.CompareWithInvariantCulture(borderStyle, "Dotted") || Validator.CompareWithInvariantCulture(borderStyle, "Dashed"))
			{
				borderStyleForLine = borderStyle;
				return true;
			}
			if (Validator.CompareWithInvariantCulture(borderStyle, "None") || Validator.CompareWithInvariantCulture(borderStyle, "Solid") || Validator.CompareWithInvariantCulture(borderStyle, "Double") || Validator.CompareWithInvariantCulture(borderStyle, "Groove") || Validator.CompareWithInvariantCulture(borderStyle, "Ridge") || Validator.CompareWithInvariantCulture(borderStyle, "Inset") || Validator.CompareWithInvariantCulture(borderStyle, "WindowInset") || Validator.CompareWithInvariantCulture(borderStyle, "Outset"))
			{
				borderStyleForLine = "Solid";
				return true;
			}
			borderStyleForLine = null;
			return false;
		}

		// Token: 0x06006B1A RID: 27418 RVA: 0x001B0878 File Offset: 0x001AEA78
		internal static bool ValidateMimeType(string mimeType)
		{
			return Validator.CompareWithInvariantCulture(mimeType, "image/bmp") || Validator.CompareWithInvariantCulture(mimeType, "image/jpeg") || Validator.CompareWithInvariantCulture(mimeType, "image/gif") || Validator.CompareWithInvariantCulture(mimeType, "image/png") || Validator.CompareWithInvariantCulture(mimeType, "image/x-png");
		}

		// Token: 0x06006B1B RID: 27419 RVA: 0x001B08CC File Offset: 0x001AEACC
		internal static bool ValidateBackgroundGradientType(string gradientType)
		{
			return Validator.CompareWithInvariantCulture(gradientType, "None") || Validator.CompareWithInvariantCulture(gradientType, "LeftRight") || Validator.CompareWithInvariantCulture(gradientType, "TopBottom") || Validator.CompareWithInvariantCulture(gradientType, "Center") || Validator.CompareWithInvariantCulture(gradientType, "DiagonalLeft") || Validator.CompareWithInvariantCulture(gradientType, "DiagonalRight") || Validator.CompareWithInvariantCulture(gradientType, "HorizontalCenter") || Validator.CompareWithInvariantCulture(gradientType, "VerticalCenter");
		}

		// Token: 0x06006B1C RID: 27420 RVA: 0x001B0944 File Offset: 0x001AEB44
		internal static bool ValidateBackgroundRepeat(string repeat)
		{
			return Validator.CompareWithInvariantCulture(repeat, "Repeat") || Validator.CompareWithInvariantCulture(repeat, "NoRepeat") || Validator.CompareWithInvariantCulture(repeat, "RepeatX") || Validator.CompareWithInvariantCulture(repeat, "RepeatY");
		}

		// Token: 0x06006B1D RID: 27421 RVA: 0x001B097D File Offset: 0x001AEB7D
		internal static bool ValidateFontStyle(string fontStyle)
		{
			return Validator.CompareWithInvariantCulture(fontStyle, "Normal") || Validator.CompareWithInvariantCulture(fontStyle, "Italic");
		}

		// Token: 0x06006B1E RID: 27422 RVA: 0x001B099C File Offset: 0x001AEB9C
		internal static bool ValidateFontWeight(string fontWeight)
		{
			return Validator.CompareWithInvariantCulture(fontWeight, "Lighter") || Validator.CompareWithInvariantCulture(fontWeight, "Normal") || Validator.CompareWithInvariantCulture(fontWeight, "Bold") || Validator.CompareWithInvariantCulture(fontWeight, "Bolder") || Validator.CompareWithInvariantCulture(fontWeight, "100") || Validator.CompareWithInvariantCulture(fontWeight, "200") || Validator.CompareWithInvariantCulture(fontWeight, "300") || Validator.CompareWithInvariantCulture(fontWeight, "400") || Validator.CompareWithInvariantCulture(fontWeight, "500") || Validator.CompareWithInvariantCulture(fontWeight, "600") || Validator.CompareWithInvariantCulture(fontWeight, "700") || Validator.CompareWithInvariantCulture(fontWeight, "800") || Validator.CompareWithInvariantCulture(fontWeight, "900");
		}

		// Token: 0x06006B1F RID: 27423 RVA: 0x001B0A5E File Offset: 0x001AEC5E
		internal static bool ValidateTextDecoration(string textDecoration)
		{
			return Validator.CompareWithInvariantCulture(textDecoration, "None") || Validator.CompareWithInvariantCulture(textDecoration, "Underline") || Validator.CompareWithInvariantCulture(textDecoration, "Overline") || Validator.CompareWithInvariantCulture(textDecoration, "LineThrough");
		}

		// Token: 0x06006B20 RID: 27424 RVA: 0x001B0A97 File Offset: 0x001AEC97
		internal static bool ValidateTextAlign(string textAlign)
		{
			return Validator.CompareWithInvariantCulture(textAlign, "General") || Validator.CompareWithInvariantCulture(textAlign, "Left") || Validator.CompareWithInvariantCulture(textAlign, "Center") || Validator.CompareWithInvariantCulture(textAlign, "Right");
		}

		// Token: 0x06006B21 RID: 27425 RVA: 0x001B0AD0 File Offset: 0x001AECD0
		internal static bool ValidateVerticalAlign(string verticalAlign)
		{
			return Validator.CompareWithInvariantCulture(verticalAlign, "Top") || Validator.CompareWithInvariantCulture(verticalAlign, "Middle") || Validator.CompareWithInvariantCulture(verticalAlign, "Bottom");
		}

		// Token: 0x06006B22 RID: 27426 RVA: 0x001B0AFC File Offset: 0x001AECFC
		internal static bool ValidateDirection(string direction)
		{
			return Validator.CompareWithInvariantCulture(direction, "LTR") || Validator.CompareWithInvariantCulture(direction, "RTL");
		}

		// Token: 0x06006B23 RID: 27427 RVA: 0x001B0B1B File Offset: 0x001AED1B
		internal static bool ValidateWritingMode(string writingMode)
		{
			return Validator.CompareWithInvariantCulture(writingMode, "lr-tb") || Validator.CompareWithInvariantCulture(writingMode, "tb-rl");
		}

		// Token: 0x06006B24 RID: 27428 RVA: 0x001B0B3A File Offset: 0x001AED3A
		internal static bool ValidateUnicodeBiDi(string unicodeBiDi)
		{
			return Validator.CompareWithInvariantCulture(unicodeBiDi, "Normal") || Validator.CompareWithInvariantCulture(unicodeBiDi, "Embed") || Validator.CompareWithInvariantCulture(unicodeBiDi, "BiDi-Override");
		}

		// Token: 0x06006B25 RID: 27429 RVA: 0x001B0B68 File Offset: 0x001AED68
		internal static bool ValidateCalendar(string calendar)
		{
			return Validator.CompareWithInvariantCulture(calendar, "Gregorian") || Validator.CompareWithInvariantCulture(calendar, "Gregorian Arabic") || Validator.CompareWithInvariantCulture(calendar, "Gregorian Middle East French") || Validator.CompareWithInvariantCulture(calendar, "Gregorian Transliterated English") || Validator.CompareWithInvariantCulture(calendar, "Gregorian Transliterated French") || Validator.CompareWithInvariantCulture(calendar, "Gregorian US English") || Validator.CompareWithInvariantCulture(calendar, "Hebrew") || Validator.CompareWithInvariantCulture(calendar, "Hijri") || Validator.CompareWithInvariantCulture(calendar, "Japanese") || Validator.CompareWithInvariantCulture(calendar, "Korea") || Validator.CompareWithInvariantCulture(calendar, "Taiwan") || Validator.CompareWithInvariantCulture(calendar, "Thai Buddhist");
		}

		// Token: 0x06006B26 RID: 27430 RVA: 0x001B0C1A File Offset: 0x001AEE1A
		internal static bool CompareWithInvariantCulture(string strOne, string strTwo)
		{
			return ReportProcessing.CompareWithInvariantCulture(strOne, strTwo, false) == 0;
		}

		// Token: 0x040035FE RID: 13822
		internal static int DecimalPrecision = 5;

		// Token: 0x040035FF RID: 13823
		internal static double NormalMin = 0.0;

		// Token: 0x04003600 RID: 13824
		internal static double NegativeMin = -Converter.Inches160;

		// Token: 0x04003601 RID: 13825
		internal static double NormalMax = Converter.Inches160;

		// Token: 0x04003602 RID: 13826
		internal static double BorderWidthMin = Converter.PtPoint25;

		// Token: 0x04003603 RID: 13827
		internal static double BorderWidthMax = Converter.Pt20;

		// Token: 0x04003604 RID: 13828
		internal static double FontSizeMin = Converter.Pt1;

		// Token: 0x04003605 RID: 13829
		internal static double FontSizeMax = Converter.Pt200;

		// Token: 0x04003606 RID: 13830
		internal static double PaddingMin = 0.0;

		// Token: 0x04003607 RID: 13831
		internal static double PaddingMax = Converter.Pt1000;

		// Token: 0x04003608 RID: 13832
		internal static double LineHeightMin = Converter.Pt1;

		// Token: 0x04003609 RID: 13833
		internal static double LineHeightMax = Converter.Pt1000;

		// Token: 0x0400360A RID: 13834
		private static Regex m_colorRegex = new Regex("^#(\\d|a|b|c|d|e|f){6}$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x0400360B RID: 13835
		private static Regex m_colorRegexTransparency = new Regex("^#(\\d|a|b|c|d|e|f){8}$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
	}
}
