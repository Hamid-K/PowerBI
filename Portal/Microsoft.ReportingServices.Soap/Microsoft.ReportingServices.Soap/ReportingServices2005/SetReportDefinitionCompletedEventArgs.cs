using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000282 RID: 642
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetReportDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x00022B4D File Offset: 0x00020D4D
		internal SetReportDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x00022B60 File Offset: 0x00020D60
		public Warning[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[0];
			}
		}

		// Token: 0x040006EB RID: 1771
		private object[] results;
	}
}
