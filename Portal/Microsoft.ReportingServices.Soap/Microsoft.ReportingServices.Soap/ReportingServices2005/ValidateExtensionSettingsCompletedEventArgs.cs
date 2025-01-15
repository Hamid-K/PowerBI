using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002D6 RID: 726
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ValidateExtensionSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600179F RID: 6047 RVA: 0x00023249 File Offset: 0x00021449
		internal ValidateExtensionSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0002325C File Offset: 0x0002145C
		public ExtensionParameter[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionParameter[])this.results[0];
			}
		}

		// Token: 0x04000709 RID: 1801
		private object[] results;
	}
}
