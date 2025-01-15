using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000058 RID: 88
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class FindItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x0000D8C6 File Offset: 0x0000BAC6
		internal FindItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0000D8D9 File Offset: 0x0000BAD9
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x04000229 RID: 553
		private object[] results;
	}
}
