using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000062 RID: 98
	public abstract class SubreportExprHost : ReportItemExprHost
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000030A0 File Offset: 0x000012A0
		public virtual object NoRowsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000030A3 File Offset: 0x000012A3
		internal IList<ParamExprHost> ParameterHostsRemotable
		{
			get
			{
				return this.m_parameterHostsRemotable;
			}
		}

		// Token: 0x040000A4 RID: 164
		[CLSCompliant(false)]
		protected IList<ParamExprHost> m_parameterHostsRemotable;
	}
}
