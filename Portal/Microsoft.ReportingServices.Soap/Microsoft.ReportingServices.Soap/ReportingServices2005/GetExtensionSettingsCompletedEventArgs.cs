using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002D4 RID: 724
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetExtensionSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001799 RID: 6041 RVA: 0x00023221 File Offset: 0x00021421
		internal GetExtensionSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00023234 File Offset: 0x00021434
		public ExtensionParameter[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionParameter[])this.results[0];
			}
		}

		// Token: 0x04000708 RID: 1800
		private object[] results;
	}
}
