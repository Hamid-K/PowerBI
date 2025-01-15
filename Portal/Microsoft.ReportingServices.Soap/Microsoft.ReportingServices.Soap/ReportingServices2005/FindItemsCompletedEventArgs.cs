using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002B9 RID: 697
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class FindItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001730 RID: 5936 RVA: 0x00022F2C File Offset: 0x0002112C
		internal FindItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00022F3F File Offset: 0x0002113F
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x040006FE RID: 1790
		private object[] results;
	}
}
