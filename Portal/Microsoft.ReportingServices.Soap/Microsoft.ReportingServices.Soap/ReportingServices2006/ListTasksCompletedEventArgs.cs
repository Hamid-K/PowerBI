using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001DE RID: 478
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListTasksCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FCD RID: 4045 RVA: 0x00018232 File Offset: 0x00016432
		internal ListTasksCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00018245 File Offset: 0x00016445
		public Task[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Task[])this.results[0];
			}
		}

		// Token: 0x040004A7 RID: 1191
		private object[] results;
	}
}
