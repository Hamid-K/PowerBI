using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000BB RID: 187
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListScheduleStatesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x0000E0C1 File Offset: 0x0000C2C1
		internal ListScheduleStatesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x04000250 RID: 592
		private object[] results;
	}
}
