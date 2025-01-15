using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001CF RID: 463
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetExtensionSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F9F RID: 3999 RVA: 0x0001811A File Offset: 0x0001631A
		internal GetExtensionSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0001812D File Offset: 0x0001632D
		public ExtensionParameter[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionParameter[])this.results[0];
			}
		}

		// Token: 0x040004A0 RID: 1184
		private object[] results;
	}
}
