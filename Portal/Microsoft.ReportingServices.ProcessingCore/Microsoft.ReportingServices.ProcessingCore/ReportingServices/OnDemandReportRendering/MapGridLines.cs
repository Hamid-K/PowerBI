using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001EF RID: 495
	public sealed class MapGridLines : IROMStyleDefinitionContainer
	{
		// Token: 0x0600129F RID: 4767 RVA: 0x0004BF00 File Offset: 0x0004A100
		internal MapGridLines(MapGridLines defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0004BF16 File Offset: 0x0004A116
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

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x0004BF53 File Offset: 0x0004A153
		public ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_hidden == null && this.m_defObject.Hidden != null)
				{
					this.m_hidden = new ReportBoolProperty(this.m_defObject.Hidden);
				}
				return this.m_hidden;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x0004BF86 File Offset: 0x0004A186
		public ReportDoubleProperty Interval
		{
			get
			{
				if (this.m_interval == null && this.m_defObject.Interval != null)
				{
					this.m_interval = new ReportDoubleProperty(this.m_defObject.Interval);
				}
				return this.m_interval;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x0004BFB9 File Offset: 0x0004A1B9
		public ReportBoolProperty ShowLabels
		{
			get
			{
				if (this.m_showLabels == null && this.m_defObject.ShowLabels != null)
				{
					this.m_showLabels = new ReportBoolProperty(this.m_defObject.ShowLabels);
				}
				return this.m_showLabels;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0004BFEC File Offset: 0x0004A1EC
		public ReportEnumProperty<MapLabelPosition> LabelPosition
		{
			get
			{
				if (this.m_labelPosition == null && this.m_defObject.LabelPosition != null)
				{
					this.m_labelPosition = new ReportEnumProperty<MapLabelPosition>(this.m_defObject.LabelPosition.IsExpression, this.m_defObject.LabelPosition.OriginalText, EnumTranslator.TranslateLabelPosition(this.m_defObject.LabelPosition.StringValue, null));
				}
				return this.m_labelPosition;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x0004C055 File Offset: 0x0004A255
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0004C05D File Offset: 0x0004A25D
		internal MapGridLines MapGridLinesDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0004C065 File Offset: 0x0004A265
		public MapGridLinesInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapGridLinesInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0004C095 File Offset: 0x0004A295
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

		// Token: 0x040008E6 RID: 2278
		private Map m_map;

		// Token: 0x040008E7 RID: 2279
		private MapGridLines m_defObject;

		// Token: 0x040008E8 RID: 2280
		private MapGridLinesInstance m_instance;

		// Token: 0x040008E9 RID: 2281
		private Style m_style;

		// Token: 0x040008EA RID: 2282
		private ReportBoolProperty m_hidden;

		// Token: 0x040008EB RID: 2283
		private ReportDoubleProperty m_interval;

		// Token: 0x040008EC RID: 2284
		private ReportBoolProperty m_showLabels;

		// Token: 0x040008ED RID: 2285
		private ReportEnumProperty<MapLabelPosition> m_labelPosition;
	}
}
