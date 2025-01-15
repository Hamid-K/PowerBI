using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200024F RID: 591
	public sealed class ChartBorderSkin : IROMStyleDefinitionContainer
	{
		// Token: 0x060016D9 RID: 5849 RVA: 0x0005BFBC File Offset: 0x0005A1BC
		internal ChartBorderSkin(ChartBorderSkin chartBorderSkinDef, Chart chart)
		{
			this.m_chartBorderSkinDef = chartBorderSkinDef;
			this.m_chart = chart;
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0005BFD4 File Offset: 0x0005A1D4
		public Style Style
		{
			get
			{
				if (this.m_style == null && !this.m_chart.IsOldSnapshot && this.m_chartBorderSkinDef.StyleClass != null)
				{
					this.m_style = new Style(this.m_chart, this.m_chart, this.m_chartBorderSkinDef, this.m_chart.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x0005C034 File Offset: 0x0005A234
		public ReportEnumProperty<ChartBorderSkinType> BorderSkinType
		{
			get
			{
				if (this.m_borderSkinType == null && !this.m_chart.IsOldSnapshot && this.m_chartBorderSkinDef.BorderSkinType != null)
				{
					this.m_borderSkinType = new ReportEnumProperty<ChartBorderSkinType>(this.m_chartBorderSkinDef.BorderSkinType.IsExpression, this.m_chartBorderSkinDef.BorderSkinType.OriginalText, EnumTranslator.TranslateChartBorderSkinType(this.m_chartBorderSkinDef.BorderSkinType.StringValue, null));
				}
				return this.m_borderSkinType;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x0005C0AA File Offset: 0x0005A2AA
		internal Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0005C0B2 File Offset: 0x0005A2B2
		internal ChartBorderSkin ChartBorderSkinDef
		{
			get
			{
				return this.m_chartBorderSkinDef;
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x0005C0BA File Offset: 0x0005A2BA
		public ChartBorderSkinInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartBorderSkinInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0005C0EA File Offset: 0x0005A2EA
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

		// Token: 0x04000B24 RID: 2852
		private Chart m_chart;

		// Token: 0x04000B25 RID: 2853
		private ChartBorderSkin m_chartBorderSkinDef;

		// Token: 0x04000B26 RID: 2854
		private ChartBorderSkinInstance m_instance;

		// Token: 0x04000B27 RID: 2855
		private Style m_style;

		// Token: 0x04000B28 RID: 2856
		private ReportEnumProperty<ChartBorderSkinType> m_borderSkinType;
	}
}
