using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000E2 RID: 226
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetReportServerConfigInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x0000E371 File Offset: 0x0000C571
		internal GetReportServerConfigInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0000E384 File Offset: 0x0000C584
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x0400025D RID: 605
		private object[] results;
	}
}
