using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000E0 RID: 224
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemHistoryOptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x0000E31F File Offset: 0x0000C51F
		internal GetItemHistoryOptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0000E332 File Offset: 0x0000C532
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0000E347 File Offset: 0x0000C547
		public bool KeepExecutionSnapshots
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0000E35C File Offset: 0x0000C55C
		public ScheduleDefinitionOrReference Item
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ScheduleDefinitionOrReference)this.results[2];
			}
		}

		// Token: 0x0400025C RID: 604
		private object[] results;
	}
}
