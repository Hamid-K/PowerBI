using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000181 RID: 385
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetReportDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x00017A5B File Offset: 0x00015C5B
		internal GetReportDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00017A6E File Offset: 0x00015C6E
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x04000483 RID: 1155
		private object[] results;
	}
}
