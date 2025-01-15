using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001C2 RID: 450
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateSubscriptionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F64 RID: 3940 RVA: 0x00017EED File Offset: 0x000160ED
		internal CreateSubscriptionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x00017F00 File Offset: 0x00016100
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x0400049B RID: 1179
		private object[] results;
	}
}
