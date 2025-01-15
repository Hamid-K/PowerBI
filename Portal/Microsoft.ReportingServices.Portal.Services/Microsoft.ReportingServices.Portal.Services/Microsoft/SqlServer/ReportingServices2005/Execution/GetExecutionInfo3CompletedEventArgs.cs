using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000BC RID: 188
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetExecutionInfo3CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x0001BD05 File Offset: 0x00019F05
		internal GetExecutionInfo3CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001BD18 File Offset: 0x00019F18
		public ExecutionInfo3 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo3)this.results[0];
			}
		}

		// Token: 0x040001E1 RID: 481
		private object[] results;
	}
}
