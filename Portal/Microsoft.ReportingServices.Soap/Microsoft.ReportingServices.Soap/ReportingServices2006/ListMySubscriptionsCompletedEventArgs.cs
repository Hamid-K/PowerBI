using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001D5 RID: 469
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListMySubscriptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FB1 RID: 4017 RVA: 0x00018192 File Offset: 0x00016392
		internal ListMySubscriptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x000181A5 File Offset: 0x000163A5
		public Subscription[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Subscription[])this.results[0];
			}
		}

		// Token: 0x040004A3 RID: 1187
		private object[] results;
	}
}
