using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000AD RID: 173
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ResetExecution2CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x0001BAF1 File Offset: 0x00019CF1
		internal ResetExecution2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001BB04 File Offset: 0x00019D04
		public ExecutionInfo2 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo2)this.results[0];
			}
		}

		// Token: 0x040001DA RID: 474
		private object[] results;
	}
}
