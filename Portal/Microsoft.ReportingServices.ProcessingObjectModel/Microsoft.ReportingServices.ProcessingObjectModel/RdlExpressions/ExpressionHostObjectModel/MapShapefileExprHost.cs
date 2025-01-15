using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000E5 RID: 229
	public abstract class MapShapefileExprHost : MapSpatialDataExprHost
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00003CB1 File Offset: 0x00001EB1
		public virtual object SourceExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00003CB4 File Offset: 0x00001EB4
		internal IList<MapFieldNameExprHost> MapFieldNamesHostsRemotable
		{
			get
			{
				return this.m_mapFieldNamesHostsRemotable;
			}
		}

		// Token: 0x0400015A RID: 346
		[CLSCompliant(false)]
		protected IList<MapFieldNameExprHost> m_mapFieldNamesHostsRemotable;
	}
}
