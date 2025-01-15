using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000183 RID: 387
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetReportDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E8C RID: 3724 RVA: 0x00017A83 File Offset: 0x00015C83
		internal SetReportDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00017A96 File Offset: 0x00015C96
		public Warning[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[0];
			}
		}

		// Token: 0x04000484 RID: 1156
		private object[] results;
	}
}
