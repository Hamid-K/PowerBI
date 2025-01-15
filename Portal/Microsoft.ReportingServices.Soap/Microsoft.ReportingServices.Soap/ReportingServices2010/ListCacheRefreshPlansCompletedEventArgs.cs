using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000106 RID: 262
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListCacheRefreshPlansCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x0000E632 File Offset: 0x0000C832
		internal ListCacheRefreshPlansCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0000E645 File Offset: 0x0000C845
		public CacheRefreshPlan[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CacheRefreshPlan[])this.results[0];
			}
		}

		// Token: 0x0400026C RID: 620
		private object[] results;
	}
}
