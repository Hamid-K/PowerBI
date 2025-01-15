using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000DD RID: 221
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemHistoryLimitCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x0000E2CD File Offset: 0x0000C4CD
		internal GetItemHistoryLimitCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0000E2E0 File Offset: 0x0000C4E0
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0000E2F5 File Offset: 0x0000C4F5
		public bool IsSystem
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0000E30A File Offset: 0x0000C50A
		public int SystemLimit
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[2];
			}
		}

		// Token: 0x0400025B RID: 603
		private object[] results;
	}
}
