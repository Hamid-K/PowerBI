using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001A5 RID: 421
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemDataSourcePromptsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000EFF RID: 3839 RVA: 0x00017CA2 File Offset: 0x00015EA2
		internal GetItemDataSourcePromptsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00017CB5 File Offset: 0x00015EB5
		public DataSourcePrompt[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSourcePrompt[])this.results[0];
			}
		}

		// Token: 0x04000490 RID: 1168
		private object[] results;
	}
}
