using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001D6 RID: 470
	public sealed class MapMarkerInstance : BaseInstance
	{
		// Token: 0x06001214 RID: 4628 RVA: 0x0004A84E File Offset: 0x00048A4E
		internal MapMarkerInstance(MapMarker defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x0004A868 File Offset: 0x00048A68
		public MapMarkerStyle MapMarkerStyle
		{
			get
			{
				if (this.m_mapMarkerStyle == null)
				{
					this.m_mapMarkerStyle = new MapMarkerStyle?(this.m_defObject.MapMarkerDef.EvaluateMapMarkerStyle(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_mapMarkerStyle.Value;
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004A8C3 File Offset: 0x00048AC3
		protected override void ResetInstanceCache()
		{
			this.m_mapMarkerStyle = null;
		}

		// Token: 0x0400089A RID: 2202
		private MapMarker m_defObject;

		// Token: 0x0400089B RID: 2203
		private MapMarkerStyle? m_mapMarkerStyle;
	}
}
