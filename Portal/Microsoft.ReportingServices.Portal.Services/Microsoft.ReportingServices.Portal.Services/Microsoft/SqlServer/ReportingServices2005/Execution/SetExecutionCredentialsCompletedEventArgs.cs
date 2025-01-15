using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200009F RID: 159
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class SetExecutionCredentialsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x0001B9D9 File Offset: 0x00019BD9
		internal SetExecutionCredentialsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001B9EC File Offset: 0x00019BEC
		public ExecutionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo)this.results[0];
			}
		}

		// Token: 0x040001D3 RID: 467
		private object[] results;
	}
}
