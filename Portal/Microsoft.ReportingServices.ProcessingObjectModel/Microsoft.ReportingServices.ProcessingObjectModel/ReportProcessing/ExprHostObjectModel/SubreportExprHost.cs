using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000033 RID: 51
	public abstract class SubreportExprHost : ReportItemExprHost
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000028BF File Offset: 0x00000ABF
		public virtual object NoRowsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000028C2 File Offset: 0x00000AC2
		internal IList<ParamExprHost> ParameterHostsRemotable
		{
			get
			{
				if (this.m_parameterHostsRemotable == null && this.ParameterHosts != null)
				{
					this.m_parameterHostsRemotable = new RemoteArrayWrapper<ParamExprHost>(this.ParameterHosts);
				}
				return this.m_parameterHostsRemotable;
			}
		}

		// Token: 0x0400003A RID: 58
		protected ParamExprHost[] ParameterHosts;

		// Token: 0x0400003B RID: 59
		[CLSCompliant(false)]
		protected IList<ParamExprHost> m_parameterHostsRemotable;
	}
}
