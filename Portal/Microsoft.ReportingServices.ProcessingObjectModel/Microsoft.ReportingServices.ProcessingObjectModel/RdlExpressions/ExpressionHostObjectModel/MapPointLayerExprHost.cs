using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000EA RID: 234
	public abstract class MapPointLayerExprHost : MapVectorLayerExprHost
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00003CFD File Offset: 0x00001EFD
		internal IList<MapPointExprHost> MapPointsHostsRemotable
		{
			get
			{
				return this.m_mapPointsHostsRemotable;
			}
		}

		// Token: 0x04000161 RID: 353
		public MapPointTemplateExprHost MapPointTemplateHost;

		// Token: 0x04000162 RID: 354
		public MapPointRulesExprHost MapPointRulesHost;

		// Token: 0x04000163 RID: 355
		[CLSCompliant(false)]
		protected IList<MapPointExprHost> m_mapPointsHostsRemotable;
	}
}
