using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200026B RID: 619
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateBatchCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001623 RID: 5667 RVA: 0x00022A0D File Offset: 0x00020C0D
		internal CreateBatchCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00022A20 File Offset: 0x00020C20
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x040006E3 RID: 1763
		private object[] results;
	}
}
