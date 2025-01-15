using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000288 RID: 648
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateReportEditSessionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001684 RID: 5764 RVA: 0x00022BB2 File Offset: 0x00020DB2
		internal CreateReportEditSessionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00022BC5 File Offset: 0x00020DC5
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x040006ED RID: 1773
		private object[] results;
	}
}
