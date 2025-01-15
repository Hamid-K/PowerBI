using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000B7 RID: 183
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListSchedulesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x0000E071 File Offset: 0x0000C271
		internal ListSchedulesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0000E084 File Offset: 0x0000C284
		public Schedule[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Schedule[])this.results[0];
			}
		}

		// Token: 0x0400024E RID: 590
		private object[] results;
	}
}
