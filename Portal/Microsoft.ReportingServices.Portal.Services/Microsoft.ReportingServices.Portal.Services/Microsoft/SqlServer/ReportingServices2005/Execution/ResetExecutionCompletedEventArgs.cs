using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000AB RID: 171
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ResetExecutionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x0001BAC9 File Offset: 0x00019CC9
		internal ResetExecutionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001BADC File Offset: 0x00019CDC
		public ExecutionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo)this.results[0];
			}
		}

		// Token: 0x040001D9 RID: 473
		private object[] results;
	}
}
