using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002C7 RID: 711
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateSubscriptionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600175E RID: 5982 RVA: 0x00022FF4 File Offset: 0x000211F4
		internal CreateSubscriptionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x00023007 File Offset: 0x00021207
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x04000703 RID: 1795
		private object[] results;
	}
}
