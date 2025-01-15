using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200009E RID: 158
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GenerateModelCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x0000DEB7 File Offset: 0x0000C0B7
		internal GenerateModelCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0000DECA File Offset: 0x0000C0CA
		public CatalogItem Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem)this.results[0];
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0000DEDF File Offset: 0x0000C0DF
		public Warning[] Warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x04000244 RID: 580
		private object[] results;
	}
}
