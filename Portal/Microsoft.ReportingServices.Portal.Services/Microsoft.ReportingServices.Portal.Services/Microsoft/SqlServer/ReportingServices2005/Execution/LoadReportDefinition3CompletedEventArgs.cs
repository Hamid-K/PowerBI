using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200009D RID: 157
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class LoadReportDefinition3CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x0001B99C File Offset: 0x00019B9C
		internal LoadReportDefinition3CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001B9AF File Offset: 0x00019BAF
		public ExecutionInfo3 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo3)this.results[0];
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001B9C4 File Offset: 0x00019BC4
		public Warning[] warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x040001D2 RID: 466
		private object[] results;
	}
}
