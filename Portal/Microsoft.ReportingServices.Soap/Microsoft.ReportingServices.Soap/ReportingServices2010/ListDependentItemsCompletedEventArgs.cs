using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000056 RID: 86
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListDependentItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x0000D89E File Offset: 0x0000BA9E
		internal ListDependentItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0000D8B1 File Offset: 0x0000BAB1
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x04000228 RID: 552
		private object[] results;
	}
}
