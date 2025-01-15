using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001AE RID: 430
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetReportHistoryOptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F1E RID: 3870 RVA: 0x00017D81 File Offset: 0x00015F81
		internal GetReportHistoryOptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00017D94 File Offset: 0x00015F94
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00017DA9 File Offset: 0x00015FA9
		public bool KeepExecutionSnapshots
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00017DBE File Offset: 0x00015FBE
		public ScheduleDefinitionOrReference Item
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ScheduleDefinitionOrReference)this.results[2];
			}
		}

		// Token: 0x04000494 RID: 1172
		private object[] results;
	}
}
