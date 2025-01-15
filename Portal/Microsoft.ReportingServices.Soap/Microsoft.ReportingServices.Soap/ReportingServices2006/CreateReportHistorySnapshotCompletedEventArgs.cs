using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001AB RID: 427
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateReportHistorySnapshotCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F13 RID: 3859 RVA: 0x00017D44 File Offset: 0x00015F44
		internal CreateReportHistorySnapshotCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00017D57 File Offset: 0x00015F57
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00017D6C File Offset: 0x00015F6C
		public Warning[] Warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x04000493 RID: 1171
		private object[] results;
	}
}
