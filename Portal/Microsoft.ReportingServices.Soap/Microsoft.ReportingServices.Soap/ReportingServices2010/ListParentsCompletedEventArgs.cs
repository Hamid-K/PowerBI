using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200005A RID: 90
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListParentsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x0000D8EE File Offset: 0x0000BAEE
		internal ListParentsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0000D901 File Offset: 0x0000BB01
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x0400022A RID: 554
		private object[] results;
	}
}
