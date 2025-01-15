using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000097 RID: 151
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class LoadReport2CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x0001B8FA File Offset: 0x00019AFA
		internal LoadReport2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0001B90D File Offset: 0x00019B0D
		public ExecutionInfo2 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo2)this.results[0];
			}
		}

		// Token: 0x040001CF RID: 463
		private object[] results;
	}
}
