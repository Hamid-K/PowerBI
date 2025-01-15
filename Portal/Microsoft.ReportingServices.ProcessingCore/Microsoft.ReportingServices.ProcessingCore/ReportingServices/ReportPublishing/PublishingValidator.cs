using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003A7 RID: 935
	internal sealed class PublishingValidator
	{
		// Token: 0x06002619 RID: 9753 RVA: 0x000B5E1E File Offset: 0x000B401E
		private PublishingValidator()
		{
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000B5E28 File Offset: 0x000B4028
		internal static bool ValidateColor(StyleInformation.StyleInformationAttribute colorAttribute, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (colorAttribute.ValueType != Microsoft.ReportingServices.ReportIntermediateFormat.ValueType.ThemeReference)
			{
				return PublishingValidator.ValidateColor(colorAttribute.Value, objectType, objectName, propertyName, errorContext);
			}
			string stringValue = colorAttribute.Value.StringValue;
			if (string.IsNullOrEmpty(stringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidColor, Severity.Error, objectType, objectName, propertyName, new string[] { stringValue });
				return false;
			}
			return true;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000B5E84 File Offset: 0x000B4084
		internal static bool ValidateColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo color, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(color != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == color.Type)
			{
				string text;
				if (!Validator.ValidateColor(color.StringValue, out text, Validator.IsDynamicImageReportItem(objectType)))
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidColor, Severity.Error, objectType, objectName, propertyName, new string[] { color.StringValue });
					return false;
				}
				color.StringValue = text;
			}
			return true;
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000B5EE8 File Offset: 0x000B40E8
		internal static void ValidateBorderColorNotTransparent(ObjectType objectType, string objectName, Microsoft.ReportingServices.ReportIntermediateFormat.Style styleClass, string styleName, ErrorContext errorContext)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo attributeInfo;
			ReportColor reportColor;
			if (styleClass.GetAttributeInfo(styleName, out attributeInfo) && !attributeInfo.IsExpression && ReportColor.TryParse(attributeInfo.Value, true, out reportColor) && reportColor.ToColor().A != 255)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidColor, Severity.Error, objectType, objectName, styleName, new string[] { attributeInfo.Value });
			}
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000B5F50 File Offset: 0x000B4150
		internal static bool ValidateSize(string size, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			double num;
			string text;
			return PublishingValidator.ValidateSize(size, false, Validator.NegativeMin, Validator.NormalMax, objectType, objectName, propertyName, errorContext, out num, out text);
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000B5F78 File Offset: 0x000B4178
		internal static bool ValidateSize(string size, ObjectType objectType, string objectName, string propertyName, bool restrictMaxValue, ErrorContext errorContext, out double sizeInMM, out string roundSize)
		{
			bool flag = ObjectType.Line == objectType;
			return PublishingValidator.ValidateSize(size, objectType, objectName, propertyName, restrictMaxValue, flag, errorContext, out sizeInMM, out roundSize);
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000B5F9C File Offset: 0x000B419C
		internal static bool ValidateSize(string size, ObjectType objectType, string objectName, string propertyName, bool restrictMaxValue, bool allowNegative, ErrorContext errorContext, out double sizeInMM, out string roundSize)
		{
			double num = (allowNegative ? Validator.NegativeMin : Validator.NormalMin);
			double num2 = (restrictMaxValue ? Validator.NormalMax : double.MaxValue);
			return PublishingValidator.ValidateSize(size, allowNegative, num, num2, objectType, objectName, propertyName, errorContext, out sizeInMM, out roundSize);
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000B5FE4 File Offset: 0x000B41E4
		internal static bool ValidateSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo size, double minValue, double maxValue, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(size != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == size.Type)
			{
				bool flag = false;
				double num;
				string text;
				return PublishingValidator.ValidateSize(size.StringValue, flag, minValue, maxValue, objectType, objectName, propertyName, errorContext, out num, out text);
			}
			return true;
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000B6028 File Offset: 0x000B4228
		internal static bool ValidateSize(string size, bool allowNegative, double minValue, double maxValue, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext, out double validSizeInMM, out string newSize)
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
			double num = Converter.ConvertToMM(rvunit);
			if (!Validator.ValidateSizeValue(num, minValue, maxValue))
			{
				errorContext.Register(ProcessingErrorCode.rsOutOfRangeSize, Severity.Error, objectType, objectName, propertyName, new string[]
				{
					size,
					Converter.ConvertSizeFromMM(allowNegative ? minValue : Math.Max(0.0, minValue), rvunit.Type),
					Converter.ConvertSizeFromMM(maxValue, rvunit.Type)
				});
				return false;
			}
			validSizeInMM = num;
			newSize = rvunit.ToString(CultureInfo.InvariantCulture);
			return true;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x000B6152 File Offset: 0x000B4352
		internal static bool ValidateEmbeddedImageName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo embeddedImageName, Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo> embeddedImages, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(embeddedImageName != null);
			return Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant != embeddedImageName.Type || PublishingValidator.ValidateEmbeddedImageName(embeddedImageName.StringValue, embeddedImages, objectType, objectName, propertyName, errorContext);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000B617F File Offset: 0x000B437F
		internal static bool ValidateEmbeddedImageName(Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo embeddedImageName, Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo> embeddedImages, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(embeddedImageName != null);
			return embeddedImageName.IsExpression || PublishingValidator.ValidateEmbeddedImageName(embeddedImageName.Value, embeddedImages, objectType, objectName, propertyName, errorContext);
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000B61AC File Offset: 0x000B43AC
		private static bool ValidateEmbeddedImageName(string embeddedImageName, Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo> embeddedImages, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateEmbeddedImageName(embeddedImageName, embeddedImages))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidEmbeddedImageProperty, Severity.Error, objectType, objectName, propertyName, new string[] { embeddedImageName });
				return false;
			}
			return true;
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000B61E4 File Offset: 0x000B43E4
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

		// Token: 0x06002626 RID: 9766 RVA: 0x000B622C File Offset: 0x000B442C
		internal static bool ValidateLanguage(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo language, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(language != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == language.Type)
			{
				CultureInfo cultureInfo = null;
				if (!Validator.ValidateLanguage(language.StringValue, out cultureInfo))
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Error, objectType, objectName, propertyName, new string[] { language.StringValue });
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000B6284 File Offset: 0x000B4484
		internal static bool ValidateSpecificLanguage(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo language, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext, out CultureInfo culture)
		{
			culture = null;
			Global.Tracer.Assert(language != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == language.Type && !Validator.ValidateSpecificLanguage(language.StringValue, out culture))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Error, objectType, objectName, propertyName, new string[] { language.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x000B62E0 File Offset: 0x000B44E0
		internal static bool ValidateColumns(int columns, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext, int sectionNumber)
		{
			if (!Validator.ValidateColumns(columns))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidColumnsInReportSection, Severity.Error, objectType, objectName, propertyName, new string[] { sectionNumber.ToString(CultureInfo.InvariantCulture) });
				return false;
			}
			return true;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x000B6320 File Offset: 0x000B4520
		private static bool ValidateNumeralVariant(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo numeralVariant, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(numeralVariant != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == numeralVariant.Type && !Validator.ValidateNumeralVariant(numeralVariant.IntValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidNumeralVariant, Severity.Error, objectType, objectName, propertyName, new string[] { numeralVariant.IntValue.ToString(CultureInfo.InvariantCulture) });
				return false;
			}
			return true;
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000B6381 File Offset: 0x000B4581
		internal static bool ValidateMimeType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo mimeType, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (mimeType == null)
			{
				errorContext.Register(ProcessingErrorCode.rsMissingMIMEType, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
				return false;
			}
			return Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant != mimeType.Type || PublishingValidator.ValidateMimeType(mimeType.StringValue, objectType, objectName, propertyName, errorContext);
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000B63BC File Offset: 0x000B45BC
		internal static bool ValidateMimeType(string mimeType, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateMimeType(mimeType))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidMIMEType, Severity.Error, objectType, objectName, propertyName, new string[] { mimeType });
				return false;
			}
			return true;
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x000B63F0 File Offset: 0x000B45F0
		private static bool ValidateTextEffect(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo textEffect, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(textEffect != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == textEffect.Type && !Validator.ValidateTextEffect(textEffect.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidTextEffect, Severity.Error, objectType, objectName, propertyName, new string[] { textEffect.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000B6444 File Offset: 0x000B4644
		private static bool ValidateBackgroundHatchType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo backgroundHatchType, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(backgroundHatchType != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == backgroundHatchType.Type && !Validator.ValidateBackgroundHatchType(backgroundHatchType.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundHatchType, Severity.Error, objectType, objectName, propertyName, new string[] { backgroundHatchType.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x000B6498 File Offset: 0x000B4698
		private static bool ValidatePosition(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo position, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(position != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == position.Type && !Validator.ValidatePosition(position.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundImagePosition, Severity.Error, objectType, objectName, propertyName, new string[] { position.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000B64EC File Offset: 0x000B46EC
		private static bool ValidateBorderStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo borderStyle, ObjectType objectType, string objectName, bool isDynamicElementSubElement, string propertyName, bool isDefaultBorder, ErrorContext errorContext)
		{
			Global.Tracer.Assert(borderStyle != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == borderStyle.Type)
			{
				string text;
				if (!Validator.ValidateBorderStyle(borderStyle.StringValue, isDefaultBorder, objectType, isDynamicElementSubElement, out text))
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidBorderStyle, Severity.Error, objectType, objectName, propertyName, new string[] { borderStyle.StringValue });
					return false;
				}
				borderStyle.StringValue = text;
			}
			return true;
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000B6550 File Offset: 0x000B4750
		private static bool ValidateBackgroundGradientType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo repeat, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(repeat != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == repeat.Type && !Validator.ValidateBackgroundGradientType(repeat.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundGradientType, Severity.Error, objectType, objectName, propertyName, new string[] { repeat.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000B65A4 File Offset: 0x000B47A4
		private static bool ValidateBackgroundRepeat(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo repeat, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(repeat != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == repeat.Type && !Validator.ValidateBackgroundRepeat(repeat.StringValue, objectType))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundRepeat, Severity.Error, objectType, objectName, propertyName, new string[] { repeat.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000B65FC File Offset: 0x000B47FC
		private static bool ValidateTransparency(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo transparency, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(transparency != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == transparency.Type)
			{
				double floatValue = transparency.FloatValue;
				if (floatValue < 0.0 || floatValue > 100.0)
				{
					errorContext.Register(ProcessingErrorCode.rsOutOfRangeSize, Severity.Error, objectType, objectName, propertyName, new string[] { transparency.OriginalText, "0", "100" });
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000B6674 File Offset: 0x000B4874
		private static bool ValidateFontStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo fontStyle, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(fontStyle != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == fontStyle.Type && !Validator.ValidateFontStyle(fontStyle.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidFontStyle, Severity.Error, objectType, objectName, propertyName, new string[] { fontStyle.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x000B66C8 File Offset: 0x000B48C8
		private static bool ValidateFontWeight(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo fontWeight, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(fontWeight != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == fontWeight.Type && !Validator.ValidateFontWeight(fontWeight.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidFontWeight, Severity.Error, objectType, objectName, propertyName, new string[] { fontWeight.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x000B671C File Offset: 0x000B491C
		private static bool ValidateTextDecoration(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo textDecoration, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(textDecoration != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == textDecoration.Type && !Validator.ValidateTextDecoration(textDecoration.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidTextDecoration, Severity.Error, objectType, objectName, propertyName, new string[] { textDecoration.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x000B6770 File Offset: 0x000B4970
		private static bool ValidateTextAlign(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo textAlign, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(textAlign != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == textAlign.Type && !Validator.ValidateTextAlign(textAlign.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidTextAlign, Severity.Error, objectType, objectName, propertyName, new string[] { textAlign.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000B67C4 File Offset: 0x000B49C4
		private static bool ValidateVerticalAlign(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo verticalAlign, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(verticalAlign != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == verticalAlign.Type && !Validator.ValidateVerticalAlign(verticalAlign.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidVerticalAlign, Severity.Error, objectType, objectName, propertyName, new string[] { verticalAlign.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000B6818 File Offset: 0x000B4A18
		private static bool ValidateDirection(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo direction, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(direction != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == direction.Type && !Validator.ValidateDirection(direction.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidDirection, Severity.Error, objectType, objectName, propertyName, new string[] { direction.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000B686C File Offset: 0x000B4A6C
		private static bool ValidateWritingMode(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo writingMode, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(writingMode != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == writingMode.Type && !Validator.ValidateWritingMode(writingMode.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidWritingMode, Severity.Error, objectType, objectName, propertyName, new string[] { writingMode.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000B68C0 File Offset: 0x000B4AC0
		private static bool ValidateUnicodeBiDi(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo unicodeBiDi, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(unicodeBiDi != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == unicodeBiDi.Type && !Validator.ValidateUnicodeBiDi(unicodeBiDi.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidUnicodeBiDi, Severity.Error, objectType, objectName, propertyName, new string[] { unicodeBiDi.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000B6914 File Offset: 0x000B4B14
		private static bool ValidateCalendar(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo calendar, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(calendar != null);
			if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == calendar.Type && !Validator.ValidateCalendar(calendar.StringValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidCalendar, Severity.Error, objectType, objectName, propertyName, new string[] { calendar.StringValue });
				return false;
			}
			return true;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000B6968 File Offset: 0x000B4B68
		private static void ValidateBackgroundImage(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo backgroundImageSource, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo backgroundImageValue, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo backgroundImageMIMEType, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo backgroundEmbeddingMode, Microsoft.ReportingServices.ReportIntermediateFormat.Style style, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			if (backgroundImageSource != null)
			{
				bool flag = true;
				Global.Tracer.Assert(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == backgroundImageSource.Type);
				Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType intValue = (Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType)backgroundImageSource.IntValue;
				Global.Tracer.Assert(backgroundImageValue != null);
				if (Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Database == intValue && Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant == backgroundImageValue.Type)
				{
					errorContext.Register(ProcessingErrorCode.rsBinaryConstant, Severity.Error, objectType, objectName, "BackgroundImageValue", Array.Empty<string>());
					flag = false;
				}
				if (Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Database == intValue && !PublishingValidator.ValidateMimeType(backgroundImageMIMEType, objectType, objectName, "BackgroundImageMIMEType", errorContext))
				{
					flag = false;
				}
				if (flag)
				{
					style.AddAttribute("BackgroundImageSource", backgroundImageSource);
					style.AddAttribute("BackgroundImageValue", backgroundImageValue);
					if (backgroundEmbeddingMode != null)
					{
						style.AddAttribute("EmbeddingMode", backgroundEmbeddingMode);
					}
					if (Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Database == intValue)
					{
						style.AddAttribute("BackgroundImageMIMEType", backgroundImageMIMEType);
					}
				}
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000B6A24 File Offset: 0x000B4C24
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Style ValidateAndCreateStyle(List<StyleInformation.StyleInformationAttribute> attributes, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			bool flag;
			return PublishingValidator.ValidateAndCreateStyle(attributes, objectType, objectName, true, errorContext, false, out flag);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000B6A40 File Offset: 0x000B4C40
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Style ValidateAndCreateStyle(List<StyleInformation.StyleInformationAttribute> attributes, ObjectType objectType, string objectName, bool isDynamicImageSubElement, ErrorContext errorContext)
		{
			bool flag;
			return PublishingValidator.ValidateAndCreateStyle(attributes, objectType, objectName, isDynamicImageSubElement, errorContext, false, out flag);
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000B6A5B File Offset: 0x000B4C5B
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Style ValidateAndCreateStyle(List<StyleInformation.StyleInformationAttribute> attributes, ObjectType objectType, string objectName, ErrorContext errorContext, bool checkForMeDotValue, out bool meDotValueReferenced)
		{
			return PublishingValidator.ValidateAndCreateStyle(attributes, objectType, objectName, false, errorContext, checkForMeDotValue, out meDotValueReferenced);
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000B6A6C File Offset: 0x000B4C6C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Style ValidateAndCreateStyle(List<StyleInformation.StyleInformationAttribute> attributes, ObjectType objectType, string objectName, bool isDynamicImageSubElement, ErrorContext errorContext, bool checkForMeDotValue, out bool meDotValueReferenced)
		{
			meDotValueReferenced = false;
			Microsoft.ReportingServices.ReportIntermediateFormat.Style style = new Microsoft.ReportingServices.ReportIntermediateFormat.Style(Microsoft.ReportingServices.ReportIntermediateFormat.ConstructionPhase.Publishing);
			Global.Tracer.Assert(attributes != null);
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = null;
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo2 = null;
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo3 = null;
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo4 = null;
			int i = 0;
			while (i < attributes.Count)
			{
				StyleInformation.StyleInformationAttribute styleInformationAttribute = attributes[i];
				if (checkForMeDotValue && styleInformationAttribute.ValueType == Microsoft.ReportingServices.ReportIntermediateFormat.ValueType.Constant && styleInformationAttribute.Value.MeDotValueDetected)
				{
					meDotValueReferenced = true;
				}
				string name = attributes[i].Name;
				if (name == null)
				{
					goto IL_0CF2;
				}
				switch (name.Length)
				{
				case 5:
					if (!(name == "Color"))
					{
						goto IL_0CF2;
					}
					goto IL_0A61;
				case 6:
					if (!(name == "Format"))
					{
						goto IL_0CF2;
					}
					style.AddAttribute(styleInformationAttribute);
					goto IL_0CFD;
				case 7:
				case 18:
				case 24:
				case 25:
					goto IL_0CF2;
				case 8:
				{
					char c = name[0];
					if (c <= 'F')
					{
						if (c != 'C')
						{
							if (c != 'F')
							{
								goto IL_0CF2;
							}
							if (!(name == "FontSize"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateSize(styleInformationAttribute.Value, Validator.FontSizeMin, Validator.FontSizeMax, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
						else
						{
							if (!(name == "Calendar"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateCalendar(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
					}
					else if (c != 'L')
					{
						if (c != 'P')
						{
							goto IL_0CF2;
						}
						if (!(name == "Position"))
						{
							goto IL_0CF2;
						}
						if (PublishingValidator.ValidatePosition(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
						{
							style.AddAttribute(styleInformationAttribute);
							goto IL_0CFD;
						}
						goto IL_0CFD;
					}
					else
					{
						if (!(name == "Language"))
						{
							goto IL_0CF2;
						}
						CultureInfo cultureInfo;
						if (PublishingValidator.ValidateSpecificLanguage(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext, out cultureInfo))
						{
							style.AddAttribute(styleInformationAttribute);
							goto IL_0CFD;
						}
						goto IL_0CFD;
					}
					break;
				}
				case 9:
				{
					char c = name[0];
					if (c != 'D')
					{
						if (c != 'F')
						{
							if (c != 'T')
							{
								goto IL_0CF2;
							}
							if (!(name == "TextAlign"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateTextAlign(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
						else
						{
							if (!(name == "FontStyle"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateFontStyle(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
					}
					else
					{
						if (!(name == "Direction"))
						{
							goto IL_0CF2;
						}
						if (PublishingValidator.ValidateDirection(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
						{
							style.AddAttribute(styleInformationAttribute);
							goto IL_0CFD;
						}
						goto IL_0CFD;
					}
					break;
				}
				case 10:
				{
					char c = name[4];
					switch (c)
					{
					case 'E':
						if (!(name == "TextEffect"))
						{
							goto IL_0CF2;
						}
						if (PublishingValidator.ValidateTextEffect(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
						{
							style.AddAttribute(styleInformationAttribute);
							goto IL_0CFD;
						}
						goto IL_0CFD;
					case 'F':
						if (!(name == "FontFamily"))
						{
							goto IL_0CF2;
						}
						style.AddAttribute(styleInformationAttribute);
						goto IL_0CFD;
					case 'G':
						goto IL_0CF2;
					case 'H':
						if (!(name == "LineHeight"))
						{
							goto IL_0CF2;
						}
						if (PublishingValidator.ValidateSize(styleInformationAttribute.Value, Validator.LineHeightMin, Validator.LineHeightMax, objectType, objectName, styleInformationAttribute.Name, errorContext))
						{
							style.AddAttribute(styleInformationAttribute);
							goto IL_0CFD;
						}
						goto IL_0CFD;
					default:
						if (c != 'W')
						{
							if (c != 'i')
							{
								goto IL_0CF2;
							}
							if (!(name == "PaddingTop"))
							{
								goto IL_0CF2;
							}
							goto IL_0A85;
						}
						else
						{
							if (!(name == "FontWeight"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateFontWeight(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
						break;
					}
					break;
				}
				case 11:
				{
					char c = name[7];
					if (c <= 'M')
					{
						if (c != 'B')
						{
							if (c != 'L')
							{
								if (c != 'M')
								{
									goto IL_0CF2;
								}
								if (!(name == "WritingMode"))
								{
									goto IL_0CF2;
								}
								if (PublishingValidator.ValidateWritingMode(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
								{
									style.AddAttribute(styleInformationAttribute);
									goto IL_0CFD;
								}
								goto IL_0CFD;
							}
							else
							{
								if (!(name == "PaddingLeft"))
								{
									goto IL_0CF2;
								}
								goto IL_0A85;
							}
						}
						else
						{
							if (!(name == "UnicodeBiDi"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateUnicodeBiDi(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
					}
					else if (c != 'i')
					{
						if (c != 'o')
						{
							if (c != 't')
							{
								goto IL_0CF2;
							}
							if (!(name == "BorderStyle"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateBorderStyle(styleInformationAttribute.Value, objectType, objectName, isDynamicImageSubElement, "BorderStyle", true, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
						else if (!(name == "BorderColor"))
						{
							if (!(name == "ShadowColor"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateColor(styleInformationAttribute.Value, objectType, objectName, "ShadowColor", errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
					}
					else
					{
						if (!(name == "BorderWidth"))
						{
							goto IL_0CF2;
						}
						goto IL_08C2;
					}
					break;
				}
				case 12:
					switch (name[0])
					{
					case 'P':
						if (!(name == "PaddingRight"))
						{
							goto IL_0CF2;
						}
						goto IL_0A85;
					case 'Q':
					case 'R':
						goto IL_0CF2;
					case 'S':
						if (!(name == "ShadowOffset"))
						{
							goto IL_0CF2;
						}
						if (PublishingValidator.ValidateSize(styleInformationAttribute.Value, Validator.NormalMin, Validator.NormalMax, objectType, objectName, styleInformationAttribute.Name, errorContext))
						{
							style.AddAttribute(styleInformationAttribute);
							goto IL_0CFD;
						}
						goto IL_0CFD;
					case 'T':
						if (!(name == "Transparency"))
						{
							goto IL_0CF2;
						}
						if (PublishingValidator.ValidateTransparency(styleInformationAttribute.Value, objectType, objectName, "Transparency", errorContext))
						{
							style.AddAttribute(styleInformationAttribute);
							goto IL_0CFD;
						}
						goto IL_0CFD;
					default:
						goto IL_0CF2;
					}
					break;
				case 13:
				{
					char c = name[0];
					if (c != 'E')
					{
						if (c != 'P')
						{
							if (c != 'V')
							{
								goto IL_0CF2;
							}
							if (!(name == "VerticalAlign"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateVerticalAlign(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
						else
						{
							if (!(name == "PaddingBottom"))
							{
								goto IL_0CF2;
							}
							goto IL_0A85;
						}
					}
					else
					{
						if (!(name == "EmbeddingMode"))
						{
							goto IL_0CF2;
						}
						expressionInfo4 = styleInformationAttribute.Value;
						goto IL_0CFD;
					}
					break;
				}
				case 14:
				{
					char c = name[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_0CF2;
							}
							if (!(name == "BorderStyleTop"))
							{
								goto IL_0CF2;
							}
							goto IL_0899;
						}
						else if (!(name == "BorderColorTop"))
						{
							goto IL_0CF2;
						}
					}
					else if (c != 'W')
					{
						if (c != 'c')
						{
							if (c != 'l')
							{
								goto IL_0CF2;
							}
							if (!(name == "NumeralVariant"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateNumeralVariant(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
						else
						{
							if (!(name == "TextDecoration"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateTextDecoration(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
					}
					else
					{
						if (!(name == "BorderWidthTop"))
						{
							goto IL_0CF2;
						}
						goto IL_08C2;
					}
					break;
				}
				case 15:
				{
					char c = name[6];
					if (c <= 'S')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								goto IL_0CF2;
							}
							if (!(name == "BorderStyleLeft"))
							{
								goto IL_0CF2;
							}
							goto IL_0899;
						}
						else if (!(name == "BorderColorLeft"))
						{
							goto IL_0CF2;
						}
					}
					else if (c != 'W')
					{
						if (c != 'l')
						{
							if (c != 'o')
							{
								goto IL_0CF2;
							}
							if (!(name == "BackgroundColor"))
							{
								goto IL_0CF2;
							}
							goto IL_0A61;
						}
						else
						{
							if (!(name == "NumeralLanguage"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateLanguage(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
					}
					else
					{
						if (!(name == "BorderWidthLeft"))
						{
							goto IL_0CF2;
						}
						goto IL_08C2;
					}
					break;
				}
				case 16:
				{
					char c = name[6];
					if (c <= 'W')
					{
						if (c != 'C')
						{
							if (c != 'S')
							{
								if (c != 'W')
								{
									goto IL_0CF2;
								}
								if (!(name == "BorderWidthRight"))
								{
									goto IL_0CF2;
								}
								goto IL_08C2;
							}
							else
							{
								if (!(name == "BorderStyleRight"))
								{
									goto IL_0CF2;
								}
								goto IL_0899;
							}
						}
						else if (!(name == "BorderColorRight"))
						{
							goto IL_0CF2;
						}
					}
					else if (c != 'a')
					{
						if (c != 'c')
						{
							if (c != 'o')
							{
								goto IL_0CF2;
							}
							if (!(name == "BackgroundRepeat"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateBackgroundRepeat(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
						else
						{
							if (!(name == "CurrencyLanguage"))
							{
								goto IL_0CF2;
							}
							if (PublishingValidator.ValidateLanguage(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
							{
								style.AddAttribute(styleInformationAttribute);
								goto IL_0CFD;
							}
							goto IL_0CFD;
						}
					}
					else
					{
						if (!(name == "TransparentColor"))
						{
							goto IL_0CF2;
						}
						if (PublishingValidator.ValidateColor(styleInformationAttribute.Value, objectType, objectName, "TransparentColor", errorContext))
						{
							style.AddAttribute(styleInformationAttribute);
							goto IL_0CFD;
						}
						goto IL_0CFD;
					}
					break;
				}
				case 17:
				{
					char c = name[6];
					if (c != 'C')
					{
						if (c != 'S')
						{
							if (c != 'W')
							{
								goto IL_0CF2;
							}
							if (!(name == "BorderWidthBottom"))
							{
								goto IL_0CF2;
							}
							goto IL_08C2;
						}
						else
						{
							if (!(name == "BorderStyleBottom"))
							{
								goto IL_0CF2;
							}
							goto IL_0899;
						}
					}
					else if (!(name == "BorderColorBottom"))
					{
						goto IL_0CF2;
					}
					break;
				}
				case 19:
					if (!(name == "BackgroundHatchType"))
					{
						goto IL_0CF2;
					}
					if (PublishingValidator.ValidateBackgroundHatchType(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
					{
						style.AddAttribute(styleInformationAttribute);
						goto IL_0CFD;
					}
					goto IL_0CFD;
				case 20:
					if (!(name == "BackgroundImageValue"))
					{
						goto IL_0CF2;
					}
					expressionInfo2 = styleInformationAttribute.Value;
					goto IL_0CFD;
				case 21:
					if (!(name == "BackgroundImageSource"))
					{
						goto IL_0CF2;
					}
					expressionInfo = styleInformationAttribute.Value;
					goto IL_0CFD;
				case 22:
					if (!(name == "BackgroundGradientType"))
					{
						goto IL_0CF2;
					}
					if (PublishingValidator.ValidateBackgroundGradientType(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
					{
						style.AddAttribute(styleInformationAttribute);
						goto IL_0CFD;
					}
					goto IL_0CFD;
				case 23:
					if (!(name == "BackgroundImageMIMEType"))
					{
						goto IL_0CF2;
					}
					expressionInfo3 = styleInformationAttribute.Value;
					goto IL_0CFD;
				case 26:
					if (!(name == "BackgroundGradientEndColor"))
					{
						goto IL_0CF2;
					}
					if (PublishingValidator.ValidateColor(styleInformationAttribute.Value, objectType, objectName, styleInformationAttribute.Name, errorContext))
					{
						style.AddAttribute(styleInformationAttribute);
						goto IL_0CFD;
					}
					goto IL_0CFD;
				default:
					goto IL_0CF2;
				}
				if (PublishingValidator.ValidateColor(styleInformationAttribute.Value, objectType, objectName, "BorderColor", errorContext))
				{
					style.AddAttribute(styleInformationAttribute);
					goto IL_0CFD;
				}
				goto IL_0CFD;
				IL_0899:
				if (PublishingValidator.ValidateBorderStyle(styleInformationAttribute.Value, objectType, objectName, isDynamicImageSubElement, "BorderStyle", false, errorContext))
				{
					style.AddAttribute(styleInformationAttribute);
					goto IL_0CFD;
				}
				goto IL_0CFD;
				IL_08C2:
				if (PublishingValidator.ValidateSize(styleInformationAttribute.Value, Validator.BorderWidthMin, Validator.BorderWidthMax, objectType, objectName, styleInformationAttribute.Name, errorContext))
				{
					style.AddAttribute(styleInformationAttribute);
					goto IL_0CFD;
				}
				goto IL_0CFD;
				IL_0A61:
				if (PublishingValidator.ValidateColor(styleInformationAttribute, objectType, objectName, styleInformationAttribute.Name, errorContext))
				{
					style.AddAttribute(styleInformationAttribute);
					goto IL_0CFD;
				}
				goto IL_0CFD;
				IL_0A85:
				if (PublishingValidator.ValidateSize(styleInformationAttribute.Value, Validator.PaddingMin, Validator.PaddingMax, objectType, objectName, styleInformationAttribute.Name, errorContext))
				{
					style.AddAttribute(styleInformationAttribute);
				}
				IL_0CFD:
				i++;
				continue;
				IL_0CF2:
				Global.Tracer.Assert(false);
				goto IL_0CFD;
			}
			PublishingValidator.ValidateBackgroundImage(expressionInfo, expressionInfo2, expressionInfo3, expressionInfo4, style, objectType, objectName, errorContext);
			if (0 < style.StyleAttributes.Count)
			{
				return style;
			}
			return null;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000B77AC File Offset: 0x000B59AC
		internal static void ValidateCalendar(CultureInfo language, string calendar, ObjectType objectType, string ObjectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateCalendar(language, calendar))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidCalendarForLanguage, Severity.Error, objectType, ObjectName, propertyName, new string[] { calendar, language.Name });
			}
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000B77E8 File Offset: 0x000B59E8
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

		// Token: 0x06002643 RID: 9795 RVA: 0x000B7830 File Offset: 0x000B5A30
		internal static void ValidateTextRunMarkupType(string value, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateTextRunMarkupType(value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidMarkupType, Severity.Error, objectType, objectName, propertyName, new string[] { value });
			}
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x000B7860 File Offset: 0x000B5A60
		internal static void ValidateParagraphListStyle(string value, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (!Validator.ValidateParagraphListStyle(value))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidListStyle, Severity.Error, objectType, objectName, propertyName, new string[] { value });
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000B7890 File Offset: 0x000B5A90
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

		// Token: 0x04001631 RID: 5681
		private static readonly string m_invalidCharacters = ";?:@&=+$,\\*<>|\"";
	}
}
