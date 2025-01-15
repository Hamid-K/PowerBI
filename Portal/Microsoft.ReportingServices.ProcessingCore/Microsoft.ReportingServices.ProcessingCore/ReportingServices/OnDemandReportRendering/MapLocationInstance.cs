using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001A1 RID: 417
	public sealed class MapLocationInstance : BaseInstance
	{
		// Token: 0x060010CB RID: 4299 RVA: 0x000471B3 File Offset: 0x000453B3
		internal MapLocationInstance(MapLocation defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x000471D0 File Offset: 0x000453D0
		public double Left
		{
			get
			{
				if (this.m_left == null)
				{
					this.m_left = new double?(this.m_defObject.MapLocationDef.EvaluateLeft(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_left.Value;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0004722C File Offset: 0x0004542C
		public double Top
		{
			get
			{
				if (this.m_top == null)
				{
					this.m_top = new double?(this.m_defObject.MapLocationDef.EvaluateTop(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_top.Value;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00047288 File Offset: 0x00045488
		public Unit Unit
		{
			get
			{
				if (this.m_unit == null)
				{
					this.m_unit = new Unit?(this.m_defObject.MapLocationDef.EvaluateUnit(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_unit.Value;
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000472E3 File Offset: 0x000454E3
		protected override void ResetInstanceCache()
		{
			this.m_left = null;
			this.m_top = null;
			this.m_unit = null;
		}

		// Token: 0x040007F0 RID: 2032
		private MapLocation m_defObject;

		// Token: 0x040007F1 RID: 2033
		private double? m_left;

		// Token: 0x040007F2 RID: 2034
		private double? m_top;

		// Token: 0x040007F3 RID: 2035
		private Unit? m_unit;
	}
}
