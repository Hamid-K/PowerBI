using System;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200065C RID: 1628
	internal abstract class StyleTranslator
	{
		// Token: 0x06005A70 RID: 23152 RVA: 0x00172A2C File Offset: 0x00170C2C
		private static bool CompareWithInvariantCulture(string strOne, string strTwo)
		{
			return ReportProcessing.CompareWithInvariantCulture(strOne, strTwo, false) == 0;
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x00172A3C File Offset: 0x00170C3C
		internal static int TranslateStyle(StyleAttributeNames styleName, string styleString, IErrorContext errorContext, bool isChartStyle)
		{
			switch (styleName)
			{
			case StyleAttributeNames.BorderStyle:
			case StyleAttributeNames.BorderStyleTop:
			case StyleAttributeNames.BorderStyleLeft:
			case StyleAttributeNames.BorderStyleRight:
			case StyleAttributeNames.BorderStyleBottom:
				return (int)StyleTranslator.TranslateBorderStyle(styleString, errorContext);
			case StyleAttributeNames.BorderWidth:
			case StyleAttributeNames.BorderWidthTop:
			case StyleAttributeNames.BorderWidthLeft:
			case StyleAttributeNames.BorderWidthRight:
			case StyleAttributeNames.BorderWidthBottom:
			case StyleAttributeNames.BackgroundColor:
			case StyleAttributeNames.FontFamily:
			case StyleAttributeNames.FontSize:
			case StyleAttributeNames.Format:
				break;
			case StyleAttributeNames.FontStyle:
				return (int)StyleTranslator.TranslateFontStyle(styleString, errorContext);
			case StyleAttributeNames.FontWeight:
				return (int)StyleTranslator.TranslateFontWeight(styleString, errorContext);
			case StyleAttributeNames.TextDecoration:
				return (int)StyleTranslator.TranslateTextDecoration(styleString, errorContext);
			case StyleAttributeNames.TextAlign:
				return (int)StyleTranslator.TranslateTextAlign(styleString, errorContext);
			case StyleAttributeNames.VerticalAlign:
				return (int)StyleTranslator.TranslateVerticalAlign(styleString, errorContext);
			default:
				switch (styleName)
				{
				case StyleAttributeNames.Direction:
					return (int)StyleTranslator.TranslateDirection(styleString, errorContext);
				case StyleAttributeNames.WritingMode:
					return (int)StyleTranslator.TranslateWritingMode(styleString, errorContext);
				case StyleAttributeNames.UnicodeBiDi:
					return (int)StyleTranslator.TranslateUnicodeBiDi(styleString, errorContext);
				case StyleAttributeNames.Calendar:
					return (int)StyleTranslator.TranslateCalendar(styleString, errorContext);
				case StyleAttributeNames.BackgroundGradientType:
					return (int)StyleTranslator.TranslateBackgroundGradientType(styleString, errorContext);
				case StyleAttributeNames.BackgroundHatchType:
					return (int)StyleTranslator.TranslateBackgroundHatchType(styleString, errorContext, isChartStyle);
				case StyleAttributeNames.Position:
					return (int)StyleTranslator.TranslatePosition(styleString, errorContext, isChartStyle);
				case StyleAttributeNames.TextEffect:
					return (int)StyleTranslator.TranslateTextEffect(styleString, errorContext, isChartStyle);
				case StyleAttributeNames.BackgroundImageRepeat:
					return (int)StyleTranslator.TranslateBackgroundRepeat(styleString, errorContext, isChartStyle);
				}
				break;
			}
			throw new NotImplementedException("cannot translate style: " + styleName.ToString());
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x00172B84 File Offset: 0x00170D84
		internal static BorderStyles TranslateBorderStyle(string styleString, IErrorContext errorContext)
		{
			return StyleTranslator.TranslateBorderStyle(styleString, BorderStyles.Default, errorContext);
		}

		// Token: 0x06005A73 RID: 23155 RVA: 0x00172B90 File Offset: 0x00170D90
		internal static BorderStyles TranslateBorderStyle(string styleString, BorderStyles defaultStyle, IErrorContext errorContext)
		{
			if (styleString == null)
			{
				return defaultStyle;
			}
			if (StyleTranslator.CompareWithInvariantCulture("None", styleString))
			{
				return BorderStyles.None;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Solid", styleString))
			{
				return BorderStyles.Solid;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Dashed", styleString))
			{
				return BorderStyles.Dashed;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Dotted", styleString))
			{
				return BorderStyles.Dotted;
			}
			if (StyleTranslator.CompareWithInvariantCulture("DashDot", styleString))
			{
				return BorderStyles.DashDot;
			}
			if (StyleTranslator.CompareWithInvariantCulture("DashDotDot", styleString))
			{
				return BorderStyles.DashDotDot;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Double", styleString))
			{
				return BorderStyles.Double;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Groove", styleString))
			{
				return BorderStyles.Solid;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Ridge", styleString))
			{
				return BorderStyles.Solid;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Inset", styleString))
			{
				return BorderStyles.Solid;
			}
			if (StyleTranslator.CompareWithInvariantCulture("WindowInset", styleString))
			{
				return BorderStyles.Solid;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Outset", styleString))
			{
				return BorderStyles.Solid;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				return defaultStyle;
			}
			if (errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBorderStyle, Severity.Warning, new string[] { styleString });
			}
			return defaultStyle;
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x00172C80 File Offset: 0x00170E80
		internal static BackgroundGradients TranslateBackgroundGradientType(string styleString, IErrorContext errorContext)
		{
			if (styleString == null)
			{
				return Style.DefaultEnumBackgroundGradient;
			}
			if (StyleTranslator.CompareWithInvariantCulture("None", styleString))
			{
				return BackgroundGradients.None;
			}
			if (StyleTranslator.CompareWithInvariantCulture("LeftRight", styleString))
			{
				return BackgroundGradients.LeftRight;
			}
			if (StyleTranslator.CompareWithInvariantCulture("TopBottom", styleString))
			{
				return BackgroundGradients.TopBottom;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Center", styleString))
			{
				return BackgroundGradients.Center;
			}
			if (StyleTranslator.CompareWithInvariantCulture("DiagonalLeft", styleString))
			{
				return BackgroundGradients.DiagonalLeft;
			}
			if (StyleTranslator.CompareWithInvariantCulture("DiagonalRight", styleString))
			{
				return BackgroundGradients.DiagonalRight;
			}
			if (StyleTranslator.CompareWithInvariantCulture("HorizontalCenter", styleString))
			{
				return BackgroundGradients.HorizontalCenter;
			}
			if (StyleTranslator.CompareWithInvariantCulture("VerticalCenter", styleString))
			{
				return BackgroundGradients.VerticalCenter;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				return Style.DefaultEnumBackgroundGradient;
			}
			if (errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundGradientType, Severity.Warning, new string[] { styleString });
			}
			return Style.DefaultEnumBackgroundGradient;
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x00172D40 File Offset: 0x00170F40
		internal static BackgroundRepeatTypes TranslateBackgroundRepeat(string styleString, IErrorContext errorContext, bool isChartStyle)
		{
			if (styleString != null && !StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				if (StyleTranslator.CompareWithInvariantCulture("Repeat", styleString))
				{
					return BackgroundRepeatTypes.Repeat;
				}
				if (StyleTranslator.CompareWithInvariantCulture("NoRepeat", styleString))
				{
					if (isChartStyle)
					{
						return BackgroundRepeatTypes.Fit;
					}
					return BackgroundRepeatTypes.Clip;
				}
				else
				{
					if (!isChartStyle)
					{
						if (StyleTranslator.CompareWithInvariantCulture("RepeatX", styleString))
						{
							return BackgroundRepeatTypes.RepeatX;
						}
						if (StyleTranslator.CompareWithInvariantCulture("RepeatY", styleString))
						{
							return BackgroundRepeatTypes.RepeatY;
						}
					}
					else if (StyleTranslator.CompareWithInvariantCulture("Fit", styleString))
					{
						return BackgroundRepeatTypes.Fit;
					}
					if (StyleTranslator.CompareWithInvariantCulture("Clip", styleString))
					{
						return BackgroundRepeatTypes.Clip;
					}
					if (errorContext != null)
					{
						errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundRepeat, Severity.Warning, new string[] { styleString });
					}
				}
			}
			if (isChartStyle)
			{
				return BackgroundRepeatTypes.Fit;
			}
			return Style.DefaultEnumBackgroundRepeatType;
		}

		// Token: 0x06005A76 RID: 23158 RVA: 0x00172DE8 File Offset: 0x00170FE8
		internal static Positions TranslatePosition(string styleString, IErrorContext errorContext, bool isChartStyle)
		{
			if (styleString != null && !StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				if (StyleTranslator.CompareWithInvariantCulture(styleString, "Top"))
				{
					return Positions.Top;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleString, "TopLeft"))
				{
					return Positions.TopLeft;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleString, "TopRight"))
				{
					return Positions.TopRight;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleString, "Left"))
				{
					return Positions.Left;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleString, "Center"))
				{
					return Positions.Center;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleString, "Right"))
				{
					return Positions.Right;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleString, "BottomRight"))
				{
					return Positions.BottomRight;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleString, "Bottom"))
				{
					return Positions.Bottom;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleString, "BottomLeft"))
				{
					return Positions.BottomLeft;
				}
				if (errorContext != null)
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundImagePosition, Severity.Warning, new string[] { styleString });
				}
			}
			if (isChartStyle)
			{
				return Positions.TopLeft;
			}
			return Positions.Center;
		}

		// Token: 0x06005A77 RID: 23159 RVA: 0x00172EB0 File Offset: 0x001710B0
		internal static FontStyles TranslateFontStyle(string styleString, IErrorContext errorContext)
		{
			if (styleString == null)
			{
				return Style.DefaultEnumFontStyle;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Normal", styleString))
			{
				return FontStyles.Normal;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Italic", styleString))
			{
				return FontStyles.Italic;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				return Style.DefaultEnumFontStyle;
			}
			if (errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidFontStyle, Severity.Warning, new string[] { styleString });
			}
			return Style.DefaultEnumFontStyle;
		}

		// Token: 0x06005A78 RID: 23160 RVA: 0x00172F18 File Offset: 0x00171118
		internal static FontWeights TranslateFontWeight(string styleString, IErrorContext errorContext)
		{
			if (styleString == null)
			{
				return Style.DefaultEnumFontWeight;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Normal", styleString))
			{
				return FontWeights.Normal;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Bold", styleString))
			{
				return FontWeights.Bold;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Bolder", styleString))
			{
				return FontWeights.Bold;
			}
			if (StyleTranslator.CompareWithInvariantCulture("100", styleString))
			{
				return FontWeights.Thin;
			}
			if (StyleTranslator.CompareWithInvariantCulture("200", styleString))
			{
				return FontWeights.ExtraLight;
			}
			if (StyleTranslator.CompareWithInvariantCulture("300", styleString))
			{
				return FontWeights.Light;
			}
			if (StyleTranslator.CompareWithInvariantCulture("400", styleString))
			{
				return FontWeights.Normal;
			}
			if (StyleTranslator.CompareWithInvariantCulture("500", styleString))
			{
				return FontWeights.Medium;
			}
			if (StyleTranslator.CompareWithInvariantCulture("600", styleString))
			{
				return FontWeights.SemiBold;
			}
			if (StyleTranslator.CompareWithInvariantCulture("700", styleString))
			{
				return FontWeights.Bold;
			}
			if (StyleTranslator.CompareWithInvariantCulture("800", styleString))
			{
				return FontWeights.ExtraBold;
			}
			if (StyleTranslator.CompareWithInvariantCulture("900", styleString))
			{
				return FontWeights.Heavy;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Thin", styleString))
			{
				return FontWeights.Thin;
			}
			if (StyleTranslator.CompareWithInvariantCulture("ExtraLight", styleString))
			{
				return FontWeights.ExtraLight;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Light", styleString))
			{
				return FontWeights.Light;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Lighter", styleString))
			{
				return FontWeights.Light;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Medium", styleString))
			{
				return FontWeights.Medium;
			}
			if (StyleTranslator.CompareWithInvariantCulture("SemiBold", styleString))
			{
				return FontWeights.SemiBold;
			}
			if (StyleTranslator.CompareWithInvariantCulture("ExtraBold", styleString))
			{
				return FontWeights.ExtraBold;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Heavy", styleString))
			{
				return FontWeights.Heavy;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				return Style.DefaultEnumFontWeight;
			}
			if (errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidFontWeight, Severity.Warning, new string[] { styleString });
			}
			return Style.DefaultEnumFontWeight;
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x00173090 File Offset: 0x00171290
		internal static TextDecorations TranslateTextDecoration(string styleString, IErrorContext errorContext)
		{
			if (styleString == null)
			{
				return Style.DefaultEnumTextDecoration;
			}
			if (StyleTranslator.CompareWithInvariantCulture("None", styleString))
			{
				return TextDecorations.None;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Underline", styleString))
			{
				return TextDecorations.Underline;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Overline", styleString))
			{
				return TextDecorations.Overline;
			}
			if (StyleTranslator.CompareWithInvariantCulture("LineThrough", styleString))
			{
				return TextDecorations.LineThrough;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				return Style.DefaultEnumTextDecoration;
			}
			if (errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidTextDecoration, Severity.Warning, new string[] { styleString });
			}
			return Style.DefaultEnumTextDecoration;
		}

		// Token: 0x06005A7A RID: 23162 RVA: 0x00173114 File Offset: 0x00171314
		internal static TextAlignments TranslateTextAlign(string styleString, IErrorContext errorContext)
		{
			if (styleString == null)
			{
				return Style.DefaultEnumTextAlignment;
			}
			if (StyleTranslator.CompareWithInvariantCulture("General", styleString))
			{
				return TextAlignments.General;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Left", styleString))
			{
				return TextAlignments.Left;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Center", styleString))
			{
				return TextAlignments.Center;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Right", styleString))
			{
				return TextAlignments.Right;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				return Style.DefaultEnumTextAlignment;
			}
			if (errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidTextAlign, Severity.Warning, new string[] { styleString });
			}
			return Style.DefaultEnumTextAlignment;
		}

		// Token: 0x06005A7B RID: 23163 RVA: 0x00173198 File Offset: 0x00171398
		internal static VerticalAlignments TranslateVerticalAlign(string styleString, IErrorContext errorContext)
		{
			if (styleString == null)
			{
				return Style.DefaultEnumVerticalAlignment;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Top", styleString))
			{
				return VerticalAlignments.Top;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Middle", styleString))
			{
				return VerticalAlignments.Middle;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Bottom", styleString))
			{
				return VerticalAlignments.Bottom;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				return Style.DefaultEnumVerticalAlignment;
			}
			if (errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidVerticalAlign, Severity.Warning, new string[] { styleString });
			}
			return Style.DefaultEnumVerticalAlignment;
		}

		// Token: 0x06005A7C RID: 23164 RVA: 0x0017320C File Offset: 0x0017140C
		internal static Directions TranslateDirection(string styleString, IErrorContext errorContext)
		{
			if (styleString == null)
			{
				return Style.DefaultEnumDirection;
			}
			if (StyleTranslator.CompareWithInvariantCulture("LTR", styleString))
			{
				return Directions.LTR;
			}
			if (StyleTranslator.CompareWithInvariantCulture("RTL", styleString))
			{
				return Directions.RTL;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				return Style.DefaultEnumDirection;
			}
			if (errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidDirection, Severity.Warning, new string[] { styleString });
			}
			return Style.DefaultEnumDirection;
		}

		// Token: 0x06005A7D RID: 23165 RVA: 0x00173274 File Offset: 0x00171474
		internal static WritingModes TranslateWritingMode(string styleString, IErrorContext errorContext)
		{
			if (styleString == null)
			{
				return Style.DefaultEnumWritingMode;
			}
			if (StyleTranslator.CompareWithInvariantCulture("lr-tb", styleString))
			{
				return WritingModes.Horizontal;
			}
			if (StyleTranslator.CompareWithInvariantCulture("tb-rl", styleString))
			{
				return WritingModes.Vertical;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Horizontal", styleString))
			{
				return WritingModes.Horizontal;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Vertical", styleString))
			{
				return WritingModes.Vertical;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Rotate270", styleString))
			{
				return WritingModes.Rotate270;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				return Style.DefaultEnumWritingMode;
			}
			if (errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidWritingMode, Severity.Warning, new string[] { styleString });
			}
			return Style.DefaultEnumWritingMode;
		}

		// Token: 0x06005A7E RID: 23166 RVA: 0x00173308 File Offset: 0x00171508
		internal static UnicodeBiDiTypes TranslateUnicodeBiDi(string styleString, IErrorContext errorContext)
		{
			if (styleString == null)
			{
				return Style.DefaultEnumUnicodeBiDiType;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Normal", styleString))
			{
				return UnicodeBiDiTypes.Normal;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Embed", styleString))
			{
				return UnicodeBiDiTypes.Embed;
			}
			if (StyleTranslator.CompareWithInvariantCulture("BiDi-Override", styleString))
			{
				return UnicodeBiDiTypes.BiDiOverride;
			}
			if (StyleTranslator.CompareWithInvariantCulture("BiDiOverride", styleString))
			{
				return UnicodeBiDiTypes.BiDiOverride;
			}
			if (StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				return Style.DefaultEnumUnicodeBiDiType;
			}
			if (errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidUnicodeBiDi, Severity.Warning, new string[] { styleString });
			}
			return Style.DefaultEnumUnicodeBiDiType;
		}

		// Token: 0x06005A7F RID: 23167 RVA: 0x0017338C File Offset: 0x0017158C
		internal static string TranslateCalendar(Calendars calendar)
		{
			switch (calendar)
			{
			case Calendars.Default:
				return "Default";
			case Calendars.Gregorian:
				return "Gregorian";
			case Calendars.GregorianArabic:
				return "Gregorian Arabic";
			case Calendars.GregorianMiddleEastFrench:
				return "Gregorian Middle East French";
			case Calendars.GregorianTransliteratedEnglish:
				return "Gregorian Transliterated English";
			case Calendars.GregorianTransliteratedFrench:
				return "Gregorian Transliterated French";
			case Calendars.GregorianUSEnglish:
				return "Gregorian US English";
			case Calendars.Hebrew:
				return "Hebrew";
			case Calendars.Hijri:
				return "Hijri";
			case Calendars.Japanese:
				return "Japanese";
			case Calendars.Korean:
				return "Korean";
			case Calendars.Julian:
				return "Julian";
			case Calendars.Taiwan:
				return "Taiwan";
			case Calendars.ThaiBuddhist:
				return "Thai Buddhist";
			default:
				return "Default";
			}
		}

		// Token: 0x06005A80 RID: 23168 RVA: 0x00173434 File Offset: 0x00171634
		internal static Calendars TranslateCalendar(string styleString, IErrorContext errorContext)
		{
			if (styleString != null && !StyleTranslator.CompareWithInvariantCulture("Default", styleString))
			{
				if (StyleTranslator.CompareWithInvariantCulture("Gregorian", styleString))
				{
					return Calendars.Gregorian;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Gregorian Arabic", styleString))
				{
					return Calendars.GregorianArabic;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Gregorian Middle East French", styleString))
				{
					return Calendars.GregorianMiddleEastFrench;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Gregorian Transliterated English", styleString))
				{
					return Calendars.GregorianTransliteratedEnglish;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Gregorian Transliterated French", styleString))
				{
					return Calendars.GregorianTransliteratedFrench;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Gregorian US English", styleString))
				{
					return Calendars.GregorianUSEnglish;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Hebrew", styleString))
				{
					return Calendars.Hebrew;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Hijri", styleString))
				{
					return Calendars.Hijri;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Japanese", styleString))
				{
					return Calendars.Japanese;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Korea", styleString))
				{
					return Calendars.Korean;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Korean", styleString))
				{
					return Calendars.Korean;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Taiwan", styleString))
				{
					return Calendars.Taiwan;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Thai Buddhist", styleString))
				{
					return Calendars.ThaiBuddhist;
				}
				if (StyleTranslator.CompareWithInvariantCulture("Julian", styleString))
				{
					return Calendars.Julian;
				}
				if (errorContext != null)
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidCalendar, Severity.Warning, new string[] { styleString });
				}
			}
			return Style.DefaultEnumCalendar;
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x00173550 File Offset: 0x00171750
		internal static BackgroundHatchTypes TranslateBackgroundHatchType(string styleValue, IErrorContext errorContext, bool isChartStyle)
		{
			if (styleValue != null)
			{
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "None"))
				{
					return BackgroundHatchTypes.None;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Default"))
				{
					return BackgroundHatchTypes.None;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "BackwardDiagonal"))
				{
					return BackgroundHatchTypes.BackwardDiagonal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Cross"))
				{
					return BackgroundHatchTypes.Cross;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DarkDownwardDiagonal"))
				{
					return BackgroundHatchTypes.DarkDownwardDiagonal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DarkHorizontal"))
				{
					return BackgroundHatchTypes.DarkHorizontal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DarkUpwardDiagonal"))
				{
					return BackgroundHatchTypes.DarkUpwardDiagonal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DarkVertical"))
				{
					return BackgroundHatchTypes.DarkVertical;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DashedDownwardDiagonal"))
				{
					return BackgroundHatchTypes.DashedDownwardDiagonal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DashedHorizontal"))
				{
					return BackgroundHatchTypes.DashedHorizontal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DashedUpwardDiagonal"))
				{
					return BackgroundHatchTypes.DashedUpwardDiagonal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DashedVertical"))
				{
					return BackgroundHatchTypes.DashedVertical;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DiagonalBrick"))
				{
					return BackgroundHatchTypes.DiagonalBrick;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DiagonalCross"))
				{
					return BackgroundHatchTypes.DiagonalCross;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Divot"))
				{
					return BackgroundHatchTypes.Divot;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DottedDiamond"))
				{
					return BackgroundHatchTypes.DottedDiamond;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "DottedGrid"))
				{
					return BackgroundHatchTypes.DottedGrid;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "ForwardDiagonal"))
				{
					return BackgroundHatchTypes.ForwardDiagonal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Horizontal"))
				{
					return BackgroundHatchTypes.Horizontal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "HorizontalBrick"))
				{
					return BackgroundHatchTypes.HorizontalBrick;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "LargeCheckerBoard"))
				{
					return BackgroundHatchTypes.LargeCheckerBoard;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "LargeConfetti"))
				{
					return BackgroundHatchTypes.LargeConfetti;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "LargeGrid"))
				{
					return BackgroundHatchTypes.LargeGrid;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "LightDownwardDiagonal"))
				{
					return BackgroundHatchTypes.LightDownwardDiagonal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "LightHorizontal"))
				{
					return BackgroundHatchTypes.LightHorizontal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "LightUpwardDiagonal"))
				{
					return BackgroundHatchTypes.LightUpwardDiagonal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "LightVertical"))
				{
					return BackgroundHatchTypes.LightVertical;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "NarrowHorizontal"))
				{
					return BackgroundHatchTypes.NarrowHorizontal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "NarrowVertical"))
				{
					return BackgroundHatchTypes.NarrowVertical;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "OutlinedDiamond"))
				{
					return BackgroundHatchTypes.OutlinedDiamond;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent05"))
				{
					return BackgroundHatchTypes.Percent05;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent10"))
				{
					return BackgroundHatchTypes.Percent10;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent20"))
				{
					return BackgroundHatchTypes.Percent20;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent25"))
				{
					return BackgroundHatchTypes.Percent25;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent30"))
				{
					return BackgroundHatchTypes.Percent30;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent40"))
				{
					return BackgroundHatchTypes.Percent40;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent50"))
				{
					return BackgroundHatchTypes.Percent50;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent60"))
				{
					return BackgroundHatchTypes.Percent60;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent70"))
				{
					return BackgroundHatchTypes.Percent70;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent75"))
				{
					return BackgroundHatchTypes.Percent75;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent80"))
				{
					return BackgroundHatchTypes.Percent80;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Percent90"))
				{
					return BackgroundHatchTypes.Percent90;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Plaid"))
				{
					return BackgroundHatchTypes.Plaid;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Shingle"))
				{
					return BackgroundHatchTypes.Shingle;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "SmallCheckerBoard"))
				{
					return BackgroundHatchTypes.SmallCheckerBoard;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "SmallConfetti"))
				{
					return BackgroundHatchTypes.SmallConfetti;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "SmallGrid"))
				{
					return BackgroundHatchTypes.SmallGrid;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "SolidDiamond"))
				{
					return BackgroundHatchTypes.SolidDiamond;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Sphere"))
				{
					return BackgroundHatchTypes.Sphere;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Trellis"))
				{
					return BackgroundHatchTypes.Trellis;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Vertical"))
				{
					return BackgroundHatchTypes.Vertical;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Wave"))
				{
					return BackgroundHatchTypes.Wave;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Weave"))
				{
					return BackgroundHatchTypes.Weave;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "WideDownwardDiagonal"))
				{
					return BackgroundHatchTypes.WideDownwardDiagonal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "WideUpwardDiagonal"))
				{
					return BackgroundHatchTypes.WideUpwardDiagonal;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "ZigZag"))
				{
					return BackgroundHatchTypes.ZigZag;
				}
			}
			else if (styleValue != null && errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundHatchType, Severity.Warning, new string[] { styleValue });
			}
			return BackgroundHatchTypes.None;
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x001738F4 File Offset: 0x00171AF4
		internal static TextEffects TranslateTextEffect(string styleValue, IErrorContext errorContext, bool isChartStyle)
		{
			if (styleValue != null && !StyleTranslator.CompareWithInvariantCulture(styleValue, "Default"))
			{
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "None"))
				{
					return TextEffects.None;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Shadow"))
				{
					return TextEffects.Shadow;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Emboss"))
				{
					return TextEffects.Emboss;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Embed"))
				{
					return TextEffects.Embed;
				}
				if (StyleTranslator.CompareWithInvariantCulture(styleValue, "Frame"))
				{
					return TextEffects.Frame;
				}
			}
			if (styleValue != null && errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidTextEffect, Severity.Warning, new string[] { styleValue });
			}
			if (isChartStyle)
			{
				return TextEffects.Shadow;
			}
			return TextEffects.None;
		}
	}
}
