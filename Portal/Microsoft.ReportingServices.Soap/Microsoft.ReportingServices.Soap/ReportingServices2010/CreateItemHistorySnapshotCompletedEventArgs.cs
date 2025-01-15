using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000D9 RID: 217
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateItemHistorySnapshotCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x0000E290 File Offset: 0x0000C490
		internal CreateItemHistorySnapshotCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0000E2A3 File Offset: 0x0000C4A3
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		public Warning[] Warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x0400025A RID: 602
		private object[] results;
	}
}
