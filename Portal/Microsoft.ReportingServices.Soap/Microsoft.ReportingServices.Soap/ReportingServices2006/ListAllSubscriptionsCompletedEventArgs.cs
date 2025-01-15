using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001D3 RID: 467
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListAllSubscriptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FAB RID: 4011 RVA: 0x0001816A File Offset: 0x0001636A
		internal ListAllSubscriptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0001817D File Offset: 0x0001637D
		public Subscription[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Subscription[])this.results[0];
			}
		}

		// Token: 0x040004A2 RID: 1186
		private object[] results;
	}
}
