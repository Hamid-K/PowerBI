using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000044 RID: 68
	public abstract class AxisExprHost : StyleExprHost
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00002A7B File Offset: 0x00000C7B
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public virtual object AxisMinExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00002AA7 File Offset: 0x00000CA7
		public virtual object AxisMaxExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00002AAA File Offset: 0x00000CAA
		public virtual object AxisCrossAtExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00002AAD File Offset: 0x00000CAD
		public virtual object AxisMajorIntervalExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public virtual object AxisMinorIntervalExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000069 RID: 105
		public ChartTitleExprHost TitleHost;

		// Token: 0x0400006A RID: 106
		public StyleExprHost MajorGridLinesHost;

		// Token: 0x0400006B RID: 107
		public StyleExprHost MinorGridLinesHost;

		// Token: 0x0400006C RID: 108
		protected DataValueExprHost[] CustomPropertyHosts;

		// Token: 0x0400006D RID: 109
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_customPropertyHostsRemotable;
	}
}
