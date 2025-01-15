using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001F8 RID: 504
	public sealed class MapLimitsInstance : BaseInstance
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x0004CF4A File Offset: 0x0004B14A
		internal MapLimitsInstance(MapLimits defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0004CF64 File Offset: 0x0004B164
		public double MinimumX
		{
			get
			{
				if (this.m_minimumX == null)
				{
					this.m_minimumX = new double?(this.m_defObject.MapLimitsDef.EvaluateMinimumX(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_minimumX.Value;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0004CFC0 File Offset: 0x0004B1C0
		public double MinimumY
		{
			get
			{
				if (this.m_minimumY == null)
				{
					this.m_minimumY = new double?(this.m_defObject.MapLimitsDef.EvaluateMinimumY(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_minimumY.Value;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0004D01C File Offset: 0x0004B21C
		public double MaximumX
		{
			get
			{
				if (this.m_maximumX == null)
				{
					this.m_maximumX = new double?(this.m_defObject.MapLimitsDef.EvaluateMaximumX(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_maximumX.Value;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x0004D078 File Offset: 0x0004B278
		public double MaximumY
		{
			get
			{
				if (this.m_maximumY == null)
				{
					this.m_maximumY = new double?(this.m_defObject.MapLimitsDef.EvaluateMaximumY(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_maximumY.Value;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0004D0D4 File Offset: 0x0004B2D4
		public bool LimitToData
		{
			get
			{
				if (this.m_limitToData == null)
				{
					this.m_limitToData = new bool?(this.m_defObject.MapLimitsDef.EvaluateLimitToData(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_limitToData.Value;
			}
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0004D12F File Offset: 0x0004B32F
		protected override void ResetInstanceCache()
		{
			this.m_minimumX = null;
			this.m_minimumY = null;
			this.m_maximumX = null;
			this.m_maximumY = null;
			this.m_limitToData = null;
		}

		// Token: 0x0400091B RID: 2331
		private MapLimits m_defObject;

		// Token: 0x0400091C RID: 2332
		private double? m_minimumX;

		// Token: 0x0400091D RID: 2333
		private double? m_minimumY;

		// Token: 0x0400091E RID: 2334
		private double? m_maximumX;

		// Token: 0x0400091F RID: 2335
		private double? m_maximumY;

		// Token: 0x04000920 RID: 2336
		private bool? m_limitToData;
	}
}
