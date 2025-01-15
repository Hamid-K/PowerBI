using System;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000780 RID: 1920
	internal sealed class PublishingValidator
	{
		// Token: 0x06006B28 RID: 27432 RVA: 0x001B0CD8 File Offset: 0x001AEED8
		private PublishingValidator()
		{
		}

		// Token: 0x06006B29 RID: 27433 RVA: 0x001B0CE0 File Offset: 0x001AEEE0
		private static bool ValidateColor(ExpressionInfo color, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(color != null);
			if (ExpressionInfo.Types.Constant == color.Type)
			{
				string text;
				if (!Validator.ValidateColor(color.Value, out text, objectType == ObjectType.Chart))
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidColor, Severity.Error, objectType, objectName, propertyName, new string[] { color.Value });
					return false;
				}
				color.Value = text;
			}
			return true;
		}

		// Token: 0x06006B2A RID: 27434 RVA: 0x001B0D44 File Offset: 0x001AEF44
		internal static bool ValidateSize(string size, ObjectType objectType, string objectName, string propertyName, bool restrictMaxValue, ErrorContext errorContext, out double sizeInMM, out string roundSize)
		{
			bool flag = ObjectType.Line == objectType;
			double num = (flag ? Validator.NegativeMin : Validator.NormalMin);
			double num2 = (restrictMaxValue ? Validator.NormalMax : double.MaxValue);
			return PublishingValidator.ValidateSize(size, flag, num, num2, objectType, objectName, propertyName, errorContext, out sizeInMM, out roundSize);
		}

		// Token: 0x06006B2B RID: 27435 RVA: 0x001B0D90 File Offset: 0x001AEF90
		private static bool ValidateSize(ExpressionInfo size, double minValue, double maxValue, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(size != null);
			if (ExpressionInfo.Types.Constant == size.Type)
			{
				bool flag = false;
				double num;
				string text;
				return PublishingValidator.ValidateSize(size.Value, flag, minValue, maxValue, objectType, objectName, propertyName, errorContext, out num, out text);
			}
			return true;
		}

		// Token: 0x06006B2C RID: 27436 RVA: 0x001B0DD4 File Offset: 0x001AEFD4
		private static bool ValidateSize(string size, bool allowNegative, double minValue, double maxValue, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext, out double validSizeInMM, out string newSize)
		{
			validSizeInMM = minValue;
			newSize = minValue.ToString() + "mm";
			RVUnit rvunit;
			if (!Validator.ValidateSizeString(size, out rvunit))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, objectType, objectName, propertyName, new string[] { size });
				return false;
			}
			if (!Validator.ValidateSizeUnitType(rvunit))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidMeasurementUnit, Severity.Error, objectType, objectName, propertyName, new string[] { rvunit.Type.ToString() });
				return false;
			}
			if (!allowNegative && !Validator.ValidateSizeIsPositive(rvunit))
			{
				errorContext.Register(ProcessingErrorCode.rsNegativeSize, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
				return false;
			}
			RVUnit rvunit2 = new RVUnit(Math.Round(rvunit.Value, Validator.DecimalPrecision), rvunit.Type);
			double num = Converter.ConvertToMM(rvunit2);
			if (!Validator.ValidateSizeValue(num, minValue, maxValue))
			{
				errorContext.Register(ProcessingErrorCode.rsOutOfRangeSize, Severity.Error, objectType, objectName, propertyName, new string[]
				{
					size,
					Converter.ConvertSize(minValue),
					Converter.ConvertSize(maxValue)
				});
				return false;
			}
			validSizeInMM = Math.Round(num, Validator.DecimalPrecision);
			newSize = rvunit2.ToString(CultureInfo.InvariantCulture);
			return true;
		}

		// Token: 0x06006B2D RID: 27437 RVA: 0x001B0F05 File Offset: 0x001AF105
		internal static bool ValidateEmbeddedImageName(ExpressionInfo embeddedImageName, EmbeddedImageHashtable embeddedImages, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(embeddedImageName != null);
			return ExpressionInfo.Types.Constant != embeddedImageName.Type || PublishingValidator.ValidateEmbeddedImageName(embeddedImageName.Value, embeddedImages, objectType, objectName, propertyName, errorContext);
		}

		// Token: 0x06006B2E RID: 27438 RVA: 0x001B0F32 File Offset: 0x001AF132
		internal static bool ValidateEmbeddedImageName(AttributeInfo embeddedImageName, EmbeddedImageHashtable embeddedImages, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(embeddedImageName != null);
			return embeddedImageName.IsExpression || PublishingValidator.ValidateEmbeddedImageName(embeddedImageName.Value, embeddedImages, objectType, objectName, propertyName, errorContext);
		}

		// Token: 0x06006B2F RID: 27439 RVA: 0x001B0F60 File Offset: 0x001AF160
		private static bool ValidateEmbeddedImageName(string embeddedImageName, EmbeddedImageHashtable embeddedImages, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateEmbeddedImageName(embeddedImageName, embeddedImages))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidEmbeddedImageProperty, Severity.Error, objectType, objectName, propertyName, new string[] { embeddedImageName });
				return false;
			}
			return true;
		}

		// Token: 0x06006B30 RID: 27440 RVA: 0x001B0F98 File Offset: 0x001AF198
		internal static bool ValidateLanguage(string languageValue, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext, out CultureInfo culture)
		{
			culture = null;
			Global.Tracer.Assert(languageValue != null);
			if (!Validator.ValidateLanguage(languageValue, out culture))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Error, objectType, objectName, propertyName, new string[] { languageValue });
				return false;
			}
			return true;
		}

		// Token: 0x06006B31 RID: 27441 RVA: 0x001B0FE0 File Offset: 0x001AF1E0
		internal static bool ValidateLanguage(ExpressionInfo language, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(language != null);
			if (ExpressionInfo.Types.Constant == language.Type)
			{
				CultureInfo cultureInfo = null;
				if (!Validator.ValidateLanguage(language.Value, out cultureInfo))
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Error, objectType, objectName, propertyName, new string[] { language.Value });
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006B32 RID: 27442 RVA: 0x001B1038 File Offset: 0x001AF238
		internal static bool ValidateSpecificLanguage(ExpressionInfo language, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext, out CultureInfo culture)
		{
			culture = null;
			Global.Tracer.Assert(language != null);
			if (ExpressionInfo.Types.Constant == language.Type && !Validator.ValidateSpecificLanguage(language.Value, out culture))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Error, objectType, objectName, propertyName, new string[] { language.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B33 RID: 27443 RVA: 0x001B1092 File Offset: 0x001AF292
		internal static bool ValidateColumns(int columns, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateColumns(columns))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidColumnsInBody, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
				return false;
			}
			return true;
		}

		// Token: 0x06006B34 RID: 27444 RVA: 0x001B10B4 File Offset: 0x001AF2B4
		private static bool ValidateNumeralVariant(ExpressionInfo numeralVariant, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(numeralVariant != null);
			if (ExpressionInfo.Types.Constant == numeralVariant.Type && !Validator.ValidateNumeralVariant(numeralVariant.IntValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidNumeralVariant, Severity.Error, objectType, objectName, propertyName, new string[] { numeralVariant.IntValue.ToString(CultureInfo.InvariantCulture) });
				return false;
			}
			return true;
		}

		// Token: 0x06006B35 RID: 27445 RVA: 0x001B1115 File Offset: 0x001AF315
		internal static bool ValidateMimeType(ExpressionInfo mimeType, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (mimeType == null)
			{
				errorContext.Register(ProcessingErrorCode.rsMissingMIMEType, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
				return false;
			}
			return ExpressionInfo.Types.Constant != mimeType.Type || PublishingValidator.ValidateMimeType(mimeType.Value, objectType, objectName, propertyName, errorContext);
		}

		// Token: 0x06006B36 RID: 27446 RVA: 0x001B1150 File Offset: 0x001AF350
		internal static bool ValidateMimeType(string mimeType, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateMimeType(mimeType))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidMIMEType, Severity.Error, objectType, objectName, propertyName, new string[] { mimeType });
				return false;
			}
			return true;
		}

		// Token: 0x06006B37 RID: 27447 RVA: 0x001B1184 File Offset: 0x001AF384
		private static bool ValidateBorderStyle(ExpressionInfo borderStyle, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(borderStyle != null);
			if (ExpressionInfo.Types.Constant == borderStyle.Type)
			{
				string text;
				if (!Validator.ValidateBorderStyle(borderStyle.Value, out text))
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidBorderStyle, Severity.Error, objectType, objectName, propertyName, new string[] { borderStyle.Value });
					return false;
				}
				if (ObjectType.Line == objectType)
				{
					borderStyle.Value = text;
				}
			}
			return true;
		}

		// Token: 0x06006B38 RID: 27448 RVA: 0x001B11E8 File Offset: 0x001AF3E8
		private static bool ValidateBackgroundGradientType(ExpressionInfo repeat, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(repeat != null);
			if (ExpressionInfo.Types.Constant == repeat.Type && !Validator.ValidateBackgroundGradientType(repeat.Value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundGradientType, Severity.Error, objectType, objectName, propertyName, new string[] { repeat.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B39 RID: 27449 RVA: 0x001B123C File Offset: 0x001AF43C
		private static bool ValidateBackgroundRepeat(ExpressionInfo repeat, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(repeat != null);
			if (ExpressionInfo.Types.Constant == repeat.Type && !Validator.ValidateBackgroundRepeat(repeat.Value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundRepeat, Severity.Error, objectType, objectName, propertyName, new string[] { repeat.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B3A RID: 27450 RVA: 0x001B1290 File Offset: 0x001AF490
		private static bool ValidateFontStyle(ExpressionInfo fontStyle, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(fontStyle != null);
			if (ExpressionInfo.Types.Constant == fontStyle.Type && !Validator.ValidateFontStyle(fontStyle.Value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidFontStyle, Severity.Error, objectType, objectName, propertyName, new string[] { fontStyle.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B3B RID: 27451 RVA: 0x001B12E4 File Offset: 0x001AF4E4
		private static bool ValidateFontWeight(ExpressionInfo fontWeight, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(fontWeight != null);
			if (ExpressionInfo.Types.Constant == fontWeight.Type && !Validator.ValidateFontWeight(fontWeight.Value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidFontWeight, Severity.Error, objectType, objectName, propertyName, new string[] { fontWeight.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B3C RID: 27452 RVA: 0x001B1338 File Offset: 0x001AF538
		private static bool ValidateTextDecoration(ExpressionInfo textDecoration, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(textDecoration != null);
			if (ExpressionInfo.Types.Constant == textDecoration.Type && !Validator.ValidateTextDecoration(textDecoration.Value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidTextDecoration, Severity.Error, objectType, objectName, propertyName, new string[] { textDecoration.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x001B138C File Offset: 0x001AF58C
		private static bool ValidateTextAlign(ExpressionInfo textAlign, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(textAlign != null);
			if (ExpressionInfo.Types.Constant == textAlign.Type && !Validator.ValidateTextAlign(textAlign.Value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidTextAlign, Severity.Error, objectType, objectName, propertyName, new string[] { textAlign.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B3E RID: 27454 RVA: 0x001B13E0 File Offset: 0x001AF5E0
		private static bool ValidateVerticalAlign(ExpressionInfo verticalAlign, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(verticalAlign != null);
			if (ExpressionInfo.Types.Constant == verticalAlign.Type && !Validator.ValidateVerticalAlign(verticalAlign.Value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidVerticalAlign, Severity.Error, objectType, objectName, propertyName, new string[] { verticalAlign.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B3F RID: 27455 RVA: 0x001B1434 File Offset: 0x001AF634
		private static bool ValidateDirection(ExpressionInfo direction, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(direction != null);
			if (ExpressionInfo.Types.Constant == direction.Type && !Validator.ValidateDirection(direction.Value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidDirection, Severity.Error, objectType, objectName, propertyName, new string[] { direction.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B40 RID: 27456 RVA: 0x001B1488 File Offset: 0x001AF688
		private static bool ValidateWritingMode(ExpressionInfo writingMode, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(writingMode != null);
			if (ExpressionInfo.Types.Constant == writingMode.Type && !Validator.ValidateWritingMode(writingMode.Value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidWritingMode, Severity.Error, objectType, objectName, propertyName, new string[] { writingMode.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B41 RID: 27457 RVA: 0x001B14DC File Offset: 0x001AF6DC
		private static bool ValidateUnicodeBiDi(ExpressionInfo unicodeBiDi, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(unicodeBiDi != null);
			if (ExpressionInfo.Types.Constant == unicodeBiDi.Type && !Validator.ValidateUnicodeBiDi(unicodeBiDi.Value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidUnicodeBiDi, Severity.Error, objectType, objectName, propertyName, new string[] { unicodeBiDi.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B42 RID: 27458 RVA: 0x001B1530 File Offset: 0x001AF730
		private static bool ValidateCalendar(ExpressionInfo calendar, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(calendar != null);
			if (ExpressionInfo.Types.Constant == calendar.Type && !Validator.ValidateCalendar(calendar.Value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidCalendar, Severity.Error, objectType, objectName, propertyName, new string[] { calendar.Value });
				return false;
			}
			return true;
		}

		// Token: 0x06006B43 RID: 27459 RVA: 0x001B1584 File Offset: 0x001AF784
		private static void ValidateBackgroundImage(ExpressionInfo backgroundImageSource, ExpressionInfo backgroundImageValue, ExpressionInfo backgroundImageMIMEType, Style style, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			if (backgroundImageSource != null)
			{
				bool flag = true;
				Global.Tracer.Assert(ExpressionInfo.Types.Constant == backgroundImageSource.Type);
				Image.SourceType intValue = (Image.SourceType)backgroundImageSource.IntValue;
				Global.Tracer.Assert(backgroundImageValue != null);
				if (Image.SourceType.Database == intValue && ExpressionInfo.Types.Constant == backgroundImageValue.Type)
				{
					errorContext.Register(ProcessingErrorCode.rsBinaryConstant, Severity.Error, objectType, objectName, "BackgroundImageValue", Array.Empty<string>());
					flag = false;
				}
				if (Image.SourceType.Database == intValue && !PublishingValidator.ValidateMimeType(backgroundImageMIMEType, objectType, objectName, "BackgroundImageMIMEType", errorContext))
				{
					flag = false;
				}
				if (flag)
				{
					style.AddAttribute("BackgroundImageSource", backgroundImageSource);
					style.AddAttribute("BackgroundImageValue", backgroundImageValue);
					if (Image.SourceType.Database == intValue)
					{
						style.AddAttribute("BackgroundImageMIMEType", backgroundImageMIMEType);
					}
				}
			}
		}

		// Token: 0x06006B44 RID: 27460 RVA: 0x001B1630 File Offset: 0x001AF830
		internal static Style ValidateAndCreateStyle(StringList names, ExpressionInfoList values, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			Style style = new Style(ConstructionPhase.Publishing);
			Global.Tracer.Assert(names != null);
			Global.Tracer.Assert(values != null);
			Global.Tracer.Assert(names.Count == values.Count);
			ExpressionInfo expressionInfo = null;
			ExpressionInfo expressionInfo2 = null;
			ExpressionInfo expressionInfo3 = null;
			int i = 0;
			while (i < names.Count)
			{
				string text = names[i];
				if (text == null)
				{
					goto IL_0BCF;
				}
				switch (text.Length)
				{
				case 5:
					if (!(text == "Color"))
					{
						goto IL_0BCF;
					}
					if (PublishingValidator.ValidateColor(values[i], objectType, objectName, names[i], errorContext))
					{
						style.AddAttribute(names[i], values[i]);
						goto IL_0BDA;
					}
					goto IL_0BDA;
				case 6:
					if (!(text == "Format"))
					{
						goto IL_0BCF;
					}
					style.AddAttribute(names[i], values[i]);
					goto IL_0BDA;
				case 7:
				case 18:
				case 19:
				case 24:
				case 25:
					goto IL_0BCF;
				case 8:
				{
					char c = text[0];
					if (c != 'C')
					{
						if (c != 'F')
						{
							if (c != 'L')
							{
								goto IL_0BCF;
							}
							if (!(text == "Language"))
							{
								goto IL_0BCF;
							}
							CultureInfo cultureInfo;
							if (PublishingValidator.ValidateSpecificLanguage(values[i], objectType, objectName, names[i], errorContext, out cultureInfo))
							{
								style.AddAttribute(names[i], values[i]);
								goto IL_0BDA;
							}
							goto IL_0BDA;
						}
						else
						{
							if (!(text == "FontSize"))
							{
								goto IL_0BCF;
							}
							if (PublishingValidator.ValidateSize(values[i], Validator.FontSizeMin, Validator.FontSizeMax, objectType, objectName, names[i], errorContext))
							{
								style.AddAttribute(names[i], values[i]);
								goto IL_0BDA;
							}
							goto IL_0BDA;
						}
					}
					else
					{
						if (!(text == "Calendar"))
						{
							goto IL_0BCF;
						}
						if (PublishingValidator.ValidateCalendar(values[i], objectType, objectName, names[i], errorContext))
						{
							style.AddAttribute(names[i], values[i]);
							goto IL_0BDA;
						}
						goto IL_0BDA;
					}
					break;
				}
				case 9:
				{
					char c = text[0];
					if (c != 'D')
					{
						if (c != 'F')
						{
							if (c != 'T')
							{
								goto IL_0BCF;
							}
							if (!(text == "TextAlign"))
							{
								goto IL_0BCF;
							}
							if (PublishingValidator.ValidateTextAlign(values[i], objectType, objectName, names[i], errorContext))
							{
								style.AddAttribute(names[i], values[i]);
								goto IL_0BDA;
							}
							goto IL_0BDA;
						}
						else
						{
							if (!(text == "FontStyle"))
							{
								goto IL_0BCF;
							}
							if (PublishingValidator.ValidateFontStyle(values[i], objectType, objectName, names[i], errorContext))
							{
								style.AddAttribute(names[i], values[i]);
								goto IL_0BDA;
							}
							goto IL_0BDA;
						}
					}
					else
					{
						if (!(text == "Direction"))
						{
							goto IL_0BCF;
						}
						if (PublishingValidator.ValidateDirection(values[i], objectType, objectName, names[i], errorContext))
						{
							style.AddAttribute(names[i], values[i]);
							goto IL_0BDA;
						}
						goto IL_0BDA;
					}
					break;
				}
				case 10:
				{
					char c = text[4];
					if (c <= 'H')
					{
						if (c != 'F')
						{
							if (c != 'H')
							{
								goto IL_0BCF;
							}
							if (!(text == "LineHeight"))
							{
								goto IL_0BCF;
							}
							if (PublishingValidator.ValidateSize(values[i], Validator.LineHeightMin, Validator.LineHeightMax, objectType, objectName, names[i], errorContext))
							{
								style.AddAttribute(names[i], values[i]);
								goto IL_0BDA;
							}
							goto IL_0BDA;
						}
						else
						{
							if (!(text == "FontFamily"))
							{
								goto IL_0BCF;
							}
							style.AddAttribute(names[i], values[i]);
							goto IL_0BDA;
						}
					}
					else if (c != 'W')
					{
						if (c != 'i')
						{
							goto IL_0BCF;
						}
						if (!(text == "PaddingTop"))
						{
							goto IL_0BCF;
						}
						goto IL_09C7;
					}
					else
					{
						if (!(text == "FontWeight"))
						{
							goto IL_0BCF;
						}
						if (PublishingValidator.ValidateFontWeight(values[i], objectType, objectName, names[i], errorContext))
						{
							style.AddAttribute(names[i], values[i]);
							goto IL_0BDA;
						}
						goto IL_0BDA;
					}
					break;
				}
				case 11:
				{
					char c = text[7];
					if (c <= 'M')
					{
						if (c != 'B')
						{
							if (c != 'L')
							{
								if (c != 'M')
								{
									goto IL_0BCF;
								}
								if (!(text == "WritingMode"))
								{
									goto IL_0BCF;
								}
								if (PublishingValidator.ValidateWritingMode(values[i], objectType, objectName, names[i], errorContext))
								{
									style.AddAttribute(names[i], values[i]);
									goto IL_0BDA;
								}
								goto IL_0BDA;
							}
							else
							{
								if (!(text == "PaddingLeft"))
								{
									goto IL_0BCF;
								}
								goto IL_09C7;
							}
						}
						else
						{
							if (!(text == "UnicodeBiDi"))
							{
								goto IL_0BCF;
							}
							if (PublishingValidator.ValidateUnicodeBiDi(values[i], objectType, objectName, names[i], errorContext))
							{
								style.AddAttribute(names[i], values[i]);
								goto IL_0BDA;
							}
							goto IL_0BDA;
						}
					}
					else if (c != 'i')
					{
						if (c != 'o')
						{
							if (c != 't')
							{
								goto IL_0BCF;
							}
							if (!(text == "BorderStyle"))
							{
								goto IL_0BCF;
							}
							goto IL_070D;
						}
						else if (!(text == "BorderColor"))
						{
							goto IL_0BCF;
						}
					}
					else
					{
						if (!(text == "BorderWidth"))
						{
							goto IL_0BCF;
						}
						goto IL_0743;
					}
					break;
				}
				case 12:
					if (!(text == "PaddingRight"))
					{
						goto IL_0BCF;
					}
					goto IL_09C7;
				case 13:
				{
					char c = text[0];
					if (c != 'P')
					{
						if (c != 'V')
						{
							goto IL_0BCF;
						}
						if (!(text == "VerticalAlign"))
						{
							goto IL_0BCF;
						}
						if (PublishingValidator.ValidateVerticalAlign(values[i], objectType, objectName, names[i], errorContext))
						{
							style.AddAttribute(names[i], values[i]);
							goto IL_0BDA;
						}
						goto IL_0BDA;
					}
					else
					{
						if (!(text == "PaddingBottom"))
						{
							goto IL_0BCF;
						}
						goto IL_09C7;
					}
					break;
				}
				case 14:
				{
					char c = text[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_0BCF;
							}
							if (!(text == "BorderStyleTop"))
							{
								goto IL_0BCF;
							}
							goto IL_070D;
						}
						else if (!(text == "BorderColorTop"))
						{
							goto IL_0BCF;
						}
					}
					else if (c != 'W')
					{
						if (c != 'c')
						{
							if (c != 'l')
							{
								goto IL_0BCF;
							}
							if (!(text == "NumeralVariant"))
							{
								goto IL_0BCF;
							}
							if (PublishingValidator.ValidateNumeralVariant(values[i], objectType, objectName, names[i], errorContext))
							{
								style.AddAttribute(names[i], values[i]);
								goto IL_0BDA;
							}
							goto IL_0BDA;
						}
						else
						{
							if (!(text == "TextDecoration"))
							{
								goto IL_0BCF;
							}
							if (PublishingValidator.ValidateTextDecoration(values[i], objectType, objectName, names[i], errorContext))
							{
								style.AddAttribute(names[i], values[i]);
								goto IL_0BDA;
							}
							goto IL_0BDA;
						}
					}
					else
					{
						if (!(text == "BorderWidthTop"))
						{
							goto IL_0BCF;
						}
						goto IL_0743;
					}
					break;
				}
				case 15:
				{
					char c = text[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_0BCF;
							}
							if (!(text == "BorderStyleLeft"))
							{
								goto IL_0BCF;
							}
							goto IL_070D;
						}
						else if (!(text == "BorderColorLeft"))
						{
							goto IL_0BCF;
						}
					}
					else if (c != 'W')
					{
						if (c != 'l')
						{
							if (c != 'o')
							{
								goto IL_0BCF;
							}
							if (!(text == "BackgroundColor"))
							{
								goto IL_0BCF;
							}
							goto IL_0786;
						}
						else
						{
							if (!(text == "NumeralLanguage"))
							{
								goto IL_0BCF;
							}
							if (PublishingValidator.ValidateLanguage(values[i], objectType, objectName, names[i], errorContext))
							{
								style.AddAttribute(names[i], values[i]);
								goto IL_0BDA;
							}
							goto IL_0BDA;
						}
					}
					else
					{
						if (!(text == "BorderWidthLeft"))
						{
							goto IL_0BCF;
						}
						goto IL_0743;
					}
					break;
				}
				case 16:
				{
					char c = text[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_0BCF;
							}
							if (!(text == "BorderStyleRight"))
							{
								goto IL_0BCF;
							}
							goto IL_070D;
						}
						else if (!(text == "BorderColorRight"))
						{
							goto IL_0BCF;
						}
					}
					else if (c != 'W')
					{
						if (c != 'o')
						{
							goto IL_0BCF;
						}
						if (!(text == "BackgroundRepeat"))
						{
							goto IL_0BCF;
						}
						if (PublishingValidator.ValidateBackgroundRepeat(values[i], objectType, objectName, names[i], errorContext))
						{
							style.AddAttribute(names[i], values[i]);
							goto IL_0BDA;
						}
						goto IL_0BDA;
					}
					else
					{
						if (!(text == "BorderWidthRight"))
						{
							goto IL_0BCF;
						}
						goto IL_0743;
					}
					break;
				}
				case 17:
				{
					char c = text[6];
					if (c != 'C')
					{
						if (c != 'S')
						{
							if (c != 'W')
							{
								goto IL_0BCF;
							}
							if (!(text == "BorderWidthBottom"))
							{
								goto IL_0BCF;
							}
							goto IL_0743;
						}
						else
						{
							if (!(text == "BorderStyleBottom"))
							{
								goto IL_0BCF;
							}
							goto IL_070D;
						}
					}
					else if (!(text == "BorderColorBottom"))
					{
						goto IL_0BCF;
					}
					break;
				}
				case 20:
					if (!(text == "BackgroundImageValue"))
					{
						goto IL_0BCF;
					}
					expressionInfo2 = values[i];
					goto IL_0BDA;
				case 21:
					if (!(text == "BackgroundImageSource"))
					{
						goto IL_0BCF;
					}
					expressionInfo = values[i];
					goto IL_0BDA;
				case 22:
					if (!(text == "BackgroundGradientType"))
					{
						goto IL_0BCF;
					}
					if (PublishingValidator.ValidateBackgroundGradientType(values[i], objectType, objectName, names[i], errorContext))
					{
						style.AddAttribute(names[i], values[i]);
						goto IL_0BDA;
					}
					goto IL_0BDA;
				case 23:
					if (!(text == "BackgroundImageMIMEType"))
					{
						goto IL_0BCF;
					}
					expressionInfo3 = values[i];
					goto IL_0BDA;
				case 26:
					if (!(text == "BackgroundGradientEndColor"))
					{
						goto IL_0BCF;
					}
					goto IL_0786;
				default:
					goto IL_0BCF;
				}
				if (PublishingValidator.ValidateColor(values[i], objectType, objectName, "BorderColor", errorContext))
				{
					style.AddAttribute(names[i], values[i]);
					goto IL_0BDA;
				}
				goto IL_0BDA;
				IL_070D:
				if (PublishingValidator.ValidateBorderStyle(values[i], objectType, objectName, "BorderStyle", errorContext))
				{
					style.AddAttribute(names[i], values[i]);
					goto IL_0BDA;
				}
				goto IL_0BDA;
				IL_0743:
				if (PublishingValidator.ValidateSize(values[i], Validator.BorderWidthMin, Validator.BorderWidthMax, objectType, objectName, names[i], errorContext))
				{
					style.AddAttribute(names[i], values[i]);
					goto IL_0BDA;
				}
				goto IL_0BDA;
				IL_0786:
				if (PublishingValidator.ValidateColor(values[i], objectType, objectName, names[i], errorContext))
				{
					style.AddAttribute(names[i], values[i]);
					goto IL_0BDA;
				}
				goto IL_0BDA;
				IL_09C7:
				if (PublishingValidator.ValidateSize(values[i], Validator.PaddingMin, Validator.PaddingMax, objectType, objectName, names[i], errorContext))
				{
					style.AddAttribute(names[i], values[i]);
				}
				IL_0BDA:
				i++;
				continue;
				IL_0BCF:
				Global.Tracer.Assert(false);
				goto IL_0BDA;
			}
			PublishingValidator.ValidateBackgroundImage(expressionInfo, expressionInfo2, expressionInfo3, style, objectType, objectName, errorContext);
			if (0 < style.StyleAttributes.Count)
			{
				return style;
			}
			return null;
		}

		// Token: 0x06006B45 RID: 27461 RVA: 0x001B2248 File Offset: 0x001B0448
		internal static void ValidateCalendar(CultureInfo language, string calendar, ObjectType objectType, string ObjectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateCalendar(language, calendar))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidCalendarForLanguage, Severity.Error, objectType, ObjectName, propertyName, new string[] { calendar, language.Name });
			}
		}

		// Token: 0x06006B46 RID: 27462 RVA: 0x001B2284 File Offset: 0x001B0484
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

		// Token: 0x06006B47 RID: 27463 RVA: 0x001B22CC File Offset: 0x001B04CC
		internal static string ValidateReportName(ICatalogItemContext reportContext, string reportName, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(reportName != null);
			if (reportName.StartsWith(Uri.UriSchemeHttp + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase) || reportName.StartsWith(Uri.UriSchemeHttps + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					new Uri(reportName);
					goto IL_00A5;
				}
				catch (UriFormatException)
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidReportUri, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
					return reportName;
				}
			}
			if (reportName.Length > 0 && -1 != reportName.IndexOfAny(PublishingValidator.m_invalidCharacters.ToCharArray()))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidReportNameCharacters, Severity.Error, objectType, objectName, propertyName, new string[] { PublishingValidator.m_invalidCharacters });
				return reportName;
			}
			IL_00A5:
			string text;
			try
			{
				text = reportContext.AdjustSubreportOrDrillthroughReportPath(reportName.Trim());
			}
			catch (RSException)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidReportUri, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
				return reportName;
			}
			if (text == null || reportName.Length == 0)
			{
				errorContext.Register((reportName.Length == 0) ? ProcessingErrorCode.rsInvalidReportName : ProcessingErrorCode.rsInvalidReportUri, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
				return reportName;
			}
			return text;
		}

		// Token: 0x0400360C RID: 13836
		private static readonly string m_invalidCharacters = ";?:@&=+$,\\*<>|\"";
	}
}
