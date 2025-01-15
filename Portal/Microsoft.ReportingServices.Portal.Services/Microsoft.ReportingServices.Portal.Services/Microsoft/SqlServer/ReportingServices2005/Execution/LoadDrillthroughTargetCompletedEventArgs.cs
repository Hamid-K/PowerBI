using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000C0 RID: 192
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class LoadDrillthroughTargetCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006B2 RID: 1714 RVA: 0x0001BD55 File Offset: 0x00019F55
		internal LoadDrillthroughTargetCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001BD68 File Offset: 0x00019F68
		public ExecutionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo)this.results[0];
			}
		}

		// Token: 0x040001E3 RID: 483
		private object[] results;
	}
}
