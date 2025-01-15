using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000042 RID: 66
	public abstract class ChartDataPointExprHost : IndexedExprHost
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00002A3C File Offset: 0x00000C3C
		public virtual object DataLabelValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00002A3F File Offset: 0x00000C3F
		internal IList<DataValueExprHost> CustomPropertyHostsRemotable
		{
			get
			{
				if (this.m_customPropertyHostsRemotable == null && this.CustomPropertyHosts != null)
				{
					this.m_customPropertyHostsRemotable = new RemoteArrayWrapper<DataValueExprHost>(this.CustomPropertyHosts);
				}
				return this.m_customPropertyHostsRemotable;
			}
		}

		// Token: 0x04000062 RID: 98
		public StyleExprHost DataLabelStyleHost;

		// Token: 0x04000063 RID: 99
		public ActionExprHost ActionHost;

		// Token: 0x04000064 RID: 100
		public StyleExprHost StyleHost;

		// Token: 0x04000065 RID: 101
		public StyleExprHost MarkerStyleHost;

		// Token: 0x04000066 RID: 102
		public ActionInfoExprHost ActionInfoHost;

		// Token: 0x04000067 RID: 103
		protected DataValueExprHost[] CustomPropertyHosts;

		// Token: 0x04000068 RID: 104
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_customPropertyHostsRemotable;
	}
}
