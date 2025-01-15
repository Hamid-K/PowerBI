using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200007E RID: 126
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateDataSourceCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x0000DC59 File Offset: 0x0000BE59
		internal CreateDataSourceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		public CatalogItem Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem)this.results[0];
			}
		}

		// Token: 0x04000238 RID: 568
		private object[] results;
	}
}
