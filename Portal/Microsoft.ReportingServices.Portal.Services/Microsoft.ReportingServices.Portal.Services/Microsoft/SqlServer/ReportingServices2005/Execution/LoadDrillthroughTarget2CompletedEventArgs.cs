using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000C2 RID: 194
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class LoadDrillthroughTarget2CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x0001BD7D File Offset: 0x00019F7D
		internal LoadDrillthroughTarget2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001BD90 File Offset: 0x00019F90
		public ExecutionInfo2 Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo2)this.results[0];
			}
		}

		// Token: 0x040001E4 RID: 484
		private object[] results;
	}
}
