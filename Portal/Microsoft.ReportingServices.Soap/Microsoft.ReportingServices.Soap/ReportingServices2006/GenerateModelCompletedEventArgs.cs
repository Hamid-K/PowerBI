using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000165 RID: 357
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GenerateModelCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E2C RID: 3628 RVA: 0x00017851 File Offset: 0x00015A51
		internal GenerateModelCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00017864 File Offset: 0x00015A64
		public CatalogItem Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem)this.results[0];
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00017879 File Offset: 0x00015A79
		public Warning[] Warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x04000477 RID: 1143
		private object[] results;
	}
}
