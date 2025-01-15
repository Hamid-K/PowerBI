using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200005C RID: 92
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x0000D916 File Offset: 0x0000BB16
		internal CreateFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0000D929 File Offset: 0x0000BB29
		public CatalogItem Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem)this.results[0];
			}
		}

		// Token: 0x0400022B RID: 555
		private object[] results;
	}
}
