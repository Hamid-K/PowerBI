using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002B6 RID: 694
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListReportHistoryCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001726 RID: 5926 RVA: 0x00022F04 File Offset: 0x00021104
		internal ListReportHistoryCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00022F17 File Offset: 0x00021117
		public ReportHistorySnapshot[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ReportHistorySnapshot[])this.results[0];
			}
		}

		// Token: 0x040006FD RID: 1789
		private object[] results;
	}
}
