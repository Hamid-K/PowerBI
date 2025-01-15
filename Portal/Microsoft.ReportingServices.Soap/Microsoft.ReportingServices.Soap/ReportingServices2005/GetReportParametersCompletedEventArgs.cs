using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200028A RID: 650
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetReportParametersCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600168A RID: 5770 RVA: 0x00022BDA File Offset: 0x00020DDA
		internal GetReportParametersCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x00022BED File Offset: 0x00020DED
		public ReportParameter[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ReportParameter[])this.results[0];
			}
		}

		// Token: 0x040006EE RID: 1774
		private object[] results;
	}
}
