using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000E4 RID: 228
	public abstract class MapLineLayerExprHost : MapVectorLayerExprHost
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00003CA1 File Offset: 0x00001EA1
		internal IList<MapLineExprHost> MapLinesHostsRemotable
		{
			get
			{
				return this.m_mapLinesHostsRemotable;
			}
		}

		// Token: 0x04000157 RID: 343
		public MapLineTemplateExprHost MapLineTemplateHost;

		// Token: 0x04000158 RID: 344
		public MapLineRulesExprHost MapLineRulesHost;

		// Token: 0x04000159 RID: 345
		[CLSCompliant(false)]
		protected IList<MapLineExprHost> m_mapLinesHostsRemotable;
	}
}
