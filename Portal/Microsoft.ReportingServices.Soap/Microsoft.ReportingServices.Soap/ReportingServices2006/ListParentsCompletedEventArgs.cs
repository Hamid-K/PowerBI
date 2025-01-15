using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000174 RID: 372
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListParentsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E5D RID: 3677 RVA: 0x00017956 File Offset: 0x00015B56
		internal ListParentsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00017969 File Offset: 0x00015B69
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x0400047D RID: 1149
		private object[] results;
	}
}
