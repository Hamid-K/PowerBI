using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000079 RID: 121
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListMySubscriptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x0000DC09 File Offset: 0x0000BE09
		internal ListMySubscriptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		public Subscription[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Subscription[])this.results[0];
			}
		}

		// Token: 0x04000236 RID: 566
		private object[] results;
	}
}
