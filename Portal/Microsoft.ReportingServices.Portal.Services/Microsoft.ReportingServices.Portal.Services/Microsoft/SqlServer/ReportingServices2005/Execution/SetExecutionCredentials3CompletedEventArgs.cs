using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000A3 RID: 163
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetExecutionCredentials3CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600064E RID: 1614 RVA: 0x0001BA29 File Offset: 0x00019C29
		internal SetExecutionCredentials3CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0001BA3C File Offset: 0x00019C3C
		public ExecutionInfo3 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo3)this.results[0];
			}
		}

		// Token: 0x040001D5 RID: 469
		private object[] results;
	}
}
