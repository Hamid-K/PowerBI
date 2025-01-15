using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200018A RID: 394
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateReportEditSessionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000EA3 RID: 3747 RVA: 0x00017B10 File Offset: 0x00015D10
		internal CreateReportEditSessionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00017B23 File Offset: 0x00015D23
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x04000487 RID: 1159
		private object[] results;
	}
}
