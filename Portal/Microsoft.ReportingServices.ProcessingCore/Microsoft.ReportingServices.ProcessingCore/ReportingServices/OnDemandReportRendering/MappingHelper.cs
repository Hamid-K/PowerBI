using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000139 RID: 313
	internal static class MappingHelper
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003A370 File Offset: 0x00038570
		internal static double ConvertToDouble(object value, bool checkForMaxMinValue, bool checkForStringDate)
		{
			bool flag = false;
			return MappingHelper.ConvertToDouble(value, checkForMaxMinValue, checkForStringDate, ref flag);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003A38C File Offset: 0x0003858C
		internal static double ConvertToDouble(object value, bool checkForMaxMinValue, bool checkForStringDate, ref bool isDateTime)
		{
			if (value == null)
			{
				return double.NaN;
			}
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Char:
				return (double)((char)value);
			case TypeCode.SByte:
				return (double)((sbyte)value);
			case TypeCode.Byte:
				return (double)((byte)value);
			case TypeCode.Int16:
				return (double)((short)value);
			case TypeCode.UInt16:
				return (double)((ushort)value);
			case TypeCode.Int32:
				return (double)((int)value);
			case TypeCode.UInt32:
				return (uint)value;
			case TypeCode.Int64:
				return (double)((long)value);
			case TypeCode.UInt64:
				return (ulong)value;
			case TypeCode.Single:
				return (double)((float)value);
			case TypeCode.Double:
				return (double)value;
			case TypeCode.Decimal:
				return decimal.ToDouble((decimal)value);
			case TypeCode.DateTime:
				isDateTime = true;
				return ((DateTime)value).ToOADate();
			case TypeCode.String:
			{
				double naN = double.NaN;
				string text = value.ToString().Trim();
				if (checkForStringDate)
				{
					DateTime minValue = DateTime.MinValue;
					if (DateTime.TryParse(text, out minValue))
					{
						isDateTime = true;
						return minValue.ToOADate();
					}
				}
				if (double.TryParse(text, out naN))
				{
					return naN;
				}
				if (checkForMaxMinValue)
				{
					if (text == "MaxValue")
					{
						return double.MaxValue;
					}
					if (text == "MinValue")
					{
						return double.MinValue;
					}
				}
				break;
			}
			}
			return double.NaN;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003A4F0 File Offset: 0x000386F0
		internal static float[] ConvertCoordinatesToRelative(float[] pixelCoordinates, float width, float height)
		{
			float[] array = new float[pixelCoordinates.Length];
			for (int i = 0; i < array.Length; i += 2)
			{
				array[i] = pixelCoordinates[i] / width * 100f;
				if (i + 1 < array.Length)
				{
					array[i + 1] = pixelCoordinates[i + 1] / height * 100f;
				}
			}
			return array;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0003A540 File Offset: 0x00038740
		internal static float[] ConvertCoordinatesToRelative(int[] pixelCoordinates, float width, float height)
		{
			float[] array = new float[pixelCoordinates.Length];
			for (int i = 0; i < array.Length; i += 2)
			{
				array[i] = (float)pixelCoordinates[i] / width * 100f;
				if (i + 1 < array.Length)
				{
					array[i + 1] = (float)pixelCoordinates[i + 1] / height * 100f;
				}
			}
			return array;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003A58F File Offset: 0x0003878F
		private static Action GetActionFromActionInfo(ActionInfo actionInfo)
		{
			if (actionInfo == null)
			{
				return null;
			}
			if (actionInfo.Actions == null)
			{
				return null;
			}
			if (actionInfo.Actions.Count == 0)
			{
				return null;
			}
			return actionInfo.Actions[0];
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003A5BC File Offset: 0x000387BC
		private static string EvaluateHref(Action action, out bool isExpression)
		{
			isExpression = false;
			if (action.Hyperlink != null)
			{
				if (!action.Hyperlink.IsExpression)
				{
					if (action.Hyperlink.Value != null)
					{
						return action.Hyperlink.Value.ToString();
					}
				}
				else
				{
					isExpression = true;
					if (action.Instance != null && action.Instance.Hyperlink != null)
					{
						return action.Instance.Hyperlink.ToString();
					}
				}
			}
			else if (action.Drillthrough != null && action.Drillthrough.ReportName != null)
			{
				if (!action.Drillthrough.ReportName.IsExpression)
				{
					if (action.Drillthrough.ReportName.Value != null)
					{
						return action.Drillthrough.ReportName.Value;
					}
				}
				else
				{
					isExpression = true;
					if (action.Drillthrough.Instance != null && action.Drillthrough.Instance.ReportName != null)
					{
						return action.Drillthrough.Instance.ReportName;
					}
				}
			}
			if (action.BookmarkLink != null)
			{
				if (!action.BookmarkLink.IsExpression)
				{
					if (action.BookmarkLink.Value != null)
					{
						return action.BookmarkLink.Value;
					}
				}
				else
				{
					isExpression = true;
					if (action.Instance != null && action.Instance.BookmarkLink != null)
					{
						return action.Instance.BookmarkLink;
					}
				}
			}
			return null;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003A704 File Offset: 0x00038904
		private static void EvaluateActionParameters(ActionDrillthrough actionDrillthroughSource, ActionDrillthrough actionDrillthroughDestination)
		{
			if (actionDrillthroughSource.Parameters == null)
			{
				return;
			}
			foreach (Parameter parameter in actionDrillthroughSource.Parameters)
			{
				Parameter parameter2 = actionDrillthroughDestination.CreateParameter(parameter.Name);
				if (!parameter.Value.IsExpression)
				{
					parameter2.Instance.Value = parameter.Value.Value;
				}
				else
				{
					parameter2.Instance.Value = parameter.Instance.Value;
				}
				if (!parameter.Omit.IsExpression)
				{
					parameter2.Instance.Omit = parameter.Omit.Value;
				}
				else
				{
					parameter2.Instance.Omit = parameter.Instance.Omit;
				}
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003A7DC File Offset: 0x000389DC
		internal static ActionInfoWithDynamicImageMap CreateActionInfoDynamic(ReportItem reportItem, ActionInfo actionInfo, string toolTip, out string href)
		{
			return MappingHelper.CreateActionInfoDynamic(reportItem, actionInfo, toolTip, out href, true);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0003A7E8 File Offset: 0x000389E8
		internal static ActionInfoWithDynamicImageMap CreateActionInfoDynamic(ReportItem reportItem, ActionInfo actionInfo, string toolTip, out string href, bool applyExpression)
		{
			Action actionFromActionInfo = MappingHelper.GetActionFromActionInfo(actionInfo);
			if (actionFromActionInfo == null)
			{
				href = null;
			}
			else
			{
				bool flag;
				href = MappingHelper.EvaluateHref(actionFromActionInfo, out flag);
				if (flag && !applyExpression)
				{
					href = null;
				}
			}
			bool flag2 = actionFromActionInfo == null || href == null;
			bool flag3 = string.IsNullOrEmpty(toolTip);
			if (flag2 && flag3)
			{
				return null;
			}
			ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap;
			if (!flag2)
			{
				actionInfoWithDynamicImageMap = new ActionInfoWithDynamicImageMap(reportItem.RenderingContext, reportItem, actionInfo.ReportScope, actionInfo.InstancePath, actionInfo.ROMActionOwner, true);
				if (actionFromActionInfo.BookmarkLink != null)
				{
					actionInfoWithDynamicImageMap.CreateBookmarkLinkAction().Instance.BookmarkLink = href;
				}
				else if (actionFromActionInfo.Hyperlink != null)
				{
					actionInfoWithDynamicImageMap.CreateHyperlinkAction().Instance.HyperlinkText = href;
				}
				else if (actionFromActionInfo.Drillthrough != null)
				{
					Action action = actionInfoWithDynamicImageMap.CreateDrillthroughAction();
					action.Drillthrough.Instance.ReportName = href;
					MappingHelper.EvaluateActionParameters(actionFromActionInfo.Drillthrough, action.Drillthrough);
					string drillthroughID = action.Drillthrough.Instance.DrillthroughID;
				}
			}
			else
			{
				actionInfoWithDynamicImageMap = new ActionInfoWithDynamicImageMap(reportItem.RenderingContext, reportItem, reportItem.ReportScope, reportItem.ReportItemDef, null, true);
			}
			return actionInfoWithDynamicImageMap;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0003A8FC File Offset: 0x00038AFC
		internal static ActionInfoWithDynamicImageMapCollection GetImageMaps(IEnumerable<MappingHelper.MapAreaInfo> mapAreaInfoList, ActionInfoWithDynamicImageMapCollection actions, ReportItem reportItem)
		{
			List<ActionInfoWithDynamicImageMap> list = new List<ActionInfoWithDynamicImageMap>();
			bool[] array = new bool[actions.Count];
			foreach (MappingHelper.MapAreaInfo mapAreaInfo in mapAreaInfoList)
			{
				int num = MappingHelper.AddMapArea(mapAreaInfo, actions, reportItem);
				if (num > -1 && !array[num])
				{
					list.Add(actions[num]);
					array[num] = true;
				}
				else if (!string.IsNullOrEmpty(mapAreaInfo.ToolTip))
				{
					string text;
					ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap = MappingHelper.CreateActionInfoDynamic(reportItem, null, mapAreaInfo.ToolTip, out text);
					if (actionInfoWithDynamicImageMap != null)
					{
						actionInfoWithDynamicImageMap.CreateImageMapAreaInstance(mapAreaInfo.MapAreaShape, mapAreaInfo.Coordinates, mapAreaInfo.ToolTip);
						list.Add(actionInfoWithDynamicImageMap);
					}
				}
			}
			actions.InternalList.Clear();
			actions.InternalList.AddRange(list);
			if (actions.Count == 0)
			{
				return null;
			}
			return actions;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003A9E0 File Offset: 0x00038BE0
		private static int AddMapArea(MappingHelper.MapAreaInfo mapAreaInfo, ActionInfoWithDynamicImageMapCollection actions, ReportItem reportItem)
		{
			if (mapAreaInfo.Tag == null)
			{
				return -1;
			}
			int num = (int)mapAreaInfo.Tag;
			ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap = actions.InternalList[num];
			if (actionInfoWithDynamicImageMap.Actions.Count > 0 || !string.IsNullOrEmpty(mapAreaInfo.ToolTip))
			{
				actionInfoWithDynamicImageMap.CreateImageMapAreaInstance(mapAreaInfo.MapAreaShape, mapAreaInfo.Coordinates, mapAreaInfo.ToolTip);
				return num;
			}
			return -1;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003AA48 File Offset: 0x00038C48
		internal static Color GetStyleColor(Style style, StyleInstance styleInstance)
		{
			ReportColorProperty color = style.Color;
			Color color2 = Color.Black;
			if (!MappingHelper.GetColorFromReportColorProperty(color, ref color2))
			{
				ReportColor color3 = styleInstance.Color;
				if (color3 != null)
				{
					color2 = color3.ToColor();
				}
			}
			return color2;
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003AA7C File Offset: 0x00038C7C
		internal static Color GetStyleBackgroundColor(Style style, StyleInstance styleInstance)
		{
			ReportColorProperty backgroundColor = style.BackgroundColor;
			Color color = Color.Empty;
			if (!MappingHelper.GetColorFromReportColorProperty(backgroundColor, ref color))
			{
				ReportColor backgroundColor2 = styleInstance.BackgroundColor;
				if (backgroundColor2 != null)
				{
					color = backgroundColor2.ToColor();
				}
			}
			return color;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003AAB0 File Offset: 0x00038CB0
		internal static Color GetStyleBackGradientEndColor(Style style, StyleInstance styleInstance)
		{
			ReportColorProperty backgroundGradientEndColor = style.BackgroundGradientEndColor;
			Color color = Color.Empty;
			if (!MappingHelper.GetColorFromReportColorProperty(backgroundGradientEndColor, ref color))
			{
				ReportColor backgroundGradientEndColor2 = styleInstance.BackgroundGradientEndColor;
				if (backgroundGradientEndColor2 != null)
				{
					color = backgroundGradientEndColor2.ToColor();
				}
			}
			return color;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003AAE4 File Offset: 0x00038CE4
		internal static Color GetStyleShadowColor(Style style, StyleInstance styleInstance)
		{
			ReportColorProperty shadowColor = style.ShadowColor;
			Color color = MappingHelper.m_defaultShadowColor;
			if (!MappingHelper.GetColorFromReportColorProperty(shadowColor, ref color))
			{
				ReportColor shadowColor2 = styleInstance.ShadowColor;
				if (shadowColor2 != null)
				{
					color = shadowColor2.ToColor();
				}
			}
			return color;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003AB18 File Offset: 0x00038D18
		internal static BackgroundGradients GetStyleBackGradientType(Style style, StyleInstance styleInstance)
		{
			ReportEnumProperty<BackgroundGradients> backgroundGradientType = style.BackgroundGradientType;
			if (!backgroundGradientType.IsExpression)
			{
				return backgroundGradientType.Value;
			}
			return styleInstance.BackgroundGradientType;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0003AB44 File Offset: 0x00038D44
		internal static BackgroundHatchTypes GetStyleBackgroundHatchType(Style style, StyleInstance styleInstance)
		{
			ReportEnumProperty<BackgroundHatchTypes> backgroundHatchType = style.BackgroundHatchType;
			if (!backgroundHatchType.IsExpression)
			{
				return backgroundHatchType.Value;
			}
			return styleInstance.BackgroundHatchType;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003AB70 File Offset: 0x00038D70
		internal static int GetStyleShadowOffset(Style style, StyleInstance styleInstance, float dpi)
		{
			ReportSizeProperty shadowOffset = style.ShadowOffset;
			if (!shadowOffset.IsExpression)
			{
				return MappingHelper.ToIntPixels(shadowOffset.Value, dpi);
			}
			ReportSize shadowOffset2 = styleInstance.ShadowOffset;
			if (shadowOffset2 != null)
			{
				return MappingHelper.ToIntPixels(shadowOffset2, dpi);
			}
			return 0;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003ABAC File Offset: 0x00038DAC
		internal static Font GetDefaultFont()
		{
			return new Font(MappingHelper.DefaultFontFamily, MappingHelper.DefaultFontSize, MappingHelper.GetStyleFontStyle(FontStyles.Normal, FontWeights.Normal, TextDecorations.None));
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003ABC8 File Offset: 0x00038DC8
		internal static TextDecorations GetStyleFontTextDecoration(Style style, StyleInstance styleInstance)
		{
			ReportEnumProperty<TextDecorations> textDecoration = style.TextDecoration;
			if (!MappingHelper.IsStylePropertyDefined(textDecoration))
			{
				return TextDecorations.None;
			}
			if (!textDecoration.IsExpression)
			{
				return textDecoration.Value;
			}
			return styleInstance.TextDecoration;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003ABFC File Offset: 0x00038DFC
		internal static FontWeights GetStyleFontWeight(Style style, StyleInstance styleInstance)
		{
			ReportEnumProperty<FontWeights> fontWeight = style.FontWeight;
			if (!MappingHelper.IsStylePropertyDefined(fontWeight))
			{
				return FontWeights.Normal;
			}
			if (!fontWeight.IsExpression)
			{
				return fontWeight.Value;
			}
			return styleInstance.FontWeight;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003AC30 File Offset: 0x00038E30
		internal static FontStyles GetStyleFontStyle(Style style, StyleInstance styleInstance)
		{
			ReportEnumProperty<FontStyles> fontStyle = style.FontStyle;
			if (!MappingHelper.IsStylePropertyDefined(fontStyle))
			{
				return FontStyles.Normal;
			}
			if (!fontStyle.IsExpression)
			{
				return fontStyle.Value;
			}
			return styleInstance.FontStyle;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003AC64 File Offset: 0x00038E64
		internal static float GetStyleFontSize(Style style, StyleInstance styleInstance)
		{
			ReportSizeProperty fontSize = style.FontSize;
			if (MappingHelper.IsStylePropertyDefined(fontSize))
			{
				if (!fontSize.IsExpression)
				{
					return (float)fontSize.Value.ToPoints();
				}
				if (styleInstance.FontSize != null)
				{
					ReportSize fontSize2 = styleInstance.FontSize;
					if (fontSize2 != null)
					{
						return (float)fontSize2.ToPoints();
					}
				}
			}
			return MappingHelper.DefaultFontSize;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0003ACB4 File Offset: 0x00038EB4
		internal static string GetStyleFontFamily(Style style, StyleInstance styleInstance, string fallbackFont)
		{
			ReportStringProperty fontFamily = style.FontFamily;
			if (MappingHelper.IsStylePropertyDefined(fontFamily))
			{
				if (!fontFamily.IsExpression)
				{
					if (fontFamily != null)
					{
						return fontFamily.Value;
					}
				}
				else if (styleInstance.FontFamily != null)
				{
					return styleInstance.FontFamily;
				}
			}
			return fallbackFont;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0003ACF4 File Offset: 0x00038EF4
		internal static FontStyle GetStyleFontStyle(FontStyles style, FontWeights weight, TextDecorations textDecoration)
		{
			FontStyle fontStyle = FontStyle.Regular;
			if (style == FontStyles.Italic)
			{
				fontStyle = FontStyle.Italic;
			}
			if (weight - FontWeights.Thin > 3 && weight - FontWeights.SemiBold <= 3)
			{
				fontStyle |= FontStyle.Bold;
			}
			switch (textDecoration)
			{
			case TextDecorations.Underline:
				fontStyle |= FontStyle.Underline;
				break;
			case TextDecorations.LineThrough:
				fontStyle |= FontStyle.Strikeout;
				break;
			}
			return fontStyle;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0003AD40 File Offset: 0x00038F40
		internal static Color GetStyleBorderColor(Border border)
		{
			ReportColorProperty color = border.Color;
			Color color2 = Color.Black;
			if (!MappingHelper.GetColorFromReportColorProperty(color, ref color2))
			{
				ReportColor color3 = border.Instance.Color;
				if (color3 != null)
				{
					color2 = color3.ToColor();
				}
			}
			return color2;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003AD7C File Offset: 0x00038F7C
		internal static int GetStyleBorderWidth(Border border, float dpi)
		{
			ReportSizeProperty width = border.Width;
			int num = MappingHelper.GetDefaultBorderWidth(dpi);
			if (!width.IsExpression)
			{
				if (width.Value != null)
				{
					num = MappingHelper.ToIntPixels(width.Value, dpi);
				}
			}
			else
			{
				ReportSize width2 = border.Instance.Width;
				if (width2 != null)
				{
					num = MappingHelper.ToIntPixels(width2, dpi);
				}
			}
			return num;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003ADD0 File Offset: 0x00038FD0
		internal static BorderStyles GetStyleBorderStyle(Border border)
		{
			ReportEnumProperty<BorderStyles> style = border.Style;
			if (!style.IsExpression)
			{
				return style.Value;
			}
			return border.Instance.Style;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003AE00 File Offset: 0x00039000
		internal static TextAlignments GetStyleTextAlign(Style style, StyleInstance styleInstance)
		{
			ReportEnumProperty<TextAlignments> textAlign = style.TextAlign;
			if (!textAlign.IsExpression)
			{
				return textAlign.Value;
			}
			return styleInstance.TextAlign;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003AE2C File Offset: 0x0003902C
		internal static VerticalAlignments GetStyleVerticalAlignment(Style style, StyleInstance styleInstance)
		{
			ReportEnumProperty<VerticalAlignments> verticalAlign = style.VerticalAlign;
			if (!verticalAlign.IsExpression)
			{
				return verticalAlign.Value;
			}
			return styleInstance.VerticalAlign;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003AE58 File Offset: 0x00039058
		internal static TextEffects GetStyleTextEffect(Style style, StyleInstance styleInstance)
		{
			ReportEnumProperty<TextEffects> textEffect = style.TextEffect;
			if (!textEffect.IsExpression)
			{
				return textEffect.Value;
			}
			return styleInstance.TextEffect;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003AE84 File Offset: 0x00039084
		internal static string GetStyleFormat(Style style, StyleInstance styleInstance)
		{
			ReportStringProperty format = style.Format;
			string text = null;
			if (!format.IsExpression)
			{
				if (format.Value != null)
				{
					text = format.Value;
				}
			}
			else if (styleInstance.Format != null)
			{
				text = styleInstance.Format;
			}
			if (text == null)
			{
				return "";
			}
			return text;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003AECC File Offset: 0x000390CC
		internal static ContentAlignment GetStyleContentAlignment(Style style, StyleInstance styleInstance)
		{
			TextAlignments styleTextAlign = MappingHelper.GetStyleTextAlign(style, styleInstance);
			VerticalAlignments styleVerticalAlignment = MappingHelper.GetStyleVerticalAlignment(style, styleInstance);
			ContentAlignment contentAlignment = ContentAlignment.TopLeft;
			if (styleTextAlign != TextAlignments.Center)
			{
				if (styleTextAlign != TextAlignments.Right)
				{
					if (styleVerticalAlignment != VerticalAlignments.Middle)
					{
						if (styleVerticalAlignment == VerticalAlignments.Bottom)
						{
							contentAlignment = ContentAlignment.BottomLeft;
						}
					}
					else
					{
						contentAlignment = ContentAlignment.MiddleLeft;
					}
				}
				else if (styleVerticalAlignment != VerticalAlignments.Middle)
				{
					if (styleVerticalAlignment == VerticalAlignments.Bottom)
					{
						contentAlignment = ContentAlignment.BottomRight;
					}
					else
					{
						contentAlignment = ContentAlignment.TopRight;
					}
				}
				else
				{
					contentAlignment = ContentAlignment.MiddleRight;
				}
			}
			else if (styleVerticalAlignment != VerticalAlignments.Middle)
			{
				if (styleVerticalAlignment == VerticalAlignments.Bottom)
				{
					contentAlignment = ContentAlignment.BottomCenter;
				}
				else
				{
					contentAlignment = ContentAlignment.TopCenter;
				}
			}
			else
			{
				contentAlignment = ContentAlignment.MiddleCenter;
			}
			return contentAlignment;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003AF3B File Offset: 0x0003913B
		internal static bool IsStylePropertyDefined(ReportProperty property)
		{
			return property != null && property.ExpressionString != null;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003AF4B File Offset: 0x0003914B
		internal static bool IsPropertyExpression(ReportProperty property)
		{
			return property != null && property.IsExpression;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003AF58 File Offset: 0x00039158
		internal static bool GetColorFromReportColorProperty(ReportColorProperty reportColorProperty, ref Color color)
		{
			if (reportColorProperty.IsExpression || reportColorProperty.Value == null)
			{
				return false;
			}
			color = reportColorProperty.Value.ToColor();
			return true;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003AF7E File Offset: 0x0003917E
		internal static RightToLeft GetStyleDirection(Style style, StyleInstance styleInstance)
		{
			if ((style.Direction.IsExpression ? styleInstance.Direction : style.Direction.Value) == Directions.RTL)
			{
				return RightToLeft.Yes;
			}
			return RightToLeft.No;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003AFA6 File Offset: 0x000391A6
		internal static double ToPixels(ReportSize size, float dpi)
		{
			return size.ToInches() * (double)dpi;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0003AFB1 File Offset: 0x000391B1
		internal static int ToIntPixels(ReportSize size, float dpi)
		{
			return Convert.ToInt32(MappingHelper.ToPixels(size, dpi));
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0003AFC0 File Offset: 0x000391C0
		internal static double ToPixels(double value, Unit unit, float dpi)
		{
			switch (unit)
			{
			case Unit.Point:
				value /= 72.0;
				break;
			case Unit.Centimeter:
				value /= 2.54;
				break;
			case Unit.Millimeter:
				value /= 25.4;
				break;
			case Unit.Pica:
				value /= 6.0;
				break;
			}
			return value * (double)dpi;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0003B025 File Offset: 0x00039225
		internal static int ToIntPixels(double value, Unit unit, float dpi)
		{
			return Convert.ToInt32(MappingHelper.ToPixels(value, unit, dpi));
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0003B034 File Offset: 0x00039234
		internal static Color DefaultBackgroundColor
		{
			get
			{
				return Color.Empty;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0003B03B File Offset: 0x0003923B
		internal static Color DefaultBorderColor
		{
			get
			{
				return Color.Black;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0003B042 File Offset: 0x00039242
		internal static Color DefaultColor
		{
			get
			{
				return Color.Black;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x0003B049 File Offset: 0x00039249
		internal static string DefaultFontFamily
		{
			get
			{
				return "Arial";
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0003B050 File Offset: 0x00039250
		internal static float DefaultFontSize
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003B057 File Offset: 0x00039257
		internal static int GetDefaultBorderWidth(float dpi)
		{
			return (int)Math.Round(0.013888888888888888 * (double)dpi);
		}

		// Token: 0x04000645 RID: 1605
		private static Color m_defaultShadowColor = Color.FromArgb(0, 0, 0, 127);

		// Token: 0x0200092F RID: 2351
		internal struct MapAreaInfo
		{
			// Token: 0x06007F70 RID: 32624 RVA: 0x0020D77D File Offset: 0x0020B97D
			public MapAreaInfo(string toolTip, object tag, ImageMapArea.ImageMapAreaShape mapAreaShape, float[] coordinates)
			{
				this.ToolTip = toolTip;
				this.MapAreaShape = mapAreaShape;
				this.Tag = tag;
				this.Coordinates = coordinates;
			}

			// Token: 0x04003F9E RID: 16286
			internal string ToolTip;

			// Token: 0x04003F9F RID: 16287
			internal ImageMapArea.ImageMapAreaShape MapAreaShape;

			// Token: 0x04003FA0 RID: 16288
			internal object Tag;

			// Token: 0x04003FA1 RID: 16289
			internal float[] Coordinates;
		}

		// Token: 0x02000930 RID: 2352
		internal struct ActionParameterInfo
		{
			// Token: 0x04003FA2 RID: 16290
			public string Name;

			// Token: 0x04003FA3 RID: 16291
			public object Value;

			// Token: 0x04003FA4 RID: 16292
			public bool Omit;
		}

		// Token: 0x02000931 RID: 2353
		internal class ActionTag
		{
			// Token: 0x04003FA5 RID: 16293
			public Action Action;

			// Token: 0x04003FA6 RID: 16294
			public List<MappingHelper.ActionParameterInfo> Parameters = new List<MappingHelper.ActionParameterInfo>();
		}
	}
}
