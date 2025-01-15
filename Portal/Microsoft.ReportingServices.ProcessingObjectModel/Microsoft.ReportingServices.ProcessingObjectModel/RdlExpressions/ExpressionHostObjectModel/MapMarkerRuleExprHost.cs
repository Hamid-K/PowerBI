using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000D5 RID: 213
	public abstract class MapMarkerRuleExprHost : MapAppearanceRuleExprHost
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00003BCE File Offset: 0x00001DCE
		internal IList<MapMarkerExprHost> MapMarkersHostsRemotable
		{
			get
			{
				return this.m_mapMarkersHostsRemotable;
			}
		}

		// Token: 0x0400014C RID: 332
		[CLSCompliant(false)]
		protected IList<MapMarkerExprHost> m_mapMarkersHostsRemotable;
	}
}
