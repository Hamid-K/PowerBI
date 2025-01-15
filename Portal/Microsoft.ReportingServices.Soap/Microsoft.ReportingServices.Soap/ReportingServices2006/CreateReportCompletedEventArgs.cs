using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200017F RID: 383
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateReportCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E7F RID: 3711 RVA: 0x00017A1E File Offset: 0x00015C1E
		internal CreateReportCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00017A31 File Offset: 0x00015C31
		public CatalogItem Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem)this.results[0];
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00017A46 File Offset: 0x00015C46
		public Warning[] Warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x04000482 RID: 1154
		private object[] results;
	}
}
