using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000E6 RID: 230
	public abstract class MapPolygonLayerExprHost : MapVectorLayerExprHost
	{
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00003CC4 File Offset: 0x00001EC4
		internal IList<MapPolygonExprHost> MapPolygonsHostsRemotable
		{
			get
			{
				return this.m_mapPolygonsHostsRemotable;
			}
		}

		// Token: 0x0400015B RID: 347
		public MapPolygonTemplateExprHost MapPolygonTemplateHost;

		// Token: 0x0400015C RID: 348
		public MapPolygonRulesExprHost MapPolygonRulesHost;

		// Token: 0x0400015D RID: 349
		public MapPointTemplateExprHost MapPointTemplateHost;

		// Token: 0x0400015E RID: 350
		public MapPointRulesExprHost MapPointRulesHost;

		// Token: 0x0400015F RID: 351
		[CLSCompliant(false)]
		protected IList<MapPolygonExprHost> m_mapPolygonsHostsRemotable;
	}
}
