using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001B3 RID: 435
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListReportHistoryCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x00017E25 File Offset: 0x00016025
		internal ListReportHistoryCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00017E38 File Offset: 0x00016038
		public ReportHistorySnapshot[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ReportHistorySnapshot[])this.results[0];
			}
		}

		// Token: 0x04000496 RID: 1174
		private object[] results;
	}
}
