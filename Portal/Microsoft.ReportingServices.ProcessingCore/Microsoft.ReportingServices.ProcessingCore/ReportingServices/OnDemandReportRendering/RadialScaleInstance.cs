using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000127 RID: 295
	public sealed class RadialScaleInstance : GaugeScaleInstance
	{
		// Token: 0x06000CFA RID: 3322 RVA: 0x00037B45 File Offset: 0x00035D45
		internal RadialScaleInstance(RadialScale defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00037B58 File Offset: 0x00035D58
		public double Radius
		{
			get
			{
				if (this.m_radius == null)
				{
					this.m_radius = new double?(((RadialScale)this.m_defObject.GaugeScaleDef).EvaluateRadius(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_radius.Value;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00037BBC File Offset: 0x00035DBC
		public double StartAngle
		{
			get
			{
				if (this.m_startAngle == null)
				{
					this.m_startAngle = new double?(((RadialScale)this.m_defObject.GaugeScaleDef).EvaluateStartAngle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_startAngle.Value;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00037C20 File Offset: 0x00035E20
		public double SweepAngle
		{
			get
			{
				if (this.m_sweepAngle == null)
				{
					this.m_sweepAngle = new double?(((RadialScale)this.m_defObject.GaugeScaleDef).EvaluateSweepAngle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_sweepAngle.Value;
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00037C81 File Offset: 0x00035E81
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_radius = null;
			this.m_startAngle = null;
			this.m_sweepAngle = null;
		}

		// Token: 0x040005CF RID: 1487
		private RadialScale m_defObject;

		// Token: 0x040005D0 RID: 1488
		private double? m_radius;

		// Token: 0x040005D1 RID: 1489
		private double? m_startAngle;

		// Token: 0x040005D2 RID: 1490
		private double? m_sweepAngle;
	}
}
