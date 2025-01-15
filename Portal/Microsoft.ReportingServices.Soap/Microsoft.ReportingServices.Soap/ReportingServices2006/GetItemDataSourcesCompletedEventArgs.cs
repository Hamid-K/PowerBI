using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001A3 RID: 419
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemDataSourcesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000EF9 RID: 3833 RVA: 0x00017C7A File Offset: 0x00015E7A
		internal GetItemDataSourcesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00017C8D File Offset: 0x00015E8D
		public DataSource[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSource[])this.results[0];
			}
		}

		// Token: 0x0400048F RID: 1167
		private object[] results;
	}
}
