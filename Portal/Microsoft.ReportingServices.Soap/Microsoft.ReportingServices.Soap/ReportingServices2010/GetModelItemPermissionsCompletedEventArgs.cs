using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000A0 RID: 160
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetModelItemPermissionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		internal GetModelItemPermissionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0000DF07 File Offset: 0x0000C107
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x04000245 RID: 581
		private object[] results;
	}
}
