using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000190 RID: 400
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetExecutionOptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000EB7 RID: 3767 RVA: 0x00017B60 File Offset: 0x00015D60
		internal GetExecutionOptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00017B73 File Offset: 0x00015D73
		public ExecutionSettingEnum Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionSettingEnum)this.results[0];
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00017B88 File Offset: 0x00015D88
		public ScheduleDefinitionOrReference Item
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ScheduleDefinitionOrReference)this.results[1];
			}
		}

		// Token: 0x04000489 RID: 1161
		private object[] results;
	}
}
