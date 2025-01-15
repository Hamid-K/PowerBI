using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000052 RID: 82
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListItemHistoryCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x0000D84E File Offset: 0x0000BA4E
		internal ListItemHistoryCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0000D861 File Offset: 0x0000BA61
		public ItemHistorySnapshot[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ItemHistorySnapshot[])this.results[0];
			}
		}

		// Token: 0x04000226 RID: 550
		private object[] results;
	}
}
