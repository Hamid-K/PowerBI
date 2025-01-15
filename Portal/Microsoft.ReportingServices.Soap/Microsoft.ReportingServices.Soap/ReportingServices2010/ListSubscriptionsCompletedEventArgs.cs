using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000077 RID: 119
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListSubscriptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x0000DBE1 File Offset: 0x0000BDE1
		internal ListSubscriptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		public Subscription[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Subscription[])this.results[0];
			}
		}

		// Token: 0x04000235 RID: 565
		private object[] results;
	}
}
