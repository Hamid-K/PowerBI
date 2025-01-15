using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000194 RID: 404
	public sealed class MapLegendTitle : IROMStyleDefinitionContainer
	{
		// Token: 0x06001068 RID: 4200 RVA: 0x00045B21 File Offset: 0x00043D21
		internal MapLegendTitle(MapLegendTitle defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00045B37 File Offset: 0x00043D37
		public Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new Style(this.m_map, this.m_map.ReportScope, this.m_defObject, this.m_map.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x00045B74 File Offset: 0x00043D74
		public ReportStringProperty Caption
		{
			get
			{
				if (this.m_caption == null && this.m_defObject.Caption != null)
				{
					this.m_caption = new ReportStringProperty(this.m_defObject.Caption);
				}
				return this.m_caption;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00045BA8 File Offset: 0x00043DA8
		public ReportEnumProperty<MapLegendTitleSeparator> TitleSeparator
		{
			get
			{
				if (this.m_titleSeparator == null && this.m_defObject.TitleSeparator != null)
				{
					this.m_titleSeparator = new ReportEnumProperty<MapLegendTitleSeparator>(this.m_defObject.TitleSeparator.IsExpression, this.m_defObject.TitleSeparator.OriginalText, EnumTranslator.TranslateMapLegendTitleSeparator(this.m_defObject.TitleSeparator.StringValue, null));
				}
				return this.m_titleSeparator;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x00045C14 File Offset: 0x00043E14
		public ReportColorProperty TitleSeparatorColor
		{
			get
			{
				if (this.m_titleSeparatorColor == null && this.m_defObject.TitleSeparatorColor != null)
				{
					ExpressionInfo titleSeparatorColor = this.m_defObject.TitleSeparatorColor;
					if (titleSeparatorColor != null)
					{
						this.m_titleSeparatorColor = new ReportColorProperty(titleSeparatorColor.IsExpression, this.m_defObject.TitleSeparatorColor.OriginalText, titleSeparatorColor.IsExpression ? null : new ReportColor(titleSeparatorColor.StringValue.Trim(), true), titleSeparatorColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_titleSeparatorColor;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x00045CA3 File Offset: 0x00043EA3
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00045CAB File Offset: 0x00043EAB
		internal MapLegendTitle MapLegendTitleDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x00045CB3 File Offset: 0x00043EB3
		public MapLegendTitleInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapLegendTitleInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00045CE3 File Offset: 0x00043EE3
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
		}

		// Token: 0x040007AA RID: 1962
		private Map m_map;

		// Token: 0x040007AB RID: 1963
		private MapLegendTitle m_defObject;

		// Token: 0x040007AC RID: 1964
		private MapLegendTitleInstance m_instance;

		// Token: 0x040007AD RID: 1965
		private Style m_style;

		// Token: 0x040007AE RID: 1966
		private ReportStringProperty m_caption;

		// Token: 0x040007AF RID: 1967
		private ReportEnumProperty<MapLegendTitleSeparator> m_titleSeparator;

		// Token: 0x040007B0 RID: 1968
		private ReportColorProperty m_titleSeparatorColor;
	}
}
