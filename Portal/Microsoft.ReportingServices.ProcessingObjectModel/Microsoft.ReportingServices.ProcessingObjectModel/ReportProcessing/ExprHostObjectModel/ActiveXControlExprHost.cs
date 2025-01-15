using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000034 RID: 52
	public abstract class ActiveXControlExprHost : ReportItemExprHost
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000028F3 File Offset: 0x00000AF3
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

		// Token: 0x0400003C RID: 60
		protected ParamExprHost[] ParameterHosts;

		// Token: 0x0400003D RID: 61
		[CLSCompliant(false)]
		protected IList<ParamExprHost> m_parameterHostsRemotable;
	}
}
