using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200028E RID: 654
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetReportLinkCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001698 RID: 5784 RVA: 0x00022C02 File Offset: 0x00020E02
		internal GetReportLinkCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x00022C15 File Offset: 0x00020E15
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x040006EF RID: 1775
		private object[] results;
	}
}
