using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000100 RID: 256
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateCacheRefreshPlanCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x0000E579 File Offset: 0x0000C779
		internal CreateCacheRefreshPlanCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0000E58C File Offset: 0x0000C78C
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x0400026A RID: 618
		private object[] results;
	}
}
