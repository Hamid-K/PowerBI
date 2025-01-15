using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200017D RID: 381
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E79 RID: 3705 RVA: 0x000179F6 File Offset: 0x00015BF6
		internal CreateFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00017A09 File Offset: 0x00015C09
		public CatalogItem Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem)this.results[0];
			}
		}

		// Token: 0x04000481 RID: 1153
		private object[] results;
	}
}
