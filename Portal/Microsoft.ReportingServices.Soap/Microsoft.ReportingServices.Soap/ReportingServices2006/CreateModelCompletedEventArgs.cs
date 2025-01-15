using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001EA RID: 490
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateModelCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FF5 RID: 4085 RVA: 0x00018324 File Offset: 0x00016524
		internal CreateModelCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00018337 File Offset: 0x00016537
		public CatalogItem Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem)this.results[0];
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0001834C File Offset: 0x0001654C
		public Warning[] Warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x040004AC RID: 1196
		private object[] results;
	}
}
