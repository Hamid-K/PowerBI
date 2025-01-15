using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000AF RID: 175
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ResetExecution3CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x0001BB19 File Offset: 0x00019D19
		internal ResetExecution3CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001BB2C File Offset: 0x00019D2C
		public ExecutionInfo3 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo3)this.results[0];
			}
		}

		// Token: 0x040001DB RID: 475
		private object[] results;
	}
}
