using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001F6 RID: 502
	public sealed class MapGridLinesInstance : BaseInstance
	{
		// Token: 0x060012DF RID: 4831 RVA: 0x0004C92C File Offset: 0x0004AB2C
		internal MapGridLinesInstance(MapGridLines defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0004C948 File Offset: 0x0004AB48
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_defObject, this.m_defObject.MapDef.ReportScope, this.m_defObject.MapDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0004C994 File Offset: 0x0004AB94
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_defObject.MapGridLinesDef.EvaluateHidden(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x0004C9F0 File Offset: 0x0004ABF0
		public double Interval
		{
			get
			{
				if (this.m_interval == null)
				{
					this.m_interval = new double?(this.m_defObject.MapGridLinesDef.EvaluateInterval(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_interval.Value;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x0004CA4C File Offset: 0x0004AC4C
		public bool ShowLabels
		{
			get
			{
				if (this.m_showLabels == null)
				{
					this.m_showLabels = new bool?(this.m_defObject.MapGridLinesDef.EvaluateShowLabels(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_showLabels.Value;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x0004CAA8 File Offset: 0x0004ACA8
		public MapLabelPosition LabelPosition
		{
			get
			{
				if (this.m_labelPosition == null)
				{
					this.m_labelPosition = new MapLabelPosition?(this.m_defObject.MapGridLinesDef.EvaluateLabelPosition(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_labelPosition.Value;
			}
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0004CB04 File Offset: 0x0004AD04
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_hidden = null;
			this.m_interval = null;
			this.m_showLabels = null;
			this.m_labelPosition = null;
		}

		// Token: 0x0400090B RID: 2315
		private MapGridLines m_defObject;

		// Token: 0x0400090C RID: 2316
		private StyleInstance m_style;

		// Token: 0x0400090D RID: 2317
		private bool? m_hidden;

		// Token: 0x0400090E RID: 2318
		private double? m_interval;

		// Token: 0x0400090F RID: 2319
		private bool? m_showLabels;

		// Token: 0x04000910 RID: 2320
		private MapLabelPosition? m_labelPosition;
	}
}
