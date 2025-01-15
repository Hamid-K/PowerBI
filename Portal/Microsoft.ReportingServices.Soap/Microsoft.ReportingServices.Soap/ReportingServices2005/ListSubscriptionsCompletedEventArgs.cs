using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002D8 RID: 728
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListSubscriptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060017A5 RID: 6053 RVA: 0x00023271 File Offset: 0x00021471
		internal ListSubscriptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00023284 File Offset: 0x00021484
		public Subscription[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Subscription[])this.results[0];
			}
		}

		// Token: 0x0400070A RID: 1802
		private object[] results;
	}
}
