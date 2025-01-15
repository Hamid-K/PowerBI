using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000D8 RID: 216
	public abstract class MapCustomColorRuleExprHost : MapColorRuleExprHost
	{
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00003BF1 File Offset: 0x00001DF1
		internal IList<MapCustomColorExprHost> MapCustomColorsHostsRemotable
		{
			get
			{
				return this.m_mapCustomColorsHostsRemotable;
			}
		}

		// Token: 0x04000150 RID: 336
		[CLSCompliant(false)]
		protected IList<MapCustomColorExprHost> m_mapCustomColorsHostsRemotable;
	}
}
