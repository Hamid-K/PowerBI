using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200019B RID: 411
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateDataSourceCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000EDD RID: 3805 RVA: 0x00017C2A File Offset: 0x00015E2A
		internal CreateDataSourceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00017C3D File Offset: 0x00015E3D
		public CatalogItem Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem)this.results[0];
			}
		}

		// Token: 0x0400048D RID: 1165
		private object[] results;
	}
}
