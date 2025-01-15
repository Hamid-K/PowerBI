using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001D1 RID: 465
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ValidateExtensionSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FA5 RID: 4005 RVA: 0x00018142 File Offset: 0x00016342
		internal ValidateExtensionSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00018155 File Offset: 0x00016355
		public ExtensionParameter[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionParameter[])this.results[0];
			}
		}

		// Token: 0x040004A1 RID: 1185
		private object[] results;
	}
}
