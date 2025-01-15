using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000C4 RID: 196
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class LoadDrillthroughTarget3CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x0001BDA5 File Offset: 0x00019FA5
		internal LoadDrillthroughTarget3CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		public ExecutionInfo3 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo3)this.results[0];
			}
		}

		// Token: 0x040001E5 RID: 485
		private object[] results;
	}
}
