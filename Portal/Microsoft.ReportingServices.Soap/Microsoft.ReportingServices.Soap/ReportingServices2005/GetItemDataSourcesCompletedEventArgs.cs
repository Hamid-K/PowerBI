using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002A6 RID: 678
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemDataSourcesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060016ED RID: 5869 RVA: 0x00022D59 File Offset: 0x00020F59
		internal GetItemDataSourcesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00022D6C File Offset: 0x00020F6C
		public DataSource[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSource[])this.results[0];
			}
		}

		// Token: 0x040006F6 RID: 1782
		private object[] results;
	}
}
