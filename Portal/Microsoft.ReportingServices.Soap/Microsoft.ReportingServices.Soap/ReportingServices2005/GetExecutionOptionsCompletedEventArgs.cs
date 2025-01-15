using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000294 RID: 660
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetExecutionOptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060016AD RID: 5805 RVA: 0x00022C67 File Offset: 0x00020E67
		internal GetExecutionOptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x00022C7A File Offset: 0x00020E7A
		public ExecutionSettingEnum Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExecutionSettingEnum)this.results[0];
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00022C8F File Offset: 0x00020E8F
		public ScheduleDefinitionOrReference Item
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ScheduleDefinitionOrReference)this.results[1];
			}
		}

		// Token: 0x040006F1 RID: 1777
		private object[] results;
	}
}
