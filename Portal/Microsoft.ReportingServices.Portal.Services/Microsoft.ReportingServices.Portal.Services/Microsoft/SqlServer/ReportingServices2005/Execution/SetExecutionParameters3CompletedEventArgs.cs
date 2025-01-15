using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000A9 RID: 169
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetExecutionParameters3CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x0001BAA1 File Offset: 0x00019CA1
		internal SetExecutionParameters3CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		public ExecutionInfo3 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo3)this.results[0];
			}
		}

		// Token: 0x040001D8 RID: 472
		private object[] results;
	}
}
