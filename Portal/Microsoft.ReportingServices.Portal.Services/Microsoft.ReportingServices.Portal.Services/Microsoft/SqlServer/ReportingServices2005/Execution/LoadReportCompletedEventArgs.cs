using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000093 RID: 147
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class LoadReportCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x0001B8AA File Offset: 0x00019AAA
		internal LoadReportCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001B8BD File Offset: 0x00019ABD
		public ExecutionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo)this.results[0];
			}
		}

		// Token: 0x040001CD RID: 461
		private object[] results;
	}
}
