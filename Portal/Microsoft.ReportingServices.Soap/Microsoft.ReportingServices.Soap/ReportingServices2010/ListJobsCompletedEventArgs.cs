using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000F6 RID: 246
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListJobsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x0000E4B1 File Offset: 0x0000C6B1
		internal ListJobsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		public Job[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Job[])this.results[0];
			}
		}

		// Token: 0x04000265 RID: 613
		private object[] results;
	}
}
