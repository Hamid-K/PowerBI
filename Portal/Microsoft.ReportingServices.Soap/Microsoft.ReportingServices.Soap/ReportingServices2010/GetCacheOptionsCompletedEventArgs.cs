using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000D6 RID: 214
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetCacheOptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600081F RID: 2079 RVA: 0x0000E253 File Offset: 0x0000C453
		internal GetCacheOptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0000E266 File Offset: 0x0000C466
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0000E27B File Offset: 0x0000C47B
		public ExpirationDefinition Item
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExpirationDefinition)this.results[1];
			}
		}

		// Token: 0x04000259 RID: 601
		private object[] results;
	}
}
