using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200018C RID: 396
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetReportParametersCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000EA9 RID: 3753 RVA: 0x00017B38 File Offset: 0x00015D38
		internal GetReportParametersCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00017B4B File Offset: 0x00015D4B
		public ReportParameter[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ReportParameter[])this.results[0];
			}
		}

		// Token: 0x04000488 RID: 1160
		private object[] results;
	}
}
