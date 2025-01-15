using System;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000781 RID: 1921
	internal sealed class ProcessingValidator
	{
		// Token: 0x06006B49 RID: 27465 RVA: 0x001B2408 File Offset: 0x001B0608
		private ProcessingValidator()
		{
		}

		// Token: 0x06006B4A RID: 27466 RVA: 0x001B2410 File Offset: 0x001B0610
		internal static string ValidateColor(string color, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateColor(color, errorContext, false);
		}

		// Token: 0x06006B4B RID: 27467 RVA: 0x001B241C File Offset: 0x001B061C
		internal static string ValidateColor(string color, IErrorContext errorContext, bool allowTransparency)
		{
			string text;
			if (Validator.ValidateColor(color, out text, allowTransparency))
			{
				return text;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidColor, Severity.Warning, new string[] { color });
			return null;
		}

		// Token: 0x06006B4C RID: 27468 RVA: 0x001B244D File Offset: 0x001B064D
		internal static string ValidateBorderWidth(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.BorderWidthMin, Validator.BorderWidthMax, errorContext);
		}

		// Token: 0x06006B4D RID: 27469 RVA: 0x001B2460 File Offset: 0x001B0660
		internal static string ValidateFontSize(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.FontSizeMin, Validator.FontSizeMax, errorContext);
		}

		// Token: 0x06006B4E RID: 27470 RVA: 0x001B2473 File Offset: 0x001B0673
		internal static string ValidatePadding(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.PaddingMin, Validator.PaddingMax, errorContext);
		}

		// Token: 0x06006B4F RID: 27471 RVA: 0x001B2486 File Offset: 0x001B0686
		internal static string ValidateLineHeight(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.LineHeightMin, Validator.LineHeightMax, errorContext);
		}

		// Token: 0x06006B50 RID: 27472 RVA: 0x001B249C File Offset: 0x001B069C
		private static string ValidateSize(string size, double minValue, double maxValue, IErrorContext errorContext)
		{
			RVUnit rvunit;
			if (!Validator.ValidateSizeString(size, out rvunit))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Warning, new string[] { size });
				return null;
			}
			if (!Validator.ValidateSizeUnitType(rvunit))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidMeasurementUnit, Severity.Warning, new string[] { rvunit.Type.ToString() });
				return null;
			}
			if (!Validator.ValidateSizeIsPositive(rvunit))
			{
				errorContext.Register(ProcessingErrorCode.rsNegativeSize, Severity.Warning, Array.Empty<string>());
				return null;
			}
			if (!Validator.ValidateSizeValue(Converter.ConvertToMM(rvunit), minValue, maxValue))
			{
				errorContext.Register(ProcessingErrorCode.rsOutOfRangeSize, Severity.Warning, new string[]
				{
					size,
					Converter.ConvertSize(minValue),
					Converter.ConvertSize(maxValue)
				});
				return null;
			}
			return size;
		}

		// Token: 0x06006B51 RID: 27473 RVA: 0x001B2554 File Offset: 0x001B0754
		internal static string ValidateEmbeddedImageName(string embeddedImageName, EmbeddedImageHashtable embeddedImages, IErrorContext errorContext)
		{
			if (embeddedImageName == null)
			{
				return null;
			}
			if (!Validator.ValidateEmbeddedImageName(embeddedImageName, embeddedImages))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidEmbeddedImageProperty, Severity.Warning, new string[] { embeddedImageName });
				return null;
			}
			return embeddedImageName;
		}

		// Token: 0x06006B52 RID: 27474 RVA: 0x001B2580 File Offset: 0x001B0780
		internal static string ValidateEmbeddedImageName(string embeddedImageName, EmbeddedImageHashtable embeddedImages, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (embeddedImageName == null)
			{
				return null;
			}
			if (!Validator.ValidateEmbeddedImageName(embeddedImageName, embeddedImages))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidEmbeddedImageProperty, Severity.Warning, objectType, objectName, propertyName, new string[] { embeddedImageName });
				return null;
			}
			return embeddedImageName;
		}

		// Token: 0x06006B53 RID: 27475 RVA: 0x001B25BA File Offset: 0x001B07BA
		internal static string ValidateLanguage(string language, IErrorContext errorContext, out CultureInfo culture)
		{
			if (Validator.ValidateLanguage(language, out culture))
			{
				return language;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Warning, new string[] { language });
			return null;
		}

		// Token: 0x06006B54 RID: 27476 RVA: 0x001B25DE File Offset: 0x001B07DE
		internal static string ValidateSpecificLanguage(string language, IErrorContext errorContext, out CultureInfo culture)
		{
			if (Validator.ValidateSpecificLanguage(language, out culture))
			{
				return language;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Warning, new string[] { language });
			return null;
		}

		// Token: 0x06006B55 RID: 27477 RVA: 0x001B2604 File Offset: 0x001B0804
		internal static Calendar CreateCalendar(Calendars calendarType)
		{
			Calendar calendar;
			Validator.CreateCalendar(calendarType, out calendar);
			return calendar;
		}

		// Token: 0x06006B56 RID: 27478 RVA: 0x001B261C File Offset: 0x001B081C
		internal static Calendar CreateCalendar(string calendarName)
		{
			Calendar calendar;
			Validator.CreateCalendar(calendarName, out calendar);
			return calendar;
		}

		// Token: 0x06006B57 RID: 27479 RVA: 0x001B2634 File Offset: 0x001B0834
		internal static bool ValidateCalendar(CultureInfo language, Calendars calendarType, ObjectType objectType, string ObjectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateCalendar(language, calendarType))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidCalendarForLanguage, Severity.Error, objectType, ObjectName, propertyName, new string[]
				{
					calendarType.ToString(),
					language.Name
				});
				return false;
			}
			return true;
		}

		// Token: 0x06006B58 RID: 27480 RVA: 0x001B2680 File Offset: 0x001B0880
		internal static bool ValidateCalendar(CultureInfo language, string calendarName, ObjectType objectType, string ObjectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateCalendar(language, calendarName))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidCalendarForLanguage, Severity.Error, objectType, ObjectName, propertyName, new string[] { calendarName, language.Name });
				return false;
			}
			return true;
		}

		// Token: 0x06006B59 RID: 27481 RVA: 0x001B26C0 File Offset: 0x001B08C0
		internal static void ValidateNumeralVariant(CultureInfo language, int numVariant, ObjectType objectType, string ObjectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateNumeralVariant(language, numVariant))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidNumeralVariantForLanguage, Severity.Error, objectType, ObjectName, propertyName, new string[]
				{
					numVariant.ToString(CultureInfo.InvariantCulture),
					language.Name
				});
			}
		}

		// Token: 0x06006B5A RID: 27482 RVA: 0x001B2706 File Offset: 0x001B0906
		internal static object ValidateNumeralVariant(int numeralVariant, IErrorContext errorContext)
		{
			if (Validator.ValidateNumeralVariant(numeralVariant))
			{
				return numeralVariant;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidNumeralVariant, Severity.Warning, new string[] { numeralVariant.ToString(CultureInfo.InvariantCulture) });
			return null;
		}

		// Token: 0x06006B5B RID: 27483 RVA: 0x001B2739 File Offset: 0x001B0939
		internal static string ValidateMimeType(string mimeType, IErrorContext errorContext)
		{
			if (Validator.ValidateMimeType(mimeType))
			{
				return mimeType;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidMIMEType, Severity.Warning, new string[] { mimeType });
			return null;
		}

		// Token: 0x06006B5C RID: 27484 RVA: 0x001B275C File Offset: 0x001B095C
		internal static string ValidateMimeType(string mimeType, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (Validator.ValidateMimeType(mimeType))
			{
				return mimeType;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidMIMEType, Severity.Warning, objectType, objectName, propertyName, new string[] { mimeType });
			return null;
		}

		// Token: 0x06006B5D RID: 27485 RVA: 0x001B2790 File Offset: 0x001B0990
		internal static string ValidateBorderStyle(string borderStyle, ObjectType objectType, IErrorContext errorContext)
		{
			string text;
			if (!Validator.ValidateBorderStyle(borderStyle, out text))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBorderStyle, Severity.Warning, new string[] { borderStyle });
				return null;
			}
			if (ObjectType.Line == objectType)
			{
				return text;
			}
			return borderStyle;
		}

		// Token: 0x06006B5E RID: 27486 RVA: 0x001B27C6 File Offset: 0x001B09C6
		internal static string ValidateBackgroundGradientType(string gradientType, IErrorContext errorContext)
		{
			if (Validator.ValidateBackgroundGradientType(gradientType))
			{
				return gradientType;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundGradientType, Severity.Warning, new string[] { gradientType });
			return null;
		}

		// Token: 0x06006B5F RID: 27487 RVA: 0x001B27E9 File Offset: 0x001B09E9
		internal static string ValidateBackgroundRepeat(string repeat, IErrorContext errorContext)
		{
			if (Validator.ValidateBackgroundRepeat(repeat))
			{
				return repeat;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundRepeat, Severity.Warning, new string[] { repeat });
			return null;
		}

		// Token: 0x06006B60 RID: 27488 RVA: 0x001B280C File Offset: 0x001B0A0C
		internal static string ValidateFontStyle(string fontStyle, IErrorContext errorContext)
		{
			if (Validator.ValidateFontStyle(fontStyle))
			{
				return fontStyle;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidFontStyle, Severity.Warning, new string[] { fontStyle });
			return null;
		}

		// Token: 0x06006B61 RID: 27489 RVA: 0x001B282F File Offset: 0x001B0A2F
		internal static string ValidateFontWeight(string fontWeight, IErrorContext errorContext)
		{
			if (Validator.ValidateFontWeight(fontWeight))
			{
				return fontWeight;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidFontWeight, Severity.Warning, new string[] { fontWeight });
			return null;
		}

		// Token: 0x06006B62 RID: 27490 RVA: 0x001B2852 File Offset: 0x001B0A52
		internal static string ValidateTextDecoration(string textDecoration, IErrorContext errorContext)
		{
			if (Validator.ValidateTextDecoration(textDecoration))
			{
				return textDecoration;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidTextDecoration, Severity.Warning, new string[] { textDecoration });
			return null;
		}

		// Token: 0x06006B63 RID: 27491 RVA: 0x001B2875 File Offset: 0x001B0A75
		internal static string ValidateTextAlign(string textAlign, IErrorContext errorContext)
		{
			if (Validator.ValidateTextAlign(textAlign))
			{
				return textAlign;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidTextAlign, Severity.Warning, new string[] { textAlign });
			return null;
		}

		// Token: 0x06006B64 RID: 27492 RVA: 0x001B2898 File Offset: 0x001B0A98
		internal static string ValidateVerticalAlign(string verticalAlign, IErrorContext errorContext)
		{
			if (Validator.ValidateVerticalAlign(verticalAlign))
			{
				return verticalAlign;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidVerticalAlign, Severity.Warning, new string[] { verticalAlign });
			return null;
		}

		// Token: 0x06006B65 RID: 27493 RVA: 0x001B28BB File Offset: 0x001B0ABB
		internal static string ValidateDirection(string direction, IErrorContext errorContext)
		{
			if (Validator.ValidateDirection(direction))
			{
				return direction;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidDirection, Severity.Warning, new string[] { direction });
			return null;
		}

		// Token: 0x06006B66 RID: 27494 RVA: 0x001B28DE File Offset: 0x001B0ADE
		internal static string ValidateWritingMode(string writingMode, IErrorContext errorContext)
		{
			if (Validator.ValidateWritingMode(writingMode))
			{
				return writingMode;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidWritingMode, Severity.Warning, new string[] { writingMode });
			return null;
		}

		// Token: 0x06006B67 RID: 27495 RVA: 0x001B2901 File Offset: 0x001B0B01
		internal static string ValidateUnicodeBiDi(string unicodeBiDi, IErrorContext errorContext)
		{
			if (Validator.ValidateUnicodeBiDi(unicodeBiDi))
			{
				return unicodeBiDi;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidUnicodeBiDi, Severity.Warning, new string[] { unicodeBiDi });
			return null;
		}

		// Token: 0x06006B68 RID: 27496 RVA: 0x001B2924 File Offset: 0x001B0B24
		internal static string ValidateCalendar(string calendar, IErrorContext errorContext)
		{
			if (Validator.ValidateCalendar(calendar))
			{
				return calendar;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidCalendar, Severity.Warning, new string[] { calendar });
			return null;
		}

		// Token: 0x06006B69 RID: 27497 RVA: 0x001B2947 File Offset: 0x001B0B47
		internal static object ValidateCustomStyle(string styleName, object styleValue, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateCustomStyle(styleName, styleValue, ObjectType.Image, errorContext);
		}

		// Token: 0x06006B6A RID: 27498 RVA: 0x001B2954 File Offset: 0x001B0B54
		internal static object ValidateCustomStyle(string styleName, object styleValue, ObjectType objectType, IErrorContext errorContext)
		{
			if (styleName != null)
			{
				switch (styleName.Length)
				{
				case 5:
					if (!(styleName == "Color"))
					{
						goto IL_073C;
					}
					goto IL_05F4;
				case 6:
					if (!(styleName == "Format"))
					{
						goto IL_073C;
					}
					return styleValue as string;
				case 7:
				case 18:
				case 19:
				case 24:
				case 25:
					goto IL_073C;
				case 8:
				{
					char c = styleName[0];
					if (c != 'C')
					{
						if (c != 'F')
						{
							if (c != 'L')
							{
								goto IL_073C;
							}
							if (!(styleName == "Language"))
							{
								goto IL_073C;
							}
							CultureInfo cultureInfo;
							return ProcessingValidator.ValidateSpecificLanguage(styleValue as string, errorContext, out cultureInfo);
						}
						else
						{
							if (!(styleName == "FontSize"))
							{
								goto IL_073C;
							}
							return ProcessingValidator.ValidateSize((styleValue as Microsoft.ReportingServices.ReportRendering.ReportSize).ToString(), Validator.FontSizeMin, Validator.FontSizeMax, errorContext);
						}
					}
					else
					{
						if (!(styleName == "Calendar"))
						{
							goto IL_073C;
						}
						return ProcessingValidator.ValidateCalendar(styleValue as string, errorContext);
					}
					break;
				}
				case 9:
				{
					char c = styleName[0];
					if (c != 'D')
					{
						if (c != 'F')
						{
							if (c != 'T')
							{
								goto IL_073C;
							}
							if (!(styleName == "TextAlign"))
							{
								goto IL_073C;
							}
							return ProcessingValidator.ValidateTextAlign(styleValue as string, errorContext);
						}
						else
						{
							if (!(styleName == "FontStyle"))
							{
								goto IL_073C;
							}
							return ProcessingValidator.ValidateFontStyle(styleValue as string, errorContext);
						}
					}
					else
					{
						if (!(styleName == "Direction"))
						{
							goto IL_073C;
						}
						return ProcessingValidator.ValidateDirection(styleValue as string, errorContext);
					}
					break;
				}
				case 10:
				{
					char c = styleName[4];
					if (c <= 'H')
					{
						if (c != 'F')
						{
							if (c != 'H')
							{
								goto IL_073C;
							}
							if (!(styleName == "LineHeight"))
							{
								goto IL_073C;
							}
							return ProcessingValidator.ValidateSize((styleValue as Microsoft.ReportingServices.ReportRendering.ReportSize).ToString(), Validator.LineHeightMin, Validator.LineHeightMax, errorContext);
						}
						else
						{
							if (!(styleName == "FontFamily"))
							{
								goto IL_073C;
							}
							return styleValue as string;
						}
					}
					else if (c != 'W')
					{
						if (c != 'i')
						{
							goto IL_073C;
						}
						if (!(styleName == "PaddingTop"))
						{
							goto IL_073C;
						}
						goto IL_067E;
					}
					else
					{
						if (!(styleName == "FontWeight"))
						{
							goto IL_073C;
						}
						return ProcessingValidator.ValidateFontWeight(styleValue as string, errorContext);
					}
					break;
				}
				case 11:
				{
					char c = styleName[7];
					if (c <= 'M')
					{
						if (c != 'B')
						{
							if (c != 'L')
							{
								if (c != 'M')
								{
									goto IL_073C;
								}
								if (!(styleName == "WritingMode"))
								{
									goto IL_073C;
								}
								return ProcessingValidator.ValidateWritingMode(styleValue as string, errorContext);
							}
							else
							{
								if (!(styleName == "PaddingLeft"))
								{
									goto IL_073C;
								}
								goto IL_067E;
							}
						}
						else
						{
							if (!(styleName == "UnicodeBiDi"))
							{
								goto IL_073C;
							}
							return ProcessingValidator.ValidateUnicodeBiDi(styleValue as string, errorContext);
						}
					}
					else if (c != 'i')
					{
						if (c != 'o')
						{
							if (c != 't')
							{
								goto IL_073C;
							}
							if (!(styleName == "BorderStyle"))
							{
								goto IL_073C;
							}
							goto IL_05CA;
						}
						else if (!(styleName == "BorderColor"))
						{
							goto IL_073C;
						}
					}
					else
					{
						if (!(styleName == "BorderWidth"))
						{
							goto IL_073C;
						}
						goto IL_05D8;
					}
					break;
				}
				case 12:
					if (!(styleName == "PaddingRight"))
					{
						goto IL_073C;
					}
					goto IL_067E;
				case 13:
				{
					char c = styleName[0];
					if (c != 'P')
					{
						if (c != 'V')
						{
							goto IL_073C;
						}
						if (!(styleName == "VerticalAlign"))
						{
							goto IL_073C;
						}
						return ProcessingValidator.ValidateVerticalAlign(styleValue as string, errorContext);
					}
					else
					{
						if (!(styleName == "PaddingBottom"))
						{
							goto IL_073C;
						}
						goto IL_067E;
					}
					break;
				}
				case 14:
				{
					char c = styleName[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_073C;
							}
							if (!(styleName == "BorderStyleTop"))
							{
								goto IL_073C;
							}
							goto IL_05CA;
						}
						else if (!(styleName == "BorderColorTop"))
						{
							goto IL_073C;
						}
					}
					else if (c != 'W')
					{
						if (c != 'c')
						{
							if (c != 'l')
							{
								goto IL_073C;
							}
							if (!(styleName == "NumeralVariant"))
							{
								goto IL_073C;
							}
							int num;
							if (int.TryParse(styleValue as string, out num))
							{
								return ProcessingValidator.ValidateNumeralVariant(num, errorContext);
							}
							errorContext.Register(ProcessingErrorCode.rsInvalidNumeralVariant, Severity.Warning, new string[] { styleValue as string });
							return null;
						}
						else
						{
							if (!(styleName == "TextDecoration"))
							{
								goto IL_073C;
							}
							return ProcessingValidator.ValidateTextDecoration(styleValue as string, errorContext);
						}
					}
					else
					{
						if (!(styleName == "BorderWidthTop"))
						{
							goto IL_073C;
						}
						goto IL_05D8;
					}
					break;
				}
				case 15:
				{
					char c = styleName[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_073C;
							}
							if (!(styleName == "BorderStyleLeft"))
							{
								goto IL_073C;
							}
							goto IL_05CA;
						}
						else if (!(styleName == "BorderColorLeft"))
						{
							goto IL_073C;
						}
					}
					else if (c != 'W')
					{
						if (c != 'l')
						{
							if (c != 'o')
							{
								goto IL_073C;
							}
							if (!(styleName == "BackgroundColor"))
							{
								goto IL_073C;
							}
							goto IL_05F4;
						}
						else
						{
							if (!(styleName == "NumeralLanguage"))
							{
								goto IL_073C;
							}
							CultureInfo cultureInfo;
							return ProcessingValidator.ValidateLanguage(styleValue as string, errorContext, out cultureInfo);
						}
					}
					else
					{
						if (!(styleName == "BorderWidthLeft"))
						{
							goto IL_073C;
						}
						goto IL_05D8;
					}
					break;
				}
				case 16:
				{
					char c = styleName[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_073C;
							}
							if (!(styleName == "BorderStyleRight"))
							{
								goto IL_073C;
							}
							goto IL_05CA;
						}
						else if (!(styleName == "BorderColorRight"))
						{
							goto IL_073C;
						}
					}
					else if (c != 'W')
					{
						if (c != 'o')
						{
							goto IL_073C;
						}
						if (!(styleName == "BackgroundRepeat"))
						{
							goto IL_073C;
						}
						goto IL_0747;
					}
					else
					{
						if (!(styleName == "BorderWidthRight"))
						{
							goto IL_073C;
						}
						goto IL_05D8;
					}
					break;
				}
				case 17:
				{
					char c = styleName[6];
					if (c != 'C')
					{
						if (c != 'S')
						{
							if (c != 'W')
							{
								goto IL_073C;
							}
							if (!(styleName == "BorderWidthBottom"))
							{
								goto IL_073C;
							}
							goto IL_05D8;
						}
						else
						{
							if (!(styleName == "BorderStyleBottom"))
							{
								goto IL_073C;
							}
							goto IL_05CA;
						}
					}
					else if (!(styleName == "BorderColorBottom"))
					{
						goto IL_073C;
					}
					break;
				}
				case 20:
					if (!(styleName == "BackgroundImageValue"))
					{
						goto IL_073C;
					}
					goto IL_0747;
				case 21:
					if (!(styleName == "BackgroundImageSource"))
					{
						goto IL_073C;
					}
					goto IL_0747;
				case 22:
					if (!(styleName == "BackgroundGradientType"))
					{
						goto IL_073C;
					}
					return ProcessingValidator.ValidateBackgroundGradientType(styleValue as string, errorContext);
				case 23:
					if (!(styleName == "BackgroundImageMIMEType"))
					{
						goto IL_073C;
					}
					goto IL_0747;
				case 26:
					if (!(styleName == "BackgroundGradientEndColor"))
					{
						goto IL_073C;
					}
					goto IL_05F4;
				default:
					goto IL_073C;
				}
				return ProcessingValidator.ValidateColor(styleValue as string, errorContext, objectType == ObjectType.Chart);
				IL_05CA:
				return ProcessingValidator.ValidateBorderStyle(styleValue as string, objectType, errorContext);
				IL_05D8:
				return ProcessingValidator.ValidateSize((styleValue as Microsoft.ReportingServices.ReportRendering.ReportSize).ToString(), Validator.BorderWidthMin, Validator.BorderWidthMax, errorContext);
				IL_05F4:
				return ProcessingValidator.ValidateColor(styleValue as string, errorContext, objectType == ObjectType.Chart);
				IL_067E:
				return ProcessingValidator.ValidateSize((styleValue as Microsoft.ReportingServices.ReportRendering.ReportSize).ToString(), Validator.PaddingMin, Validator.PaddingMax, errorContext);
			}
			IL_073C:
			Global.Tracer.Assert(false);
			IL_0747:
			return null;
		}
	}
}
