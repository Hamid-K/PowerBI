using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000BA RID: 186
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class GetExecutionInfo2CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x0001BCDD File Offset: 0x00019EDD
		internal GetExecutionInfo2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001BCF0 File Offset: 0x00019EF0
		public ExecutionInfo2 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo2)this.results[0];
			}
		}

		// Token: 0x040001E0 RID: 480
		private object[] results;
	}
}
