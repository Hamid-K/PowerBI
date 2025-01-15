using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000A5 RID: 165
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class SetExecutionParametersCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x0001BA51 File Offset: 0x00019C51
		internal SetExecutionParametersCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0001BA64 File Offset: 0x00019C64
		public ExecutionInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionInfo)this.results[0];
			}
		}

		// Token: 0x040001D6 RID: 470
		private object[] results;
	}
}
