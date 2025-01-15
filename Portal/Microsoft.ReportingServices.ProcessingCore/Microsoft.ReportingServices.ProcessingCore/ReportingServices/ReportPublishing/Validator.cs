using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000389 RID: 905
	internal sealed class Validator
	{
		// Token: 0x06002486 RID: 9350 RVA: 0x000ADBE0 File Offset: 0x000ABDE0
		internal static bool ValidateGaugeAntiAliasings(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "All") || Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Text") || Validator.CompareWithInvariantCulture(val, "Graphics"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000ADC2D File Offset: 0x000ABE2D
		internal static bool ValidateGaugeBarStarts(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "ScaleStart") || Validator.CompareWithInvariantCulture(val, "Zero"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000ADC58 File Offset: 0x000ABE58
		internal static bool ValidateGaugeCapStyles(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "RoundedDark") || Validator.CompareWithInvariantCulture(val, "Rounded") || Validator.CompareWithInvariantCulture(val, "RoundedLight") || Validator.CompareWithInvariantCulture(val, "RoundedWithAdditionalTop") || Validator.CompareWithInvariantCulture(val, "RoundedWithWideIndentation") || Validator.CompareWithInvariantCulture(val, "FlattenedWithIndentation") || Validator.CompareWithInvariantCulture(val, "FlattenedWithWideIndentation") || Validator.CompareWithInvariantCulture(val, "RoundedGlossyWithIndentation") || Validator.CompareWithInvariantCulture(val, "RoundedWithIndentation"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000ADCE8 File Offset: 0x000ABEE8
		internal static bool ValidateGaugeFrameShapes(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Default") || Validator.CompareWithInvariantCulture(val, "Circular") || Validator.CompareWithInvariantCulture(val, "Rectangular") || Validator.CompareWithInvariantCulture(val, "RoundedRectangular") || Validator.CompareWithInvariantCulture(val, "AutoShape") || Validator.CompareWithInvariantCulture(val, "CustomCircular1") || Validator.CompareWithInvariantCulture(val, "CustomCircular2") || Validator.CompareWithInvariantCulture(val, "CustomCircular3") || Validator.CompareWithInvariantCulture(val, "CustomCircular4") || Validator.CompareWithInvariantCulture(val, "CustomCircular5") || Validator.CompareWithInvariantCulture(val, "CustomCircular6") || Validator.CompareWithInvariantCulture(val, "CustomCircular7") || Validator.CompareWithInvariantCulture(val, "CustomCircular8") || Validator.CompareWithInvariantCulture(val, "CustomCircular9") || Validator.CompareWithInvariantCulture(val, "CustomCircular10") || Validator.CompareWithInvariantCulture(val, "CustomCircular11") || Validator.CompareWithInvariantCulture(val, "CustomCircular12") || Validator.CompareWithInvariantCulture(val, "CustomCircular13") || Validator.CompareWithInvariantCulture(val, "CustomCircular14") || Validator.CompareWithInvariantCulture(val, "CustomCircular15") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularN1") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularN2") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularN3") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularN4") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularS1") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularS2") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularS3") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularS4") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularE1") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularE2") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularE3") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularE4") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularW1") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularW2") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularW3") || Validator.CompareWithInvariantCulture(val, "CustomSemiCircularW4") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularNE1") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularNE2") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularNE3") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularNE4") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularNW1") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularNW2") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularNW3") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularNW4") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularSE1") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularSE2") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularSE3") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularSE4") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularSW1") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularSW2") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularSW3") || Validator.CompareWithInvariantCulture(val, "CustomQuarterCircularSW4"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000AE023 File Offset: 0x000AC223
		internal static bool ValidateGaugeFrameStyles(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Simple") || Validator.CompareWithInvariantCulture(val, "Edged"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000AE058 File Offset: 0x000AC258
		internal static bool ValidateGaugeGlassEffects(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Simple"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000AE080 File Offset: 0x000AC280
		internal static bool ValidateGaugeInputValueFormulas(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Average") || Validator.CompareWithInvariantCulture(val, "Linear") || Validator.CompareWithInvariantCulture(val, "Max") || Validator.CompareWithInvariantCulture(val, "Min") || Validator.CompareWithInvariantCulture(val, "Median") || Validator.CompareWithInvariantCulture(val, "OpenClose") || Validator.CompareWithInvariantCulture(val, "Percentile") || Validator.CompareWithInvariantCulture(val, "Variance") || Validator.CompareWithInvariantCulture(val, "RateOfChange") || Validator.CompareWithInvariantCulture(val, "Integral"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000AE12B File Offset: 0x000AC32B
		internal static bool ValidateGaugeLabelPlacements(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Inside") || Validator.CompareWithInvariantCulture(val, "Outside") || Validator.CompareWithInvariantCulture(val, "Cross"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000AE160 File Offset: 0x000AC360
		internal static bool ValidateGaugeMarkerStyles(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Triangle") || Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Rectangle") || Validator.CompareWithInvariantCulture(val, "Circle") || Validator.CompareWithInvariantCulture(val, "Diamond") || Validator.CompareWithInvariantCulture(val, "Trapezoid") || Validator.CompareWithInvariantCulture(val, "Star") || Validator.CompareWithInvariantCulture(val, "Wedge") || Validator.CompareWithInvariantCulture(val, "Pentagon"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000AE1EE File Offset: 0x000AC3EE
		internal static bool ValidateGaugeOrientations(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Auto") || Validator.CompareWithInvariantCulture(val, "Horizontal") || Validator.CompareWithInvariantCulture(val, "Vertical"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000AE223 File Offset: 0x000AC423
		internal static bool ValidateGaugePointerPlacements(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Cross") || Validator.CompareWithInvariantCulture(val, "Outside") || Validator.CompareWithInvariantCulture(val, "Inside"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000AE258 File Offset: 0x000AC458
		internal static bool ValidateGaugeThermometerStyles(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Standard") || Validator.CompareWithInvariantCulture(val, "Flask"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000AE280 File Offset: 0x000AC480
		internal static bool ValidateGaugeTickMarkShapes(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Rectangle") || Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Triangle") || Validator.CompareWithInvariantCulture(val, "Circle") || Validator.CompareWithInvariantCulture(val, "Diamond") || Validator.CompareWithInvariantCulture(val, "Trapezoid") || Validator.CompareWithInvariantCulture(val, "Star") || Validator.CompareWithInvariantCulture(val, "Wedge") || Validator.CompareWithInvariantCulture(val, "Pentagon"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000AE30E File Offset: 0x000AC50E
		internal static bool ValidateLinearPointerTypes(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Marker") || Validator.CompareWithInvariantCulture(val, "Bar") || Validator.CompareWithInvariantCulture(val, "Thermometer"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000AE344 File Offset: 0x000AC544
		internal static bool ValidateRadialPointerNeedleStyles(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Triangular") || Validator.CompareWithInvariantCulture(val, "Rectangular") || Validator.CompareWithInvariantCulture(val, "TaperedWithTail") || Validator.CompareWithInvariantCulture(val, "Tapered") || Validator.CompareWithInvariantCulture(val, "ArrowWithTail") || Validator.CompareWithInvariantCulture(val, "Arrow") || Validator.CompareWithInvariantCulture(val, "StealthArrowWithTail") || Validator.CompareWithInvariantCulture(val, "StealthArrow") || Validator.CompareWithInvariantCulture(val, "TaperedWithStealthArrow") || Validator.CompareWithInvariantCulture(val, "StealthArrowWithWideTail") || Validator.CompareWithInvariantCulture(val, "TaperedWithRoundedPoint"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000AE3EF File Offset: 0x000AC5EF
		internal static bool ValidateRadialPointerTypes(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Needle") || Validator.CompareWithInvariantCulture(val, "Marker") || Validator.CompareWithInvariantCulture(val, "Bar"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000AE424 File Offset: 0x000AC624
		internal static bool ValidateScaleRangePlacements(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Inside") || Validator.CompareWithInvariantCulture(val, "Outside") || Validator.CompareWithInvariantCulture(val, "Cross"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000AE45C File Offset: 0x000AC65C
		internal static bool ValidateBackgroundGradientTypes(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "StartToEnd") || Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "LeftRight") || Validator.CompareWithInvariantCulture(val, "TopBottom") || Validator.CompareWithInvariantCulture(val, "Center") || Validator.CompareWithInvariantCulture(val, "DiagonalLeft") || Validator.CompareWithInvariantCulture(val, "DiagonalRight") || Validator.CompareWithInvariantCulture(val, "HorizontalCenter") || Validator.CompareWithInvariantCulture(val, "VerticalCenter"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000AE4EA File Offset: 0x000AC6EA
		internal static bool ValidateTextAntiAliasingQualities(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "High") || Validator.CompareWithInvariantCulture(val, "Normal") || Validator.CompareWithInvariantCulture(val, "SystemDefault"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000AE51F File Offset: 0x000AC71F
		internal static bool ValidateGaugeResizeModes(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "AutoFit") || Validator.CompareWithInvariantCulture(val, "None"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000AE547 File Offset: 0x000AC747
		internal static bool ValidateImageSourceType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.ValidateImageSourceType(val))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000AE55D File Offset: 0x000AC75D
		internal static bool ValidateMimeType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.ValidateMimeType(val))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000AE573 File Offset: 0x000AC773
		internal static bool ValidateGaugeIndicatorStyles(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Mechanical") || Validator.CompareWithInvariantCulture(val, "Digital7Segment") || Validator.CompareWithInvariantCulture(val, "Digital14Segment"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000AE5A8 File Offset: 0x000AC7A8
		internal static bool ValidateGaugeShowSigns(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "NegativeOnly") || Validator.CompareWithInvariantCulture(val, "Both") || Validator.CompareWithInvariantCulture(val, "None"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000AE5E0 File Offset: 0x000AC7E0
		internal static bool ValidateGaugeStateIndicatorStyles(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Circle") || Validator.CompareWithInvariantCulture(val, "Flag") || Validator.CompareWithInvariantCulture(val, "ArrowDown") || Validator.CompareWithInvariantCulture(val, "ArrowDownIncline") || Validator.CompareWithInvariantCulture(val, "ArrowSide") || Validator.CompareWithInvariantCulture(val, "ArrowUp") || Validator.CompareWithInvariantCulture(val, "ArrowUpIncline") || Validator.CompareWithInvariantCulture(val, "BoxesAllFilled") || Validator.CompareWithInvariantCulture(val, "BoxesNoneFilled") || Validator.CompareWithInvariantCulture(val, "BoxesOneFilled") || Validator.CompareWithInvariantCulture(val, "BoxesTwoFilled") || Validator.CompareWithInvariantCulture(val, "BoxesThreeFilled") || Validator.CompareWithInvariantCulture(val, "LightArrowDown") || Validator.CompareWithInvariantCulture(val, "LightArrowDownIncline") || Validator.CompareWithInvariantCulture(val, "LightArrowSide") || Validator.CompareWithInvariantCulture(val, "LightArrowUp") || Validator.CompareWithInvariantCulture(val, "LightArrowUpIncline") || Validator.CompareWithInvariantCulture(val, "QuartersAllFilled") || Validator.CompareWithInvariantCulture(val, "QuartersNoneFilled") || Validator.CompareWithInvariantCulture(val, "QuartersOneFilled") || Validator.CompareWithInvariantCulture(val, "QuartersTwoFilled") || Validator.CompareWithInvariantCulture(val, "QuartersThreeFilled") || Validator.CompareWithInvariantCulture(val, "SignalMeterFourFilled") || Validator.CompareWithInvariantCulture(val, "SignalMeterNoneFilled") || Validator.CompareWithInvariantCulture(val, "SignalMeterOneFilled") || Validator.CompareWithInvariantCulture(val, "SignalMeterThreeFilled") || Validator.CompareWithInvariantCulture(val, "SignalMeterTwoFilled") || Validator.CompareWithInvariantCulture(val, "StarQuartersAllFilled") || Validator.CompareWithInvariantCulture(val, "StarQuartersNoneFilled") || Validator.CompareWithInvariantCulture(val, "StarQuartersOneFilled") || Validator.CompareWithInvariantCulture(val, "StarQuartersTwoFilled") || Validator.CompareWithInvariantCulture(val, "StarQuartersThreeFilled") || Validator.CompareWithInvariantCulture(val, "ThreeSignsCircle") || Validator.CompareWithInvariantCulture(val, "ThreeSignsDiamond") || Validator.CompareWithInvariantCulture(val, "ThreeSignsTriangle") || Validator.CompareWithInvariantCulture(val, "ThreeSymbolCheck") || Validator.CompareWithInvariantCulture(val, "ThreeSymbolCross") || Validator.CompareWithInvariantCulture(val, "ThreeSymbolExclamation") || Validator.CompareWithInvariantCulture(val, "ThreeSymbolUnCircledCheck") || Validator.CompareWithInvariantCulture(val, "ThreeSymbolUnCircledCross") || Validator.CompareWithInvariantCulture(val, "ThreeSymbolUnCircledExclamation") || Validator.CompareWithInvariantCulture(val, "TrafficLight") || Validator.CompareWithInvariantCulture(val, "TrafficLightUnrimmed") || Validator.CompareWithInvariantCulture(val, "TriangleDash") || Validator.CompareWithInvariantCulture(val, "TriangleDown") || Validator.CompareWithInvariantCulture(val, "TriangleUp") || Validator.CompareWithInvariantCulture(val, "ButtonStop") || Validator.CompareWithInvariantCulture(val, "ButtonPlay") || Validator.CompareWithInvariantCulture(val, "ButtonPause") || Validator.CompareWithInvariantCulture(val, "FaceSmile") || Validator.CompareWithInvariantCulture(val, "FaceNeutral") || Validator.CompareWithInvariantCulture(val, "FaceFrown") || Validator.CompareWithInvariantCulture(val, "Image") || Validator.CompareWithInvariantCulture(val, "None"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000AE93B File Offset: 0x000ACB3B
		internal static bool ValidateGaugeTransformationType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.IsStateIndicatorTransformationTypePercent(val) || Validator.CompareWithInvariantCulture(val, "None"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000AE95E File Offset: 0x000ACB5E
		internal static bool IsStateIndicatorTransformationTypePercent(string val)
		{
			return Validator.CompareWithInvariantCulture(val, "Percentage");
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000AE96C File Offset: 0x000ACB6C
		internal static bool ValidateMapLegendTitleSeparator(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Line") || Validator.CompareWithInvariantCulture(val, "ThickLine") || Validator.CompareWithInvariantCulture(val, "DoubleLine") || Validator.CompareWithInvariantCulture(val, "DashLine") || Validator.CompareWithInvariantCulture(val, "DotLine") || Validator.CompareWithInvariantCulture(val, "GradientLine") || Validator.CompareWithInvariantCulture(val, "ThickGradientLine"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000AE9F0 File Offset: 0x000ACBF0
		internal static bool ValidateMapLegendLayout(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "AutoTable") || Validator.CompareWithInvariantCulture(val, "Column") || Validator.CompareWithInvariantCulture(val, "Row") || Validator.CompareWithInvariantCulture(val, "WideTable") || Validator.CompareWithInvariantCulture(val, "TallTable"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000AEA4A File Offset: 0x000ACC4A
		internal static bool ValidateMapAutoBool(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Auto") || Validator.CompareWithInvariantCulture(val, "True") || Validator.CompareWithInvariantCulture(val, "False"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000AEA7F File Offset: 0x000ACC7F
		internal static bool ValidateLabelPlacement(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Alternate") || Validator.CompareWithInvariantCulture(val, "Top") || Validator.CompareWithInvariantCulture(val, "Bottom"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000AEAB4 File Offset: 0x000ACCB4
		internal static bool ValidateLabelBehavior(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Auto") || Validator.CompareWithInvariantCulture(val, "ShowMiddleValue") || Validator.CompareWithInvariantCulture(val, "ShowBorderValue"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000AEAEC File Offset: 0x000ACCEC
		internal static bool ValidateUnit(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Percentage") || Validator.CompareWithInvariantCulture(val, "Inch") || Validator.CompareWithInvariantCulture(val, "Point") || Validator.CompareWithInvariantCulture(val, "Centimeter") || Validator.CompareWithInvariantCulture(val, "Millimeter") || Validator.CompareWithInvariantCulture(val, "Pica"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000AEB54 File Offset: 0x000ACD54
		internal static bool ValidateLabelPosition(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Near") || Validator.CompareWithInvariantCulture(val, "OneQuarter") || Validator.CompareWithInvariantCulture(val, "Center") || Validator.CompareWithInvariantCulture(val, "ThreeQuarters") || Validator.CompareWithInvariantCulture(val, "Far"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000AEBB0 File Offset: 0x000ACDB0
		internal static bool ValidateMapPosition(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "TopCenter") || Validator.CompareWithInvariantCulture(val, "TopLeft") || Validator.CompareWithInvariantCulture(val, "TopRight") || Validator.CompareWithInvariantCulture(val, "LeftTop") || Validator.CompareWithInvariantCulture(val, "LeftCenter") || Validator.CompareWithInvariantCulture(val, "LeftBottom") || Validator.CompareWithInvariantCulture(val, "RightTop") || Validator.CompareWithInvariantCulture(val, "RightCenter") || Validator.CompareWithInvariantCulture(val, "RightBottom") || Validator.CompareWithInvariantCulture(val, "BottomRight") || Validator.CompareWithInvariantCulture(val, "BottomCenter") || Validator.CompareWithInvariantCulture(val, "BottomLeft"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000AEC6B File Offset: 0x000ACE6B
		internal static bool ValidateMapCoordinateSystem(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Planar") || Validator.CompareWithInvariantCulture(val, "Geographic"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000AEC94 File Offset: 0x000ACE94
		internal static bool ValidateMapProjection(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Equirectangular") || Validator.CompareWithInvariantCulture(val, "Mercator") || Validator.CompareWithInvariantCulture(val, "Robinson") || Validator.CompareWithInvariantCulture(val, "Fahey") || Validator.CompareWithInvariantCulture(val, "Eckert1") || Validator.CompareWithInvariantCulture(val, "Eckert3") || Validator.CompareWithInvariantCulture(val, "HammerAitoff") || Validator.CompareWithInvariantCulture(val, "Wagner3") || Validator.CompareWithInvariantCulture(val, "Bonne"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000AED24 File Offset: 0x000ACF24
		internal static bool ValidateMapPalette(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Random") || Validator.CompareWithInvariantCulture(val, "Light") || Validator.CompareWithInvariantCulture(val, "SemiTransparent") || Validator.CompareWithInvariantCulture(val, "BrightPastel") || Validator.CompareWithInvariantCulture(val, "Pacific"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000AED80 File Offset: 0x000ACF80
		internal static bool ValidateMapRuleDistributionType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Optimal") || Validator.CompareWithInvariantCulture(val, "EqualInterval") || Validator.CompareWithInvariantCulture(val, "EqualDistribution") || Validator.CompareWithInvariantCulture(val, "Custom"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000AEDCD File Offset: 0x000ACFCD
		internal static bool ValidateMapResizeMode(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "AutoFit") || Validator.CompareWithInvariantCulture(val, "None"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000AEDF8 File Offset: 0x000ACFF8
		internal static bool ValidateMapMarkerStyle(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Rectangle") || Validator.CompareWithInvariantCulture(val, "Circle") || Validator.CompareWithInvariantCulture(val, "Diamond") || Validator.CompareWithInvariantCulture(val, "Triangle") || Validator.CompareWithInvariantCulture(val, "Trapezoid") || Validator.CompareWithInvariantCulture(val, "Star") || Validator.CompareWithInvariantCulture(val, "Wedge") || Validator.CompareWithInvariantCulture(val, "Pentagon") || Validator.CompareWithInvariantCulture(val, "PushPin") || Validator.CompareWithInvariantCulture(val, "Image"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000AEEA3 File Offset: 0x000AD0A3
		internal static bool ValidateMapLineLabelPlacement(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Above") || Validator.CompareWithInvariantCulture(val, "Center") || Validator.CompareWithInvariantCulture(val, "Below"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000AEED8 File Offset: 0x000AD0D8
		internal static bool ValidateMapPolygonLabelPlacement(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "MiddleCenter") || Validator.CompareWithInvariantCulture(val, "MiddleLeft") || Validator.CompareWithInvariantCulture(val, "MiddleRight") || Validator.CompareWithInvariantCulture(val, "TopCenter") || Validator.CompareWithInvariantCulture(val, "TopLeft") || Validator.CompareWithInvariantCulture(val, "TopRight") || Validator.CompareWithInvariantCulture(val, "BottomCenter") || Validator.CompareWithInvariantCulture(val, "BottomLeft") || Validator.CompareWithInvariantCulture(val, "BottomRight"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000AEF68 File Offset: 0x000AD168
		internal static bool ValidateMapPointLabelPlacement(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Bottom") || Validator.CompareWithInvariantCulture(val, "Top") || Validator.CompareWithInvariantCulture(val, "Left") || Validator.CompareWithInvariantCulture(val, "Right") || Validator.CompareWithInvariantCulture(val, "Center"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000AEFC2 File Offset: 0x000AD1C2
		internal static bool ValidateMapTileStyle(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Road") || Validator.CompareWithInvariantCulture(val, "Aerial") || Validator.CompareWithInvariantCulture(val, "Hybrid"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000AEFF7 File Offset: 0x000AD1F7
		internal static bool ValidateMapVisibilityMode(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Visible") || Validator.CompareWithInvariantCulture(val, "Hidden") || Validator.CompareWithInvariantCulture(val, "ZoomBased"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000AF02C File Offset: 0x000AD22C
		internal static bool ValidateDataType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Boolean") || Validator.CompareWithInvariantCulture(val, "DateTime") || Validator.CompareWithInvariantCulture(val, "Integer") || Validator.CompareWithInvariantCulture(val, "Float") || Validator.CompareWithInvariantCulture(val, "String"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000AF088 File Offset: 0x000AD288
		internal static bool ValidateMapBorderSkinType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Emboss") || Validator.CompareWithInvariantCulture(val, "Raised") || Validator.CompareWithInvariantCulture(val, "Sunken") || Validator.CompareWithInvariantCulture(val, "FrameThin1") || Validator.CompareWithInvariantCulture(val, "FrameThin2") || Validator.CompareWithInvariantCulture(val, "FrameThin3") || Validator.CompareWithInvariantCulture(val, "FrameThin4") || Validator.CompareWithInvariantCulture(val, "FrameThin5") || Validator.CompareWithInvariantCulture(val, "FrameThin6") || Validator.CompareWithInvariantCulture(val, "FrameTitle1") || Validator.CompareWithInvariantCulture(val, "FrameTitle2") || Validator.CompareWithInvariantCulture(val, "FrameTitle3") || Validator.CompareWithInvariantCulture(val, "FrameTitle4") || Validator.CompareWithInvariantCulture(val, "FrameTitle5") || Validator.CompareWithInvariantCulture(val, "FrameTitle6") || Validator.CompareWithInvariantCulture(val, "FrameTitle7") || Validator.CompareWithInvariantCulture(val, "FrameTitle8"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000AF1A4 File Offset: 0x000AD3A4
		internal static bool ValidateMapAntiAliasing(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "All") || Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Text") || Validator.CompareWithInvariantCulture(val, "Graphics"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000AF1F1 File Offset: 0x000AD3F1
		internal static bool ValidateMapTextAntiAliasingQuality(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "High") || Validator.CompareWithInvariantCulture(val, "Normal") || Validator.CompareWithInvariantCulture(val, "SystemDefault"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000AF226 File Offset: 0x000AD426
		private Validator()
		{
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000AF230 File Offset: 0x000AD430
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

		// Token: 0x060024BA RID: 9402 RVA: 0x000AF2B4 File Offset: 0x000AD4B4
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

		// Token: 0x060024BB RID: 9403 RVA: 0x000AF350 File Offset: 0x000AD550
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

		// Token: 0x060024BC RID: 9404 RVA: 0x000AF3D4 File Offset: 0x000AD5D4
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
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				c = Color.FromArgb(0, 0, 0);
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000AF460 File Offset: 0x000AD660
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

		// Token: 0x060024BE RID: 9406 RVA: 0x000AF4D8 File Offset: 0x000AD6D8
		internal static bool ValidateSize(string size, bool allowNegative, out double sizeInMM)
		{
			return Validator.ValidateSize(size, allowNegative ? Validator.NegativeMin : Validator.NormalMin, Validator.NormalMax, out sizeInMM);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000AF4F8 File Offset: 0x000AD6F8
		private static bool ValidateSize(string size, double minValue, double maxValue, out double sizeInMM)
		{
			RVUnit rvunit;
			if (Validator.ValidateSizeString(size, out rvunit) && Validator.ValidateSizeUnitType(rvunit))
			{
				sizeInMM = Converter.ConvertToMM(rvunit);
				return Validator.ValidateSizeValue(sizeInMM, minValue, maxValue);
			}
			sizeInMM = 0.0;
			return false;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000AF538 File Offset: 0x000AD738
		internal static bool ValidateSizeString(string sizeString, out RVUnit sizeValue)
		{
			bool flag;
			try
			{
				sizeValue = new RVUnit(sizeString, CultureInfo.InvariantCulture, RVUnitType.Pixel);
				if (sizeValue.Type == RVUnitType.Pixel)
				{
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				sizeValue = RVUnit.Empty;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000AF594 File Offset: 0x000AD794
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

		// Token: 0x060024C2 RID: 9410 RVA: 0x000AF5DA File Offset: 0x000AD7DA
		internal static bool ValidateSizeIsPositive(RVUnit sizeValue)
		{
			return sizeValue.Value >= 0.0;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000AF5F1 File Offset: 0x000AD7F1
		internal static bool ValidateSizeValue(double sizeInMM, double minValue, double maxValue)
		{
			return sizeInMM >= minValue && sizeInMM <= maxValue;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000AF600 File Offset: 0x000AD800
		internal static void ParseSize(string size, out double sizeInMM)
		{
			RVUnit rvunit = RVUnit.Parse(size, CultureInfo.InvariantCulture);
			sizeInMM = Converter.ConvertToMM(rvunit);
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000AF624 File Offset: 0x000AD824
		internal static int CompareDoubles(double first, double second)
		{
			double num = Math.Round(first, 4);
			double num2 = Math.Round(second, 4);
			long num3 = (long)(num * Math.Pow(10.0, 3.0));
			long num4 = (long)(num2 * Math.Pow(10.0, 3.0));
			if (num3 < num4)
			{
				return -1;
			}
			if (num3 > num4)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000AF682 File Offset: 0x000AD882
		internal static bool ValidateEmbeddedImageName(string embeddedImageName, Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo> embeddedImages)
		{
			return embeddedImageName != null && embeddedImages != null && embeddedImages.ContainsKey(embeddedImageName);
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000AF698 File Offset: 0x000AD898
		internal static bool ValidateSpecificLanguage(string language, out CultureInfo culture)
		{
			if (language == null)
			{
				culture = null;
				return true;
			}
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

		// Token: 0x060024C8 RID: 9416 RVA: 0x000AF6F4 File Offset: 0x000AD8F4
		internal static bool ValidateLanguage(string language, out CultureInfo culture)
		{
			if (language == null)
			{
				culture = null;
				return true;
			}
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

		// Token: 0x060024C9 RID: 9417 RVA: 0x000AF730 File Offset: 0x000AD930
		private static bool CreateCalendar(string calendarName, out Calendar calendar)
		{
			calendar = null;
			bool flag = false;
			if (Validator.CompareWithInvariantCulture(calendarName, "GregorianUSEnglish"))
			{
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "GregorianArabic"))
			{
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.Arabic);
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "GregorianMiddleEastFrench"))
			{
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.MiddleEastFrench);
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "GregorianTransliteratedEnglish"))
			{
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.TransliteratedEnglish);
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "GregorianTransliteratedFrench"))
			{
				flag = true;
				calendar = new GregorianCalendar(GregorianCalendarTypes.TransliteratedFrench);
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
			else if (Validator.CompareWithInvariantCulture(calendarName, "Korean"))
			{
				calendar = new KoreanCalendar();
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "Taiwan"))
			{
				calendar = new TaiwanCalendar();
			}
			else if (Validator.CompareWithInvariantCulture(calendarName, "ThaiBuddhist"))
			{
				calendar = new ThaiBuddhistCalendar();
			}
			return flag;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000AF858 File Offset: 0x000ADA58
		internal static bool ValidateCalendar(CultureInfo langauge, string calendarName)
		{
			if (Validator.CompareWithInvariantCulture(calendarName, "Default") || Validator.CompareWithInvariantCulture(calendarName, "Gregorian"))
			{
				return true;
			}
			Calendar calendar;
			bool flag = Validator.CreateCalendar(calendarName, out calendar);
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
						if (!flag)
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

		// Token: 0x060024CB RID: 9419 RVA: 0x000AF8E4 File Offset: 0x000ADAE4
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

		// Token: 0x060024CC RID: 9420 RVA: 0x000AFA70 File Offset: 0x000ADC70
		internal static bool ValidateColumns(int columns)
		{
			return columns >= 1 && columns <= 1000;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000AFA81 File Offset: 0x000ADC81
		internal static bool ValidateNumeralVariant(int numeralVariant)
		{
			return numeralVariant >= 1 && numeralVariant <= 7;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000AFA90 File Offset: 0x000ADC90
		internal static bool ValidatePalette(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Default") || Validator.CompareWithInvariantCulture(val, "EarthTones") || Validator.CompareWithInvariantCulture(val, "Excel") || Validator.CompareWithInvariantCulture(val, "GrayScale") || Validator.CompareWithInvariantCulture(val, "Light") || Validator.CompareWithInvariantCulture(val, "Pastel") || Validator.CompareWithInvariantCulture(val, "SemiTransparent") || Validator.CompareWithInvariantCulture(val, "Berry") || Validator.CompareWithInvariantCulture(val, "Chocolate") || Validator.CompareWithInvariantCulture(val, "Fire") || Validator.CompareWithInvariantCulture(val, "SeaGreen") || Validator.CompareWithInvariantCulture(val, "BrightPastel") || Validator.CompareWithInvariantCulture(val, "Pacific") || Validator.CompareWithInvariantCulture(val, "PacificLight") || Validator.CompareWithInvariantCulture(val, "PacificSemiTransparent") || Validator.CompareWithInvariantCulture(val, "Custom"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000AFB8B File Offset: 0x000ADD8B
		internal static bool ValidatePaletteHatchBehavior(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Default") || Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Always"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000AFBC0 File Offset: 0x000ADDC0
		internal static bool ValidateChartIntervalType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Default") || Validator.CompareWithInvariantCulture(val, "Auto") || Validator.CompareWithInvariantCulture(val, "Number") || Validator.CompareWithInvariantCulture(val, "Years") || Validator.CompareWithInvariantCulture(val, "Months") || Validator.CompareWithInvariantCulture(val, "Weeks") || Validator.CompareWithInvariantCulture(val, "Days") || Validator.CompareWithInvariantCulture(val, "Hours") || Validator.CompareWithInvariantCulture(val, "Minutes") || Validator.CompareWithInvariantCulture(val, "Seconds") || Validator.CompareWithInvariantCulture(val, "Milliseconds"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000AFC6C File Offset: 0x000ADE6C
		internal static bool ValidateChartTickMarksType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Inside") || Validator.CompareWithInvariantCulture(val, "Outside") || Validator.CompareWithInvariantCulture(val, "Cross"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000AFCB9 File Offset: 0x000ADEB9
		internal static bool ValidateChartColumnType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Text") || Validator.CompareWithInvariantCulture(val, "SeriesSymbol"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000AFCE1 File Offset: 0x000ADEE1
		internal static bool ValidateChartCellType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Text") || Validator.CompareWithInvariantCulture(val, "SeriesSymbol") || Validator.CompareWithInvariantCulture(val, "Image"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000AFD18 File Offset: 0x000ADF18
		internal static bool ValidateChartCellAlignment(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Center") || Validator.CompareWithInvariantCulture(val, "Top") || Validator.CompareWithInvariantCulture(val, "TopLeft") || Validator.CompareWithInvariantCulture(val, "TopRight") || Validator.CompareWithInvariantCulture(val, "Left") || Validator.CompareWithInvariantCulture(val, "Right") || Validator.CompareWithInvariantCulture(val, "BottomRight") || Validator.CompareWithInvariantCulture(val, "Bottom") || Validator.CompareWithInvariantCulture(val, "BottomLeft"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000AFDA6 File Offset: 0x000ADFA6
		internal static bool ValidateChartAllowOutsideChartArea(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Partial") || Validator.CompareWithInvariantCulture(val, "True") || Validator.CompareWithInvariantCulture(val, "False"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000AFDDC File Offset: 0x000ADFDC
		internal static bool ValidateChartCalloutLineAnchor(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Arrow") || Validator.CompareWithInvariantCulture(val, "Diamond") || Validator.CompareWithInvariantCulture(val, "Square") || Validator.CompareWithInvariantCulture(val, "Round") || Validator.CompareWithInvariantCulture(val, "None"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000AFE38 File Offset: 0x000AE038
		internal static bool ValidateChartCalloutLineStyle(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Solid") || Validator.CompareWithInvariantCulture(val, "Dotted") || Validator.CompareWithInvariantCulture(val, "Dashed") || Validator.CompareWithInvariantCulture(val, "Double") || Validator.CompareWithInvariantCulture(val, "DashDot") || Validator.CompareWithInvariantCulture(val, "DashDotDot") || Validator.CompareWithInvariantCulture(val, "None"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000AFEAC File Offset: 0x000AE0AC
		internal static bool ValidateChartCalloutStyle(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Underline") || Validator.CompareWithInvariantCulture(val, "Box") || Validator.CompareWithInvariantCulture(val, "None"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000AFEE4 File Offset: 0x000AE0E4
		internal static bool ValidateChartCustomItemSeparator(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Line") || Validator.CompareWithInvariantCulture(val, "ThickLine") || Validator.CompareWithInvariantCulture(val, "DoubleLine") || Validator.CompareWithInvariantCulture(val, "DashLine") || Validator.CompareWithInvariantCulture(val, "DotLine") || Validator.CompareWithInvariantCulture(val, "GradientLine") || Validator.CompareWithInvariantCulture(val, "ThickGradientLine"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000AFF68 File Offset: 0x000AE168
		internal static bool ValidateChartSeriesFormula(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "BollingerBands") || Validator.CompareWithInvariantCulture(val, "MovingAverage") || Validator.CompareWithInvariantCulture(val, "ExponentialMovingAverage") || Validator.CompareWithInvariantCulture(val, "TriangularMovingAverage") || Validator.CompareWithInvariantCulture(val, "WeightedMovingAverage") || Validator.CompareWithInvariantCulture(val, "MACD") || Validator.CompareWithInvariantCulture(val, "DetrendedPriceOscillator") || Validator.CompareWithInvariantCulture(val, "Envelopes") || Validator.CompareWithInvariantCulture(val, "Performance") || Validator.CompareWithInvariantCulture(val, "RateOfChange") || Validator.CompareWithInvariantCulture(val, "RelativeStrengthIndex") || Validator.CompareWithInvariantCulture(val, "StandardDeviation") || Validator.CompareWithInvariantCulture(val, "TRIX") || Validator.CompareWithInvariantCulture(val, "Mean") || Validator.CompareWithInvariantCulture(val, "Median") || Validator.CompareWithInvariantCulture(val, "Error"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000B0064 File Offset: 0x000AE264
		internal static bool ValidateChartSeriesType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Column") || Validator.CompareWithInvariantCulture(val, "Bar") || Validator.CompareWithInvariantCulture(val, "Line") || Validator.CompareWithInvariantCulture(val, "Shape") || Validator.CompareWithInvariantCulture(val, "Scatter") || Validator.CompareWithInvariantCulture(val, "Area") || Validator.CompareWithInvariantCulture(val, "Range") || Validator.CompareWithInvariantCulture(val, "Polar"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000B00E8 File Offset: 0x000AE2E8
		internal static bool ValidateChartSeriesSubtype(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName, string elementNamespace)
		{
			if (Validator.CompareWithInvariantCulture(val, "Plain") || Validator.CompareWithInvariantCulture(val, "Stacked") || Validator.CompareWithInvariantCulture(val, "PercentStacked") || Validator.CompareWithInvariantCulture(val, "Smooth") || Validator.CompareWithInvariantCulture(val, "Stepped") || Validator.CompareWithInvariantCulture(val, "Pie") || Validator.CompareWithInvariantCulture(val, "ExplodedPie") || Validator.CompareWithInvariantCulture(val, "Doughnut") || Validator.CompareWithInvariantCulture(val, "ExplodedDoughnut") || Validator.CompareWithInvariantCulture(val, "Funnel") || Validator.CompareWithInvariantCulture(val, "Pyramid") || Validator.CompareWithInvariantCulture(val, "Bubble") || Validator.CompareWithInvariantCulture(val, "Candlestick") || Validator.CompareWithInvariantCulture(val, "Stock") || Validator.CompareWithInvariantCulture(val, "Bar") || Validator.CompareWithInvariantCulture(val, "Column") || Validator.CompareWithInvariantCulture(val, "BoxPlot") || Validator.CompareWithInvariantCulture(val, "ErrorBar") || Validator.CompareWithInvariantCulture(val, "Radar"))
			{
				return true;
			}
			if (RdlNamespaceComparer.Instance.Compare(elementNamespace, "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition") >= 0 && Validator.CompareWithInvariantCulture(val, "Map"))
			{
				return true;
			}
			if (RdlNamespaceComparer.Instance.Compare(elementNamespace, "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition") >= 0 && (Validator.CompareWithInvariantCulture(val, "TreeMap") || Validator.CompareWithInvariantCulture(val, "Sunburst")))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000B0268 File Offset: 0x000AE468
		internal static bool IsValidChartSeriesSubType(string type, string subType)
		{
			return Validator.CompareWithInvariantCulture(subType, "Plain") || (Validator.CompareWithInvariantCulture(type, "Area") && (Validator.CompareWithInvariantCulture(subType, "Smooth") || Validator.CompareWithInvariantCulture(subType, "Stacked") || Validator.CompareWithInvariantCulture(subType, "PercentStacked"))) || ((Validator.CompareWithInvariantCulture(type, "Bar") || Validator.CompareWithInvariantCulture(type, "Column")) && (Validator.CompareWithInvariantCulture(subType, "Stacked") || Validator.CompareWithInvariantCulture(subType, "PercentStacked"))) || (Validator.CompareWithInvariantCulture(type, "Line") && (Validator.CompareWithInvariantCulture(subType, "Smooth") || Validator.CompareWithInvariantCulture(subType, "Stepped"))) || (Validator.CompareWithInvariantCulture(type, "Polar") && Validator.CompareWithInvariantCulture(subType, "Radar")) || (Validator.CompareWithInvariantCulture(type, "Range") && (Validator.CompareWithInvariantCulture(subType, "Candlestick") || Validator.CompareWithInvariantCulture(subType, "Stock") || Validator.CompareWithInvariantCulture(subType, "Smooth") || Validator.CompareWithInvariantCulture(subType, "Bar") || Validator.CompareWithInvariantCulture(subType, "Column") || Validator.CompareWithInvariantCulture(subType, "BoxPlot") || Validator.CompareWithInvariantCulture(subType, "ErrorBar"))) || (Validator.CompareWithInvariantCulture(type, "Scatter") && Validator.CompareWithInvariantCulture(subType, "Bubble")) || (Validator.CompareWithInvariantCulture(type, "Shape") && (Validator.CompareWithInvariantCulture(subType, "Pie") || Validator.CompareWithInvariantCulture(subType, "ExplodedPie") || Validator.CompareWithInvariantCulture(subType, "Doughnut") || Validator.CompareWithInvariantCulture(subType, "ExplodedDoughnut") || Validator.CompareWithInvariantCulture(subType, "Funnel") || Validator.CompareWithInvariantCulture(subType, "Pyramid") || Validator.CompareWithInvariantCulture(subType, "TreeMap") || Validator.CompareWithInvariantCulture(subType, "Sunburst")));
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000B0433 File Offset: 0x000AE633
		internal static bool ValidateChartAxisLocation(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Default") || Validator.CompareWithInvariantCulture(val, "Opposite"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000B045C File Offset: 0x000AE65C
		internal static bool ValidateChartAxisArrow(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Triangle") || Validator.CompareWithInvariantCulture(val, "SharpTriangle") || Validator.CompareWithInvariantCulture(val, "Lines"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000B04AC File Offset: 0x000AE6AC
		internal static bool ValidateChartBorderSkinType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Emboss") || Validator.CompareWithInvariantCulture(val, "Raised") || Validator.CompareWithInvariantCulture(val, "Sunken") || Validator.CompareWithInvariantCulture(val, "FrameThin1") || Validator.CompareWithInvariantCulture(val, "FrameThin2") || Validator.CompareWithInvariantCulture(val, "FrameThin3") || Validator.CompareWithInvariantCulture(val, "FrameThin4") || Validator.CompareWithInvariantCulture(val, "FrameThin5") || Validator.CompareWithInvariantCulture(val, "FrameThin6") || Validator.CompareWithInvariantCulture(val, "FrameTitle1") || Validator.CompareWithInvariantCulture(val, "FrameTitle2") || Validator.CompareWithInvariantCulture(val, "FrameTitle3") || Validator.CompareWithInvariantCulture(val, "FrameTitle4") || Validator.CompareWithInvariantCulture(val, "FrameTitle5") || Validator.CompareWithInvariantCulture(val, "FrameTitle6") || Validator.CompareWithInvariantCulture(val, "FrameTitle7") || Validator.CompareWithInvariantCulture(val, "FrameTitle8"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000B05C8 File Offset: 0x000AE7C8
		internal static bool ValidateChartTitlePositions(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "TopCenter") || Validator.CompareWithInvariantCulture(val, "TopLeft") || Validator.CompareWithInvariantCulture(val, "TopRight") || Validator.CompareWithInvariantCulture(val, "LeftTop") || Validator.CompareWithInvariantCulture(val, "LeftCenter") || Validator.CompareWithInvariantCulture(val, "LeftBottom") || Validator.CompareWithInvariantCulture(val, "RightTop") || Validator.CompareWithInvariantCulture(val, "RightCenter") || Validator.CompareWithInvariantCulture(val, "RightBottom") || Validator.CompareWithInvariantCulture(val, "BottomRight") || Validator.CompareWithInvariantCulture(val, "BottomCenter") || Validator.CompareWithInvariantCulture(val, "BottomLeft"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000B0683 File Offset: 0x000AE883
		internal static bool ValidateChartAxisTitlePositions(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Center") || Validator.CompareWithInvariantCulture(val, "Near") || Validator.CompareWithInvariantCulture(val, "Far"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000B06B8 File Offset: 0x000AE8B8
		internal static bool ValidateChartTitleDockings(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Top") || Validator.CompareWithInvariantCulture(val, "Left") || Validator.CompareWithInvariantCulture(val, "Right") || Validator.CompareWithInvariantCulture(val, "Bottom"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000B0708 File Offset: 0x000AE908
		internal static bool ValidateChartAxisLabelRotation(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Rotate15") || Validator.CompareWithInvariantCulture(val, "Rotate30") || Validator.CompareWithInvariantCulture(val, "Rotate45") || Validator.CompareWithInvariantCulture(val, "Rotate90"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000B0764 File Offset: 0x000AE964
		internal static bool ValidateChartBreakLineType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Ragged") || Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Straight") || Validator.CompareWithInvariantCulture(val, "Wave"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000B07B1 File Offset: 0x000AE9B1
		internal static bool ValidateChartAutoBool(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Auto") || Validator.CompareWithInvariantCulture(val, "True") || Validator.CompareWithInvariantCulture(val, "False"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000B07E8 File Offset: 0x000AE9E8
		internal static bool ValidateChartDataLabelPosition(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Auto") || Validator.CompareWithInvariantCulture(val, "Top") || Validator.CompareWithInvariantCulture(val, "TopLeft") || Validator.CompareWithInvariantCulture(val, "TopRight") || Validator.CompareWithInvariantCulture(val, "Left") || Validator.CompareWithInvariantCulture(val, "Center") || Validator.CompareWithInvariantCulture(val, "Right") || Validator.CompareWithInvariantCulture(val, "BottomRight") || Validator.CompareWithInvariantCulture(val, "BottomLeft") || Validator.CompareWithInvariantCulture(val, "Bottom") || Validator.CompareWithInvariantCulture(val, "Outside"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000B0894 File Offset: 0x000AEA94
		internal static bool ValidateChartMarkerType(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Square") || Validator.CompareWithInvariantCulture(val, "Circle") || Validator.CompareWithInvariantCulture(val, "Diamond") || Validator.CompareWithInvariantCulture(val, "Triangle") || Validator.CompareWithInvariantCulture(val, "Cross") || Validator.CompareWithInvariantCulture(val, "Star4") || Validator.CompareWithInvariantCulture(val, "Star5") || Validator.CompareWithInvariantCulture(val, "Star6") || Validator.CompareWithInvariantCulture(val, "Star10") || Validator.CompareWithInvariantCulture(val, "Auto"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000B093F File Offset: 0x000AEB3F
		internal static bool ValidateChartThreeDProjectionMode(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Oblique") || Validator.CompareWithInvariantCulture(val, "Perspective"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000B0967 File Offset: 0x000AEB67
		internal static bool ValidateChartThreeDShading(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "None") || Validator.CompareWithInvariantCulture(val, "Real") || Validator.CompareWithInvariantCulture(val, "Simple"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000B099C File Offset: 0x000AEB9C
		internal static bool ValidateBackgroundHatchType(string backgroundHatchType)
		{
			return Validator.CompareWithInvariantCulture(backgroundHatchType, "Default") || Validator.CompareWithInvariantCulture(backgroundHatchType, "None") || Validator.CompareWithInvariantCulture(backgroundHatchType, "BackwardDiagonal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Cross") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DarkDownwardDiagonal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DarkHorizontal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DarkUpwardDiagonal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DarkVertical") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DashedDownwardDiagonal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DashedHorizontal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DashedUpwardDiagonal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DashedVertical") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DiagonalBrick") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DiagonalCross") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Divot") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DottedDiamond") || Validator.CompareWithInvariantCulture(backgroundHatchType, "DottedGrid") || Validator.CompareWithInvariantCulture(backgroundHatchType, "ForwardDiagonal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Horizontal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "HorizontalBrick") || Validator.CompareWithInvariantCulture(backgroundHatchType, "LargeCheckerBoard") || Validator.CompareWithInvariantCulture(backgroundHatchType, "LargeConfetti") || Validator.CompareWithInvariantCulture(backgroundHatchType, "LargeGrid") || Validator.CompareWithInvariantCulture(backgroundHatchType, "LightDownwardDiagonal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "LightHorizontal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "LightUpwardDiagonal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "LightVertical") || Validator.CompareWithInvariantCulture(backgroundHatchType, "NarrowHorizontal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "NarrowVertical") || Validator.CompareWithInvariantCulture(backgroundHatchType, "OutlinedDiamond") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent05") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent10") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent20") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent25") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent30") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent40") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent50") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent60") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent70") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent75") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent80") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Percent90") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Plaid") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Shingle") || Validator.CompareWithInvariantCulture(backgroundHatchType, "SmallCheckerBoard") || Validator.CompareWithInvariantCulture(backgroundHatchType, "SmallConfetti") || Validator.CompareWithInvariantCulture(backgroundHatchType, "SmallGrid") || Validator.CompareWithInvariantCulture(backgroundHatchType, "SolidDiamond") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Sphere") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Trellis") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Vertical") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Wave") || Validator.CompareWithInvariantCulture(backgroundHatchType, "Weave") || Validator.CompareWithInvariantCulture(backgroundHatchType, "WideDownwardDiagonal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "WideUpwardDiagonal") || Validator.CompareWithInvariantCulture(backgroundHatchType, "ZigZag");
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000B0D10 File Offset: 0x000AEF10
		internal static bool ValidatePosition(string position)
		{
			return Validator.CompareWithInvariantCulture(position, "Default") || Validator.CompareWithInvariantCulture(position, "Top") || Validator.CompareWithInvariantCulture(position, "TopLeft") || Validator.CompareWithInvariantCulture(position, "TopRight") || Validator.CompareWithInvariantCulture(position, "Left") || Validator.CompareWithInvariantCulture(position, "Center") || Validator.CompareWithInvariantCulture(position, "Right") || Validator.CompareWithInvariantCulture(position, "BottomRight") || Validator.CompareWithInvariantCulture(position, "Bottom") || Validator.CompareWithInvariantCulture(position, "BottomLeft");
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000B0DA4 File Offset: 0x000AEFA4
		internal static bool ValidateTextEffect(string textEffect)
		{
			return Validator.CompareWithInvariantCulture(textEffect, "Default") || Validator.CompareWithInvariantCulture(textEffect, "None") || Validator.CompareWithInvariantCulture(textEffect, "Shadow") || Validator.CompareWithInvariantCulture(textEffect, "Emboss") || Validator.CompareWithInvariantCulture(textEffect, "Embed") || Validator.CompareWithInvariantCulture(textEffect, "Frame");
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000B0E02 File Offset: 0x000AF002
		internal static bool IsDynamicImageReportItem(ObjectType objectType)
		{
			return objectType == ObjectType.Chart || objectType == ObjectType.GaugePanel || objectType == ObjectType.Map;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000B0E15 File Offset: 0x000AF015
		internal static bool IsDynamicImageSubElement(IStyleContainer styleContainer)
		{
			return Validator.IsDynamicImageReportItem(styleContainer.ObjectType) && (!(styleContainer is Microsoft.ReportingServices.ReportIntermediateFormat.Chart) && !(styleContainer is GaugePanel)) && !(styleContainer is Map);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000B0E44 File Offset: 0x000AF044
		internal static bool ValidateBorderStyle(string borderStyle, bool isDefaultBorder, ObjectType objectType, bool isDynamicImageSubElement, out string validatedStyle)
		{
			bool flag = Validator.IsDynamicImageReportItem(objectType);
			bool flag2 = objectType == ObjectType.Line;
			if (Validator.CompareWithInvariantCulture(borderStyle, "Dotted") || Validator.CompareWithInvariantCulture(borderStyle, "Dashed"))
			{
				validatedStyle = borderStyle;
				return true;
			}
			if (Validator.CompareWithInvariantCulture(borderStyle, "None") || Validator.CompareWithInvariantCulture(borderStyle, "Solid") || Validator.CompareWithInvariantCulture(borderStyle, "Double"))
			{
				if (flag2)
				{
					validatedStyle = "Solid";
				}
				else
				{
					validatedStyle = borderStyle;
				}
				return true;
			}
			if (Validator.CompareWithInvariantCulture(borderStyle, "DashDot") || Validator.CompareWithInvariantCulture(borderStyle, "DashDotDot"))
			{
				if (flag)
				{
					if (isDynamicImageSubElement)
					{
						validatedStyle = borderStyle;
					}
					else
					{
						validatedStyle = "Dashed";
					}
				}
				else
				{
					validatedStyle = null;
				}
				return flag;
			}
			if (Validator.CompareWithInvariantCulture(borderStyle, "Default"))
			{
				if (isDefaultBorder)
				{
					if (flag2)
					{
						validatedStyle = "Solid";
					}
					else
					{
						validatedStyle = "None";
					}
				}
				else
				{
					validatedStyle = borderStyle;
				}
				return true;
			}
			validatedStyle = null;
			return false;
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000B0F1F File Offset: 0x000AF11F
		internal static bool ValidateImageSourceType(string val)
		{
			return Validator.CompareWithInvariantCulture(val, "External") || Validator.CompareWithInvariantCulture(val, "Embedded") || Validator.CompareWithInvariantCulture(val, "Database");
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000B0F4C File Offset: 0x000AF14C
		internal static bool ValidateMimeType(string mimeType)
		{
			return mimeType != null && (Validator.CompareWithInvariantCultureIgnoreCase(mimeType, "image/bmp") || Validator.CompareWithInvariantCultureIgnoreCase(mimeType, "image/jpeg") || Validator.CompareWithInvariantCultureIgnoreCase(mimeType, "image/gif") || Validator.CompareWithInvariantCultureIgnoreCase(mimeType, "image/png") || Validator.CompareWithInvariantCultureIgnoreCase(mimeType, "image/x-png"));
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000B0FA4 File Offset: 0x000AF1A4
		internal static bool ValidateBackgroundGradientType(string gradientType)
		{
			return Validator.CompareWithInvariantCulture(gradientType, "Default") || Validator.CompareWithInvariantCulture(gradientType, "None") || Validator.CompareWithInvariantCulture(gradientType, "LeftRight") || Validator.CompareWithInvariantCulture(gradientType, "TopBottom") || Validator.CompareWithInvariantCulture(gradientType, "Center") || Validator.CompareWithInvariantCulture(gradientType, "DiagonalLeft") || Validator.CompareWithInvariantCulture(gradientType, "DiagonalRight") || Validator.CompareWithInvariantCulture(gradientType, "HorizontalCenter") || Validator.CompareWithInvariantCulture(gradientType, "VerticalCenter");
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000B1029 File Offset: 0x000AF229
		internal static bool ValidateBackgroundRepeatForNamespace(string repeat, string rdlNamespace)
		{
			return !Validator.CompareWithInvariantCulture(repeat, "FitProportional") || RdlNamespaceComparer.Instance.Compare(rdlNamespace, "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition") >= 0;
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000B1050 File Offset: 0x000AF250
		internal static bool ValidateBackgroundRepeat(string repeat, ObjectType objectType)
		{
			bool flag = objectType == ObjectType.Chart;
			bool flag2 = objectType == ObjectType.ReportSection;
			if (Validator.CompareWithInvariantCulture(repeat, "Default") || Validator.CompareWithInvariantCulture(repeat, "Repeat") || Validator.CompareWithInvariantCulture(repeat, "Clip"))
			{
				return true;
			}
			if (Validator.CompareWithInvariantCulture(repeat, "RepeatX") || Validator.CompareWithInvariantCulture(repeat, "RepeatY"))
			{
				return !flag;
			}
			if (Validator.CompareWithInvariantCulture(repeat, "Fit"))
			{
				return flag || flag2;
			}
			return Validator.CompareWithInvariantCulture(repeat, "FitProportional") && flag2;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000B10D2 File Offset: 0x000AF2D2
		internal static bool ValidateFontStyle(string fontStyle)
		{
			return Validator.CompareWithInvariantCulture(fontStyle, "Default") || Validator.CompareWithInvariantCulture(fontStyle, "Normal") || Validator.CompareWithInvariantCulture(fontStyle, "Italic");
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000B1100 File Offset: 0x000AF300
		internal static bool ValidateFontWeight(string fontWeight)
		{
			return Validator.CompareWithInvariantCulture(fontWeight, "Thin") || Validator.CompareWithInvariantCulture(fontWeight, "ExtraLight") || Validator.CompareWithInvariantCulture(fontWeight, "Light") || Validator.CompareWithInvariantCulture(fontWeight, "Normal") || Validator.CompareWithInvariantCulture(fontWeight, "Default") || Validator.CompareWithInvariantCulture(fontWeight, "Medium") || Validator.CompareWithInvariantCulture(fontWeight, "SemiBold") || Validator.CompareWithInvariantCulture(fontWeight, "Bold") || Validator.CompareWithInvariantCulture(fontWeight, "ExtraBold") || Validator.CompareWithInvariantCulture(fontWeight, "Heavy");
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000B1194 File Offset: 0x000AF394
		internal static bool ValidateTextDecoration(string textDecoration)
		{
			return Validator.CompareWithInvariantCulture(textDecoration, "Default") || Validator.CompareWithInvariantCulture(textDecoration, "None") || Validator.CompareWithInvariantCulture(textDecoration, "Underline") || Validator.CompareWithInvariantCulture(textDecoration, "Overline") || Validator.CompareWithInvariantCulture(textDecoration, "LineThrough");
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000B11E8 File Offset: 0x000AF3E8
		internal static bool ValidateTextAlign(string textAlign)
		{
			return Validator.CompareWithInvariantCulture(textAlign, "Default") || Validator.CompareWithInvariantCulture(textAlign, "General") || Validator.CompareWithInvariantCulture(textAlign, "Left") || Validator.CompareWithInvariantCulture(textAlign, "Center") || Validator.CompareWithInvariantCulture(textAlign, "Right");
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000B1239 File Offset: 0x000AF439
		internal static bool ValidateVerticalAlign(string verticalAlign)
		{
			return Validator.CompareWithInvariantCulture(verticalAlign, "Default") || Validator.CompareWithInvariantCulture(verticalAlign, "Top") || Validator.CompareWithInvariantCulture(verticalAlign, "Middle") || Validator.CompareWithInvariantCulture(verticalAlign, "Bottom");
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000B1272 File Offset: 0x000AF472
		internal static bool ValidateDirection(string direction)
		{
			return Validator.CompareWithInvariantCulture(direction, "Default") || Validator.CompareWithInvariantCulture(direction, "LTR") || Validator.CompareWithInvariantCulture(direction, "RTL");
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000B129E File Offset: 0x000AF49E
		internal static bool ValidateWritingMode(string writingMode)
		{
			return Validator.CompareWithInvariantCulture(writingMode, "Default") || Validator.CompareWithInvariantCulture(writingMode, "Horizontal") || Validator.CompareWithInvariantCulture(writingMode, "Vertical") || Validator.CompareWithInvariantCulture(writingMode, "Rotate270");
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000B12D7 File Offset: 0x000AF4D7
		internal static bool ValidateUnicodeBiDi(string unicodeBiDi)
		{
			return Validator.CompareWithInvariantCulture(unicodeBiDi, "Normal") || Validator.CompareWithInvariantCulture(unicodeBiDi, "Embed") || Validator.CompareWithInvariantCulture(unicodeBiDi, "BiDiOverride");
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000B1304 File Offset: 0x000AF504
		internal static bool ValidateCalendar(string calendar)
		{
			return Validator.CompareWithInvariantCulture(calendar, "Default") || Validator.CompareWithInvariantCulture(calendar, "Gregorian") || Validator.CompareWithInvariantCulture(calendar, "GregorianUSEnglish") || Validator.CompareWithInvariantCulture(calendar, "GregorianArabic") || Validator.CompareWithInvariantCulture(calendar, "GregorianMiddleEastFrench") || Validator.CompareWithInvariantCulture(calendar, "GregorianTransliteratedEnglish") || Validator.CompareWithInvariantCulture(calendar, "GregorianTransliteratedFrench") || Validator.CompareWithInvariantCulture(calendar, "Hebrew") || Validator.CompareWithInvariantCulture(calendar, "Hijri") || Validator.CompareWithInvariantCulture(calendar, "Japanese") || Validator.CompareWithInvariantCulture(calendar, "Korean") || Validator.CompareWithInvariantCulture(calendar, "Taiwan") || Validator.CompareWithInvariantCulture(calendar, "ThaiBuddhist");
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000B13C6 File Offset: 0x000AF5C6
		internal static bool CompareWithInvariantCulture(string strOne, string strTwo)
		{
			return ReportProcessing.CompareWithInvariantCulture(strOne, strTwo, false) == 0;
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000B13D5 File Offset: 0x000AF5D5
		internal static bool CompareWithInvariantCultureIgnoreCase(string strOne, string strTwo)
		{
			return ReportProcessing.CompareWithInvariantCulture(strOne, strTwo, true) == 0;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000B13E4 File Offset: 0x000AF5E4
		internal static bool ValidateTextOrientations(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(val, "Auto") || Validator.CompareWithInvariantCulture(val, "Horizontal") || Validator.CompareWithInvariantCulture(val, "Rotated90") || Validator.CompareWithInvariantCulture(val, "Rotated270") || Validator.CompareWithInvariantCulture(val, "Stacked"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(val, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000B1440 File Offset: 0x000AF640
		private static void RegisterInvalidEnumValueError(string val, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (errorContext == null)
			{
				return;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidEnumValue, Severity.Error, context.ObjectType, context.ObjectName, propertyName, new string[] { val });
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000B1477 File Offset: 0x000AF677
		internal static bool ValidateTextRunMarkupType(string value)
		{
			return Validator.CompareWithInvariantCulture(value, "None") || Validator.CompareWithInvariantCulture(value, "HTML");
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000B1496 File Offset: 0x000AF696
		internal static bool ValidateParagraphListStyle(string value)
		{
			return Validator.CompareWithInvariantCulture(value, "None") || Validator.CompareWithInvariantCulture(value, "Numbered") || Validator.CompareWithInvariantCulture(value, "Bulleted");
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000B14C4 File Offset: 0x000AF6C4
		internal static bool ValidateTextBoxStructureTypeOverwrite(string value, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(value, "None") || Validator.CompareWithInvariantCulture(value, "Heading1") || Validator.CompareWithInvariantCulture(value, "Heading2") || Validator.CompareWithInvariantCulture(value, "Heading3") || Validator.CompareWithInvariantCulture(value, "Heading4") || Validator.CompareWithInvariantCulture(value, "Heading5") || Validator.CompareWithInvariantCulture(value, "Heading6"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(value, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000B1538 File Offset: 0x000AF738
		internal static bool ValidateTablixCellStructureTypeOverwrite(string value, ErrorContext errorContext, PublishingContextStruct context, string propertyName)
		{
			if (Validator.CompareWithInvariantCulture(value, "None") || Validator.CompareWithInvariantCulture(value, "ColumnHeaderCell") || Validator.CompareWithInvariantCulture(value, "RowHeaderCell") || Validator.CompareWithInvariantCulture(value, "DataCell"))
			{
				return true;
			}
			Validator.RegisterInvalidEnumValueError(value, errorContext, context, propertyName);
			return false;
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000B1585 File Offset: 0x000AF785
		internal static bool ValidateParagraphListLevel(int value, out int? adjustedValue)
		{
			if (value < 0)
			{
				adjustedValue = null;
				return false;
			}
			if (value > 9)
			{
				adjustedValue = new int?(9);
				return false;
			}
			adjustedValue = new int?(value);
			return true;
		}

		// Token: 0x04001563 RID: 5475
		internal const int DecimalPrecision = 10;

		// Token: 0x04001564 RID: 5476
		internal const int RoundingPrecision = 4;

		// Token: 0x04001565 RID: 5477
		internal const int TruncatePrecision = 3;

		// Token: 0x04001566 RID: 5478
		internal static double NormalMin = 0.0;

		// Token: 0x04001567 RID: 5479
		internal static double NegativeMin = -11557.0;

		// Token: 0x04001568 RID: 5480
		internal static double NormalMax = 11557.0;

		// Token: 0x04001569 RID: 5481
		internal static double BorderWidthMin = 0.08814;

		// Token: 0x0400156A RID: 5482
		internal static double BorderWidthMax = 7.055555555555555;

		// Token: 0x0400156B RID: 5483
		internal static double FontSizeMin = 0.35277777777777775;

		// Token: 0x0400156C RID: 5484
		internal static double FontSizeMax = 70.55555555555554;

		// Token: 0x0400156D RID: 5485
		internal static double PaddingMin = 0.0;

		// Token: 0x0400156E RID: 5486
		internal static double PaddingMax = 352.77777777777777;

		// Token: 0x0400156F RID: 5487
		internal static double LineHeightMin = 0.35277777777777775;

		// Token: 0x04001570 RID: 5488
		internal static double LineHeightMax = 352.77777777777777;

		// Token: 0x04001571 RID: 5489
		internal const int ParagraphListLevelMin = 0;

		// Token: 0x04001572 RID: 5490
		internal const int ParagraphListLevelMax = 9;

		// Token: 0x04001573 RID: 5491
		private static Regex m_colorRegex = new Regex("^#(\\d|a|b|c|d|e|f){6}$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04001574 RID: 5492
		private static Regex m_colorRegexTransparency = new Regex("^#(\\d|a|b|c|d|e|f){8}$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x02000954 RID: 2388
		internal class DoubleComparer : IComparer<double>
		{
			// Token: 0x06007FF3 RID: 32755 RVA: 0x002104B0 File Offset: 0x0020E6B0
			private DoubleComparer()
			{
			}

			// Token: 0x06007FF4 RID: 32756 RVA: 0x002104B8 File Offset: 0x0020E6B8
			public int Compare(double x, double y)
			{
				return Validator.CompareDoubles(x, y);
			}

			// Token: 0x17002979 RID: 10617
			// (get) Token: 0x06007FF5 RID: 32757 RVA: 0x002104C1 File Offset: 0x0020E6C1
			public static Validator.DoubleComparer Instance
			{
				get
				{
					if (Validator.DoubleComparer.m_instance == null)
					{
						Validator.DoubleComparer.m_instance = new Validator.DoubleComparer();
					}
					return Validator.DoubleComparer.m_instance;
				}
			}

			// Token: 0x04004076 RID: 16502
			private static Validator.DoubleComparer m_instance;
		}
	}
}
