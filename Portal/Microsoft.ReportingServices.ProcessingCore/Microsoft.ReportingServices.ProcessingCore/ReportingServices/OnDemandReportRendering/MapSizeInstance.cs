using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001A2 RID: 418
	public sealed class MapSizeInstance : BaseInstance
	{
		// Token: 0x060010D0 RID: 4304 RVA: 0x00047309 File Offset: 0x00045509
		internal MapSizeInstance(MapSize defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00047324 File Offset: 0x00045524
		public double Width
		{
			get
			{
				if (this.m_width == null)
				{
					this.m_width = new double?(this.m_defObject.MapSizeDef.EvaluateWidth(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_width.Value;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00047380 File Offset: 0x00045580
		public double Height
		{
			get
			{
				if (this.m_height == null)
				{
					this.m_height = new double?(this.m_defObject.MapSizeDef.EvaluateHeight(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_height.Value;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x000473DC File Offset: 0x000455DC
		public Unit Unit
		{
			get
			{
				if (this.m_unit == null)
				{
					this.m_unit = new Unit?(this.m_defObject.MapSizeDef.EvaluateUnit(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_unit.Value;
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00047437 File Offset: 0x00045637
		protected override void ResetInstanceCache()
		{
			this.m_width = null;
			this.m_height = null;
			this.m_unit = null;
		}

		// Token: 0x040007F4 RID: 2036
		private MapSize m_defObject;

		// Token: 0x040007F5 RID: 2037
		private double? m_width;

		// Token: 0x040007F6 RID: 2038
		private double? m_height;

		// Token: 0x040007F7 RID: 2039
		private Unit? m_unit;
	}
}
