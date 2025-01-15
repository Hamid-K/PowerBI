using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200009B RID: 155
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class LoadReportDefinition2CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x0001B95F File Offset: 0x00019B5F
		internal LoadReportDefinition2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001B972 File Offset: 0x00019B72
		public ExecutionInfo2 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo2)this.results[0];
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001B987 File Offset: 0x00019B87
		public Warning[] warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x040001D1 RID: 465
		private object[] results;
	}
}
