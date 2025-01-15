using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002AE RID: 686
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateReportHistorySnapshotCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001707 RID: 5895 RVA: 0x00022E23 File Offset: 0x00021023
		internal CreateReportHistorySnapshotCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00022E36 File Offset: 0x00021036
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00022E4B File Offset: 0x0002104B
		public Warning[] Warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x040006FA RID: 1786
		private object[] results;
	}
}
