using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000172 RID: 370
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListChildrenCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E57 RID: 3671 RVA: 0x0001792E File Offset: 0x00015B2E
		internal ListChildrenCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00017941 File Offset: 0x00015B41
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x0400047C RID: 1148
		private object[] results;
	}
}
