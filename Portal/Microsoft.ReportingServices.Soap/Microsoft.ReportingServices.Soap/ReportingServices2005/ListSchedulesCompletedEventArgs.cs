using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002C3 RID: 707
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListSchedulesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001750 RID: 5968 RVA: 0x00022FCC File Offset: 0x000211CC
		internal ListSchedulesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00022FDF File Offset: 0x000211DF
		public Schedule[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Schedule[])this.results[0];
			}
		}

		// Token: 0x04000702 RID: 1794
		private object[] results;
	}
}
