using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002E1 RID: 737
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListTasksCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060017C1 RID: 6081 RVA: 0x00023311 File Offset: 0x00021511
		internal ListTasksCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x00023324 File Offset: 0x00021524
		public Task[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Task[])this.results[0];
			}
		}

		// Token: 0x0400070E RID: 1806
		private object[] results;
	}
}
