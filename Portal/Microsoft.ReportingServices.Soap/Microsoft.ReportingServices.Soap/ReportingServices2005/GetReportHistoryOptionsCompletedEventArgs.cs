using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002B1 RID: 689
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetReportHistoryOptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001712 RID: 5906 RVA: 0x00022E60 File Offset: 0x00021060
		internal GetReportHistoryOptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x00022E73 File Offset: 0x00021073
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x00022E88 File Offset: 0x00021088
		public bool KeepExecutionSnapshots
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x00022E9D File Offset: 0x0002109D
		public ScheduleDefinitionOrReference Item
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ScheduleDefinitionOrReference)this.results[2];
			}
		}

		// Token: 0x040006FB RID: 1787
		private object[] results;
	}
}
