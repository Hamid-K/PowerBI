using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000297 RID: 663
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetCacheOptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060016B8 RID: 5816 RVA: 0x00022CA4 File Offset: 0x00020EA4
		internal GetCacheOptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x00022CB7 File Offset: 0x00020EB7
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x00022CCC File Offset: 0x00020ECC
		public ExpirationDefinition Item
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExpirationDefinition)this.results[1];
			}
		}

		// Token: 0x040006F2 RID: 1778
		private object[] results;
	}
}
