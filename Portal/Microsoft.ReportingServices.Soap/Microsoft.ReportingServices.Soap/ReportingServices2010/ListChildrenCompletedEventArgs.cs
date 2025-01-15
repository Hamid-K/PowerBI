using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000054 RID: 84
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListChildrenCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0000D876 File Offset: 0x0000BA76
		internal ListChildrenCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0000D889 File Offset: 0x0000BA89
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x04000227 RID: 551
		private object[] results;
	}
}
