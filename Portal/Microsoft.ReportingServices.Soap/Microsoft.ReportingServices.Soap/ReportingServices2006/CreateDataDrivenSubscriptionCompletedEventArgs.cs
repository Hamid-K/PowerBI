using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001C4 RID: 452
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateDataDrivenSubscriptionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F6A RID: 3946 RVA: 0x00017F15 File Offset: 0x00016115
		internal CreateDataDrivenSubscriptionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x00017F28 File Offset: 0x00016128
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x0400049C RID: 1180
		private object[] results;
	}
}
