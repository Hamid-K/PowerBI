using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000176 RID: 374
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListDependentItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E63 RID: 3683 RVA: 0x0001797E File Offset: 0x00015B7E
		internal ListDependentItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00017991 File Offset: 0x00015B91
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x0400047E RID: 1150
		private object[] results;
	}
}
