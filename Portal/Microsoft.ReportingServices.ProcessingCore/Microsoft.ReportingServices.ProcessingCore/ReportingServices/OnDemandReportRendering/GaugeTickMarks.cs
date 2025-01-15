using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200012C RID: 300
	public sealed class GaugeTickMarks : TickMarkStyle
	{
		// Token: 0x06000D2B RID: 3371 RVA: 0x00038767 File Offset: 0x00036967
		internal GaugeTickMarks(GaugeTickMarks defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0003877F File Offset: 0x0003697F
		public ReportDoubleProperty Interval
		{
			get
			{
				if (this.m_interval == null && this.GaugeTickMarksDef.Interval != null)
				{
					this.m_interval = new ReportDoubleProperty(this.GaugeTickMarksDef.Interval);
				}
				return this.m_interval;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x000387B2 File Offset: 0x000369B2
		public ReportDoubleProperty IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null && this.GaugeTickMarksDef.IntervalOffset != null)
				{
					this.m_intervalOffset = new ReportDoubleProperty(this.GaugeTickMarksDef.IntervalOffset);
				}
				return this.m_intervalOffset;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x000387E5 File Offset: 0x000369E5
		internal GaugeTickMarks GaugeTickMarksDef
		{
			get
			{
				return (GaugeTickMarks)this.m_defObject;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x000387F2 File Offset: 0x000369F2
		public new GaugeTickMarksInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = this.GetInstance();
				}
				return (GaugeTickMarksInstance)this.m_instance;
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00038827 File Offset: 0x00036A27
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00038842 File Offset: 0x00036A42
		protected override TickMarkStyleInstance GetInstance()
		{
			return new GaugeTickMarksInstance(this);
		}

		// Token: 0x040005F8 RID: 1528
		private ReportDoubleProperty m_interval;

		// Token: 0x040005F9 RID: 1529
		private ReportDoubleProperty m_intervalOffset;
	}
}
