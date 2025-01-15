using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000B8 RID: 184
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class GetExecutionInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x0001BCB5 File Offset: 0x00019EB5
		internal GetExecutionInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001BCC8 File Offset: 0x00019EC8
		public ExecutionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo)this.results[0];
			}
		}

		// Token: 0x040001DF RID: 479
		private object[] results;
	}
}
