using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000099 RID: 153
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class LoadReportDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600062D RID: 1581 RVA: 0x0001B922 File Offset: 0x00019B22
		internal LoadReportDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001B935 File Offset: 0x00019B35
		public ExecutionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo)this.results[0];
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0001B94A File Offset: 0x00019B4A
		public Warning[] warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x040001D0 RID: 464
		private object[] results;
	}
}
