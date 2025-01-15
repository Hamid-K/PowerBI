using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000276 RID: 630
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListDependentItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001649 RID: 5705 RVA: 0x00022A85 File Offset: 0x00020C85
		internal ListDependentItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00022A98 File Offset: 0x00020C98
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x040006E6 RID: 1766
		private object[] results;
	}
}
