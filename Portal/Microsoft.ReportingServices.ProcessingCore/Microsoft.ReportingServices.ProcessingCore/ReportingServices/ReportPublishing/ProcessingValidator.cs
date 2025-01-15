using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003A8 RID: 936
	internal sealed class ProcessingValidator
	{
		// Token: 0x06002647 RID: 9799 RVA: 0x000B79CC File Offset: 0x000B5BCC
		private ProcessingValidator()
		{
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000B79D4 File Offset: 0x000B5BD4
		internal static string ValidateColor(string color, IErrorContext errorContext, bool allowTransparency)
		{
			if (color == null)
			{
				return null;
			}
			string text;
			if (Validator.ValidateColor(color, out text, allowTransparency))
			{
				return text;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidColor, Severity.Warning, new string[] { color });
			return null;
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000B7A0A File Offset: 0x000B5C0A
		internal static string ValidateSize(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, false, errorContext);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000B7A14 File Offset: 0x000B5C14
		internal static string ValidateSize(string size, bool allowNegative, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, double.MinValue, double.MaxValue, allowNegative, errorContext);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000B7A30 File Offset: 0x000B5C30
		internal static string ValidateBorderWidth(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.BorderWidthMin, Validator.BorderWidthMax, false, errorContext);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000B7A44 File Offset: 0x000B5C44
		internal static string ValidateFontSize(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.FontSizeMin, Validator.FontSizeMax, false, errorContext);
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000B7A58 File Offset: 0x000B5C58
		internal static string ValidatePadding(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.PaddingMin, Validator.PaddingMax, false, errorContext);
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000B7A6C File Offset: 0x000B5C6C
		internal static string ValidateLineHeight(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.LineHeightMin, Validator.LineHeightMax, false, errorContext);
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000B7A80 File Offset: 0x000B5C80
		private static string ValidateSize(string size, double minValue, double maxValue, bool allowNegative, IErrorContext errorContext)
		{
			if (size == null)
			{
				return null;
			}
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
			if (!allowNegative && !Validator.ValidateSizeIsPositive(rvunit))
			{
				errorContext.Register(ProcessingErrorCode.rsNegativeSize, Severity.Warning, Array.Empty<string>());
				return null;
			}
			if (!Validator.ValidateSizeValue(Converter.ConvertToMM(rvunit), minValue, maxValue))
			{
				errorContext.Register(ProcessingErrorCode.rsOutOfRangeSize, Severity.Warning, new string[]
				{
					size,
					Converter.ConvertSizeFromMM(minValue, rvunit.Type),
					Converter.ConvertSizeFromMM(maxValue, rvunit.Type)
				});
				return null;
			}
			return size;
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000B7B52 File Offset: 0x000B5D52
		internal static string ValidateEmbeddedImageName(string embeddedImageName, Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo> embeddedImages, IErrorContext errorContext)
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

		// Token: 0x06002651 RID: 9809 RVA: 0x000B7B7C File Offset: 0x000B5D7C
		internal static string ValidateEmbeddedImageName(string embeddedImageName, Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo> embeddedImages, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
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

		// Token: 0x06002652 RID: 9810 RVA: 0x000B7BB6 File Offset: 0x000B5DB6
		internal static string ValidateLanguage(string language, IErrorContext errorContext, out CultureInfo culture)
		{
			if (Validator.ValidateLanguage(language, out culture))
			{
				return language;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Warning, new string[] { language });
			return null;
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000B7BDA File Offset: 0x000B5DDA
		internal static string ValidateSpecificLanguage(string language, IErrorContext errorContext, out CultureInfo culture)
		{
			if (Validator.ValidateSpecificLanguage(language, out culture))
			{
				return language;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Warning, new string[] { language });
			return null;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000B7BFE File Offset: 0x000B5DFE
		internal static object ValidateNumeralVariant(int numeralVariant, IErrorContext errorContext)
		{
			if (Validator.ValidateNumeralVariant(numeralVariant))
			{
				return numeralVariant;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidNumeralVariant, Severity.Warning, new string[] { numeralVariant.ToString(CultureInfo.InvariantCulture) });
			return null;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000B7C31 File Offset: 0x000B5E31
		internal static string ValidateMimeType(string mimeType, IErrorContext errorContext)
		{
			if (Validator.ValidateMimeType(mimeType))
			{
				return mimeType;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidMIMEType, Severity.Warning, new string[] { mimeType });
			return null;
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000B7C54 File Offset: 0x000B5E54
		internal static string ValidateMimeType(string mimeType, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (Validator.ValidateMimeType(mimeType))
			{
				return mimeType;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidMIMEType, Severity.Warning, objectType, objectName, propertyName, new string[] { mimeType });
			return null;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000B7C87 File Offset: 0x000B5E87
		internal static string ValidateBackgroundHatchType(string backgroundHatchType, IErrorContext errorContext)
		{
			if (Validator.ValidateBackgroundHatchType(backgroundHatchType))
			{
				return backgroundHatchType;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundHatchType, Severity.Warning, new string[] { backgroundHatchType });
			return null;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000B7CA7 File Offset: 0x000B5EA7
		internal static string ValidateTextEffect(string textEffect, IErrorContext errorContext)
		{
			if (Validator.ValidateTextEffect(textEffect))
			{
				return textEffect;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidTextEffect, Severity.Warning, new string[] { textEffect });
			return null;
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000B7CC8 File Offset: 0x000B5EC8
		internal static string ValidateBorderStyle(string borderStyle, ObjectType objectType, bool isDynamicImageSubElement, IErrorContext errorContext, bool isDefaultBorder)
		{
			string text;
			if (!Validator.ValidateBorderStyle(borderStyle, isDefaultBorder, objectType, isDynamicImageSubElement, out text))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBorderStyle, Severity.Warning, new string[] { borderStyle });
				text = null;
			}
			return text;
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000B7CFC File Offset: 0x000B5EFC
		internal static string ValidateBackgroundGradientType(string gradientType, IErrorContext errorContext)
		{
			if (Validator.ValidateBackgroundGradientType(gradientType))
			{
				return gradientType;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundGradientType, Severity.Warning, new string[] { gradientType });
			return null;
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000B7D1F File Offset: 0x000B5F1F
		internal static string ValidateFontStyle(string fontStyle, IErrorContext errorContext)
		{
			if (Validator.ValidateFontStyle(fontStyle))
			{
				return fontStyle;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidFontStyle, Severity.Warning, new string[] { fontStyle });
			return null;
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000B7D42 File Offset: 0x000B5F42
		internal static string ValidateFontWeight(string fontWeight, IErrorContext errorContext)
		{
			if (Validator.ValidateFontWeight(fontWeight))
			{
				return fontWeight;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidFontWeight, Severity.Warning, new string[] { fontWeight });
			return null;
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000B7D65 File Offset: 0x000B5F65
		internal static string ValidateTextDecoration(string textDecoration, IErrorContext errorContext)
		{
			if (Validator.ValidateTextDecoration(textDecoration))
			{
				return textDecoration;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidTextDecoration, Severity.Warning, new string[] { textDecoration });
			return null;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000B7D88 File Offset: 0x000B5F88
		internal static string ValidateTextAlign(string textAlign, IErrorContext errorContext)
		{
			if (Validator.ValidateTextAlign(textAlign))
			{
				return textAlign;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidTextAlign, Severity.Warning, new string[] { textAlign });
			return null;
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000B7DAB File Offset: 0x000B5FAB
		internal static string ValidateVerticalAlign(string verticalAlign, IErrorContext errorContext)
		{
			if (Validator.ValidateVerticalAlign(verticalAlign))
			{
				return verticalAlign;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidVerticalAlign, Severity.Warning, new string[] { verticalAlign });
			return null;
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000B7DCE File Offset: 0x000B5FCE
		internal static string ValidateDirection(string direction, IErrorContext errorContext)
		{
			if (Validator.ValidateDirection(direction))
			{
				return direction;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidDirection, Severity.Warning, new string[] { direction });
			return null;
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000B7DF1 File Offset: 0x000B5FF1
		internal static string ValidateWritingMode(string writingMode, IErrorContext errorContext)
		{
			if (Validator.ValidateWritingMode(writingMode))
			{
				return writingMode;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidWritingMode, Severity.Warning, new string[] { writingMode });
			return null;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000B7E14 File Offset: 0x000B6014
		internal static string ValidateUnicodeBiDi(string unicodeBiDi, IErrorContext errorContext)
		{
			if (Validator.ValidateUnicodeBiDi(unicodeBiDi))
			{
				return unicodeBiDi;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidUnicodeBiDi, Severity.Warning, new string[] { unicodeBiDi });
			return null;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000B7E37 File Offset: 0x000B6037
		internal static string ValidateCalendar(string calendar, IErrorContext errorContext)
		{
			if (Validator.ValidateCalendar(calendar))
			{
				return calendar;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidCalendar, Severity.Warning, new string[] { calendar });
			return null;
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000B7E5A File Offset: 0x000B605A
		internal static object ValidateCustomStyle(string styleName, object styleValue, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateCustomStyle(styleName, styleValue, ObjectType.Image, errorContext);
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000B7E68 File Offset: 0x000B6068
		internal static object ValidateCustomStyle(string styleName, object styleValue, ObjectType objectType, IErrorContext errorContext)
		{
			if (styleName != null)
			{
				switch (styleName.Length)
				{
				case 5:
					if (!(styleName == "Color"))
					{
						goto IL_0780;
					}
					goto IL_0625;
				case 6:
					if (!(styleName == "Format"))
					{
						goto IL_0780;
					}
					return styleValue as string;
				case 7:
				case 18:
				case 19:
				case 24:
				case 25:
					goto IL_0780;
				case 8:
				{
					char c = styleName[0];
					if (c != 'C')
					{
						if (c != 'F')
						{
							if (c != 'L')
							{
								goto IL_0780;
							}
							if (!(styleName == "Language"))
							{
								goto IL_0780;
							}
							CultureInfo cultureInfo;
							return ProcessingValidator.ValidateSpecificLanguage(styleValue as string, errorContext, out cultureInfo);
						}
						else
						{
							if (!(styleName == "FontSize"))
							{
								goto IL_0780;
							}
							return ProcessingValidator.ValidateSize((styleValue as ReportSize).ToString(), Validator.FontSizeMin, Validator.FontSizeMax, false, errorContext);
						}
					}
					else
					{
						if (!(styleName == "Calendar"))
						{
							goto IL_0780;
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
								goto IL_0780;
							}
							if (!(styleName == "TextAlign"))
							{
								goto IL_0780;
							}
							return ProcessingValidator.ValidateTextAlign(styleValue as string, errorContext);
						}
						else
						{
							if (!(styleName == "FontStyle"))
							{
								goto IL_0780;
							}
							return ProcessingValidator.ValidateFontStyle(styleValue as string, errorContext);
						}
					}
					else
					{
						if (!(styleName == "Direction"))
						{
							goto IL_0780;
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
								goto IL_0780;
							}
							if (!(styleName == "LineHeight"))
							{
								goto IL_0780;
							}
							return ProcessingValidator.ValidateSize((styleValue as ReportSize).ToString(), Validator.LineHeightMin, Validator.LineHeightMax, false, errorContext);
						}
						else
						{
							if (!(styleName == "FontFamily"))
							{
								goto IL_0780;
							}
							return styleValue as string;
						}
					}
					else if (c != 'W')
					{
						if (c != 'i')
						{
							goto IL_0780;
						}
						if (!(styleName == "PaddingTop"))
						{
							goto IL_0780;
						}
						goto IL_06B1;
					}
					else
					{
						if (!(styleName == "FontWeight"))
						{
							goto IL_0780;
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
									goto IL_0780;
								}
								if (!(styleName == "WritingMode"))
								{
									goto IL_0780;
								}
								return ProcessingValidator.ValidateWritingMode(styleValue as string, errorContext);
							}
							else
							{
								if (!(styleName == "PaddingLeft"))
								{
									goto IL_0780;
								}
								goto IL_06B1;
							}
						}
						else
						{
							if (!(styleName == "UnicodeBiDi"))
							{
								goto IL_0780;
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
								goto IL_0780;
							}
							if (!(styleName == "BorderStyle"))
							{
								goto IL_0780;
							}
							return ProcessingValidator.ValidateBorderStyle(styleValue as string, objectType, false, errorContext, true);
						}
						else if (!(styleName == "BorderColor"))
						{
							goto IL_0780;
						}
					}
					else
					{
						if (!(styleName == "BorderWidth"))
						{
							goto IL_0780;
						}
						goto IL_0608;
					}
					break;
				}
				case 12:
					if (!(styleName == "PaddingRight"))
					{
						goto IL_0780;
					}
					goto IL_06B1;
				case 13:
				{
					char c = styleName[0];
					if (c != 'P')
					{
						if (c != 'V')
						{
							goto IL_0780;
						}
						if (!(styleName == "VerticalAlign"))
						{
							goto IL_0780;
						}
						return ProcessingValidator.ValidateVerticalAlign(styleValue as string, errorContext);
					}
					else
					{
						if (!(styleName == "PaddingBottom"))
						{
							goto IL_0780;
						}
						goto IL_06B1;
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
								goto IL_0780;
							}
							if (!(styleName == "BorderStyleTop"))
							{
								goto IL_0780;
							}
							goto IL_05F8;
						}
						else if (!(styleName == "BorderColorTop"))
						{
							goto IL_0780;
						}
					}
					else if (c != 'W')
					{
						if (c != 'c')
						{
							if (c != 'l')
							{
								goto IL_0780;
							}
							if (!(styleName == "NumeralVariant"))
							{
								goto IL_0780;
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
								goto IL_0780;
							}
							return ProcessingValidator.ValidateTextDecoration(styleValue as string, errorContext);
						}
					}
					else
					{
						if (!(styleName == "BorderWidthTop"))
						{
							goto IL_0780;
						}
						goto IL_0608;
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
								goto IL_0780;
							}
							if (!(styleName == "BorderStyleLeft"))
							{
								goto IL_0780;
							}
							goto IL_05F8;
						}
						else if (!(styleName == "BorderColorLeft"))
						{
							goto IL_0780;
						}
					}
					else if (c != 'W')
					{
						if (c != 'l')
						{
							if (c != 'o')
							{
								goto IL_0780;
							}
							if (!(styleName == "BackgroundColor"))
							{
								goto IL_0780;
							}
							goto IL_0625;
						}
						else
						{
							if (!(styleName == "NumeralLanguage"))
							{
								goto IL_0780;
							}
							CultureInfo cultureInfo;
							return ProcessingValidator.ValidateLanguage(styleValue as string, errorContext, out cultureInfo);
						}
					}
					else
					{
						if (!(styleName == "BorderWidthLeft"))
						{
							goto IL_0780;
						}
						goto IL_0608;
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
								goto IL_0780;
							}
							if (!(styleName == "BorderStyleRight"))
							{
								goto IL_0780;
							}
							goto IL_05F8;
						}
						else if (!(styleName == "BorderColorRight"))
						{
							goto IL_0780;
						}
					}
					else if (c != 'W')
					{
						if (c != 'c')
						{
							if (c != 'o')
							{
								goto IL_0780;
							}
							if (!(styleName == "BackgroundRepeat"))
							{
								goto IL_0780;
							}
							goto IL_078B;
						}
						else
						{
							if (!(styleName == "CurrencyLanguage"))
							{
								goto IL_0780;
							}
							CultureInfo cultureInfo;
							return ProcessingValidator.ValidateLanguage(styleValue as string, errorContext, out cultureInfo);
						}
					}
					else
					{
						if (!(styleName == "BorderWidthRight"))
						{
							goto IL_0780;
						}
						goto IL_0608;
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
								goto IL_0780;
							}
							if (!(styleName == "BorderWidthBottom"))
							{
								goto IL_0780;
							}
							goto IL_0608;
						}
						else
						{
							if (!(styleName == "BorderStyleBottom"))
							{
								goto IL_0780;
							}
							goto IL_05F8;
						}
					}
					else if (!(styleName == "BorderColorBottom"))
					{
						goto IL_0780;
					}
					break;
				}
				case 20:
					if (!(styleName == "BackgroundImageValue"))
					{
						goto IL_0780;
					}
					goto IL_078B;
				case 21:
					if (!(styleName == "BackgroundImageSource"))
					{
						goto IL_0780;
					}
					goto IL_078B;
				case 22:
					if (!(styleName == "BackgroundGradientType"))
					{
						goto IL_0780;
					}
					return ProcessingValidator.ValidateBackgroundGradientType(styleValue as string, errorContext);
				case 23:
					if (!(styleName == "BackgroundImageMIMEType"))
					{
						goto IL_0780;
					}
					goto IL_078B;
				case 26:
					if (!(styleName == "BackgroundGradientEndColor"))
					{
						goto IL_0780;
					}
					goto IL_0625;
				default:
					goto IL_0780;
				}
				return ProcessingValidator.ValidateColor(styleValue as string, errorContext, Validator.IsDynamicImageReportItem(objectType));
				IL_05F8:
				return ProcessingValidator.ValidateBorderStyle(styleValue as string, objectType, false, errorContext, false);
				IL_0608:
				return ProcessingValidator.ValidateSize((styleValue as ReportSize).ToString(), Validator.BorderWidthMin, Validator.BorderWidthMax, false, errorContext);
				IL_0625:
				return ProcessingValidator.ValidateColor(styleValue as string, errorContext, Validator.IsDynamicImageReportItem(objectType));
				IL_06B1:
				return ProcessingValidator.ValidateSize((styleValue as ReportSize).ToString(), Validator.PaddingMin, Validator.PaddingMax, false, errorContext);
			}
			IL_0780:
			Global.Tracer.Assert(false);
			IL_078B:
			return null;
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000B8601 File Offset: 0x000B6801
		internal static string ValidateTextRunMarkupType(string value, IErrorContext errorContext)
		{
			if (Validator.ValidateTextRunMarkupType(value))
			{
				return value;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidMarkupType, Severity.Warning, new string[] { value });
			return "None";
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000B8628 File Offset: 0x000B6828
		internal static string ValidateParagraphListStyle(string value, IErrorContext errorContext)
		{
			if (Validator.ValidateParagraphListStyle(value))
			{
				return value;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidListStyle, Severity.Warning, new string[] { value });
			return "None";
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000B8650 File Offset: 0x000B6850
		internal static int? ValidateParagraphListLevel(int value, IErrorContext errorContext)
		{
			int? num;
			if (!Validator.ValidateParagraphListLevel(value, out num))
			{
				errorContext.Register(ProcessingErrorCode.rsOutOfRangeSize, Severity.Warning, new string[]
				{
					Convert.ToString(value, CultureInfo.InvariantCulture),
					Convert.ToString(0, CultureInfo.InvariantCulture),
					Convert.ToString(9, CultureInfo.InvariantCulture)
				});
				return num;
			}
			return new int?(value);
		}
	}
}
