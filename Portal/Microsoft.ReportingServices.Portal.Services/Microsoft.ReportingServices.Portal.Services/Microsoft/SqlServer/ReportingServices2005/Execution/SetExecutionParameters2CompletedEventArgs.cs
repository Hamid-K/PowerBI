using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000A7 RID: 167
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class SetExecutionParameters2CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x0001BA79 File Offset: 0x00019C79
		internal SetExecutionParameters2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001BA8C File Offset: 0x00019C8C
		public ExecutionInfo2 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo2)this.results[0];
			}
		}

		// Token: 0x040001D7 RID: 471
		private object[] results;
	}
}
