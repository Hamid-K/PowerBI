using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200025E RID: 606
	public sealed class ChartElementPositionInstance : BaseInstance
	{
		// Token: 0x06001794 RID: 6036 RVA: 0x0005FDD3 File Offset: 0x0005DFD3
		internal ChartElementPositionInstance(ChartElementPosition defObject)
			: base(defObject.ChartDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x0005FDE8 File Offset: 0x0005DFE8
		public double Top
		{
			get
			{
				if (this.m_top == null)
				{
					this.m_top = new double?(this.m_defObject.ChartElementPositionDef.EvaluateTop(this.ReportScopeInstance, this.m_defObject.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_top.Value;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x0005FE44 File Offset: 0x0005E044
		public double Left
		{
			get
			{
				if (this.m_left == null)
				{
					this.m_left = new double?(this.m_defObject.ChartElementPositionDef.EvaluateLeft(this.ReportScopeInstance, this.m_defObject.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_left.Value;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x0005FEA0 File Offset: 0x0005E0A0
		public double Height
		{
			get
			{
				if (this.m_height == null)
				{
					this.m_height = new double?(this.m_defObject.ChartElementPositionDef.EvaluateHeight(this.ReportScopeInstance, this.m_defObject.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_height.Value;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0005FEFC File Offset: 0x0005E0FC
		public double Width
		{
			get
			{
				if (this.m_width == null)
				{
					this.m_width = new double?(this.m_defObject.ChartElementPositionDef.EvaluateWidth(this.ReportScopeInstance, this.m_defObject.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_width.Value;
			}
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0005FF58 File Offset: 0x0005E158
		protected override void ResetInstanceCache()
		{
			this.m_top = null;
			this.m_left = null;
			this.m_height = null;
			this.m_width = null;
		}

		// Token: 0x04000BB4 RID: 2996
		private ChartElementPosition m_defObject;

		// Token: 0x04000BB5 RID: 2997
		private double? m_top;

		// Token: 0x04000BB6 RID: 2998
		private double? m_left;

		// Token: 0x04000BB7 RID: 2999
		private double? m_height;

		// Token: 0x04000BB8 RID: 3000
		private double? m_width;
	}
}
