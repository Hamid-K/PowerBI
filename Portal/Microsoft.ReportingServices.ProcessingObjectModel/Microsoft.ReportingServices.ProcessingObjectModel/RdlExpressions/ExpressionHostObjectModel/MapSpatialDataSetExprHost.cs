using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000E9 RID: 233
	public abstract class MapSpatialDataSetExprHost : MapSpatialDataExprHost
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00003CE7 File Offset: 0x00001EE7
		public virtual object DataSetNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00003CEA File Offset: 0x00001EEA
		public virtual object SpatialFieldExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00003CED File Offset: 0x00001EED
		internal IList<MapFieldNameExprHost> MapFieldNamesHostsRemotable
		{
			get
			{
				return this.m_mapFieldNamesHostsRemotable;
			}
		}

		// Token: 0x04000160 RID: 352
		[CLSCompliant(false)]
		protected IList<MapFieldNameExprHost> m_mapFieldNamesHostsRemotable;
	}
}
