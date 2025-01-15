using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200026F RID: 623
	public sealed class ChartStripLineInstance : BaseInstance
	{
		// Token: 0x06001827 RID: 6183 RVA: 0x0006323D File Offset: 0x0006143D
		internal ChartStripLineInstance(ChartStripLine chartStripLineDef)
			: base(chartStripLineDef.ChartDef)
		{
			this.m_chartStripLineDef = chartStripLineDef;
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x00063252 File Offset: 0x00061452
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartStripLineDef, this.m_chartStripLineDef.ChartDef, this.m_chartStripLineDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x00063290 File Offset: 0x00061490
		public string Title
		{
			get
			{
				if (this.m_title == null && !this.m_chartStripLineDef.ChartDef.IsOldSnapshot)
				{
					this.m_title = this.m_chartStripLineDef.ChartStripLineDef.EvaluateTitle(this.ReportScopeInstance, this.m_chartStripLineDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_title;
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x000632F0 File Offset: 0x000614F0
		[Obsolete("Use TextOrientation instead.")]
		public int TitleAngle
		{
			get
			{
				if (this.m_titleAngle == null && !this.m_chartStripLineDef.ChartDef.IsOldSnapshot)
				{
					this.m_titleAngle = new int?(this.m_chartStripLineDef.ChartStripLineDef.EvaluateTitleAngle(this.ReportScopeInstance, this.m_chartStripLineDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_titleAngle.Value;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x00063360 File Offset: 0x00061560
		public TextOrientations TextOrientation
		{
			get
			{
				if (this.m_textOrientation == null)
				{
					this.m_textOrientation = new TextOrientations?(this.m_chartStripLineDef.ChartStripLineDef.EvaluateTextOrientation(this.ReportScopeInstance, this.m_chartStripLineDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_textOrientation.Value;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x000633BC File Offset: 0x000615BC
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chartStripLineDef.ChartDef.IsOldSnapshot)
				{
					this.m_toolTip = this.m_chartStripLineDef.ChartStripLineDef.EvaluateToolTip(this.ReportScopeInstance, this.m_chartStripLineDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x0006341C File Offset: 0x0006161C
		public double Interval
		{
			get
			{
				if (this.m_interval == null && !this.m_chartStripLineDef.ChartDef.IsOldSnapshot)
				{
					this.m_interval = new double?(this.m_chartStripLineDef.ChartStripLineDef.EvaluateInterval(this.ReportScopeInstance, this.m_chartStripLineDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_interval.Value;
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x0006348C File Offset: 0x0006168C
		public ChartIntervalType IntervalType
		{
			get
			{
				if (this.m_intervalType == null && !this.m_chartStripLineDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalType = new ChartIntervalType?(this.m_chartStripLineDef.ChartStripLineDef.EvaluateIntervalType(this.ReportScopeInstance, this.m_chartStripLineDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalType.Value;
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x000634FC File Offset: 0x000616FC
		public double IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null && !this.m_chartStripLineDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalOffset = new double?(this.m_chartStripLineDef.ChartStripLineDef.EvaluateIntervalOffset(this.ReportScopeInstance, this.m_chartStripLineDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalOffset.Value;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x0006356C File Offset: 0x0006176C
		public ChartIntervalType IntervalOffsetType
		{
			get
			{
				if (this.m_intervalOffsetType == null && !this.m_chartStripLineDef.ChartDef.IsOldSnapshot)
				{
					this.m_intervalOffsetType = new ChartIntervalType?(this.m_chartStripLineDef.ChartStripLineDef.EvaluateIntervalOffsetType(this.ReportScopeInstance, this.m_chartStripLineDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_intervalOffsetType.Value;
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x000635DC File Offset: 0x000617DC
		public double StripWidth
		{
			get
			{
				if (this.m_stripWidth == null && !this.m_chartStripLineDef.ChartDef.IsOldSnapshot)
				{
					this.m_stripWidth = new double?(this.m_chartStripLineDef.ChartStripLineDef.EvaluateStripWidth(this.ReportScopeInstance, this.m_chartStripLineDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_stripWidth.Value;
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0006364C File Offset: 0x0006184C
		public ChartIntervalType StripWidthType
		{
			get
			{
				if (this.m_stripWidthType == null && !this.m_chartStripLineDef.ChartDef.IsOldSnapshot)
				{
					this.m_stripWidthType = new ChartIntervalType?(this.m_chartStripLineDef.ChartStripLineDef.EvaluateStripWidthType(this.ReportScopeInstance, this.m_chartStripLineDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_stripWidthType.Value;
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000636BC File Offset: 0x000618BC
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_title = null;
			this.m_textOrientation = null;
			this.m_titleAngle = null;
			this.m_toolTip = null;
			this.m_interval = null;
			this.m_intervalType = null;
			this.m_intervalOffset = null;
			this.m_intervalOffsetType = null;
			this.m_stripWidth = null;
			this.m_stripWidthType = null;
		}

		// Token: 0x04000C38 RID: 3128
		private ChartStripLine m_chartStripLineDef;

		// Token: 0x04000C39 RID: 3129
		private StyleInstance m_style;

		// Token: 0x04000C3A RID: 3130
		private string m_title;

		// Token: 0x04000C3B RID: 3131
		private int? m_titleAngle;

		// Token: 0x04000C3C RID: 3132
		private TextOrientations? m_textOrientation;

		// Token: 0x04000C3D RID: 3133
		private string m_toolTip;

		// Token: 0x04000C3E RID: 3134
		private double? m_interval;

		// Token: 0x04000C3F RID: 3135
		private ChartIntervalType? m_intervalType;

		// Token: 0x04000C40 RID: 3136
		private double? m_intervalOffset;

		// Token: 0x04000C41 RID: 3137
		private ChartIntervalType? m_intervalOffsetType;

		// Token: 0x04000C42 RID: 3138
		private double? m_stripWidth;

		// Token: 0x04000C43 RID: 3139
		private ChartIntervalType? m_stripWidthType;
	}
}
