using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000D0 RID: 208
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class Sort2CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x0001BED4 File Offset: 0x0001A0D4
		internal Sort2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001BEE7 File Offset: 0x0001A0E7
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001BEFC File Offset: 0x0001A0FC
		public string ReportItem
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001BF11 File Offset: 0x0001A111
		public ExecutionInfo2 ExecutionInfo
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo2)this.results[2];
			}
		}

		// Token: 0x040001EB RID: 491
		private object[] results;
	}
}
