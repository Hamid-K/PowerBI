using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000D2 RID: 210
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetExecutionOptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x0000E216 File Offset: 0x0000C416
		internal GetExecutionOptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0000E229 File Offset: 0x0000C429
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0000E23E File Offset: 0x0000C43E
		public ScheduleDefinitionOrReference Item
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ScheduleDefinitionOrReference)this.results[1];
			}
		}

		// Token: 0x04000258 RID: 600
		private object[] results;
	}
}
