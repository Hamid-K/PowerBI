using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001F9 RID: 505
	public sealed class MapCustomViewInstance : MapViewInstance
	{
		// Token: 0x060012F8 RID: 4856 RVA: 0x0004D16D File Offset: 0x0004B36D
		internal MapCustomViewInstance(MapCustomView defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0004D180 File Offset: 0x0004B380
		public double CenterX
		{
			get
			{
				if (this.m_centerX == null)
				{
					this.m_centerX = new double?(((MapCustomView)this.m_defObject.MapViewDef).EvaluateCenterX(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_centerX.Value;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0004D1E4 File Offset: 0x0004B3E4
		public double CenterY
		{
			get
			{
				if (this.m_centerY == null)
				{
					this.m_centerY = new double?(((MapCustomView)this.m_defObject.MapViewDef).EvaluateCenterY(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_centerY.Value;
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0004D245 File Offset: 0x0004B445
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_centerX = null;
			this.m_centerY = null;
		}

		// Token: 0x04000921 RID: 2337
		private MapCustomView m_defObject;

		// Token: 0x04000922 RID: 2338
		private double? m_centerX;

		// Token: 0x04000923 RID: 2339
		private double? m_centerY;
	}
}
