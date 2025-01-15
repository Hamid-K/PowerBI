using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000ED RID: 237
	public abstract class MapVectorLayerExprHost : MapLayerExprHost
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00003D2E File Offset: 0x00001F2E
		internal IList<MapBindingFieldPairExprHost> MapBindingFieldPairsHostsRemotable
		{
			get
			{
				return this.m_mapBindingFieldPairsHostsRemotable;
			}
		}

		// Token: 0x04000165 RID: 357
		[CLSCompliant(false)]
		protected IList<MapBindingFieldPairExprHost> m_mapBindingFieldPairsHostsRemotable;

		// Token: 0x04000166 RID: 358
		public MapSpatialDataExprHost MapSpatialDataHost;
	}
}
