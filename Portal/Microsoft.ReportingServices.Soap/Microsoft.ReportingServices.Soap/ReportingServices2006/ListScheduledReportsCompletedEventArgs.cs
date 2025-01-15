using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001BC RID: 444
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListScheduledReportsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F50 RID: 3920 RVA: 0x00017E9D File Offset: 0x0001609D
		internal ListScheduledReportsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00017EB0 File Offset: 0x000160B0
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x04000499 RID: 1177
		private object[] results;
	}
}
