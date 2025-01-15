using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000CF RID: 207
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListExecutionSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x0000E1EE File Offset: 0x0000C3EE
		internal ListExecutionSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0000E201 File Offset: 0x0000C401
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x04000257 RID: 599
		private object[] results;
	}
}
