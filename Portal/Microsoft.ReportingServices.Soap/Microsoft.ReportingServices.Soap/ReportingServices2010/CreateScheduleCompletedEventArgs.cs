using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000B4 RID: 180
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateScheduleCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x0000E049 File Offset: 0x0000C249
		internal CreateScheduleCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0000E05C File Offset: 0x0000C25C
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x0400024D RID: 589
		private object[] results;
	}
}
