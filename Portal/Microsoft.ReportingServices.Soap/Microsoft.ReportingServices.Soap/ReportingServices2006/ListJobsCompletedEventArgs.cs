using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000197 RID: 407
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListJobsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000ED1 RID: 3793 RVA: 0x00017BDA File Offset: 0x00015DDA
		internal ListJobsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00017BED File Offset: 0x00015DED
		public Job[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Job[])this.results[0];
			}
		}

		// Token: 0x0400048B RID: 1163
		private object[] results;
	}
}
