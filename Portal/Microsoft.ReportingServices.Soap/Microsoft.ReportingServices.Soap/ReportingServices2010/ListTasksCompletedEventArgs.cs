using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000097 RID: 151
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListTasksCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x0000DE2A File Offset: 0x0000C02A
		internal ListTasksCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0000DE3D File Offset: 0x0000C03D
		public Task[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Task[])this.results[0];
			}
		}

		// Token: 0x04000241 RID: 577
		private object[] results;
	}
}
