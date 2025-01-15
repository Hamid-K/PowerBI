using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000036 RID: 54
	public abstract class GroupingExprHost : IndexedExprHost
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00002958 File Offset: 0x00000B58
		public virtual object LabelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000295B File Offset: 0x00000B5B
		internal IList<FilterExprHost> FilterHostsRemotable
		{
			get
			{
				if (this.m_filterHostsRemotable == null && this.FilterHosts != null)
				{
					this.m_filterHostsRemotable = new RemoteArrayWrapper<FilterExprHost>(this.FilterHosts);
				}
				return this.m_filterHostsRemotable;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00002984 File Offset: 0x00000B84
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

		// Token: 0x04000041 RID: 65
		protected FilterExprHost[] FilterHosts;

		// Token: 0x04000042 RID: 66
		[CLSCompliant(false)]
		protected IList<FilterExprHost> m_filterHostsRemotable;

		// Token: 0x04000043 RID: 67
		public IndexedExprHost ParentExpressionsHost;

		// Token: 0x04000044 RID: 68
		protected DataValueExprHost[] CustomPropertyHosts;

		// Token: 0x04000045 RID: 69
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_customPropertyHostsRemotable;

		// Token: 0x04000046 RID: 70
		public IndexedExprHost UserSortExpressionsHost;
	}
}
