using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001B1 RID: 433
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetReportHistoryLimitCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x00017DD3 File Offset: 0x00015FD3
		internal GetReportHistoryLimitCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00017DE6 File Offset: 0x00015FE6
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00017DFB File Offset: 0x00015FFB
		public bool IsSystem
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00017E10 File Offset: 0x00016010
		public int SystemLimit
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[2];
			}
		}

		// Token: 0x04000495 RID: 1173
		private object[] results;
	}
}
