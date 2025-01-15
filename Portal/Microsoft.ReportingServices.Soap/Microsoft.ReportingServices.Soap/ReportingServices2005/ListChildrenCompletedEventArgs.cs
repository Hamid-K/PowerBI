using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000274 RID: 628
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListChildrenCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001643 RID: 5699 RVA: 0x00022A5D File Offset: 0x00020C5D
		internal ListChildrenCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x00022A70 File Offset: 0x00020C70
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x040006E5 RID: 1765
		private object[] results;
	}
}
