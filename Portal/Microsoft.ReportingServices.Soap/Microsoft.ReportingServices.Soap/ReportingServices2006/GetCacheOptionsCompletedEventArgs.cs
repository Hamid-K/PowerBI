using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000193 RID: 403
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetCacheOptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000EC2 RID: 3778 RVA: 0x00017B9D File Offset: 0x00015D9D
		internal GetCacheOptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00017BB0 File Offset: 0x00015DB0
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00017BC5 File Offset: 0x00015DC5
		public ExpirationDefinition Item
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExpirationDefinition)this.results[1];
			}
		}

		// Token: 0x0400048A RID: 1162
		private object[] results;
	}
}
