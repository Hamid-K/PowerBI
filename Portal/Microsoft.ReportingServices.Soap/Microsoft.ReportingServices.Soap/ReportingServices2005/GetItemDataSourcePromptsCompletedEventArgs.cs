using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002A8 RID: 680
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemDataSourcePromptsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060016F3 RID: 5875 RVA: 0x00022D81 File Offset: 0x00020F81
		internal GetItemDataSourcePromptsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00022D94 File Offset: 0x00020F94
		public DataSourcePrompt[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSourcePrompt[])this.results[0];
			}
		}

		// Token: 0x040006F7 RID: 1783
		private object[] results;
	}
}
