using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001D9 RID: 473
	public sealed class MapCustomColorInstance : BaseInstance
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x0004A905 File Offset: 0x00048B05
		internal MapCustomColorInstance(MapCustomColor defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0004A920 File Offset: 0x00048B20
		public ReportColor Color
		{
			get
			{
				if (this.m_color == null)
				{
					this.m_color = new ReportColor(this.m_defObject.MapCustomColorDef.EvaluateColor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_color;
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0004A971 File Offset: 0x00048B71
		protected override void ResetInstanceCache()
		{
			this.m_color = null;
		}

		// Token: 0x0400089E RID: 2206
		private MapCustomColor m_defObject;

		// Token: 0x0400089F RID: 2207
		private ReportColor m_color;
	}
}
