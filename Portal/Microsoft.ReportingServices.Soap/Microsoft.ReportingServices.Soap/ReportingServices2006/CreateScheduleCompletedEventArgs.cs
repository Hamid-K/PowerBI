using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001B6 RID: 438
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateScheduleCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F3C RID: 3900 RVA: 0x00017E4D File Offset: 0x0001604D
		internal CreateScheduleCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00017E60 File Offset: 0x00016060
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x04000497 RID: 1175
		private object[] results;
	}
}
