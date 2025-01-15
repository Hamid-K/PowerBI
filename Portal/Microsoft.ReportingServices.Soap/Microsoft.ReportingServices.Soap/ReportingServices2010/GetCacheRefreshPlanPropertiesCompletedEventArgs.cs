using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000103 RID: 259
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetCacheRefreshPlanPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x0000E5A1 File Offset: 0x0000C7A1
		internal GetCacheRefreshPlanPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0000E5C9 File Offset: 0x0000C7C9
		public string LastRunStatus
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0000E5DE File Offset: 0x0000C7DE
		public CacheRefreshPlanState State
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CacheRefreshPlanState)this.results[2];
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0000E5F3 File Offset: 0x0000C7F3
		public string EventType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[3];
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0000E608 File Offset: 0x0000C808
		public string MatchData
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[4];
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0000E61D File Offset: 0x0000C81D
		public ParameterValue[] Parameters
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ParameterValue[])this.results[5];
			}
		}

		// Token: 0x0400026B RID: 619
		private object[] results;
	}
}
