using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000A1 RID: 161
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class SetExecutionCredentials2CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x0001BA01 File Offset: 0x00019C01
		internal SetExecutionCredentials2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001BA14 File Offset: 0x00019C14
		public ExecutionInfo2 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo2)this.results[0];
			}
		}

		// Token: 0x040001D4 RID: 468
		private object[] results;
	}
}
