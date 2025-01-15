using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000C0 RID: 192
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListScheduledItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x0000E0E9 File Offset: 0x0000C2E9
		internal ListScheduledItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0000E0FC File Offset: 0x0000C2FC
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x04000251 RID: 593
		private object[] results;
	}
}
