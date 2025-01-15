using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000280 RID: 640
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetReportDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001669 RID: 5737 RVA: 0x00022B25 File Offset: 0x00020D25
		internal GetReportDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x00022B38 File Offset: 0x00020D38
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x040006EA RID: 1770
		private object[] results;
	}
}
