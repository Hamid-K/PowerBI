using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001BE RID: 446
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListSchedulesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F56 RID: 3926 RVA: 0x00017EC5 File Offset: 0x000160C5
		internal ListSchedulesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00017ED8 File Offset: 0x000160D8
		public Schedule[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Schedule[])this.results[0];
			}
		}

		// Token: 0x0400049A RID: 1178
		private object[] results;
	}
}
