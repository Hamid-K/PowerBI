using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200029B RID: 667
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListJobsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060016C7 RID: 5831 RVA: 0x00022CE1 File Offset: 0x00020EE1
		internal ListJobsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x00022CF4 File Offset: 0x00020EF4
		public Job[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Job[])this.results[0];
			}
		}

		// Token: 0x040006F3 RID: 1779
		private object[] results;
	}
}
