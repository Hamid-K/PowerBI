using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000047 RID: 71
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateCatalogItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x0000D799 File Offset: 0x0000B999
		internal CreateCatalogItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		public CatalogItem Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem)this.results[0];
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0000D7C1 File Offset: 0x0000B9C1
		public Warning[] Warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x04000222 RID: 546
		private object[] results;
	}
}
