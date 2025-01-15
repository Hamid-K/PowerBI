using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000C1 RID: 193
	public abstract class MapElementViewExprHost : MapViewExprHost
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00003A64 File Offset: 0x00001C64
		public virtual object LayerNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00003A67 File Offset: 0x00001C67
		internal IList<MapBindingFieldPairExprHost> MapBindingFieldPairsHostsRemotable
		{
			get
			{
				return this.m_mapBindingFieldPairsHostsRemotable;
			}
		}

		// Token: 0x04000140 RID: 320
		[CLSCompliant(false)]
		protected IList<MapBindingFieldPairExprHost> m_mapBindingFieldPairsHostsRemotable;
	}
}
