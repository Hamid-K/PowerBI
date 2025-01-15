using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200027E RID: 638
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateReportCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001663 RID: 5731 RVA: 0x00022AFD File Offset: 0x00020CFD
		internal CreateReportCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x00022B10 File Offset: 0x00020D10
		public Warning[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[0];
			}
		}

		// Token: 0x040006E9 RID: 1769
		private object[] results;
	}
}
