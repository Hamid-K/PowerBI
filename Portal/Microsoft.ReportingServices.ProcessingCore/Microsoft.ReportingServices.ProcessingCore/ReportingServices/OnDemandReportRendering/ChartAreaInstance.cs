using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200025C RID: 604
	public sealed class ChartAreaInstance : BaseInstance
	{
		// Token: 0x06001788 RID: 6024 RVA: 0x0005F9F5 File Offset: 0x0005DBF5
		internal ChartAreaInstance(ChartArea chartAreaDef)
			: base(chartAreaDef.ChartDef)
		{
			this.m_chartAreaDef = chartAreaDef;
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x0005FA0A File Offset: 0x0005DC0A
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartAreaDef, this.m_chartAreaDef.ChartDef, this.m_chartAreaDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x0005FA48 File Offset: 0x0005DC48
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null && !this.m_chartAreaDef.ChartDef.IsOldSnapshot)
				{
					this.m_hidden = new bool?(this.m_chartAreaDef.ChartAreaDef.EvaluateHidden(this.ReportScopeInstance, this.m_chartAreaDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0005FAB8 File Offset: 0x0005DCB8
		public ChartAreaAlignOrientations AlignOrientation
		{
			get
			{
				if (this.m_alignOrientation == null && !this.m_chartAreaDef.ChartDef.IsOldSnapshot)
				{
					this.m_alignOrientation = new ChartAreaAlignOrientations?(this.m_chartAreaDef.ChartAreaDef.EvaluateAlignOrientation(this.ReportScopeInstance, this.m_chartAreaDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_alignOrientation.Value;
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x0005FB28 File Offset: 0x0005DD28
		public bool EquallySizedAxesFont
		{
			get
			{
				if (this.m_equallySizedAxesFont == null && !this.m_chartAreaDef.ChartDef.IsOldSnapshot)
				{
					this.m_equallySizedAxesFont = new bool?(this.m_chartAreaDef.ChartAreaDef.EvaluateEquallySizedAxesFont(this.ReportScopeInstance, this.m_chartAreaDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_equallySizedAxesFont.Value;
			}
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0005FB95 File Offset: 0x0005DD95
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_hidden = null;
			this.m_alignOrientation = null;
			this.m_equallySizedAxesFont = null;
		}

		// Token: 0x04000BAA RID: 2986
		private ChartArea m_chartAreaDef;

		// Token: 0x04000BAB RID: 2987
		private StyleInstance m_style;

		// Token: 0x04000BAC RID: 2988
		private bool? m_hidden;

		// Token: 0x04000BAD RID: 2989
		private ChartAreaAlignOrientations? m_alignOrientation;

		// Token: 0x04000BAE RID: 2990
		private bool? m_equallySizedAxesFont;
	}
}
