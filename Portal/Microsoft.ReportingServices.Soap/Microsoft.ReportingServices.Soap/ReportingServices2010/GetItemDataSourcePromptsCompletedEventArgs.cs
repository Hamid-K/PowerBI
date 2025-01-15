using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200009C RID: 156
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemDataSourcePromptsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x0000DE8F File Offset: 0x0000C08F
		internal GetItemDataSourcePromptsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0000DEA2 File Offset: 0x0000C0A2
		public DataSourcePrompt[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSourcePrompt[])this.results[0];
			}
		}

		// Token: 0x04000243 RID: 579
		private object[] results;
	}
}
