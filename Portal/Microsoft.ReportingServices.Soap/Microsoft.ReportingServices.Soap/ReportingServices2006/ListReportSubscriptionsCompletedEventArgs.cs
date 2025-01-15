using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001D7 RID: 471
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListReportSubscriptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FB7 RID: 4023 RVA: 0x000181BA File Offset: 0x000163BA
		internal ListReportSubscriptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x000181CD File Offset: 0x000163CD
		public Subscription[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Subscription[])this.results[0];
			}
		}

		// Token: 0x040004A4 RID: 1188
		private object[] results;
	}
}
