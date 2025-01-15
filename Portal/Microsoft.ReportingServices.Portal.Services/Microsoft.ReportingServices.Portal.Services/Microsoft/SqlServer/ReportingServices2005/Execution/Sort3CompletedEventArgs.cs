using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000D2 RID: 210
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class Sort3CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x0001BF26 File Offset: 0x0001A126
		internal Sort3CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001BF39 File Offset: 0x0001A139
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001BF4E File Offset: 0x0001A14E
		public string ReportItem
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001BF63 File Offset: 0x0001A163
		public ExecutionInfo3 ExecutionInfo
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo3)this.results[2];
			}
		}

		// Token: 0x040001EC RID: 492
		private object[] results;
	}
}
