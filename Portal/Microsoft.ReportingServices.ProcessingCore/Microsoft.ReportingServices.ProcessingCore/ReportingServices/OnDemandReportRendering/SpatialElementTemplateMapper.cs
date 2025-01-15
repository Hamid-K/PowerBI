using System;
using System.Drawing;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200016D RID: 365
	internal abstract class SpatialElementTemplateMapper
	{
		// Token: 0x06000F48 RID: 3912 RVA: 0x00042636 File Offset: 0x00040836
		internal SpatialElementTemplateMapper(MapMapper mapMapper, MapVectorLayer mapVectorLayer)
		{
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_mapMapper = mapMapper;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0004264C File Offset: 0x0004084C
		protected void RenderSpatialElementTemplate(MapSpatialElementTemplate mapSpatialElementTemplate, ISpatialElement coreSpatialElement, bool ignoreBackgroundColor, bool hasScope)
		{
			ReportStringProperty toolTip = mapSpatialElementTemplate.ToolTip;
			string text = null;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					text = toolTip.Value;
				}
				else if (hasScope)
				{
					text = mapSpatialElementTemplate.Instance.ToolTip;
				}
				if (text != null)
				{
					text = VectorLayerMapper.AddPrefixToFieldNames(this.m_mapVectorLayer.Name, text);
					coreSpatialElement.ToolTip = text;
				}
			}
			this.m_mapMapper.RenderActionInfo(mapSpatialElementTemplate.ActionInfo, text, coreSpatialElement, this.m_mapVectorLayer.Name, hasScope);
			ReportBoolProperty hidden = mapSpatialElementTemplate.Hidden;
			if (hidden != null)
			{
				if (!hidden.IsExpression)
				{
					coreSpatialElement.Visible = !hidden.Value;
				}
				else if (hasScope)
				{
					coreSpatialElement.Visible = !mapSpatialElementTemplate.Instance.Hidden;
				}
				else
				{
					coreSpatialElement.Visible = true;
				}
			}
			else
			{
				coreSpatialElement.Visible = true;
			}
			ReportStringProperty label = mapSpatialElementTemplate.Label;
			if (label != null)
			{
				string text2 = "";
				if (!label.IsExpression)
				{
					text2 = label.Value;
				}
				else if (hasScope)
				{
					text2 = mapSpatialElementTemplate.Instance.Label;
				}
				if (text2 != null)
				{
					coreSpatialElement.Text = VectorLayerMapper.AddPrefixToFieldNames(this.m_mapVectorLayer.Name, text2);
				}
			}
			ReportDoubleProperty reportDoubleProperty = mapSpatialElementTemplate.OffsetX;
			double num = 0.0;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					num = reportDoubleProperty.Value;
				}
				else if (hasScope)
				{
					num = mapSpatialElementTemplate.Instance.OffsetX;
				}
				coreSpatialElement.Offset.X = num;
			}
			reportDoubleProperty = mapSpatialElementTemplate.OffsetY;
			num = 0.0;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					num = reportDoubleProperty.Value;
				}
				else if (hasScope)
				{
					num = mapSpatialElementTemplate.Instance.OffsetY;
				}
				coreSpatialElement.Offset.Y = num;
			}
			Style style = mapSpatialElementTemplate.Style;
			StyleInstance style2 = mapSpatialElementTemplate.Instance.Style;
			this.RenderStyle(style, style2, coreSpatialElement, ignoreBackgroundColor, hasScope);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00042814 File Offset: 0x00040A14
		protected void RenderStyle(Style style, StyleInstance styleInstance, ISpatialElement coreSpatialElement, bool ignoreBackgroundColor, bool hasScope)
		{
			if (!ignoreBackgroundColor)
			{
				coreSpatialElement.Color = this.GetBackgroundColor(style, styleInstance, hasScope);
			}
			coreSpatialElement.SecondaryColor = this.GetBackGradientEndColor(style, styleInstance, hasScope);
			coreSpatialElement.GradientType = this.GetGradientType(style, styleInstance, hasScope);
			coreSpatialElement.HatchStyle = this.GetHatchStyle(style, styleInstance, hasScope);
			coreSpatialElement.ShadowOffset = this.GetShadowOffset(style, styleInstance, hasScope);
			coreSpatialElement.BorderColor = this.GetBorderColor(style, styleInstance, hasScope);
			coreSpatialElement.BorderWidth = this.GetBorderWidth(style, styleInstance, hasScope);
			coreSpatialElement.TextColor = this.GetTextColor(style, styleInstance, hasScope);
			coreSpatialElement.Font = this.GetFont(style, styleInstance, hasScope);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x000428B8 File Offset: 0x00040AB8
		internal Font GetFont(bool hasScope)
		{
			Style style;
			StyleInstance styleInstance;
			this.GetDefaultStyle(out style, out styleInstance);
			return this.GetFont(style, styleInstance, hasScope);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x000428D8 File Offset: 0x00040AD8
		internal Font GetFont(Style style, StyleInstance styleInstance, bool hasScope)
		{
			if (style == null)
			{
				return this.m_mapMapper.GetDefaultFontFromCache(0);
			}
			string text = MappingHelper.DefaultFontFamily;
			if (this.m_mapMapper.GetDefaultFont() != null)
			{
				text = this.m_mapMapper.GetDefaultFont().Name;
			}
			if (!MappingHelper.IsPropertyExpression(style.FontFamily) || hasScope)
			{
				text = MappingHelper.GetStyleFontFamily(style, styleInstance, text);
			}
			float num;
			if (!MappingHelper.IsPropertyExpression(style.FontSize) || hasScope)
			{
				num = MappingHelper.GetStyleFontSize(style, styleInstance);
			}
			else
			{
				num = MappingHelper.DefaultFontSize;
			}
			FontStyles fontStyles;
			if (!MappingHelper.IsPropertyExpression(style.FontStyle) || hasScope)
			{
				fontStyles = MappingHelper.GetStyleFontStyle(style, styleInstance);
			}
			else
			{
				fontStyles = FontStyles.Normal;
			}
			FontWeights fontWeights;
			if (!MappingHelper.IsPropertyExpression(style.FontWeight) || hasScope)
			{
				fontWeights = MappingHelper.GetStyleFontWeight(style, styleInstance);
			}
			else
			{
				fontWeights = FontWeights.Normal;
			}
			TextDecorations textDecorations;
			if (!MappingHelper.IsPropertyExpression(style.TextDecoration) || hasScope)
			{
				textDecorations = MappingHelper.GetStyleFontTextDecoration(style, styleInstance);
			}
			else
			{
				textDecorations = TextDecorations.None;
			}
			return this.m_mapMapper.GetFontFromCache(0, text, num, fontStyles, fontWeights, textDecorations);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x000429C4 File Offset: 0x00040BC4
		internal Color GetTextColor(bool hasScope)
		{
			Style style;
			StyleInstance styleInstance;
			this.GetDefaultStyle(out style, out styleInstance);
			return this.GetTextColor(style, styleInstance, hasScope);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x000429E4 File Offset: 0x00040BE4
		internal Color GetTextColor(Style style, StyleInstance styleInstance, bool hasScope)
		{
			if (style != null && (!MappingHelper.IsPropertyExpression(style.Color) || hasScope))
			{
				return MappingHelper.GetStyleColor(style, styleInstance);
			}
			return MappingHelper.DefaultColor;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00042A08 File Offset: 0x00040C08
		internal int GetShadowOffset(bool hasScope)
		{
			Style style;
			StyleInstance styleInstance;
			this.GetDefaultStyle(out style, out styleInstance);
			return this.GetShadowOffset(style, styleInstance, hasScope);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00042A28 File Offset: 0x00040C28
		internal int GetShadowOffset(Style style, StyleInstance styleInstance, bool hasScope)
		{
			if (style != null && (!MappingHelper.IsPropertyExpression(style.ShadowOffset) || hasScope))
			{
				return MapMapper.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, styleInstance, this.m_mapMapper.DpiX));
			}
			return 0;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00042A58 File Offset: 0x00040C58
		internal MapHatchStyle GetHatchStyle(bool hasScope)
		{
			Style style;
			StyleInstance styleInstance;
			this.GetDefaultStyle(out style, out styleInstance);
			return this.GetHatchStyle(style, styleInstance, hasScope);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00042A78 File Offset: 0x00040C78
		internal MapHatchStyle GetHatchStyle(Style style, StyleInstance styleInstance, bool hasScope)
		{
			if (style != null && (!MappingHelper.IsPropertyExpression(style.BackgroundHatchType) || hasScope))
			{
				return MapMapper.GetHatchStyle(style, styleInstance);
			}
			return 0;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00042A98 File Offset: 0x00040C98
		internal GradientType GetGradientType(bool hasScope)
		{
			Style style;
			StyleInstance styleInstance;
			this.GetDefaultStyle(out style, out styleInstance);
			return this.GetGradientType(style, styleInstance, hasScope);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00042AB8 File Offset: 0x00040CB8
		internal GradientType GetGradientType(Style style, StyleInstance styleInstance, bool hasScope)
		{
			if (style != null && (!MappingHelper.IsPropertyExpression(style.BackgroundGradientType) || hasScope))
			{
				return MapMapper.GetGradientType(style, styleInstance);
			}
			return 0;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00042AD8 File Offset: 0x00040CD8
		internal Color GetBackGradientEndColor(bool hasScope)
		{
			Style style;
			StyleInstance styleInstance;
			this.GetDefaultStyle(out style, out styleInstance);
			return this.GetBackGradientEndColor(style, styleInstance, hasScope);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00042AF8 File Offset: 0x00040CF8
		internal Color GetBackGradientEndColor(Style style, StyleInstance styleInstance, bool hasScope)
		{
			if (style != null && (!MappingHelper.IsPropertyExpression(style.BackgroundGradientEndColor) || hasScope))
			{
				return MappingHelper.GetStyleBackGradientEndColor(style, styleInstance);
			}
			return Color.Empty;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00042B1C File Offset: 0x00040D1C
		internal Color GetBackgroundColor(bool hasScope)
		{
			Style style;
			StyleInstance styleInstance;
			this.GetDefaultStyle(out style, out styleInstance);
			return this.GetBackgroundColor(style, styleInstance, hasScope);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00042B3C File Offset: 0x00040D3C
		internal Color GetBackgroundColor(Style style, StyleInstance styleInstance, bool hasScope)
		{
			if (style == null)
			{
				return MappingHelper.DefaultBackgroundColor;
			}
			if (!MappingHelper.IsPropertyExpression(style.BackgroundColor) || hasScope)
			{
				return MappingHelper.GetStyleBackgroundColor(style, styleInstance);
			}
			return MappingHelper.DefaultBackgroundColor;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00042B68 File Offset: 0x00040D68
		internal int GetBorderWidth(bool hasScope)
		{
			Style style;
			StyleInstance styleInstance;
			this.GetDefaultStyle(out style, out styleInstance);
			return this.GetBorderWidth(style, styleInstance, hasScope);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00042B88 File Offset: 0x00040D88
		internal int GetBorderWidth(Style style, StyleInstance styleInstance, bool hasScope)
		{
			if (style != null)
			{
				Border border = style.Border;
				if (border != null && (!MappingHelper.IsPropertyExpression(border.Width) || hasScope))
				{
					return MappingHelper.GetStyleBorderWidth(border, this.m_mapMapper.DpiX);
				}
			}
			return MappingHelper.GetDefaultBorderWidth(this.m_mapMapper.DpiX);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00042BD8 File Offset: 0x00040DD8
		internal Color GetBorderColor(bool hasScope)
		{
			Style style;
			StyleInstance styleInstance;
			this.GetDefaultStyle(out style, out styleInstance);
			return this.GetBorderColor(style, styleInstance, hasScope);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00042BF8 File Offset: 0x00040DF8
		internal Color GetBorderColor(Style style, StyleInstance styleInstance, bool hasScope)
		{
			if (style != null)
			{
				Border border = style.Border;
				if (border != null && (!MappingHelper.IsPropertyExpression(border.Color) || hasScope))
				{
					return MappingHelper.GetStyleBorderColor(border);
				}
			}
			return MappingHelper.DefaultBorderColor;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00042C30 File Offset: 0x00040E30
		internal MapDashStyle GetBorderStyle(bool hasScope)
		{
			Style style;
			StyleInstance styleInstance;
			this.GetDefaultStyle(out style, out styleInstance);
			return this.GetBorderStyle(style, styleInstance, hasScope);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00042C50 File Offset: 0x00040E50
		internal MapDashStyle GetBorderStyle(Style style, StyleInstance styleInstance, bool hasScope)
		{
			if (style != null)
			{
				Border border = style.Border;
				if (border != null)
				{
					return MapMapper.GetDashStyle(border, hasScope, false);
				}
			}
			return 5;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00042C74 File Offset: 0x00040E74
		private void GetDefaultStyle(out Style style, out StyleInstance styleInstance)
		{
			MapSpatialElementTemplate defaultTemplate = this.DefaultTemplate;
			if (defaultTemplate == null)
			{
				style = null;
				styleInstance = null;
				return;
			}
			style = defaultTemplate.Style;
			styleInstance = defaultTemplate.Instance.Style;
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06000F60 RID: 3936
		protected abstract MapSpatialElementTemplate DefaultTemplate { get; }

		// Token: 0x0400072B RID: 1835
		protected MapVectorLayer m_mapVectorLayer;

		// Token: 0x0400072C RID: 1836
		protected MapMapper m_mapMapper;
	}
}
