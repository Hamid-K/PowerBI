using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000095 RID: 149
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class LoadReport3CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x0001B8D2 File Offset: 0x00019AD2
		internal LoadReport3CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001B8E5 File Offset: 0x00019AE5
		public ExecutionInfo3 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo3)this.results[0];
			}
		}

		// Token: 0x040001CE RID: 462
		private object[] results;
	}
}
