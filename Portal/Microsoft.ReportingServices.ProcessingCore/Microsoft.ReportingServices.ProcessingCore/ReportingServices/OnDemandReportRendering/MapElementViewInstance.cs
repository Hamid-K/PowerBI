using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001FB RID: 507
	public sealed class MapElementViewInstance : MapViewInstance
	{
		// Token: 0x060012FE RID: 4862 RVA: 0x0004D27D File Offset: 0x0004B47D
		internal MapElementViewInstance(MapElementView defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0004D290 File Offset: 0x0004B490
		public string LayerName
		{
			get
			{
				if (this.m_layerName == null)
				{
					this.m_layerName = ((MapElementView)this.m_defObject.MapViewDef).EvaluateLayerName(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
				}
				return this.m_layerName;
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0004D2E1 File Offset: 0x0004B4E1
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_layerName = null;
		}

		// Token: 0x04000925 RID: 2341
		private MapElementView m_defObject;

		// Token: 0x04000926 RID: 2342
		private string m_layerName;
	}
}
