using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200017B RID: 379
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemTypeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E73 RID: 3699 RVA: 0x000179CE File Offset: 0x00015BCE
		internal GetItemTypeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x000179E1 File Offset: 0x00015BE1
		public ItemTypeEnum Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ItemTypeEnum)this.results[0];
			}
		}

		// Token: 0x04000480 RID: 1152
		private object[] results;
	}
}
